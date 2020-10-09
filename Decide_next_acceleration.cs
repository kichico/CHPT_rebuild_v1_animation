using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Decide_next_acceleration : Calculate_Rerecognition_Velocity_Difference
    {
        /// <summary>
        /// 次タイムステップの加速度を計算する
        /// </summary>
        public void decide_next_acceleration()
        {
            for (int ID = 0; ID < N; ID++)
            {
                //最適速度を再認識するかどうか
                if (driver[ID].running.RR.random_value.Pg <= calculate_Rerecognition_Rate(ID)) if (driver[ID].running.RR.effectiveness) _recognition_optimal_velocity(ID);
                //速度超過や減速しすぎに気が付くかどうか
                if (driver[ID].running.RR.random_value.Pv <= calculate_Rerecognition_Vd_Rate(ID)) _redecide_acceleration(ID);
                //ペダルの踏みかえの判断
                _pedal_judgement(ID);
                //情報更新
                car[ID].running.velocity.previous = car[ID].running.velocity.current;
                car[ID].running.position.previous = car[ID].running.position.current;
            }
        }

        /// <summary>
        /// 最適速度の再認識処理
        /// </summary>
        /// <param name="ID">車両ID</param>
        private void _recognition_optimal_velocity(int ID)
        {
            double Voptimal_previous = driver[ID].running.v_optimal.current;
            double Voptimal = driver[ID].running.v_optimal.current = driver[ID].running.v_optimal.instantaneous;
            DDelta delta = driver[ID].running.delta;
            delta.velocity = car[ID].running.velocity.current - Voptimal;
            DVDifference DVD = driver[ID].running.v_difference;
            double Vd_n = DVD.current;
            double Nv_ = 2 / Vd_n * Math.Abs(Voptimal_previous - Voptimal) - 1;
            double fv_ = 1 / (1 + Math.Exp(-Nv_ / 0.1));
            DVelocity DV = driver[ID].eigenvalue.velocity;
            double Vd_optimal = (DV.c_difference - DV.s_difference) / DV.cruise * Voptimal + DV.s_difference;
            double Vd = (Vd_optimal - Vd_n) * fv_ + Vd_n;
            if (Vd > Vd_optimal) DVD.at_firtst_time = DVD.current = Vd_optimal;
            else
            {
                if (Vd > DV.s_difference) DVD.at_firtst_time = DVD.current = Vd;
                else DVD.at_firtst_time = DVD.current = DV.s_difference;
            }
            if (Driver_Mode == DriverMode.Human) driver[ID].running.RR.random_value.Pg = random.NextDouble();
            driver[ID].running.RR.random_value.Pv = 0;
            if (car[ID].running.gap > driver[ID].running.gap.influenced) driver[ID].running.RR.effectiveness = false;
        }

        /// <summary>
        /// 最適速度へ加速度の再調節処理
        /// </summary>
        /// <param name="ID">車両ID</param>
        private void _redecide_acceleration(int ID)
        {
            DVDifference DVD = driver[ID].running.v_difference;
            double Voptimal = driver[ID].running.v_optimal.current;
            double V = car[ID].running.velocity.current;
            double Nv_n = 2 / DVD.current * (Voptimal - V) - 1;
            double fv_n = 1 / (1 + Math.Exp(-Nv_n / 0.1));
            double fg = driver[ID].running.RR.fg.value;
            double a_max = __calculate_maximum_acceleration(ID) * DVD.current / DVD.at_firtst_time;
            double deltaG = driver[ID].running.delta.gap;
            double minimum_a = car[ID].eigenvalue.acceleration.minimum;
            double f, next_a;
            if (deltaG <= 0) f = (1 - fv_n) * fg + fv_n;
            else f = (fv_n - 1) * fg + 1;
            next_a = a_max * f;
            if (Math.Abs(next_a) < minimum_a)
            {
                if (a_max > 0) next_a = minimum_a;
                else next_a = -minimum_a;
            }
            driver[ID].running.acceleration.value = next_a;
            __pedal_changing(ID);
            if (Driver_Mode == DriverMode.Human) driver[ID].running.RR.random_value.Pv = random.NextDouble();
            double ABS = Math.Abs(Voptimal - V);
            if (ABS < driver[ID].running.v_difference.current)
            {
                if (ABS > driver[ID].eigenvalue.velocity.s_difference) DVD.current = ABS;
                else DVD.current = driver[ID].eigenvalue.velocity.s_difference;
            }
        }

        /// <summary>
        /// 最大加減速度の計算
        /// </summary>
        /// <param name="ID">車両ID</param>
        /// <returns>最大加減速度</returns>
        private double __calculate_maximum_acceleration(int ID)
        {
            double alpha;
            double deltaV = driver[ID].running.delta.velocity;
            double deltaG = driver[ID].running.delta.gap;
            if (deltaV <= 0)
            {
                //加速できる
                alpha = driver[ID].eigenvalue.acceleration.acceleration;
                if (deltaG <= 0) alpha *= 1 - driver[ID].running.RR.fg.value;
            }
            else
            {
                //減速しなければならない
                double a = driver[ID].eigenvalue.acceleration.deceleration;
                if (deltaG <= 0) alpha = (car[ID].eigenvalue.acceleration.braking - a) * driver[ID].running.RR.fg.value + a;
                else alpha = a * (1 - driver[ID].running.RR.fg.value);
                alpha *= -1;
            }
            return alpha;
        }

        /// <summary>
        /// 速度の調節をペダルで行う
        /// </summary>
        /// <param name="ID">車両ID</param>
        private void __pedal_changing(int ID)
        {
            Pedal pedal = driver[ID].running.pedal;
            double deltaV = driver[ID].running.delta.velocity;
            if (deltaV <= 0)
            {
                //加速するとき
                driver[ID].running.acceleration.sign = SignAcceleration.acceleration;
                if (pedal.foot_position == FootPosition.brake_pedal)
                {
                    //ブレーキペダルからアクセルペダルへ
                    pedal.foot_position = FootPosition.brake_to_accel;
                    pedal.time_required = driver[ID].eigenvalue.operation_time;
                }
                else if (pedal.foot_position == FootPosition.accel_to_brake)
                {
                    //ブレーキしようと思ったけどやっぱり加速
                    pedal.foot_position = FootPosition.brake_to_accel;
                    pedal.time_required = driver[ID].running.pedal.time_elapsed;
                    pedal.time_elapsed = 0;
                }
            }
            else
            {
                //減速するとき
                driver[ID].running.acceleration.sign = SignAcceleration.deceleration;
                if (driver[ID].running.acceleration.value >= -car[ID].eigenvalue.acceleration.resistance)
                {
                    //この時はアクセルペダルで調節する
                    if (pedal.foot_position == FootPosition.brake_pedal)
                    {
                        //ブレーキペダルからアクセルペダルへ
                        pedal.foot_position = FootPosition.brake_to_accel;
                        pedal.time_required = driver[ID].eigenvalue.operation_time;
                    }
                    else if (pedal.foot_position == FootPosition.accel_to_brake)
                    {
                        //ブレーキしようと思ったけどやっぱり加速
                        pedal.foot_position = FootPosition.brake_to_accel;
                        pedal.time_required = driver[ID].running.pedal.time_elapsed;
                        pedal.time_elapsed = 0;
                    }
                }
                else
                {
                    if (pedal.foot_position == FootPosition.accel_pedal)
                    {
                        //アクセルペダルからブレーキペダルへ
                        pedal.foot_position = FootPosition.accel_to_brake;
                        pedal.time_required = driver[ID].eigenvalue.operation_time;
                    }
                    else if (pedal.foot_position == FootPosition.brake_to_accel)
                    {
                        //加速しようと思ったけどやっぱりブレーキ
                        pedal.foot_position = FootPosition.accel_to_brake;
                        pedal.time_required = driver[ID].running.pedal.time_elapsed;
                        pedal.time_elapsed = 0;
                    }
                }
            }
        }

        /// <summary>
        /// ペダルの踏みかえ
        /// </summary>
        /// <param name="ID">車両ID</param>
        private void _pedal_judgement(int ID)
        {
            //踏みかえ中なら判定を，そうでないなら今の加速度を保持
            Pedal pedal = driver[ID].running.pedal;
            bool put_on;    //足がペダルに置いてあるかどうか
            if (pedal.foot_position == FootPosition.accel_to_brake || pedal.foot_position == FootPosition.brake_to_accel)
            {
                //現在ペダル踏みかえ中
                if (pedal.time_elapsed >= pedal.time_required)
                {
                    //踏みかえが終了
                    if (pedal.foot_position == FootPosition.accel_to_brake) pedal.foot_position = FootPosition.brake_pedal;
                    else pedal.foot_position = FootPosition.accel_pedal;
                    put_on = true;
                }
                else
                {
                    //まだ踏みかえ中
                    pedal.time_elapsed += parameter.t;
                    put_on = false;
                }
            }
            else put_on = true;
            if (put_on)
            {
                //足がペダルにあるので，加減速を調節できる
                car[ID].running.acceleration = driver[ID].running.acceleration.value;
                pedal.time_elapsed = pedal.time_required = 0;
            }
            else car[ID].running.acceleration = -car[ID].eigenvalue.acceleration.resistance;
        }
    }
}

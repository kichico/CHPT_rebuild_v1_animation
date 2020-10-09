using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    /// <summary>
    /// モデルパラメータの情報を保持する
    /// </summary>
    class Parameter
    {
        public double length;   //全周[m]
        public double t;        //時間解像度[s]

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public Parameter()
        {
            length = new double();
            t = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="length">全周</param>
        /// <param name="t">時間解像度</param>
        public Parameter(double length, double t)
        {
            this.length = length;
            this.t = t;
        }

        /// <summary>
        /// コピーを作成する
        /// </summary>
        /// <param name="parameter">Parameter</param>
        public Parameter(Parameter parameter)
        {
            length = parameter.length;
            t = parameter.t;
        }
    }

    /// <summary>
    /// 先頭車両情報を保持する
    /// </summary>
    class Lead_Car
    {
        public int ID;      //先頭車両ID
        public double gap;  //最大車間距離

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public Lead_Car()
        {
            ID = new int();
            gap = new double();
        }

        /// <summary>
        /// /値を指定して初期化
        /// </summary>
        /// <param name="ID">先頭車両ID</param>
        /// <param name="gap">最大車間距離</param>
        public Lead_Car(int ID, double gap)
        {
            this.ID = ID;
            this.gap = gap;
        }

        /// <summary>
        /// コピーを作成する
        /// </summary>
        /// <param name="lead">Lead_Car</param>
        public Lead_Car(Lead_Car lead)
        {
            ID = lead.ID;
            gap = lead.gap;
        }
    }

    class Calculate_Optimal_Velocity
    {
        public int N;
        public int Mode;
        public int Driver_Mode;
        public Parameter parameter;
        public Lead_Car lead_car;
        public List<Driver_Structure> driver;
        public List<Car_Structure> car;
        public Random random;

        /// <summary>
        /// 最適速度を計算する
        /// </summary>
        /// <param name="ID">車両ID</param>
        public void calculate_optimal_velocity(int ID)
        {
            double deltaG = driver[ID].running.delta.gap = _calculate_Gseries(ID);
            double fg = driver[ID].running.RR.fg.value = _calculate_fg(ID);
            double Vgap;
            double Vf = car[car[ID].running.around.front].running.velocity.current;
            double Vcruise = driver[ID].eigenvalue.velocity.cruise;
            if (deltaG <= 0) Vgap = (1 - fg) * Vf;
            else Vgap = (Vcruise - Vf) * fg + Vf;
            if (Vgap > Vcruise) driver[ID].running.v_optimal.instantaneous = Vcruise;
            else driver[ID].running.v_optimal.instantaneous = Vgap;
        }

        /// <summary>
        /// 車間距離シリーズを計算する
        /// </summary>
        /// <param name="ID">車両ID</param>
        /// <returns>true -> 内側，false -> 外側</returns>
        private double _calculate_Gseries(int ID)
        {
            DGap DG = driver[ID].running.gap;
            DEGap DEG = driver[ID].eigenvalue.gap;
            int front = car[ID].running.around.front;
            double v = car[ID].running.velocity.current;
            double v_f = car[front].running.velocity.current;
            double T = driver[ID].eigenvalue.operation_time;
            double Ab = car[ID].eigenvalue.acceleration.braking;
            double Ab_f = car[front].eigenvalue.acceleration.braking;
            double Abs = driver[ID].eigenvalue.acceleration.deceleration;
            double Didling = v * T;
            double Dbrake = v * v / (2 * Ab);
            double Dbrake_f = v_f * v_f / (2 * Ab_f);
            DG.closest = Didling + Dbrake - Dbrake_f;
            if (DG.closest < DEG.stop) DG.closest = DEG.stop;
            else DG.closest += DEG.stop;
            DG.cruise = Didling + v * v / (2 * Abs) + (DEG.stop + DEG.move) / 2;
            if (DG.cruise - 10 > DG.closest) DG.cruise = DG.closest + 10;
            DG.influenced = 2 * DG.cruise - DG.closest;
            return car[ID].running.gap - DG.cruise;
        }

        /// <summary>
        /// 車間距離の認識率を計算する
        /// </summary>
        /// <param name="ID">車両ID</param>
        /// <param name="sign">車間距離の場合分け</param>
        /// <returns>車間距離の認識率</returns>
        private double _calculate_fg(int ID)
        {
            double NG = driver[ID].running.RR.fg.normalized = __calculate_NG(ID);
            return 1 / (1 + Math.Exp(-NG / 0.1));
        }

        /// <summary>
        /// 車間距離を-1から1正規化にする
        /// </summary>
        /// <param name="ID">車両ID</param>
        /// <param name="sign">車間距離の場合分け</param>
        /// <returns>正規化された値</returns>
        private double __calculate_NG(int ID)
        {
            double NG;
            DGap DG = driver[ID].running.gap;
            double G = car[ID].running.gap;
            double deltaG = driver[ID].running.delta.gap;
            if (G <= DG.influenced) driver[ID].running.RR.effectiveness = true;
            if (deltaG <= 0) NG = -2 * (G - DG.closest) / (DG.cruise - DG.closest) + 1;
            else NG = 2 * (G - DG.cruise) / (DG.influenced - DG.cruise) - 1;
            if (NG < -1) NG = -1;
            else if (NG > 1) NG = 1;
            return NG;
        }
    }
}

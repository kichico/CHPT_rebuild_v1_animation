using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Initialize : Decide_next_acceleration
    {
        private double all_length;

        /// <summary>
        /// シミュレーションの初期化
        /// </summary>
        public bool initialize()
        {
            all_length = 0;
            _initialize_car_eigenvalue();
            _initialize_driver_eigenvalue();
            return _initialize_postion();
        }

        /// <summary>
        /// 車の特徴を初期化
        /// </summary>
        private void _initialize_car_eigenvalue()
        {
            car = new List<Car_Structure>(N);
            for (int i = 0; i < N; i++)
            {
                Change_Unit change = new Change_Unit();
                Car_Structure CS = new Car_Structure();
                CS.eigenvalue.acceleration.maximum = change.km_h__to__m_s(100) / 6.8;
                CS.eigenvalue.acceleration.braking = Math.Pow(change.km_h__to__m_s(100), 2) / 2 / 38.6;
                CS.eigenvalue.acceleration.resistance = change.km_h__to__m_s(1);
                CS.eigenvalue.acceleration.minimum = change.km_h__to__m_s(1) / 5;
                CS.eigenvalue.length = 4.435;
                all_length += CS.eigenvalue.length;
                CS.eigenvalue.maximum_velocity = change.km_h__to__m_s(183);
                CS.running.velocity.current = CS.running.velocity.previous = 0;
                car.Add(CS);
            }
        }

        /// <summary>
        /// ドライバーの特徴を初期化
        /// </summary>
        private void _initialize_driver_eigenvalue()
        {
            driver = new List<Driver_Structure>(N);
            for (int i = 0; i < N; i++)
            {
                Change_Unit change = new Change_Unit();
                DVelocity DV = new DVelocity();
                DV.cruise = change.km_h__to__m_s(100);
                DV.c_difference = change.km_h__to__m_s(15);
                DV.s_difference = change.km_h__to__m_s(1);
                Pedal pedal = new Pedal();
                pedal.foot_position = FootPosition.accel_pedal;
                pedal.time_elapsed = pedal.time_required = 0;
                DGap dg = new DGap();
                dg.closest = dg.cruise = dg.influenced = 0;
                DEGap deg = new DEGap();
                double operation_time;
                if (Driver_Mode == DriverMode.Human)
                {
                    operation_time = 0.75;
                    deg.stop = 3;
                    deg.move = 6;
                }
                else
                {
                    operation_time = 0.05;
                    deg.stop = 1;
                    deg.move = 1.1;
                }
                Driver_Structure DS = new Driver_Structure();
                DS.eigenvalue.acceleration.acceleration = change.km_h__to__m_s(60) / 10;
                DS.eigenvalue.acceleration.deceleration = Math.Pow(change.km_h__to__m_s(100), 2) / 2 / 60;
                DS.eigenvalue.operation_time = operation_time;
                DS.eigenvalue.velocity = new DVelocity(DV);
                DS.eigenvalue.gap = new DEGap(deg);
                DS.running.pedal = new Pedal(pedal);
                DS.running.gap = new DGap(dg);
                DS.running.v_optimal = new DIC(0, 0);
                DS.running.RR.random_value = new Random_Value(0, 0);
                DS.running.RR.effectiveness = true;
                DS.running.v_difference = new DVDifference(deg.stop, deg.stop);
                driver.Add(DS);
            }
        }

        /// <summary>
        /// 車両を配置する
        /// </summary>
        /// <returns>配置出来たらtrue，重なったらfalse</returns>
        private bool _initialize_postion()
        {
            //本来，複数車線があるだろうから，ここで車線毎に呼び出しを行う
            //従って，複数車線がある場合は引数に車線番号を持たせる必要がある
            bool fg = __position_deploy();
            if (fg)
            {
                if (Mode == SimulationMode.RandomMode) __position_move();
                decide_next_acceleration();
            }
            return fg;
        }

        /// <summary>
        /// 車両を均等に並べ，前後関係を記録する
        /// </summary>
        /// <returns>配置出来たらtrue，重なったらfalse</returns>
        private bool __position_deploy()
        {
            bool fg = true;
            lead_car = new Lead_Car();
            //!!!!並列計算禁止!!!!
            //後方車両から，順番に車両を車間距離が同じになるように並べる
            double distance;
            if (Mode == SimulationMode.PolarizationInitialMode) distance = 1;
            else distance = (parameter.length - all_length) / N;
            if (distance < 1)
            {
                Console.WriteLine("車両を重ならないように配置することが不可能です");
                Console.ReadLine();
                fg = false;
            }
            car[N - 1].running.position.current = car[N - 1].running.position.previous = 0;
            car[N - 1].running.around.front = 0;
            car[N - 1].running.around.rear = N - 2;
            car[N - 1].running.gap = distance;
            for (int i = N - 2; i >= 0; i--)
            {
                double next_postion = new double();
                if (i == N - 2) next_postion = parameter.length;
                else next_postion = car[i + 1].running.position.current;
                next_postion -= car[i + 1].eigenvalue.length + distance;
                car[i].running.position.current = car[i].running.position.previous = next_postion;
                car[i].running.around.front = i + 1;
                car[i].running.around.rear = i - 1;
                car[i].running.gap = distance;
            }
            car[0].running.around.rear = N - 1;
            double xF = car[0].running.position.current - car[0].eigenvalue.length;
            if (xF < 0) xF += parameter.length;
            double x = car[N - 1].running.position.current;
            double gap = xF - x;
            if (gap < 0) gap += parameter.length;
            car[N - 1].running.gap = gap;
            lead_car.ID = N - 1;
            lead_car.gap = gap;
            return fg;
        }

        /// <summary>
        /// 初期配置をランダムに変更する
        /// </summary>
        private void __position_move()
        {
            //!!!!並列計算禁止!!!!
            //各車両に対し，ランダムアップデートで，50%の確率で初期位置を変更させる
            //↓___move_avoid_collicionで初期位置の変更の詳細が書いてある
            List<int> remaining = new List<int>(N);
            for (int i = 0; i < N; i++)
            {
                remaining.Add(i);
            }
            while (remaining.Count() > 0)
            {
                int ID = remaining[random.Next(remaining.Count)];
                remaining.Remove(ID);
                ___move_avoid_collision(ID);
            }

        }

        /// <summary>
        /// 配置を実際に変える
        /// </summary>
        /// <param name="ID">車両ID</param>
        private void ___move_avoid_collision(int ID)
        {
            //衝突を回避して，前方車両と後方車両の間でランダムに位置を変更する
            //↓↓↓↓前準備↓↓↓↓
            int front = car[ID].running.around.front;
            int rear = car[ID].running.around.rear;
            double front_position = car[ID].running.position.current;
            double rear_position = car[ID].running.position.current - car[ID].eigenvalue.length - 0.001;
            if (rear_position < 0) rear_position += parameter.length;
            double front_end_position = car[front].running.position.current - car[front].eigenvalue.length - 0.001;
            if (front_end_position < 0) front_end_position += parameter.length;
            double rear_front_position = car[rear].running.position.current;
            if (front_end_position < front_position) front_end_position += parameter.length;
            double forward = front_end_position - front_position;
            if (rear_front_position > rear_position) rear_position += parameter.length;
            double backward = rear_position - rear_front_position;
            //↑↑↑↑前準備↑↑↑↑
            //↓↓↓↓位置変更↓↓↓↓
            double move_value = (forward + backward) * random.NextDouble() - backward;
            //double move_value = forward;
            double next_position = car[ID].running.position.current + move_value;
            if (next_position < 0) next_position += parameter.length;
            else if (next_position >= parameter.length) next_position -= parameter.length;
            car[ID].running.position.current = car[ID].running.position.previous = next_position;
            //車間距離更新
            car[ID].running.gap -= move_value;
            car[rear].running.gap += move_value;
        }
    }
}

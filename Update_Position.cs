using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Update_Position : Initialize
    {
        /// <summary>
        /// 車両位置を更新する
        /// </summary>
        public void update_position()
        {
            Lead_Car LC = new Lead_Car(lead_car);
            int ID = LC.ID;
            while (true)
            {
                _move_position(ID);
                if (ID == LC.ID) lead_car.gap = car[ID].running.gap;
                else
                {
                    if (lead_car.gap < car[ID].running.gap)
                    {
                        lead_car.gap = car[ID].running.gap;
                        lead_car.ID = ID;
                    }
                }
                ID = car[ID].running.around.rear;
                if (ID == LC.ID)
                {
                    int front = car[ID].running.around.front;
                    double front_rear_position = car[front].running.position.current - car[front].eigenvalue.length;
                    if (front_rear_position < 0) front_rear_position += parameter.length;
                    if (front_rear_position < car[ID].running.position.current) front_rear_position += parameter.length;
                    car[ID].running.gap = front_rear_position - car[ID].running.position.current;
                    if (lead_car.gap < car[ID].running.gap)
                    {
                        lead_car.gap = car[ID].running.gap;
                        lead_car.ID = ID;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 実際に車両を動かす
        /// </summary>
        /// <param name="ID">車両ID</param>
        private void _move_position(int ID)
        {
            double t = parameter.t;
            double v_previous = car[ID].running.velocity.previous;
            double a = car[ID].running.acceleration;
            double next_velocity = v_previous + a * t;
            double next_position = car[ID].running.position.previous;
            if (next_velocity < 0)
            {
                //時間解像度に対して，停止するまでの時間の方が短い
                //従って，きちんと停止する位置まで動かし，速度と加速度を0に変更する
                next_position += -v_previous * v_previous / (2 * a);
                car[ID].running.acceleration = 0;
                next_velocity = 0;
            }
            else next_position += v_previous * t + a * t * t / 2;
            if (next_position >= parameter.length) next_position -= parameter.length;
            //速度更新
            car[ID].running.velocity.current = next_velocity;
            //位置更新
            car[ID].running.position.current = next_position;
            //車間距離更新
            int front = car[ID].running.around.front;
            double front_rear_position = car[front].running.position.current - car[front].eigenvalue.length;
            if (front_rear_position < 0) front_rear_position += parameter.length;
            if (front_rear_position < next_position) front_rear_position += parameter.length;
            car[ID].running.gap = front_rear_position - next_position;
        }
    }
}

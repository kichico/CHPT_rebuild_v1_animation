using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Calculate_Rerecognition_Velocity_Difference : Calculate_Rerecognition_Optimal_Velocity
    {
        /// <summary>
        /// 速度差の認識確率
        /// </summary>
        /// <param name="ID">車両ID</param>
        /// <returns>速度差の認識確率</returns>
        public double calculate_Rerecognition_Vd_Rate(int ID)
        {
            double P;
            int acceleration_sign = driver[ID].running.acceleration.sign;
            double Nv = _calculate_Nv(ID);
            if (Nv < -1) Nv = -1;
            else if (Nv > 1) Nv = 1;
            double Av = 2 * A;
            double delta_v = driver[ID].running.delta.velocity = car[ID].running.velocity.current - driver[ID].running.v_optimal.current;
            if (delta_v <= 0) P = 1 / Av * Math.Log((1 + Math.Exp(-Nv / 0.1)) / (1 + Math.Exp(-1 / 0.1)));
            else P = 0.5 + 1 / Av * Math.Log((1 + Math.Exp(1 / 0.1)) / (1 + Math.Exp(-Nv / 0.1)));
            if (acceleration_sign == SignAcceleration.deceleration) P = 1 - P;
            return P;
        }

        /// <summary>
        /// 速度差を-1から1に正規化
        /// </summary>
        /// <param name="ID">車両ID</param>
        /// <returns>正規化された値</returns>
        private double _calculate_Nv(int ID)
        {
            double Vd = driver[ID].running.v_difference.current;
            double V = car[ID].running.velocity.current;
            double Voptimal = driver[ID].running.v_optimal.current;
            double Nv = 2 * Math.Abs(Voptimal - V) / Vd - 1;
            driver[ID].running.RR.fv.normalized = Nv;
            driver[ID].running.RR.fv.value = 1 / (1 + Math.Exp(-Nv / 0.1));
            return Nv;
        }
    }
}

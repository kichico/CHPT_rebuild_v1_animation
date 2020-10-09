using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Calculate_Rerecognition_Optimal_Velocity : Calculate_Optimal_Velocity
    {
        public double A;    //代表面積

        /// <summary>
        /// 車間距離の認識確率
        /// </summary>
        /// <param name="ID">車両ID</param>
        /// <returns>車間距離の認識率</returns>
        public double calculate_Rerecognition_Rate(int ID)
        {
            double P;
            calculate_optimal_velocity(ID);
            int front = car[ID].running.around.front;
            double V = car[ID].running.velocity.current;
            double V_f = car[front].running.velocity.current;
            double NG = driver[ID].running.RR.fg.normalized;
            if (NG < -1) NG = -1;
            else if (NG > 1) NG = 1;
            DGap DG = driver[ID].running.gap;
            double Ag = (DG.influenced - DG.closest) * A;
            double delta_G = driver[ID].running.delta.gap;
            if (delta_G <= 0) P = (DG.cruise - DG.closest) / Ag * Math.Log((1 + Math.Exp(-NG / 0.1)) / (1 + Math.Exp(-1 / 0.1)));
            else P = ((DG.cruise - DG.closest) * Math.Log((1 + Math.Exp(1 / 0.1)) / (1 + Math.Exp(-1 / 0.1))) + (DG.influenced - DG.cruise) * Math.Log((1 + Math.Exp(1 / 0.1)) / (1 + Math.Exp(-NG / 0.1)))) / Ag;
            if (V > V_f) P = 1 - P;
            return P;
        }
    }
}

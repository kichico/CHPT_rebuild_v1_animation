using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Change_Unit
    {
        /// <summary>
        /// km/hをm/sに変換
        /// </summary>
        /// <param name="v">速度[km/h]</param>
        /// <returns></returns>
        public double km_h__to__m_s(double v)
        {
            return v * 1000 / 3600;
        }

        /// <summary>
        /// m/sをkm/hに変換
        /// </summary>
        /// <param name="v">速度[m/s]</param>
        /// <returns></returns>
        public double m_s__to__km_h(double v)
        {
            return v * 3600 / 1000;
        }
    }
}

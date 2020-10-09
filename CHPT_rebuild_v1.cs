using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class CHPT_rebuild_v1 : Update_Position
    {
        public CHPT_rebuild_v1(double length, int N, double t, int Mode, int Driver_Mode)
        {
            parameter = new Parameter(length, t);
            random = new Random();
            this.N = N;
            this.Mode = Mode;
            this.Driver_Mode = Driver_Mode;
            A = _calculate_A();
        }

        public void Simulate()
        {
            decide_next_acceleration();
            update_position();
        }

        private	double _calculate_A()
        {
            return Math.Log((1 + Math.Exp(1 / 0.1)) / (1 + Math.Exp(-1 / 0.1)));
        }
    }
}

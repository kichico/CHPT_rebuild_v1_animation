using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Revised_Information
    {
        public List<int> v0;
        public List<int> v0_;
        public int lane_velocity;
        public void initialize_information(int N)
        {
            v0 = new List<int>(N);
            v0_ = new List<int>(N);
            for (int i = 0; i < N; i++)
            {
                v0.Add(0);
                v0_.Add(0);
            }
            lane_velocity = 0;
        }
    }
}

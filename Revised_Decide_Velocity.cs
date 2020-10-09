using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Revised_Decide_Velocity : Revised_Initialize
    {
        public void apply_rules_1_to_4()
        {
            _apply_rules_1_to_4();
        }

        private void _apply_rules_1_to_4()
        {
            information.lane_velocity = 0;
            for (int ID = 0; ID < N; ID++)
            {
                information.v0_[ID] = information.v0[ID];
                information.v0[ID] = car.velocity[ID];
                _rule1(ID);
                _rule2(ID);
                _rule3(ID);
                _rule4(ID);
                information.lane_velocity += car.velocity[ID];
            }
        }

        private void _rule1(int ID)
        {
            if (car.around.front.current.distance[ID] >= Revised_CI.G || car.velocity[ID] <= information.v0[car.around.front.current.ID[ID]]) car.velocity[ID]++;
            if (car.velocity[ID] > Revised_CI.Vmax) car.velocity[ID] = Revised_CI.Vmax;
        }

        private void _rule2(int ID)
        {
            int distance_S = 0;
            if (random.NextDouble() <= Revised_CI.q)
            {
                car.S[ID] = 1;
                if (random.NextDouble() < Revised_CI.r) car.S[ID] = Revised_CI.S;
                int s = 0;
                int id = ID;
                while (s < car.S[ID])
                {
                    distance_S += car.around.front.previous.distance[id];
                    id = car.around.front.previous.ID[id];
                    s++;
                }
                int canditate_v2 = distance_S - car.S[ID];
                if (canditate_v2 < car.velocity[ID]) car.velocity[ID] = canditate_v2;
            }
        }

        private void _rule3(int ID)
        {
            int s = 0;
            int id = ID;
            int distance_S = 0;
            while (s < car.S[ID])
            {
                distance_S += car.around.front.current.distance[id];
                id = car.around.front.current.ID[id];
                s++;
            }
            int canditate_v3 = distance_S - car.S[ID];
            if (canditate_v3 < car.velocity[ID]) car.velocity[ID] = canditate_v3;
        }

        private void _rule4(int ID)
        {
            double p = Revised_CI.p4;
            if (car.around.front.current.distance[ID] < Revised_CI.G)
            {
                int front = car.around.front.current.ID[ID];
                if (information.v0[ID] < information.v0[front]) p = Revised_CI.p3;
                else if (information.v0[ID] == information.v0[front]) p = Revised_CI.p2;
                else p = Revised_CI.p1;
            }
            if (random.NextDouble() < p)
            {
                int canditate_v4 = car.velocity[ID] - 1;
                if (1 < canditate_v4) car.velocity[ID] = canditate_v4;
                else car.velocity[ID] = 1;
            }
        }
    }
}

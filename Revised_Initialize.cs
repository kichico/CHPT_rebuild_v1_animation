using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    static class Revised_CI
    {
        public const int Vmax = 5;                 //最大速度
        public const int G = 15;                   //速度計算用車間基準
        public const int S = 2;                    //見通す車両は何台前か
        public const double r = 0.99;              //見通し確率
        public const double q = 0.99;              //スロースタート確率
        public const double p1 = 0.99;
        public const double p2 = 0.02;
        public const double p3 = 0.01;
        public const double p4 = 0.001;
    }

    class Revised_Lead_Car_ID
    {
        public int ID;
        public int maximum_gap;

        public Revised_Lead_Car_ID()
        {
            ID = new int();
            maximum_gap = new int();
        }

        public Revised_Lead_Car_ID(int ID,int maximum_gap)
        {
            this.ID = ID;
            this.maximum_gap = maximum_gap;
        }

        public Revised_Lead_Car_ID(Revised_Lead_Car_ID rr)
        {
            ID = rr.ID;
            maximum_gap = rr.maximum_gap;
        }
    }

    class Revised_Initialize
    {
        public int Mode;
        public Revised_Car car;
        public Revised_Information information;
        public Revised_Map_Information map_information;
        public int LaneLength;
        public int N;
        public Revised_Lead_Car_ID lead_car_ID;
        public Revised_Lead_Car_ID LCI_previous;
        public Random random;

        public bool initialize() { return _initialize(); }

        private bool _initialize()
        {
            car = new Revised_Car();
            information = new Revised_Information();
            map_information = new Revised_Map_Information();
            car.initialize_car(N);
            information.initialize_information(N);
            map_information.initialize_Map_Information(LaneLength);
            if (_initialize_cars_position())
            {
                _initial_search();
                return true;
            }
            else return false;
        }

        private bool _initialize_cars_position()
        {
            //各車両の初期位置をランダムに決定する
            if (Mode == SimulationMode.RandomMode)
            {
                List<int> cp = new List<int>(LaneLength);
                for (int i = 0; i < LaneLength; i++) cp.Add(i);
                for (int ID = 0; ID < N; ID++)
                {
                    int num = cp[random.Next(cp.Count)];
                    cp.Remove(num);
                    car.position.current[ID] = car.position.previous[ID] = num;
                    map_information.map.existence.current[num] = map_information.map.existence.previous[num] = true;
                    map_information.map.ID.current[num] = map_information.map.ID.previous[num] = ID;
                    if (random.NextDouble() < Revised_CI.r) car.S[ID] = Revised_CI.S;
                    else car.S[ID] = 1;
                    if (ID < N - 1 && cp.Count == 0) return false;
                }
            }
            else
            {
                for (int ID = 0; ID < N; ID++)
                {
                    int num = LaneLength - ID - 1;
                    if (num < 0) return false;
                    car.position.current[ID] = car.position.previous[ID] = num;
                    map_information.map.existence.current[num] = map_information.map.existence.previous[num] = true;
                    map_information.map.ID.current[num] = map_information.map.ID.previous[num] = ID;
                    if (random.NextDouble() < Revised_CI.r) car.S[ID] = Revised_CI.S;
                    else car.S[ID] = 1;
                }
            }
            return true;
        }

        private void _initial_search()
        {
            //ここでは初期配置の車について，前後車両情報のみを調査する
            lead_car_ID = new Revised_Lead_Car_ID();

            for (int i = 0; i < N; i++)
            {
                int start = 0;
                int rear = 0;
                for (int j = 0; j < LaneLength; j++)
                {
                    if (map_information.map.existence.current[j])
                    {
                        start = j;
                        rear = map_information.map.ID.current[j];
                        break;
                    }
                }
                bool existence = false;
                for (int j = 1; j <= LaneLength; j++)
                {
                    int position = start + j;
                    if (position >= LaneLength) position -= LaneLength;
                    if (map_information.map.existence.current[position])
                    {
                        int front = map_information.map.ID.current[position];
                        car.around.front.current.ID[rear] = car.around.front.previous.ID[rear] = front;
                        car.around.rear.current.ID[front] = car.around.rear.previous.ID[front] = rear;
                        int distance = car.position.current[front] - car.position.current[rear];
                        if (distance <= 0) distance += LaneLength;
                        car.around.front.current.distance[rear] = car.around.front.previous.distance[rear] = distance;
                        car.around.rear.current.distance[front] = car.around.rear.previous.distance[front] = distance;
                        if (!existence)
                        {
                            existence = true;
                            lead_car_ID.ID = rear;
                            lead_car_ID.maximum_gap = distance;
                        }
                        else
                        {
                            if (distance >= lead_car_ID.maximum_gap)
                            {
                                lead_car_ID.ID = rear;
                                lead_car_ID.maximum_gap = distance;
                            }
                        }
                        rear = front;
                    }
                }
            }
            LCI_previous = new Revised_Lead_Car_ID(lead_car_ID);
        }
    }
}

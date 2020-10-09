using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    static class Revised_Signal
    {
        public const int left = 0;
        public const int right = 1;
    }

    class Revised_Old_and_New_List
    {
        public List<int> previous;
        public List<int> current;
        public void initialize(int N)
        {
            previous = new List<int>(N);
            current = new List<int>(N);
            for (int i = 0; i < N; i++)
            {
                previous.Add(0);
                current.Add(0);
            }
        }
    }

    class Revised_ID_and_Distance
    {
        public List<int> ID;
        public List<int> distance;
        public void initialize(int N)
        {
            ID = new List<int>(N);
            distance = new List<int>(N);
            for (int i = 0; i < N; i++)
            {
                ID.Add(0);
                distance.Add(0);
            }
        }
    }

    class Revised_Old_and_New_Arround_Cars
    {
        public Revised_ID_and_Distance previous;
        public Revised_ID_and_Distance current;
        public void initialize(int N)
        {
            previous = new Revised_ID_and_Distance();
            current = new Revised_ID_and_Distance();
            previous.initialize(N);
            current.initialize(N);
        }
    }

    class Revised_Around_Cars_Information
    {
        public Revised_Old_and_New_Arround_Cars front;
        public Revised_Old_and_New_Arround_Cars rear;
        public void initialize(int N)
        {
            front = new Revised_Old_and_New_Arround_Cars();
            rear = new Revised_Old_and_New_Arround_Cars();
            front.initialize(N);
            rear.initialize(N);
        }
    }

    class Revised_Car
    {
        public Revised_Old_and_New_List position;
        public List<int> velocity;
        public List<int> S;
        public Revised_Around_Cars_Information around;
        public List<double> accelaration;

        public void initialize_car(int N)
        {
            velocity = new List<int>(N);
            S = new List<int>(N);
            accelaration = new List<double>(N);
            for (int i = 0; i < N; i++)
            {
                velocity.Add(0);
                S.Add(0);
                accelaration.Add(0);
            }
            position = new Revised_Old_and_New_List();
            around = new Revised_Around_Cars_Information();
            position.initialize(N);
            around.initialize(N);
        }

        public void update_previous_information()
        {
            position.previous = new List<int>(position.current);
            around.front.previous.ID = new List<int>(around.front.current.ID);
            around.rear.previous.ID = new List<int>(around.rear.current.ID);
            around.front.previous.distance = new List<int>(around.front.current.distance);
            around.rear.previous.distance = new List<int>(around.rear.current.distance);
        }
    }
}

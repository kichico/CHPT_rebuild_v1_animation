using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Revised_Old_and_New_Bool
    {
        public List<bool> current;
        public List<bool> previous;

        public void initialize(int length)
        {
            current = new List<bool>(length);
            previous = new List<bool>(length);
            for (int i = 0; i < length; i++)
            {
                current.Add(false);
                previous.Add(false);
            }
        }
    }

    class Revised_Old_and_New_Int
    {
        public List<int> current;
        public List<int> previous;
        public void initialize(int length)
        {
            current = new List<int>(length);
            previous = new List<int>(length);
            for (int i = 0; i < length; i++)
            {
                current.Add(0);
                previous.Add(0);
            }
        }
    }

    class Revised_Existence_and_ID
    {
        public Revised_Old_and_New_Bool existence;
        public Revised_Old_and_New_Int ID;

        public void initialize(int length)
        {
            existence = new Revised_Old_and_New_Bool();
            ID = new Revised_Old_and_New_Int();
            existence.initialize(length);
            ID.initialize(length);
        }
    }

    class Revised_Updating_Position
    {
        public List<bool> existence;
        public List<int> ID;
        public void initialize(int length)
        {
            existence = new List<bool>(length);
            ID = new List<int>(length);
            for (int i = 0; i < length; i++)
            {
                existence.Add(false);
                ID.Add(0);
            }
        }
    }

    class Revised_Map_Information
    {
        public Revised_Existence_and_ID map;
        public Revised_Updating_Position update_position;

        public void initialize_Map_Information(int lane_length)
        {
            map = new Revised_Existence_and_ID();
            update_position = new Revised_Updating_Position();
            map.initialize(lane_length);
            update_position.initialize(lane_length);
        }
    }
}

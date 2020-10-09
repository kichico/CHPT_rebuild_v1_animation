using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Revised : Revised_Update_Position
    {
        public Revised(int lane_length, int number_of_cars, int Mode)
        {
            LaneLength = (int)(1.0 * lane_length / 1000 / 100 * 3600 * 5 * 0.5);    //V5=時速100kmになる
            N = number_of_cars;
            random = new Random();
            this.Mode = Mode;
        }

        public bool initialize_position() { return initialize(); }

        public void Simulate()
        {
            apply_rules_1_to_4();
            //位置情報更新の前に，現在情報を保存
            car.update_previous_information();  //一括情報更新 (位置情報，自車周り情報)
            map_information.map.existence.previous = new List<bool>(map_information.map.existence.current);
            map_information.map.ID.previous = new List<int>(map_information.map.ID.current);
            map_information.update_position.ID = new List<int>(map_information.map.ID.current);
            LCI_previous = new Revised_Lead_Car_ID(lead_car_ID);
            canditate_velocity = new List<int>(N);
            still_room_for_movement = new List<bool>(N);
            for (int i = 0; i < N; i++)
            {
                canditate_velocity.Add(0);
                still_room_for_movement.Add(true);
            }
            //位置を更新
            update_position();
            //全情報更新
            car.velocity = new List<int>(canditate_velocity);
            map_information.map.existence.current = new List<bool>(map_information.update_position.existence);
            map_information.map.ID.current = new List<int>(map_information.update_position.ID);
        }
    }
}

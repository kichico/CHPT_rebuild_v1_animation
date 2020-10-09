using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    class Revised_Update_Position : Revised_Decide_Velocity
    {
        public List<bool> still_room_for_movement;
        public List<int> canditate_velocity;
        private Revised_Lead_Car_ID LCI;

        public void update_position() { _update_position(); }

        private void _update_position()
        {
            //全車両の速度が変化しなくなるまで繰り返す
            //常時，前後関係を更新する
            //次ステップに備え，先頭車両を選定する
            LCI = new Revised_Lead_Car_ID(0, 0);
            //ここも並列計算できる
            bool fg = true;
            while (fg) fg = _update_position_front_to_back();
            lead_car_ID = new Revised_Lead_Car_ID(LCI);
        }

        private bool _update_position_front_to_back()
        {
            for (int i = 0; i < LaneLength; i++) map_information.update_position.existence[i] = false;
            bool flag = true;
            int previous_lane_velocity = information.lane_velocity;
            information.lane_velocity = 0;
            int ID = lead_car_ID.ID;
            //ループの前に先頭車両の前方車両(最後尾)の位置を登録しておく
            map_information.update_position.existence[car.position.current[car.around.front.current.ID[ID]]] = true;
            //先頭車両から，後続車両へ向かってループ
            //つまり，先頭車両から順に後続車両に向かって自車の位置を更新していく
            bool existence = false;
            while (true)
            {
                //まだ移動の余地がある車両のみ考える
                if (still_room_for_movement[ID]) _move_forward_car(ID);
                else
                {
                    int next_position = car.position.current[ID];
                    map_information.update_position.existence[next_position] = true;
                    map_information.update_position.ID[next_position] = ID;
                }
                if (ID == lead_car_ID.ID) map_information.update_position.existence[car.position.current[car.around.front.current.ID[ID]]] = false;
                information.lane_velocity += canditate_velocity[ID];
                if (!existence)
                {
                    existence = true;
                    LCI.ID = ID;
                    LCI.maximum_gap = car.around.front.current.distance[ID];
                }
                else
                {
                    if (car.around.front.current.distance[ID] > LCI.maximum_gap)
                    {
                        LCI.ID = ID;
                        LCI.maximum_gap = car.around.front.current.distance[ID];
                    }
                }
                if (lead_car_ID.ID == car.around.rear.current.ID[ID])
                {
                    //先頭車両に戻ってきたので終了
                    //先頭車両と最後尾車両の距離を更新
                    int rear_ID = car.around.rear.current.ID[ID];
                    int rear_distance = car.position.current[ID] - car.position.current[rear_ID];
                    if (rear_distance <= 0) rear_distance += LaneLength;
                    car.around.rear.current.distance[ID] = car.around.front.current.distance[rear_ID] = rear_distance;
                    if (rear_distance > LCI.maximum_gap)
                    {
                        LCI.ID = rear_ID;
                        LCI.maximum_gap = rear_distance;
                    }
                    break;
                }
                else ID = car.around.rear.current.ID[ID];
            }
            if (previous_lane_velocity == information.lane_velocity || !still_room_for_movement[lead_car_ID.ID]) flag = false;
            return flag;
        }

        private void _move_forward_car(int ID)
        {
            //現在の位置を使って次のpositionを一つずつ加算していく
            //最大移動位置になっても前方車両に追いつかなければ，最大移動できる
            int remaining_distance = car.velocity[ID] - canditate_velocity[ID];
            int _v = canditate_velocity[ID];
            bool clash = false;
            int front_position = 0;
            for (int i = 1; i <= remaining_distance; i++)
            {
                front_position = car.position.current[ID] + i;
                if (front_position >= LaneLength) front_position -= LaneLength;
                if (map_information.update_position.existence[front_position])
                {
                    clash = true;
                    break;
                }
            }
            int next_position = car.position.previous[ID] + car.velocity[ID];
            if (clash) next_position = front_position - 1;
            else still_room_for_movement[ID] = false;   //最大移動を達成
            if (next_position >= LaneLength) next_position -= LaneLength;
            if (next_position < 0) next_position += LaneLength;
            int v = next_position - car.position.previous[ID];
            if (v < 0) v += LaneLength;
            canditate_velocity[ID] = v;
            car.accelaration[ID] = v - information.v0[ID];
            map_information.update_position.existence[next_position] = true;
            map_information.update_position.ID[next_position] = ID;
            car.position.current[ID] = next_position;
            //自車と前方車両の距離を更新
            int front_ID = car.around.front.current.ID[ID];
            int front_distance = car.position.current[front_ID] - car.position.current[ID];
            if (front_distance <= 0) front_distance += LaneLength;
            car.around.front.current.distance[ID] = car.around.rear.current.distance[front_ID] = front_distance;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    /// <summary>
    /// 加減速の定義
    /// </summary>
    static class SignAcceleration
    {
        public const int acceleration = 0;
        public const int deceleration = 1;
    }

    /// <summary>
    /// 足のペダル位置
    /// </summary>
    static class FootPosition
    {
        public const int accel_pedal = 0;
        public const int brake_pedal = 1;
        public const int accel_to_brake = 2;
        public const int brake_to_accel = 3;
    }

    /// <summary>
    /// ドライバーモード
    /// </summary>
    static class DriverMode
    {
        public const int Human = 0;
        public const int Auto = 1;
    }

    /// <summary>
    /// シミュレーションのモード
    /// </summary>
    static class SimulationMode
    {
        public const int RandomMode = 0;
        public const int EqualMode = 1;
        public const int PolarizationInitialMode = 2;
    }
}

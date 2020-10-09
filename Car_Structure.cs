using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    /// <summary>
    /// 現在と前の値を保持
    /// </summary>
    class Current_Previous
    {
        public double current;
        public double previous;

        /// <summary>
        /// 値指定なしで実態を持たせる
        /// </summary>
        public Current_Previous()
        {
            current = new double();
            previous = new double();
        }

        /// <summary>
        /// 値を直接指定し，初期化する
        /// </summary>
        /// <param name="current">現在の値</param>
        /// <param name="previous">前の値</param>
        public Current_Previous(double current, double previous)
        {
            this.current = current;
            this.previous = previous;
        }

        /// <summary>
        /// 値コピーを作成する
        /// </summary>
        /// <param name="CP">Current_Previous</param>
        public Current_Previous(Current_Previous CP)
        {
            current = CP.current;
            previous = CP.previous;
        }
    }

    /// <summary>
    /// 周りの車両のIDを保持
    /// </summary>
    class CAround
    {
        public int front;  //前方車両ID
        public int rear;   //後方車両ID

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public CAround()
        {
            front = new int();
            rear = new int();
        }

        /// <summary>
        /// 値を直接指定する
        /// </summary>
        /// <param name="front">前方車両ID</param>
        /// <param name="rear">後方車両ID</param>
        public CAround(int front, int rear)
        {
            this.front = front;
            this.rear = rear;
        }

        /// <summary>
        /// 値コピーを作成する
        /// </summary>
        /// <param name="around">CAround</param>
        public CAround(CAround around)
        {
            front = around.front;
            rear = around.rear;
        }
    }

    /// <summary>
    /// 車の持っている加速度を保持
    /// </summary>
    class CAcceleration
    {
        public double maximum;     //最大加速度
        public double braking;     //最大減速度
        public double resistance;  //走行抵抗減速度
        public double minimum;     //最小加速度の大きさ

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public CAcceleration()
        {
            maximum = new double();
            braking = new double();
            resistance = new double();
            minimum = new double();
        }

        /// <summary>
        /// 値指定で初期化
        /// </summary>
        /// <param name="maximum">最大加速度</param>
        /// <param name="braking">最大減速度の大きさ(>0)</param>
        /// <param name="resistance">抵抗減速度の大きさ(>0)</param>
        public CAcceleration(double maximum, double braking, double resistance, double minimum)
        {
            this.maximum = maximum;
            this.braking = braking;
            this.resistance = resistance;
            this.minimum = minimum;
        }

        /// <summary>
        /// 値コピーの作成
        /// </summary>
        /// <param name="acceleration">CAcceleration</param>
        public CAcceleration(CAcceleration acceleration)
        {
            maximum = acceleration.maximum;
            braking = acceleration.braking;
            resistance = acceleration.resistance;
            minimum = acceleration.minimum;
        }
    }

    /// <summary>
    /// 時々刻々変化する値を保持する
    /// </summary>
    class CRunning
    {
        public double acceleration;
        public Current_Previous velocity;
        public double gap;
        public Current_Previous position;  //全てフロントの位置
        public CAround around;

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public CRunning()
        {
            acceleration = new double();
            velocity = new Current_Previous();
            gap = new double();
            position = new Current_Previous();
            around = new CAround();
        }

        /// <summary>
        /// 値コピーを作成する
        /// </summary>
        /// <param name="running">CRunning</param>
        public CRunning(CRunning running)
        {
            acceleration = running.acceleration;
            velocity = new Current_Previous(running.velocity);
            gap = running.gap;
            position = new Current_Previous(running.position);
            around = new CAround(running.around);
        }
    }

    /// <summary>
    /// 車両が持っている特性を保持する
    /// </summary>
    class CEigenvalue
    {
        public CAcceleration acceleration; //加速度
        public double maximum_velocity;    //最大速度
        public double length;              //全長

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public CEigenvalue()
        {
            acceleration = new CAcceleration();
            maximum_velocity = new double();
            length = new double();
        }

        /// <summary>
        /// 値コピーを作成する
        /// </summary>
        /// <param name="eigenvalue"></param>
        public CEigenvalue(CEigenvalue eigenvalue)
        {
            acceleration = new CAcceleration(eigenvalue.acceleration);
            maximum_velocity = eigenvalue.maximum_velocity;
            length = eigenvalue.length;
        }
    }

    /// <summary>
    /// 車両関係の情報を保持する
    /// </summary>
    class Car_Structure
    {
        public CRunning running;
        public CEigenvalue eigenvalue;

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public Car_Structure()
        {
            running = new CRunning();
            eigenvalue = new CEigenvalue();
        }

        /// <summary>
        /// 値コピーを作成する
        /// </summary>
        /// <param name="car">Car_Structure</param>
        public Car_Structure(Car_Structure car)
        {
            running = new CRunning(car.running);
            eigenvalue = new CEigenvalue(car.eigenvalue);
        }
    }
}

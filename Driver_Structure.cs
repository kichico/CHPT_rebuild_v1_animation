using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHPT_rebuild_v1_animation
{
    /// <summary>
    /// 車間距離関係保持
    /// </summary>
    class DGap
    {
        public double closest;      //安全最短車間距離
        public double cruise;       //巡航車間距離
        public double influenced;   //影響を受ける最大車間距離（絶対安全車間距離）

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DGap()
        {
            closest = new double();
            cruise = new double();
            influenced = new double();
        }

        /// <summary>
        /// 値を代入して初期化
        /// </summary>
        /// <param name="closest">最短安全車間距離</param>
        /// <param name="cruise">巡航車間距離</param>
        /// <param name="influenced">絶対安全車間距離</param>
        /// <param name="feeling">ドライバーの気持ち</param>
        public DGap(double closest, double cruise, double influenced, int feeling)
        {
            this.closest = closest;
            this.cruise = cruise;
            this.influenced = influenced;
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="gap"></param>
        public DGap(DGap gap)
        {
            closest = gap.closest;
            cruise = gap.cruise;
            influenced = gap.influenced;
        }
    }

    /// <summary>
    /// 停止時における車間距離関係保持
    /// </summary>
    class DEGap
    {
        public double stop;
        public double move;

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DEGap()
        {
            stop = new double();
            move = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="stop">double</param>
        /// <param name="move">double</param>
        public DEGap(double stop, double move)
        {
            this.stop = stop;
            this.move = move;
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="gap">DEGap</param>
        public DEGap(DEGap gap)
        {
            stop = gap.stop;
            move = gap.move;
        }
    }

    /// <summary>
    /// 認識関係の乱数の情報を保持する
    /// </summary>
    class Random_Value
    {
        public double Pg;        //認識確率
        public double Pv;        //メーター確認確率

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public Random_Value()
        {
            Pg = new double();
            Pv = new double();
        }

        /// <summary>
        /// 値指定で初期化
        /// </summary>
        /// <param name="Pg">Pgの乱数</param>
        /// <param name="Pv">Pvの乱数</param>
        public Random_Value(double Pg, double Pv)
        {
            this.Pg = Pg;
            this.Pv = Pv;
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="random_value">Random_Value</param>
        public Random_Value(Random_Value random_value)
        {
            Pg = random_value.Pg;
            Pv = random_value.Pv;
        }
    }

    /// <summary>
    /// 認識率の値と正規化された値の情報を保持する
    /// </summary>
    class FNV
    {
        public double value;        //値
        public double normalized;   //正規化された値

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public FNV()
        {
            value = new double();
            normalized = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="normalized">正規化された値</param>
        public FNV(double value, double normalized)
        {
            this.value = value;
            this.normalized = normalized;
        }

        /// <summary>
        /// 値コピーを作成する
        /// </summary>
        /// <param name="fnv">FNV</param>
        public FNV(FNV fnv)
        {
            value = fnv.value;
            normalized = fnv.normalized;
        }
    }
    
    /// <summary>
    /// 認識率関係の情報を保持する
    /// </summary>
    class Recognition_Rate
    {
        public bool effectiveness;          //判断が有効かどうか
        public Random_Value random_value;   //乱数
        public FNV fv;                      //速度差
        public FNV fg;                      //車間距離

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public Recognition_Rate()
        {
            effectiveness = new bool();
            random_value = new Random_Value();
            fv = new FNV();
            fg = new FNV();
        }

        /// <summary>
        /// 値コピーを作成する
        /// </summary>
        /// <param name="recognition">Recognition_Rate</param>
        public Recognition_Rate(Recognition_Rate recognition)
        {
            effectiveness = recognition.effectiveness;
            random_value = new Random_Value(recognition.random_value);
            fv = new FNV(recognition.fv);
            fg = new FNV(recognition.fg);
        }
    }

    /// <summary>
    /// ペダルの踏みかえの情報を保持する
    /// </summary>
    class Pedal
    {
        public int foot_position;      //踏みかえているか否か
        public double time_elapsed;    //踏みかえ始めからの経過時間
        public double time_required;   //踏みかえに要する時間

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public Pedal()
        {
            foot_position = new int();
            time_elapsed = new double();
            time_required = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="changing">踏みかえているか</param>
        /// <param name="time_elapsed">経過時間</param>
        /// <param name="time_required">必要時間</param>
        public Pedal(int foot_position, double time_elapsed, double time_required)
        {
            this.foot_position = foot_position;
            this.time_elapsed = time_elapsed;
            this.time_required = time_required;
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="pedal">Pedal</param>
        public Pedal(Pedal pedal)
        {
            foot_position = pedal.foot_position;
            time_elapsed = pedal.time_elapsed;
            time_required = pedal.time_required;
        }
    }

    /// <summary>
    /// 速度関係の情報を保持
    /// </summary>
    class DVelocity
    {
        public double cruise;          //巡航速度
        public double c_difference;    //絶対速度差 at v_cruise
        public double s_difference;    //絶対速度差 at v_stop

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DVelocity()
        {
            cruise = new double();
            c_difference = new double();
            s_difference = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="cruise">巡航速度</param>
        /// <param name="c_difference">巡航速度における絶対に速度差を認識する最小速度差</param>
        /// <param name="s_difference">停止しなければならない時に停止していないことを絶対に認識する最小速度差</param>
        public DVelocity(double cruise, double c_difference, double s_difference)
        {
            this.cruise = cruise;
            this.c_difference = c_difference;
            this.s_difference = s_difference;
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="velocity">DVelocity</param>
        public DVelocity(DVelocity velocity)
        {
            cruise = velocity.cruise;
            c_difference = velocity.c_difference;
            s_difference = velocity.s_difference;
        }
    }
    
    /// <summary>
    /// 瞬時値と現在の値
    /// </summary>
    class DIC
    {
        public double instantaneous;       //瞬時値
        public double current;             //ドライバーが現在保持している値

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DIC()
        {
            instantaneous = new double();
            current = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="instantaneous">瞬時値</param>
        /// <param name="current">現在の値</param>
        public DIC(double instantaneous, double current)
        {
            this.instantaneous = instantaneous;
            this.current = current;
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="dic">DIC</param>
        public DIC(DIC dic)
        {
            instantaneous = dic.instantaneous;
            current = dic.current;
        }
    }

    /// <summary>
    /// 速度差関係の情報を保持する
    /// </summary>
    class DVDifference
    {
        public double at_firtst_time;   //最初に気が付いた時の値
        public double current;          //ドライバーが現在保持している値

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DVDifference()
        {
            at_firtst_time = new double();
            current = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="at_firtst_time">最初の値</param>
        /// <param name="current">現在の値</param>
        public DVDifference(double at_firtst_time, double current)
        {
            this.at_firtst_time = at_firtst_time;
            this.current = current;
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="difference">DVDifference</param>
        public DVDifference(DVDifference difference)
        {
            at_firtst_time = difference.at_firtst_time;
            current = difference.current;
        }
    }

    /// <summary>
    /// 加速度の情報を保持する
    /// </summary>
    class DAcceleration
    {
        public double value;    //最後に判断した際の加速度(14)式より
        public int sign;        //加速か減速か

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DAcceleration()
        {
            value = new double();
            sign = new int();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="sign">加減速の向き</param>
        public DAcceleration(double value, int sign)
        {
            this.value = value;
            this.sign = sign;
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="acceleration">DAcceleration</param>
        public DAcceleration(DAcceleration acceleration)
        {
            value = acceleration.value;
            sign = acceleration.sign;
        }
    }

    /// <summary>
    /// ドライバー特有の加速度の情報を保持する
    /// </summary>
    class DEAcceleration
    {
        public double acceleration;
        public double deceleration;

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DEAcceleration()
        {
            acceleration = new double();
            deceleration = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="acceleration">加速度</param>
        /// <param name="deceleration">減速度の大きさ</param>
        public DEAcceleration(double acceleration, double deceleration)
        {
            this.acceleration = acceleration;
            this.deceleration = deceleration;
        }

        /// <summary>
        /// コピーを作成する
        /// </summary>
        /// <param name="DEA">DEAcceleration</param>
        public DEAcceleration(DEAcceleration DEA)
        {
            acceleration = DEA.acceleration;
            deceleration = DEA.deceleration;
        }
    }

    /// <summary>
    /// 差の情報を保持する
    /// </summary>
    class DDelta
    {
        public double velocity;
        public double gap;

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DDelta()
        {
            velocity = new double();
            gap = new double();
        }

        /// <summary>
        /// 値を指定して初期化
        /// </summary>
        /// <param name="velocity">速度差</param>
        /// <param name="gap">車間距離差</param>
        public DDelta(double velocity,double gap)
        {
            this.velocity = velocity;
            this.gap = gap;
        }

        /// <summary>
        /// コピーを作成する
        /// </summary>
        /// <param name="delta">DDelta</param>
        public DDelta(DDelta delta)
        {
            velocity = delta.velocity;
            gap = delta.gap;
        }
    }
    
    /// <summary>
    /// 時々刻々変化する情報を保持する
    /// </summary>
    class DRunning
    {
        public DAcceleration acceleration;  //加速度
        public DIC v_optimal;               //希求速度
        public DVDifference v_difference;   //速度差の認識範囲
        public DGap gap;                    //車間距離関係
        public DDelta delta;                //速度差，車間距離差
        public Recognition_Rate RR;         //判断確率, 認識率関係
        public Pedal pedal;                 //ペダルの踏みかえ関係

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DRunning()
        {
            acceleration = new DAcceleration();
            v_optimal = new DIC();
            v_difference = new DVDifference();
            gap = new DGap();
            delta = new DDelta();
            RR = new Recognition_Rate();
            pedal = new Pedal();
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="running">DRunning</param>
        public DRunning(DRunning running)
        {
            acceleration = new DAcceleration(running.acceleration);
            v_optimal = new DIC(running.v_optimal);
            v_difference = new DVDifference(running.v_difference);
            gap = new DGap(running.gap);
            delta = new DDelta(running.delta);
            RR = new Recognition_Rate(running.RR);
            pedal = new Pedal(running.pedal);
        }
    }

    /// <summary>
    /// ドライバー特有の情報
    /// </summary>
    class DEigenvalue
    {
        public double operation_time;       //動作時間[s]
        public DEAcceleration acceleration; //許容最大加速度[m/s^2]
        public DVelocity velocity;          //速度関係
        public DEGap gap;			        //停止時における車間距離関係

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public DEigenvalue()
        {
            operation_time = new double();
            acceleration = new DEAcceleration();
            velocity = new DVelocity();
            gap = new DEGap();
        }

        /// <summary>
        /// 値コピーを作成する
        /// </summary>
        /// <param name="eigenvalue">DEigenvalue</param>
        public DEigenvalue(DEigenvalue eigenvalue)
        {
            operation_time = eigenvalue.operation_time;
            acceleration = new DEAcceleration(eigenvalue.acceleration);
            velocity = new DVelocity(eigenvalue.velocity);
            gap = new DEGap(eigenvalue.gap);
        }
    }

    /// <summary>
    /// ドライバー関係の情報を保持する
    /// </summary>
    class Driver_Structure
    {
        public DRunning running;
        public DEigenvalue eigenvalue;

        /// <summary>
        /// 実態を持たせる
        /// </summary>
        public Driver_Structure()
        {
            running = new DRunning();
            eigenvalue = new DEigenvalue();
        }

        /// <summary>
        /// 値のコピーを作成する
        /// </summary>
        /// <param name="driver">Driver_Structure</param>
        public Driver_Structure(Driver_Structure driver)
        {
            running = new DRunning(driver.running);
            eigenvalue = new DEigenvalue(driver.eigenvalue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHPT_rebuild_v1_animation
{
    public partial class Form1 : Form
    {
        private bool Acceptable;
        private bool DrawCarsFlag;
        private bool Animated;
        private double Length;
        private int CarNum;
        private double time;
        private double Bai;
        private CHPT_rebuild_v1 v1;
        private Revised revised;
        private List<CarSize> CarSize;
        private Timer timer;
        private int n;
        private ulong z;

        private float RoadWidth;
        private float hundred_meter;
        private int RoadTop;
        private int RoadBottom;
        private float startY;
        private float endY;
        private float hundred_meter_X;
        private float fifty_meter_X;
        private Rectangle point0;
        private Rectangle point50;
        private Rectangle point100;
        private Rectangle Velocity;
        private Rectangle ETime;

        private List<float> pposition;
        private bool CHPT;

        public Form1()
        {
            InitializeComponent();
            DrawCarsFlag = false;
            Animated = false;
            CarSize = new List<CarSize>();
            Invalidate();
            Refresh();
        }

        /// <summary>
        /// Formロード時に，画面中央に横幅最大で表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Width = Screen.PrimaryScreen.Bounds.Width;
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;
            Left = 0;
            CMBClass mM1 = new CMBClass(SimulationMode.RandomMode, "ランダムモード");
            CMBClass mM2 = new CMBClass(SimulationMode.EqualMode, "均等モード");
            CMBClass mM3 = new CMBClass(SimulationMode.PolarizationInitialMode, "偏極初期配置モード");
            CMBClass mD1 = new CMBClass(DriverMode.Human, "手動運転モード");
            CMBClass mD2 = new CMBClass(DriverMode.Auto, "自動運転モード");
            CMBClass MM1 = new CMBClass(0, "CHPTモデル");
            CMBClass MM2 = new CMBClass(1, "Revised-SBFSモデル");
            CMB_Mode.Items.Add(mM1);
            CMB_Mode.Items.Add(mM2);
            CMB_Mode.Items.Add(mM3);
            CMB_Mode.Text = mM1.ToString();
            CMB_DriverSelect.Items.Add(mD1);
            CMB_DriverSelect.Items.Add(mD2);
            CMB_DriverSelect.Text = mD1.ToString();
            CMB_ModelMode.Items.Add(MM1);
            CMB_ModelMode.Items.Add(MM2);
            CMB_ModelMode.Text = MM1.ToString();
            z = 0;
        }

        /// <summary>
        /// 初期配置ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Set_Click(object sender, EventArgs e)
        {
            Length = 1000;
            CarNum = 1;
            time = 0.05;
            Bai = 1;
            BTN_Start.Text = "Start";
            BTN_Start.Enabled = false;
            BTN_Reset.Enabled = false;
            if (Check_TextBox_TXT(TXT_Length, ref Length, "全周"))
            {
                if (Check_TextBox_TXT(TXT_CarNum, ref CarNum, "車両台数"))
                {
                    if (Check_TextBox_TXT(TXT_time, ref time, "時間解像度"))
                    {
                        if (Check_TextBox_TXT(TXT_Bai, ref Bai, "倍速"))
                        {
                            CMBClass mM = (CMBClass)CMB_Mode.SelectedItem;
                            CMBClass mD = (CMBClass)CMB_DriverSelect.SelectedItem;
                            if (CMB_ModelMode.Text == "CHPTモデル")
                            {
                                CHPT = true;
                                v1 = new CHPT_rebuild_v1(Length, CarNum, time, mM.Key, mD.Key);
                                Acceptable = v1.initialize();
                            }
                            else
                            {
                                CHPT = false;
                                revised = new Revised((int)(Length + 0.5), CarNum, mM.Key);
                                Acceptable = revised.initialize_position();
                                pposition = new List<float>(revised.N);
                                if (Acceptable) for (int i = 0; i < revised.N; i++) pposition.Add(revised.car.position.previous[i]);
                            }
                            if (Acceptable)
                            {
                                DrawCarsFlag = true;
                                GetCarSize();
                                BTN_Start.Enabled = true;
                                n = 0;
                                z = 0;
                            }
                        }
                    }
                }
            }
            Invalidate();
            Refresh();
        }

        /// <summary>
        /// 初期配置ボタンの使用可能変更時呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Set_EnabledChanged(object sender, EventArgs e)
        {
            if (BTN_Set.Enabled)
            {
                //全ての編集を有効にする
                TXT_Length.Enabled = true;
                TXT_CarNum.Enabled = true;
                TXT_time.Enabled = true;
                TXT_Bai.Enabled = true;
                CMB_Mode.Enabled = true;
                CMB_DriverSelect.Enabled = true;
            }
            else
            {
                //全ての編集を無効にする
                TXT_Length.Enabled = false;
                TXT_CarNum.Enabled = false;
                TXT_time.Enabled = false;
                TXT_Bai.Enabled = false;
                CMB_Mode.Enabled = false;
                CMB_DriverSelect.Enabled = false;
            }
        }

        /// <summary>
        /// スタートボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Start_Click(object sender, EventArgs e)
        {
            if (BTN_Start.Text == "Start")
            {
                if (Acceptable)
                {
                    Animated = true;
                    BTN_Set.Enabled = false;
                    BTN_Start.Text = "Stop";
                    BTN_Start.Refresh();
                    timer = new Timer();
                    timer.Interval = (int)(1000 * time / Bai + 0.5);
                    timer.Tick += new EventHandler(Update);
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("全周を増やすか車両台数を減らしてください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TXT_Length.Focus();
                }
            }
            else if (BTN_Start.Text == "Stop")
            {
                Animated = false;
                timer.Stop();
                BTN_Start.Text = "Restart";
                TXT_Bai.Enabled = true;
                BTN_Reset.Enabled = true;
            }
            else
            {
                if (Check_TextBox_TXT(TXT_Bai, ref Bai, "倍速"))
                {
                    Animated = true;
                    if (!CHPT) revised.Simulate();
                    timer = new Timer();
                    timer.Interval = (int)(1000 * time / Bai + 0.5);
                    timer.Tick += new EventHandler(Update);
                    timer.Start();
                    BTN_Start.Text = "Stop";
                    TXT_Bai.Enabled = false;
                    BTN_Set.Enabled = false;
                    BTN_Reset.Enabled = false;
                }
            }
            Refresh();
        }

        /// <summary>
        /// リセットボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Reset_Click(object sender, EventArgs e)
        {
            BTN_Set.Enabled = true;
            BTN_Start.Text = "Start";
            BTN_Start.Enabled = false;
            DrawCarsFlag = false;
            Acceptable = false;
            Animated = false;
            z = 0;
            Invalidate();
            Refresh();
        }

        /// <summary>
        /// キャンセルボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// サイズ変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            Bitmap canvas = new Bitmap(PB.Width, PB.Height);
            Graphics g = Graphics.FromImage(canvas);
            PB.Width = Width - 16;
            if (DrawCarsFlag) GetCarSize();
            Invalidate();
            int left = Width - 143;
            int dY = 17 + BTN_Set.Width;
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    BTN_Cancel.Left = left;
                    dY = 17 + BTN_Cancel.Width;
                }
                else if (i == 1)
                {
                    BTN_Reset.Left = left;
                    dY = 17 + BTN_Reset.Width;
                }
                else if (i == 2)
                {
                    BTN_Start.Left = left;
                    dY = 17 + BTN_Start.Width;
                }
                else if (i == 3)
                {
                    BTN_Set.Left = left;
                    dY = 17 + BTN_Set.Width;
                }
                left -= dY;
            }
            Refresh();
        }

        /// <summary>
        /// 整数値要求のTextの値が正しいかどうかチェック
        /// </summary>
        /// <param name="obj">objectの名前</param>
        /// <param name="Num">入力値を格納する変数名(ref)</param>
        /// <param name="SDisplay">ボックスの名前</param>
        /// <returns></returns>
        private bool Check_TextBox_TXT(TextBox obj, ref int Num, string SDisplay)
        {
            bool fg = false;
            string S = obj.Text.ToString();
            if (S == "")
            {
                MessageBox.Show(SDisplay + "を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obj.Focus();
            }
            else if (!int.TryParse(S, out Num))
            {
                MessageBox.Show(SDisplay + "に数字以外，または整数値以外が含まれています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obj.Text = Num.ToString();
                obj.Focus();
            }
            else
            {
                if (Num == 0)
                {
                    MessageBox.Show(SDisplay + "に0より大きな整数値を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    obj.Focus();
                }
                else fg = true;
            }
            return fg;
        }

        /// <summary>
        /// 倍精度浮遊点小数値要求のTextの値が正しいかどうかチェック
        /// </summary>
        /// <param name="obj">objectの名前</param>
        /// <param name="Num">入力値を格納する変数名(ref)</param>
        /// <param name="SDisplay">ボックスの名前</param>
        /// <returns></returns>
        private bool Check_TextBox_TXT(TextBox obj, ref double Num, string SDisplay)
        {
            bool fg = false;
            string S = obj.Text.ToString();
            if (S == "")
            {
                MessageBox.Show(SDisplay + "を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obj.Focus();
            }
            else if (!double.TryParse(S, out Num))
            {
                MessageBox.Show(SDisplay + "に数字以外が含まれています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obj.Text = Num.ToString();
                obj.Focus();
            }
            else
            {
                if (Num == 0)
                {
                    MessageBox.Show(SDisplay + "に0より大きな値を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    obj.Focus();
                }
                else fg = true;
            }
            return fg;
        }

        /// <summary>
        /// 車や車線のサイズを取得する
        /// </summary>
        private void GetCarSize()
        {
            CarSize = new List<CarSize>(CarNum);
            float Ch = (float)(2.0 * PB.Width / Length);    //全車共通
            float a = (float)(Ch / 2);
            for (int i = 0; i < CarNum; i++)
            {
                CarSize CS = new CarSize();
                CS.height = Ch;
                float w;
                if (CMB_ModelMode.Text == "CHPTモデル") w = (float)((int)(v1.car[i].eigenvalue.length * 10 + 0.5) * 0.1);
                else w = (float)((int)(4.435 * 10 + 0.5) * 0.1);
                CS.width = w * a;
                CarSize.Add(CS);
            }
            RoadTop = 10;
            RoadWidth = Ch + 2;
            RoadBottom = RoadTop + (int)(RoadWidth + 2.5);
            hundred_meter = PB.Width / 10;
            startY = RoadBottom + 7;
            endY = startY + 6;
            hundred_meter_X = hundred_meter + 10;
            fifty_meter_X = hundred_meter / 2 + 10;
            int topY = (int)(endY + 4.5);
            point0 = new Rectangle(2, topY, 21, 21);
            point50 = new Rectangle((int)(fifty_meter_X - 14 + 0.5), topY, 32, 21);
            point100 = new Rectangle((int)(hundred_meter_X - 19 + 0.5), topY, 43, 21);
            Velocity = new Rectangle(Width - 480, topY, 230, 21);
            ETime = new Rectangle(Width - 250, topY, 250, 21);
        }

        /// <summary>
        /// 全てを描画する
        /// </summary>
        private void Drawing(object sender, PaintEventArgs e)
        {
            Bitmap canvas = new Bitmap(PB.Width, PB.Height);
            Graphics g = Graphics.FromImage(canvas);
            FillWhitePB(ref g);
            if (DrawCarsFlag && Acceptable) DrawingCars(ref g);
            PB.Image = canvas;
            g.Dispose();
        }

        /// <summary>
        /// PictureBoxボックスを白色で埋める
        /// </summary>
        /// <param name="g">ref Graphics</param>
        private void FillWhitePB(ref Graphics g)
        {
            g.FillRectangle(Brushes.White, 0, 0, PB.Width, PB.Height);
        }

        /// <summary>
        /// 車と車線を描画する
        /// </summary>
        /// <param name="g">ref Graphics</param>
        private void DrawingCars(ref Graphics g)
        {
            Change_Unit unit = new Change_Unit();
            Pen pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, 0, RoadTop, PB.Width, RoadTop);
            int CY = RoadTop + 2;
            float R = (float)(PB.Width / Length);
            float velocity = 0;
            if (CHPT)
            {
                for (int i = 0; i < v1.car.Count; i++)
                {
                    Brush brush = Brushes.Black;
                    if (v1.driver[i].running.pedal.foot_position == FootPosition.brake_pedal || v1.car[i].running.velocity.current <= unit.km_h__to__m_s(0.3)) brush = Brushes.Red;
                    CarSize size = CarSize[i];
                    int CX = (int)(R * v1.car[i].running.position.current - size.width + 0.5);
                    g.FillRectangle(brush, CX, CY, size.width, size.height);
                    if (CX < 0)
                    {
                        CX += PB.Width;
                        g.FillRectangle(brush, CX, CY, PB.Width, size.height);
                    }
                    velocity += (float)v1.car[i].running.velocity.current;
                }
            }
            else
            {
                double RRR = Length / revised.LaneLength;
                for (int i = 0; i < revised.car.position.current.Count; i++)
                {
                    Brush brush = Brushes.Black;
                    if (revised.car.velocity[i] == 0 || revised.car.accelaration[i] < 0) brush = Brushes.Red;
                    CarSize size = CarSize[i];
                    double next_p = pposition[i];
                    if (Animated) next_p += revised.car.velocity[i] * time;
                    double position = RRR * next_p;
                    pposition[i] = (float)next_p;
                    int CX = (int)(R * position - size.width + 0.5);
                    g.FillRectangle(brush, CX, CY, size.width, size.height);
                    if (CX < 0)
                    {
                        CX += PB.Width;
                        g.FillRectangle(brush, CX, CY, PB.Width, size.height);
                    }
                    velocity += (float)1.0 * revised.car.velocity[i] / 5 * 100;
                }
            }
            g.DrawLine(pen, 0, RoadBottom, PB.Width, RoadBottom);
            g.DrawLine(pen, 10, RoadBottom + 10, hundred_meter_X, RoadBottom + 10);
            g.DrawLine(pen, 10, startY, 10, endY);
            g.DrawLine(pen, fifty_meter_X, startY, fifty_meter_X, endY);
            g.DrawLine(pen, hundred_meter_X, startY, hundred_meter_X, endY);
            Font fnt = new Font("MS UI Gothic", 16, FontStyle.Bold);
            g.DrawString("0", fnt, Brushes.Black, point0);
            g.DrawString("50", fnt, Brushes.Black, point50);
            g.DrawString("100", fnt, Brushes.Black, point100);
            string S = "平均速度 : ";
            double km;
            if (CHPT) km = unit.m_s__to__km_h(velocity / v1.car.Count);
            else km = 1.0 * velocity / revised.N;
            S += string.Format("{0, 5}", string.Format("{0:f1}", 0.1 * (int)(km * 10 + 0.5)));
            S += "[km/h]";
            g.DrawString(S, fnt, Brushes.Black, Velocity);
            S = " 経過時間 : ";
            double etime = time * z;
            S += string.Format("{0, 8}", string.Format("{0:f2}", 0.01 * (int)(etime * 100 + 0.5)));
            S += "[s]";
            g.DrawString(S, fnt, Brushes.Black, ETime);
            fnt.Dispose();
            pen.Dispose();
        }

        /// <summary>
        /// アニメーションの描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update(object sender, EventArgs e)
        {
            if (CHPT) v1.Simulate();
            else
            {
                if (n * time >= 1)
                {
                    revised.Simulate();
                    for (int i = 0; i < revised.N; i++) pposition[i] = revised.car.position.previous[i];
                    n = 0;
                }
                else n++;
            }
            z++;
            if (z == 18446744073709551615) z = 0;
            Invalidate();
        }
    }

    /// <summary>
    /// 各車両の表示ピクセルサイズを保存する
    /// </summary>
    class CarSize
    {
        public float width;
        public float height;

        public CarSize()
        {
            width = new float();
            height = new float();
        }

        public CarSize(CarSize size)
        {
            width = new float();
            height = new float();
            width = size.width;
            height = size.height;
        }
    }

    /// <summary>
    /// コンボボックス用のクラス
    /// </summary>
    class CMBClass
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public CMBClass(int Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}

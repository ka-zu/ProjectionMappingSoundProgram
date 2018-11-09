using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK.Graphics.OpenGL;
using System.IO;


namespace ProjectionMapping
{
    /// <summary>
    /// ラメール上画面を投影するためのフォーム
    /// </summary>
    public partial class Form4 : Form
    {

        //窓を表すポリゴンを格納するリスト左から順
        private List<Point> winList1 = new List<Point>();
        private List<Point> winList2 = new List<Point>();
        private List<Point> winList3 = new List<Point>();
        private List<Point> winList4 = new List<Point>();

        //線の色用　初期値はHSVのH=0に合わせる（赤）
        //RGBA色
        //R:赤 0~1.0 G:緑 0~1.0 B:青 0~1.0 A:透明度 0~1.0
        public double[] RGBA = { 1.0, 0.0, 0.0, 1.0 };

        //HSV色
        //H:色相 0~360 S:彩度 0~1.0 V(,L,B):明度 0~1.0
        public double[] HSV = { 0.0, 1.0, 0.5 };

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //テクスチャを使えるように
            GL.Enable(EnableCap.Texture2D);

            reshape(glControl1.Width, glControl1.Height);

            window1FromTxt();
            window2FromTxt();
            window3FromTxt();
            window4FromTxt();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            //消去
            GL.Clear(ClearBufferMask.ColorBufferBit); ;

            //なにか表示しないと透明になる
            /*GL.Begin(BeginMode.Lines);
            GL.Vertex2(0, 0);
            GL.Vertex2(10,10);
            GL.End();*/

            drawPolygon();

            //表示
            glControl1.SwapBuffers();
        }

        //******
        //******自作関数
        //******

        /// <summary>
        /// GL表示座標系の変換
        /// </summary>
        /// <param name="wid">横幅ピクセル</param>
        /// <param name="hei">縦幅ピクセル</param>
        private void reshape(int wid, int hei)
        {
            int top = hei / 2;
            int bottom = -hei / 2;
            int left = -wid / 2;
            int right = wid / 2;
            GL.Viewport(new Rectangle(0, 0, wid, hei));
            GL.LoadIdentity();
            GL.Ortho(left, right, bottom, top, -1.0, 1.0);
        }

        /// <summary>
        /// 窓１を読み込む関数
        /// </summary>
        private void window1FromTxt()
        {
            Console.WriteLine("窓1読み込み");

            int x = 0;
            int y = 0;

            Point point = new Point();


            //ファイルが存在しない場合適当な値を入れて終わる
            if (File.Exists(@"window_ue1.txt") == false)
            {
                Console.WriteLine("読み込むファイルが存在しません");
                winList1.Add(new Point(0, 0));
                winList1.Add(new Point(100, 0));
                winList1.Add(new Point(100, 100));
                winList1.Add(new Point(0, 100));
                return;
            }

            //usingを使うことで自動Close
            using (var rea = new StreamReader(@"window_ue1.txt"))
            {
                //末尾まで読み込む
                while (!rea.EndOfStream)
                {
                    //一行読み込み
                    String lineX = rea.ReadLine();
                    x = int.Parse(lineX);
                    String lineY = rea.ReadLine();
                    y = int.Parse(lineY);

                    point.X = x;
                    point.Y = y;

                    Console.WriteLine("add point x:" + x + " y:" + y);
                    winList1.Add(point);
                }
            }
        }

        /// <summary>
        /// 窓２を読み込む関数
        /// </summary>
        private void window2FromTxt()
        {
            Console.WriteLine("窓2読み込み");

            int x = 0;
            int y = 0;

            Point point = new Point();


            //ファイルが存在しない場合適当な値を入れて終わる
            if (File.Exists(@"window_ue2.txt") == false)
            {
                Console.WriteLine("読み込むファイルが存在しません");
                winList2.Add(new Point(0, 0));
                winList2.Add(new Point(100, 0));
                winList2.Add(new Point(100, 100));
                winList2.Add(new Point(0, 100));
                return;
            }

            //usingを使うことで自動Close
            using (var rea = new StreamReader(@"window_ue2.txt"))
            {
                //末尾まで読み込む
                while (!rea.EndOfStream)
                {
                    //一行読み込み
                    String lineX = rea.ReadLine();
                    x = int.Parse(lineX);
                    String lineY = rea.ReadLine();
                    y = int.Parse(lineY);

                    point.X = x;
                    point.Y = y;

                    Console.WriteLine("add point x:" + x + " y:" + y);
                    winList2.Add(point);
                }
            }
        }

        /// <summary>
        /// 窓３を読み込む関数
        /// </summary>
        private void window3FromTxt()
        {
            Console.WriteLine("窓3読み込み");

            int x = 0;
            int y = 0;

            Point point = new Point();


            //ファイルが存在しない場合適当な値を入れて終わる
            if (File.Exists(@"window_ue3.txt") == false)
            {
                Console.WriteLine("読み込むファイルが存在しません");
                winList3.Add(new Point(0, 0));
                winList3.Add(new Point(100, 0));
                winList3.Add(new Point(100, 100));
                winList3.Add(new Point(0, 100));
                return;
            }

            //usingを使うことで自動Close
            using (var rea = new StreamReader(@"window_ue3.txt"))
            {
                //末尾まで読み込む
                while (!rea.EndOfStream)
                {
                    //一行読み込み
                    String lineX = rea.ReadLine();
                    x = int.Parse(lineX);
                    String lineY = rea.ReadLine();
                    y = int.Parse(lineY);

                    point.X = x;
                    point.Y = y;

                    Console.WriteLine("add point x:" + x + " y:" + y);
                    winList3.Add(point);
                }
            }
        }

        /// <summary>
        /// 窓４を読み込む関数
        /// </summary>
        private void window4FromTxt()
        {
            Console.WriteLine("窓2読み込み");

            int x = 0;
            int y = 0;

            Point point = new Point();


            //ファイルが存在しない場合適当な値を入れて終わる
            if (File.Exists(@"window_ue4.txt") == false)
            {
                Console.WriteLine("読み込むファイルが存在しません");
                winList4.Add(new Point(0, 0));
                winList4.Add(new Point(100, 0));
                winList4.Add(new Point(100, 100));
                winList4.Add(new Point(0, 100));
                return;
            }

            //usingを使うことで自動Close
            using (var rea = new StreamReader(@"window_ue4.txt"))
            {
                //末尾まで読み込む
                while (!rea.EndOfStream)
                {
                    //一行読み込み
                    String lineX = rea.ReadLine();
                    x = int.Parse(lineX);
                    String lineY = rea.ReadLine();
                    y = int.Parse(lineY);

                    point.X = x;
                    point.Y = y;

                    Console.WriteLine("add point x:" + x + " y:" + y);
                    winList4.Add(point);
                }
            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(glControl1.BackColor);
        }

        /// <summary>
        /// 窓ポリゴンの描画
        /// </summary>
        private void drawPolygon()
        {
            GL.Color4(RGBA[0], RGBA[1], RGBA[2], RGBA[3]);

            GL.Begin(BeginMode.Polygon);
            foreach (Point p in winList1)
            {
                GL.Vertex2(p.X,-p.Y);
            }
            GL.End();

            GL.Begin(BeginMode.Polygon);
            foreach (Point p in winList2)
            {
                GL.Vertex2(p.X, -p.Y);
            }
            GL.End();

            GL.Begin(BeginMode.Polygon);
            foreach (Point p in winList3)
            {
                GL.Vertex2(p.X, -p.Y);
            }
            GL.End();

            GL.Begin(BeginMode.Polygon);
            foreach (Point p in winList4)
            {
                GL.Vertex2(p.X, -p.Y);
            }
            GL.End();
        }

        //外部から画面を更新する
        public void refresh()
        {
            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);

            drawPolygon();
            //表示
            glControl1.SwapBuffers();


        }


    }
}

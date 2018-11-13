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
    /// 左窓投影ウィンドウ
    /// </summary>
    public partial class Form5 : Form
    {
        //窓ポリゴン座標格納用
        //中のListのインスタンスはできていないので注意
        List<Point>[] winLists = new List<Point>[12];

        //線の色用　初期値はHSVのH=0に合わせる（赤）
        //RGBA色
        //R:赤 0~1.0 G:緑 0~1.0 B:青 0~1.0 A:透明度 0~1.0
        public double[] RGBA = { 1.0, 0.0, 0.0, 1.0 };

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //テクスチャを使えるように
            GL.Enable(EnableCap.Texture2D);

            reshape(glControl1.Width, glControl1.Height);

            makeListInstance();

            windowsFromTxt();
        }


        private void glControl1_Load(object sender, EventArgs e)
        {

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            //消去
            GL.Clear(ClearBufferMask.ColorBufferBit);

            /*//なにか表示しないと透明になる
            GL.Begin(BeginMode.Lines);
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
        /// List型配列の中身のinstanceを作る
        /// </summary>
        private void makeListInstance()
        {
            for(int i=0; i < winLists.Length; i++)
            {
                winLists[i] = new List<Point>();
            }
        }

        /// <summary>
        /// 窓ポリゴン座標を格納したテキストを読み込み
        /// </summary>
        private void windowsFromTxt()
        {
            for(int i=0; i < winLists.Length; i++)
            {
                //ファイル名
                String str = "./window_left" + (i+1).ToString() + ".txt";

                Console.WriteLine("窓" + (i+1) +"読み込み");

                int x = 0;
                int y = 0;

                Point point = new Point();


                //ファイルが存在しない場合適当な値を入れて終わる
                if (File.Exists(@str) == false)
                {
                    Console.WriteLine(str + "が存在しません");
                    winLists[i].Add(new Point(0, 0));
                    winLists[i].Add(new Point(100, 0));
                    winLists[i].Add(new Point(100, 100));
                    winLists[i].Add(new Point(0, 100));
                    continue;
                }

                //usingを使うことで自動Close
                using (var rea = new StreamReader(str))
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
                        winLists[i].Add(point);
                    }
                }
            }
        }

        /// <summary>
        /// 窓ポリゴンの描画
        /// </summary>
        private void drawPolygon()
        {
            GL.Color4(RGBA[0], RGBA[1], RGBA[2], RGBA[3]);

            for (int i = 0; i < winLists.Length; i++)
            {
                GL.Begin(BeginMode.Polygon);

                foreach (Point p in winLists[i])
                {
                    GL.Vertex2(p.X, -p.Y);
                }

                GL.End();
            }

        }

        //外部から画面を更新する
        public void refresh()
        {
            //
            //複数ウィンドウを処理するのに必須！！！！！OpenGL処理を自分に回す
            //
            glControl1.MakeCurrent();

            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);

            drawPolygon();

            //表示
            glControl1.SwapBuffers();
        }

        //徐々に背景色（白）に戻る
        public void backBaseColor()
        {
            Console.WriteLine("back...");

            if (RGBA[0] < 1.0) { RGBA[0] += (5 / 255.0); }
            if (RGBA[1] < 1.0) { RGBA[1] += (5 / 255.0); }
            if (RGBA[2] < 1.0) { RGBA[2] += (5 / 255.0); }

            Console.WriteLine("R : " + RGBA[0] +
                             " G : " + RGBA[1] +
                             " B : " + RGBA[2]);
        }
    }
}

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

//音で上下するバーを投影する部分(下画面)
namespace ProjectionMapping
{
    public partial class Form3 : Form
    {
        //ラインの太さ
        public float lineWidth = 35;

        //縦線の数(表示する周波数の範囲)
        public int lineNum = 40;

        //外部から受け取るようの値
        public double maxHz = 0;//最大周波数
        public double maxPower = 0;//最大周波数のパワー
        public double sumHist = 0;//ヒストグラムの合計値
        public double[] HzHist;//周波数ヒストグラムの格納配列
        public bool increaseBarMode = false;
        private int maxHzToBar = 0;//バーを増やすため　今までの最大周波数
        
        //線の色用　初期値はHSVのH=0に合わせる（赤）
        //RGBA色
        //R:赤 0~1.0 G:緑 0~1.0 B:青 0~1.0 A:透明度 0~1.0s
        public double[] RGBA = { 1.0, 0.0, 0.0, 1.0 };

        //HSV色
        //H:色相 0~360 S:彩度 0~1.0 V(,L,B):明度 0~1.0
        public double[] HSV = { 0.0, 1.0, 0.5 };

        //パーティクルを表示する座標を格納する乱数
        Random rnd = new Random();

        //花火パーティクルを複数生成できるようにするためのリスト
        List<Fireworks> fList = new List<Fireworks>();
        //波紋パーティクル
        List<Ripple> ripList = new List<Ripple>();
        //パーティクルタイマー
        int pt = 10;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);


            //テクスチャを使えるように
            GL.Enable(EnableCap.Texture2D);

            //GL.ClearColor(OpenTK.Graphics.Color4.White);
            GL.ClearColor(glControl1.BackColor);

            reshape(glControl1.Width, glControl1.Height);

        }

        private void glControl1_Load(object sender, EventArgs e)
        {

            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);


            //線を引く
            drawLines();
        }

        //******
        //******自作関数
        //******

        //GL表示座標系の変換
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

        //一定の間隔・長さのラインを引く
        private void drawLines()
        {
            //ライン間のマージン
            int margin = (int)lineWidth + 5;
            //ラインの長さ
            int len = 50;
            //ラインの左端の位置座標　-1 * 太さ * (ラインの数/2)
            double left = -((lineNum-1)/2.0)*margin;

            //波打つように表示するテスト用
            /*double[] sin = new double[lineNum];
            for(int i =0; i < sin.Length; i++)
            {
                sin[i] = 50 * Math.Sin((i * 10) * Math.PI / 180);
            }*/

            //GL.Color4(OpenTK.Graphics.Color4.Red);
            //GL.Color4(1.0,0,0,1.0);
            GL.Color4(RGBA[0], RGBA[1], RGBA[2], RGBA[3]);

            //線の太さ
            /*GL.LineWidth(lineWidth);

            GL.Begin(BeginMode.Lines);

            for (int i = 0; i < num; i++)
            {
                GL.Vertex2(left + (i * margin), -100);
                GL.Vertex2(left + (i * margin), 5*HzHist[i]-100);
            }

            GL.End();*/

            //ラインの太さ上限が小さかったので四角で描く
            for (int i = 0; i < lineNum; i++)
            {
                GL.Begin(BeginMode.Polygon);

                GL.Vertex2(left + (i * margin), -100);//左下
                GL.Vertex2(left + (i * margin) + lineWidth, -100);//右下
                GL.Vertex2(left + (i * margin) + lineWidth, 10 * HzHist[i] - 100);//右上
                GL.Vertex2(left + (i * margin), 10 * HzHist[i] - 100);//左上

                GL.End();


            }
        }




        //絵かきはこの中
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            /*GL.Color4(OpenTK.Graphics.Color4.Red);
            GL.Begin(BeginMode.Lines);
            GL.Vertex2(0, 0);
            GL.Vertex2(0, 50);
            GL.End();*/

            //
            //複数ウィンドウを処理するのに必須！！！！！OpenGL処理を自分に回す
            //
            glControl1.MakeCurrent();

            //drawBackGround();
            drawLines();
            

            //表示
            glControl1.SwapBuffers();
        }
        
        //外部から画面を更新する
        public void refresh()
        {
            
            generateParticle();


            //
            //複数ウィンドウを処理するのに必須！！！！！OpenGL処理を自分に回す
            //
            glControl1.MakeCurrent();

            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);

            drawBackGround();
            drawLines();
            drawParticle();

            //drawCircle();

            //表示
            glControl1.SwapBuffers();

            removeParticle();
        }

        //親フォームから値を受け取る
        public void setFFTData(double hz, double pow, double sum)
        {
            maxHz = hz;
            maxPower = pow;
            sumHist = sum;

            //バーが増えるモード
            //600Hzで20本のバーになるように増やしていく（減らない）
            //                     今までよりバーが増えるHzだったら
            Console.WriteLine("mode =" + increaseBarMode + " pow ="+maxPower);
            Console.WriteLine("maxTo =" + maxHzToBar + " maxHz" + maxHz);
            if (increaseBarMode && maxHzToBar < maxHz && maxPower > 15)
            {
                maxHzToBar = (int)maxHz;
                lineNum = (int)((18 / 600.0) * maxHz) + 2;
                Console.WriteLine("increase LineNum =" + lineNum);
            }
        }

        //親フォームから周波数ヒストグラムを受け取る
        public void setFFTHist(double[] hist)
        {
            HzHist = hist;
        }

        //最大周波数をHSVのHとする
        public void setColorByMaxHz(double mHz)
        {
            //              360(°) / 4000(Hz)
            //HSV[0] = ((360 / 4000.0) * mHz);
            HSV[0] = ((360 / 2000.0) * mHz) / 360.0 + 90/360.0;//90°足す(スタートを緑にするため)
            /*Console.WriteLine("H : "  + HSV[0] +
                             " S : " + HSV[1] +
                             " V : " + HSV[2]);*/
        }

        //ヒストグラムの合計値をHSVのHとする
        public void setColorBySumHistgram(double sum)
        {
            //大体合計値が300以上ないのでとりあえず300で割る
            HSV[0] = ((360 / 500.0) * sum) / 360.0 + 90 / 360.0;//90°足す(スタートを緑にするため)
            /*Console.WriteLine("H : " + HSV[0] +
                             " S : " + HSV[1] +
                             " V : " + HSV[2]);*/
        }

        //HSV色をRGB色に変換
        public void convertingHLSToRGB()
        {
            float h = (float)HSV[0];
            float s = (float)HSV[1];
            float v = (float)HSV[2];

            float r = v;
            float g = v;
            float b = v;
            if (s > 0.0f)
            {
                h *= 6.0f;
                int i = (int)h;
                float f = h - (float)i;
                switch (i)
                {
                    default:
                    case 0:
                        g *= 1 - s * (1 - f);
                        b *= 1 - s;
                        break;
                    case 1:
                        r *= 1 - s * f;
                        b *= 1 - s;
                        break;
                    case 2:
                        r *= 1 - s;
                        b *= 1 - s * (1 - f);
                        break;
                    case 3:
                        r *= 1 - s;
                        g *= 1 - s * f;
                        break;
                    case 4:
                        r *= 1 - s * (1 - f);
                        g *= 1 - s;
                        break;
                    case 5:
                        g *= 1 - s;
                        b *= 1 - s * f;
                        break;
                }
            }

            RGBA[0] = r;
            RGBA[1] = g;
            RGBA[2] = b;

            /*Console.WriteLine("R : " + r +
                             " G : " + g +
                             " B : " + b);*/
        }

        //背景を描く
        private void drawBackGround()
        {
            /*int top = glControl1.Height / 2;
            int bottom = -glControl1.Height / 2;
            int left = -glControl1.Width / 2;
            int right = glControl1.Width / 2;

            GL.Color4(OpenTK.Graphics.Color4.White);

            GL.Begin(BeginMode.Polygon);

            GL.Vertex2(left, bottom);//左下
            GL.Vertex2(right, bottom);//右下
            GL.Vertex2(right, top);//右上
            GL.Vertex2(left, top);//左上

            GL.End();*/

            
        }

        //音をチェックしてパーティクルを生み出す
        private void generateParticle()
        {
            if(sumHist >50)
            {
                fList.Add(new Fireworks(rnd.Next(-glControl1.Width/2+300, glControl1.Width / 2-300), rnd.Next(0, glControl1.Width / 2), rnd));
                //Console.WriteLine("generate now : " + fList.Count());
                ripList.Add(new Ripple(rnd.Next(-glControl1.Width / 2 + 300, glControl1.Width / 2 - 300), rnd.Next(0, glControl1.Width / 2), rnd, glControl1.BackColor));
            }
        }

        //パーティクルを描く
        private void drawParticle()
        {
            foreach (Ripple rip in ripList)
            {
                rip.draw();
            }
            foreach (Fireworks fw in fList)
            {
                fw.draw();

            }
        }

        //存在しないパーティクルを消す
        private void removeParticle()
        {
            //存在しなくなったものを消すラムダ式
            fList.RemoveAll(fw => fw.isExist == false);
            ripList.RemoveAll(rip => rip.isExist == false);

        }

        //円を描くテスト
        private void drawCircle()
        {
            int num = 30;//角の数
            double theta = 360 / num;//num角形の角度
            int r = 50;//半径

            GL.Begin(BeginMode.TriangleFan);

            GL.Vertex2(100, 100);

            for (int i = 0; i <= num; i++)
                GL.Vertex2(r * Math.Sin(i * theta * Math.PI / 180) + 100, r * Math.Cos(i * theta * Math.PI / 180) + 100);

            GL.End();

        }

    }
}

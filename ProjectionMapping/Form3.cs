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

//音で上下するバーを投影する部分
namespace ProjectionMapping
{
    public partial class Form3 : Form
    {
        //ラインの太さ
        public float lineWidth = 5;

        //縦線の数(表示する周波数の範囲)
        const int LINE_NUM = 2;

        //外部から受け取るようの値
        public double maxHz = 0;//最大周波数
        public double maxPower = 0;//最大周波数のパワー
        public double sumHist = 0;//ヒストグラムの合計値
        public double[] HzHist;

        
        //線の色用　初期値はHSVのH=0に合わせる（赤）
        //RGBA色
        //R:赤 0~1.0 G:緑 0~1.0 B:青 0~1.0 A:透明度 0~1.0
        public double[] RGBA = { 1.0, 0.0, 0.0, 1.0 };

        //HSV色
        //H:色相 0~360 S:彩度 0~1.0 V(,L,B):明度 0~1.0
        public double[] HSV = { 0.0, 1.0, 0.5 };

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

            reshape(glControl1.Width, glControl1.Height);

            GL.Enable(EnableCap.LineSmooth);
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
            //引くラインの数
            int num = LINE_NUM;
            //ライン間のマージン
            int margin = (int)lineWidth + 5;
            //ラインの長さ
            int len = 50;
            //ラインの左端の位置座標　-1 * 太さ * (ラインの数/2)
            double left = -((num-1)/2.0)*margin;

            //波打つように表示するテスト用
            double[] sin = new double[LINE_NUM];
            for(int i =0; i < sin.Length; i++)
            {
                sin[i] = 50 * Math.Sin((i * 10) * Math.PI / 180);
            }

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
            for (int i = 0; i < num; i++)
            {
                GL.Begin(BeginMode.Polygon);


                GL.Vertex2(left + (i * margin), -100);//左下
                GL.Vertex2(left + (i * margin) + lineWidth, -100);//右下
                GL.Vertex2(left + (i * margin) + lineWidth, 5 * HzHist[i] - 100);//右上
                GL.Vertex2(left + (i * margin), 5 * HzHist[i] - 100);//左上

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

            drawLines();

            //表示
            glControl1.SwapBuffers();
        }
        
        //外部から画面を更新する
        public void LabelRefresh()
        {
            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);

            drawLines();
            //表示
            glControl1.SwapBuffers();

            
        }

        //親フォームから値を受け取る
        public void setFFTData(double hz, double pow, double sum)
        {
            maxHz = hz;
            maxPower = pow;
            sumHist = sum;
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
            HSV[0] = ((360 / 4000.0) * mHz) / 360.0;
            Console.WriteLine("H : "  + HSV[0] +
                              " S : " + HSV[1] +
                              " V : " + HSV[2]);
        }

        //ヒストグラムの合計値をHSVのHとする
        public void setColorBySumHistgram(double sum)
        {
            //大体合計値が300以上ないのでとりあえず300で割る
            HSV[0] = ((360 / 300.0) * sum) / 360.0;
            Console.WriteLine("H : " + HSV[0] +
                              " S : " + HSV[1] +
                              " V : " + HSV[2]);
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

            Console.WriteLine("R : " + r +
                             " G : " + g +
                             " B : " + b);
        }
    }
}

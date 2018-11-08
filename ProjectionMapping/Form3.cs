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
        int lineWidth = 5;

        //外部から受け取るようの値
        public double Hz = 0;//最大周波数
        public double power = 0;//最大周波数のパワー

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

            GL.LineWidth(lineWidth);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {

            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //線を引く
            //drawLines();

            

            
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
            int num = 30;
            //ライン間のマージン
            int margin = lineWidth+5;
            //ラインの長さ
            int len = 50;
            //ラインの左端の位置座標　-1 * 太さ * (ラインの数/2)
            double left = -((num-1)/2.0)*margin;

            //波打つように表示するテスト用
            double[] sin = new double[30];
            for(int i =0; i < sin.Length; i++)
            {
                sin[i] = 50 * Math.Sin((i * 10) * Math.PI / 180);
            }

            GL.Color4(OpenTK.Graphics.Color4.Red);
            GL.Begin(BeginMode.Lines);

            for (int i = 0; i < num; i++)
            {
                GL.Vertex2(left + (i * margin), 0);
                GL.Vertex2(left + (i * margin), sin[i]);
            }

            GL.End();
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
            
        }

        //親フォームから値を受け取る
        public void setIndexHz(double hz, double pow)
        {
            Hz = hz;
            power = pow;
        }
    }
}

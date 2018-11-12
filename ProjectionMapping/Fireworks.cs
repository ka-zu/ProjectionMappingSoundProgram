using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using OpenTK.Graphics.OpenGL;

namespace ProjectionMapping
{
    /// <summary>
    /// パーティクル花火
    /// </summary>
    class Fireworks
    {
        //表示する中心座標
        public Point p = new Point();

        //時間遷移用変数
        private double t = 0;

        //存在を表すフラグ
        public bool isExist = false;

        //R:赤 0~1.0 G:緑 0~1.0 B:青 0~1.0 A:透明度 0~1.0s
        public double[] RGBA = { 1.0, 0.0, 0.0, 1.0 };

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Fireworks(int x, int y)
        {
            p.X = x;
            p.Y = y;
            isExist = true;

            GL.PointSize(5);
            
        }

        /// <summary>
        /// 花火の色をランダムにできるコンストラクタ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tick">Environment.Tickcountを入れる</param>
        public Fireworks(int x, int y, Random rnd)
        {

            p.X = x;
            p.Y = y;
            isExist = true;

            RGBA[0] = rnd.NextDouble();
            RGBA[1] = rnd.NextDouble();
            RGBA[2] = rnd.NextDouble();

            GL.PointSize(5);

        }

        //カウントをリセットする
        public void reset()
        {
            t = 0;
        }

        //カウンターを進める
        public void count()
        {

            if (t < 50)
            {
                t+=1;
            }
            else
            {
                reset();
                isExist = false;
            }
        }

        //描く部分
        public void draw()
        {
            //カウンターを進める
            count();

            //Console.WriteLine(t);

            //存在してないなら描かない
            //if (isExist) { return; }
            //Console.WriteLine("draw Fireworks");

            //GL.Color4(OpenTK.Graphics.Color4.Red);
            GL.Color4(RGBA[0], RGBA[1], RGBA[2], RGBA[3]);
            GL.PointSize(2 + (float)(t/5));
            GL.Begin(BeginMode.Points);

            GL.Vertex2(p.X + t, p.Y);
            GL.Vertex2(p.X - t, p.Y);
            GL.Vertex2(p.X, p.Y + t);
            GL.Vertex2(p.X, p.Y - t);
            GL.Vertex2(p.X + t, p.Y + t);
            GL.Vertex2(p.X - t, p.Y + t);
            GL.Vertex2(p.X + t, p.Y - t);
            GL.Vertex2(p.X - t, p.Y - t);

            GL.End();
        }       

    }
}

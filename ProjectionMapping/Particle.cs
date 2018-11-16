using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using OpenTK.Graphics.OpenGL;

namespace ProjectionMapping
{
    /// <summary>
    /// いろんなパーティクルの親クラス　にしたい
    /// </summary>
    class Particle
    {
        //表示する中心座標
        protected Point p = new Point();

        //時間遷移用変数
        protected double t = 0;

        //存在を表すフラグ
        public bool isExist = false;

        //R:赤 0~1.0 G:緑 0~1.0 B:青 0~1.0 A:透明度 0~1.0s
        protected double[] RGBA = { 1.0, 0.0, 0.0, 1.0 };

        //パーティクルの寿命
        protected int lifeTime = 50;

        public Particle()
        {
            p.X = 0;
            p.Y = 0;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Particle(int x, int y)
        {
            p.X = x;
            p.Y = y;
            isExist = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="life">寿命</param>
        /// <param name="var">寿命のゆらぎ</param>
        /// <param name="rnd">RGB用ランダム</param>
        public Particle(int x, int y, int life,int var,Random rnd)
        {
            p.X = x;
            p.Y = y;
            isExist = true;

            RGBA[0] = rnd.NextDouble();
            RGBA[1] = rnd.NextDouble();
            RGBA[2] = rnd.NextDouble();

            lifeTime = rnd.Next(50) + 20;
        }

        //カウントをリセットする
        public void reset()
        {
            t = 0;
        }

        //カウンターを進める
        public void count()
        {

            if (t < lifeTime)
            {
                t += 1;
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
            //Console.WriteLine(t);

            //存在してないなら描かない
            //if (isExist) { return; }
            //Console.WriteLine("draw Fireworks");

            //GL.Color4(OpenTK.Graphics.Color4.Red);
            GL.Color4(RGBA[0], RGBA[1], RGBA[2], RGBA[3]);
            
            GL.Begin(BeginMode.Polygon);//デフォルトは四角

            GL.Vertex2(p.X,p.Y);
            GL.Vertex2(p.X+100, p.Y);
            GL.Vertex2(p.X+100, p.Y+100);
            GL.Vertex2(p.X, p.Y+100);

            GL.End();

            //カウンターを進める
            count();
        }
    }
}

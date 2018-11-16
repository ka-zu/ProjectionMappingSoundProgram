using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace ProjectionMapping
{
    /// <summary>
    /// パーティクル　波紋
    /// </summary>
    class Ripple
    {
        //表示する中心座標
        public Point p = new Point();

        //時間遷移用変数
        private double t = 0;

        //存在を表すフラグ
        public bool isExist = false;

        //R:赤 0~1.0 G:緑 0~1.0 B:青 0~1.0 A:透明度 0~1.0s
        public double[] RGBA = { 1.0, 0.0, 0.0, 1.0 };

        //フォームの背景色取得用
        private Color bgColor = Color.White;

        //パーティクルの寿命
        private int lifeTime = 50;


        //半径
        double r = 50;
        //内側が発生するタイミング(大きいほど早い)
        int t2 = 20;
        //内側のカウント
        int t2count = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Ripple(int x, int y)
        {
            p.X = x;
            p.Y = y;
            isExist = true;
        }

        public Ripple(int x, int y, Random rnd)
        {
            p.X = x;
            p.Y = y;
            isExist = true;

            RGBA[0] = rnd.NextDouble();
            RGBA[1] = rnd.NextDouble();
            RGBA[2] = rnd.NextDouble();

            lifeTime = rnd.Next(50) + 20;
            t2 = lifeTime / 5;
        }

        public Ripple(int x, int y, Random rnd, Color col)
        {
            p.X = x;
            p.Y = y;
            isExist = true;

            RGBA[0] = rnd.NextDouble();
            RGBA[1] = rnd.NextDouble();
            RGBA[2] = rnd.NextDouble();

            lifeTime = rnd.Next(50) + 20;
            t2 = lifeTime / 5;

            bgColor = col;
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
            

            int num = 30;//角の数
            double theta = 360 / num;//num角形の角度
            

            GL.Color4(RGBA[0], RGBA[1], RGBA[2], RGBA[3]);
            
            GL.Begin(BeginMode.TriangleFan);

            GL.Vertex2(p.X, p.Y);

            //円
            for (int i = 0; i <= num; i++)
            {
                GL.Vertex2((r/lifeTime) * t * Math.Sin(i * theta * Math.PI / 180) + p.X, (r / lifeTime) * t * Math.Cos(i * theta * Math.PI / 180) + p.Y);
            }

            GL.End();

            //消えるように見えるために背景色の円を描く
            GL.Color4(bgColor.R/255.0, bgColor.G / 255.0, bgColor.B / 255.0, RGBA[3]);

            //内側から追いかける円
            //消えるt2秒前になったら
            if (lifeTime - t2 < t)
            {
                t2count++;

                GL.Begin(BeginMode.TriangleFan);

                GL.Vertex2(p.X, p.Y);

                for (int i = 0; i <= num; i++)
                {
                    GL.Vertex2((r/t2)*t2count * Math.Sin(i * theta * Math.PI / 180) + p.X, (r / t2) * t2count * Math.Cos(i * theta * Math.PI / 180) + p.Y);
                }

                GL.End();
            }
            
            //カウンターを進める
            count();
        }
    }
}

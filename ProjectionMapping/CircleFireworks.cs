using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace ProjectionMapping
{
    /// <summary>
    /// 丸い花火のパーティクル
    /// </summary>
    class CircleFireworks : Particle
    {

        public CircleFireworks(int x, int y, int life, int var, Random rnd):base(x, y, life, var, rnd)
        {

            p.X = x;
            p.Y = y;
            isExist = true;

            RGBA[0] = rnd.NextDouble();
            RGBA[1] = rnd.NextDouble();
            RGBA[2] = rnd.NextDouble();
            lifeTime = rnd.Next(var) + life;
        }

        public new void draw()
        {
            //親のクラスを持ってくるときは変数baseを使う
            //base.draw();

            GL.Color4(RGBA[0], RGBA[1], RGBA[2], RGBA[3]);

            GL.PointSize((float)(t / 5));

            GL.Begin(BeginMode.Points);


            for(int i=0; i < 10; i++)
            {
                GL.Vertex2(base.t * Math.Sin(36* i * Math.PI / 180.0) + base.p.X, base.t * Math.Cos(36 * i * Math.PI / 180.0) + base.p.Y);
            }

            /*GL.Vertex2(p.X + t, p.Y);
            GL.Vertex2(p.X - t, p.Y);
            GL.Vertex2(p.X, p.Y + t);
            GL.Vertex2(p.X, p.Y - t);
            GL.Vertex2(p.X + t, p.Y + t);
            GL.Vertex2(p.X - t, p.Y + t);
            GL.Vertex2(p.X + t, p.Y - t);
            GL.Vertex2(p.X - t, p.Y - t);*/

            GL.End();

            count();
        }

        public void dddd()
        {
            GL.PointSize(5);

            GL.Begin(BeginMode.Points);//デフォルトは四角

            GL.Vertex2(100, 100);

            GL.End();

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.IO;//ファイル読み込み
using System.Collections;//IEnumerable用

namespace ProjectionMapping
{
    public partial class Form2 : Form
    {

        //描画領域の初期化用
        const int WID = 1920;
        const int HEI = 1080;
        const int TOP = 1080 / 2;
        const int BOTTOM = -1080 / 2;
        const int LEFT = -1920 / 2;
        const int RIGHT = 1920 / 2;

        int px = 1;
        IntPtr[] textureData;

        //読み込んだ座標を保存 リストにしたのはクリックする回数がわからないから
        List<Point> pList = new List<Point>();

        int texture;
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //画面を消す
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //座標系の初期化
            reshape(WID, HEI);

            //テクスチャを使えるように
            GL.Enable(EnableCap.Texture2D);

            //テクスチャ用バッファ
            texture = GL.GenTexture();

            //テクスチャバッファの紐付け
            GL.BindTexture(TextureTarget.Texture2D, texture);

            //テクスチャの設定
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            //テクスチャにする画像
            Bitmap texPic = new Bitmap(@"left.png");

            //png画像の反転を直す
            texPic.RotateFlip(RotateFlipType.RotateNoneFlipY);

            //データの読み込み
            BitmapData texData = texPic.LockBits(new Rectangle(0, 0, texPic.Width, texPic.Height), ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //テクスチャ用バッファに色情報を流し込む
            GL.TexImage2D(TextureTarget.Texture2D, 0,
                PixelInternalFormat.Rgba, texData.Width, texData.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte,
                texData.Scan0);

            //ラインの太さを設定
            GL.LineWidth(5);
            
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(glControl1.BackColor);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(glControl1.BackColor);

            //背景画像を表示
            drawingTop();

            //テキストから読み込んでリストに入れる
            readAppendList();

            //リストの中を描く
            drawFromList();

            //表示
            glControl1.SwapBuffers();
        }

        private void glControl1_Click(object sender, EventArgs e)
        {
            //マウスポインタの位置を取得（中心を原点とする）
            //x座標を取得
            int x = Cursor.Position.X - WID / 2;
            //y座標を取得
            int y = Cursor.Position.Y - HEI / 2;

            addPointList(x, y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void glControl1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'r':
                    Console.WriteLine("r");
                    callPaint();
                    break;

                case 'q':
                    Console.WriteLine("q");
                    outputList();
                    this.Dispose();
                    break;

                case 'z':
                    Console.WriteLine("z");
                    deleteList();
                    callPaint();
                    glControl1.SwapBuffers();
                    break;

                case 's':
                    Console.WriteLine("s");
                    WriteAllItems(pList);
                    break;
            }
        }

        //******
        //******自作関数******
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

        //上画像の表示
        private void drawingTop()
        {
            //ポリゴンを作成して
            //テクスチャを貼り付け

            GL.Enable(EnableCap.Texture2D);

            GL.Color4(OpenTK.Graphics.Color4.White);

            //ポリゴン作成
            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(1.0, 1.0);
            GL.Vertex2(RIGHT, TOP);

            GL.TexCoord2(0.0, 1.0);
            GL.Vertex2(LEFT, TOP);

            GL.TexCoord2(0.0, 0.0);
            GL.Vertex2(LEFT, BOTTOM);

            GL.TexCoord2(1.0, 0.0);
            GL.Vertex2(RIGHT, BOTTOM);

            GL.End();
        }

        //テキストを読み込んでリストに追加
        private void readAppendList()
        {
            Console.WriteLine("読み込んでリストに追加");

            int x = 0;
            int y = 0;

            Point point = new Point();

            //ファイルが存在しない場合適当な値を入れて終わる
            if (File.Exists(@"click.txt") == false)
            {
                Console.WriteLine("読み込むファイルが存在しません");
                pList.Add(new Point(0, 0));
                pList.Add(new Point(100, 100));
                return;
            }

            //usingを使うことで自動Close
            using (var rea = new StreamReader(@"click.txt"))
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
                    pList.Add(point);
                }
            }
        }

        //List<Point>型のデータから線を引く
        private void drawFromList()
        {
            Console.WriteLine("リストから線を引く");
            int x = 0;
            int y = 0;

            //色指定
            GL.Color4(OpenTK.Graphics.Color4.Green);

            GL.Begin(BeginMode.Lines);

            //要素の中を取り出す
            foreach(Point pt in pList)
            {
                //代入
                x = pt.X;
                y = pt.Y;

                //線を引く GLは座標系が左下原点なので
                GL.Vertex2(x, (-1.0) * y);
            }

            GL.End();

        }

        //座標をリストに追加
        private void addPointList(int x, int y)
        {
            Console.WriteLine("リスト追加");
            Console.WriteLine("click point x:" + x + " y" + y);

            pList.Add(new Point(x, y));
        }

        //リストの末尾を消す(引いた線を引く)
        private void deleteList()
        {
            Console.WriteLine("リスト削除");
            if(pList.Count() < 2)
            {
                Console.WriteLine("リストの数が2以下です");
                return;
            }
            
            //後ろから2を削除
            //point[] p = { pList[pList.Count() - 1], pList[pList.Count() - 2]};
            pList.RemoveRange(pList.Count() - 2, 2);
        }

        //Listの中身をテキスト出力
        private void outputList()
        {
            Console.WriteLine("テキスト出力");
            //usingで自動Close falseは上書き保存
            using (var wri = new StreamWriter(@"click.txt", false))
            {
                foreach(Point poi in pList)
                {
                    wri.WriteLine(poi.X);
                    wri.WriteLine(poi.Y);
                }
            }
        }

        //Listの中身を表示
        static void WriteAllItems(IEnumerable collection)
        {
            var strings = new List<string>();
            foreach(object o in collection)
            {
                if(o is string s)
                {
                    strings.Add($"\"{s}\"");
                }
                else
                {
                    strings.Add(o?.ToString());
                }
            }
            Console.WriteLine(string.Join(", ", strings));
        }

        //呼び出し用描画
        private void callPaint()
        {
            //消去
            GL.Clear(ClearBufferMask.ColorBufferBit); ;

            //上画面の絵
            drawingTop();
            //リストから表示
            drawFromList();

            glControl1.SwapBuffers();
        }

        
    }
}

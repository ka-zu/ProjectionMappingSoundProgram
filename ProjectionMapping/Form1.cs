using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * OpenTKの入れ方
 * Nugetマネージャで
 * OpenTK
 * OpenTK.NETCore
 * OpenTK.GLControl
 * を入れる
 * ソリューションエクスプローラーの参照より
 * 参照の追加でpackages\OpenTK.3.0.1\lib\net20内のOpenTK.dll
 * を追加する
 * 
 * GLControlツールの出し方
 * ツールボックス右クリック->アイテムの選択で
 * ソリューションと同じ階層のpackages内の
 * OpenTK.GLControl.3.0.1\lib\net20内のOpenTK.GLControl.dll
 * を参照する
*/
using OpenTK;

namespace ProjectionMapping
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //画像に合わせて線をクリックでなぞる画面
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();

            f2.Show();
        }

        //線を引く画面
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();

            f3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    
}

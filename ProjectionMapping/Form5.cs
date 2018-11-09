using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectionMapping
{
    /// <summary>
    /// 左窓投影ウィンドウ
    /// </summary>
    public partial class Form5 : Form
    {
        //窓ポリゴン座標格納用
        List<Point>[] winLists = new List<Point>[12];

        public Form5()
        {
            InitializeComponent();
        }
    }
}

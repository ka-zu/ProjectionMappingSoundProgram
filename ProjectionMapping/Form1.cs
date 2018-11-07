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
 * NAudioの入れ方
 * Nugetマネージャで
 * NAudioで検索して入れる
 */
using NAudio.Wave;//NAudio用

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

/*
 * OxyPlotの入れ方
 * Nugetマネージャで
 * OxyPlot.Core
 * OxyPlot.WindowsForm
 * を入れる
 * OxyPlot.Core.1.0.0\lib\net45内のOxyPlot.dllを
 * OxyPlot.WindowsForms.1.0.0\lib\net45にコピーしておく
 * 
 * プロットモデルの出し方
 * ツールボックス右クリック->アイテムの選択で
 * ソリューションと同じ階層のpackages内の
 * OxyPlot.WindowsForms.1.0.0\lib\net45内のOxyPlot.WindowsForms.dll
 * を参照する
 */
using OxyPlot;
using OxyPlot.Axes;//プロット軸用
using OxyPlot.Series;

namespace ProjectionMapping
{
    public partial class Form1 : Form
    {
        WaveIn waveIn;

        //子のフォームが開いていたら
        bool form2Opened = false;

        //周波数ヒストグラムの最大値
        double maxPow = 0;
        //最大値のindex
        int maxIndex = 0;
        //そのindexに対応する周波数
        double maxHz = 0;

        //一度の表示する点の数
        int samplingNum = 256;

        //グラフ用変数
        private PlotModel _plotmodel = new PlotModel();
        private PlotModel _plotmodel2 = new PlotModel();

        //プロットの軸
        private LinearAxis linearAxis1 = new LinearAxis
        {
            Position = AxisPosition.Bottom
        };
        private LinearAxis _linearAxis2 = new LinearAxis
        {
            Minimum = -1.0,
            Maximum = 1.0,
            Position = AxisPosition.Left
        };

        private LinearAxis _linearAxis3 = new LinearAxis
        {
            Position = AxisPosition.Bottom
        };
        private LinearAxis _linearAxis4 = new LinearAxis
        {
            Maximum = 25.0,
            Position = AxisPosition.Left
        };

        //たぶん格納した値がプロットされる
        private LineSeries _linerSeries = new LineSeries();//波形用
        private LineSeries _linerSeries2 = new LineSeries();//周波数用

        //音声データ
        List<float> _recorded = new List<float>(); //波形用
        List<float> _recorded2 = new List<float>(); //周波数ヒストグラム用

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //デバイスを探してひょうじ
            for(int i=0; i<WaveIn.DeviceCount; i++)
            {
                var deviceInfo = WaveIn.GetCapabilities(i);
                this.label1.Text = String.Format("Device {0}: {1}, {2} cannels",
                    i, deviceInfo.ProductName, deviceInfo.Channels);
            }
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

        

        private void plotView2_Click(object sender, EventArgs e)
        {

        }

        private void plotView1_Click(object sender, EventArgs e)
        {

        }

        //******
        //******自作関数
        //******

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            //32bitで最大値1.0fとする
            for(int index = 0; index < e.BytesRecorded; index += 2)
            {
                short sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index + 0]);
                float sample32 = sample / 32768f;
                //ProcessSample(sample32);
                //ProcessSample2(sample32);
            }
        }


    }
    
}

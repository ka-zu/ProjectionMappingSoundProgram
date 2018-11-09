using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Numerics;//Conplex型用

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
using OxyPlot;//プロット用
using OxyPlot.Axes;//プロット軸用
using OxyPlot.Series;//プロット用

/*
 * Math.Numericsの入れ方
 * Nugetマネージャで
 * Math.Numericsで検索して入れる
 */
using MathNet.Numerics;//窓関数用
using MathNet.Numerics.IntegralTransforms;//フーリエ変換用

namespace ProjectionMapping
{
    public partial class Form1 : Form
    {
        Form3 f3 = new Form3();
        Form4 f4 = new Form4();

        WaveIn waveIn;

        //子のフォームが開いていたら
        bool isForm3Open = false;
        bool isForm4Open = false;

        //周波数ヒストグラムの最大値
        double maxPow = 0;
        //最大値のindex
        int maxIndex = 0;
        //そのindexに対応する周波数
        double maxHz = 0;
        //周波数ヒストグラムの合計（全体的な音の大きさ）
        double sumHist = 0;

        //一度の表示する点の数
        int samplingNum = 256;

        //波形をFFTしたデータ格納
        double[] fftNum;

        //グラフ用変数
        private PlotModel _plotmodel = new PlotModel();
        private PlotModel _plotmodel2 = new PlotModel();

        //プロットの軸
        private LinearAxis _linearAxis1 = new LinearAxis
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

        const int COLOR_TIMER = 50;

        //色を変えるタイミング用変数 ゼロになったら変える
        int colorChangeTime1 = COLOR_TIMER;
        int colorChangeTime2 = COLOR_TIMER;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //デバイスを探して表示
            for(int i=0; i<WaveIn.DeviceCount; i++)
            {
                var deviceInfo = WaveIn.GetCapabilities(i);
                this.label1.Text = String.Format("Device {0}: {1}, {2} cannels",
                    i, deviceInfo.ProductName, deviceInfo.Channels);
            }

            //PlotViewの初期化
            InitProt();
            InitProt2();

            //音声の取得
            waveIn = new WaveIn()
            {
                DeviceNumber = 0,//デフォルトを指定
            };

            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.WaveFormat = new WaveFormat(sampleRate: 8000, channels: 1);
            waveIn.StartRecording();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //画像に合わせて線をクリックでなぞる画面

            Form2 f2 = new Form2();

            f2.Show();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            //線を引く画面を開く

            isForm3Open = true;
            f3.setFFTHist(fftNum);
            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //上投影画面
            isForm4Open = true;
            f4.Show();
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
                ProcessSample(sample32);
                ProcessSample2(sample32);
            }
        }

        //プロット部分の初期化
        //波形
        private void InitProt()
        {
            _plotmodel.Axes.Add(_linearAxis1);
            _plotmodel.Axes.Add(_linearAxis2);
            _plotmodel.Series.Add(_linerSeries);
            this.plotView1.Model = _plotmodel;
        }
        //周波数
        private void InitProt2()
        {
            _plotmodel2.Axes.Add(_linearAxis3);
            _plotmodel2.Axes.Add(_linearAxis4);
            _plotmodel2.Series.Add(_linerSeries2);
            this.plotView2.Model = _plotmodel2;
        }

        //たぶん波形グラフ表示
        private void ProcessSample(float sample)
        {
            //表示する範囲の波形を取得
            _recorded.Add(sample);

            if(_recorded.Count == samplingNum)
            {
                //プロットする点の作成
                var points = _recorded.Select((v, index) =>
                        new DataPoint((double)index, v)
                        ).ToList();
                //表示領域を消す
                _linerSeries.Points.Clear();
                //プロット
                _linerSeries.Points.AddRange(points);
                //無効な点をなんらかする
                this.plotView1.InvalidatePlot(true);
                //消す
                _recorded.Clear();
            }

        }

        //周波数ヒストグラム表示
        private void ProcessSample2(float sample)
        {
            int windowSize = samplingNum;
            var window = Window.Hamming(windowSize);

            _recorded2.Add(sample);
            if (_recorded2.Count == samplingNum)
            {
                _recorded2 = _recorded2.Select((v, i) => v * (float)window[i]).ToList();
                Complex[] complexData = _recorded2.Select(v => new Complex(v, 0.0)).ToArray();

                Fourier.Forward(complexData, FourierOptions.Matlab);
                //Fourier.Radix2Forward(complexData.ToArray(), FourierOptions.Default);

                double s = (double)windowSize * (1.0 / 8000.0);

                //Take(int num)　は　配列の最初からnum番目までを見る関数
                //Take(complexData.Count() / 2)で配列の半分までを見てる（フーリエ変換は半分で対称だから）
                //Select()は配列を新たな配列に変換できる
                //Select(int num1, int num2)で　num2番目の値num1を見ている　num1=complexData[num2]　みたいな感じ
                //Select(v, index)はindex番目の値vを取り出して
                //=> new DataPoint()でDataPoint型の(周波数,その周波数の大きさ)に変換してる
                var points = complexData.Take(complexData.Count() / 2).Select((v, index) => new DataPoint((double)index / s,
                                                                              Math.Sqrt(v.Real * v.Real + v.Imaginary * v.Imaginary))
                ).ToList();

                //格納されている値がその周波数の大きさ
                //配列のインデックス / sをすることで周波数になる
                //double[] fftNum = complexData.Take(complexData.Count() / 2).Select(v => Math.Sqrt(v.Real * v.Real + v.Imaginary * v.Imaginary)).ToArray();
                fftNum = complexData.Take(complexData.Count() / 2).Select(v => Math.Sqrt(v.Real * v.Real + v.Imaginary * v.Imaginary)).ToArray();

                _linerSeries2.Points.Clear();
                _linerSeries2.Points.AddRange(points);

                this.plotView2.InvalidatePlot(true);
                _recorded2.Clear();

                //Console.WriteLine("complexData.Count() = " + complexData.Count() / 2);

                //一番大きい周波数を調べる
                for (int i = 0; i < complexData.Count() / 2; i++)
                {
                    if (maxPow <= fftNum[i])
                    {
                        maxPow = fftNum[i];
                        maxIndex = i;
                    }
                    //Console.WriteLine(maxIndex.ToString() + " : " + maxPow.ToString());
                    sumHist += fftNum[i];
                }

                //インデックスの値を周波数にする
                maxHz = (maxIndex / s);
                //表示
                this.label4.Text = ("最大周波数" + maxHz.ToString() + " : " + maxPow.ToString());
                this.label5.Text = ("ヒストグラム合計" + sumHist.ToString());

                //
                //以下子フォーム操作系
                //

                //バー表示画面に書き込み
                if (isForm3Open == true)
                {
                    f3.setFFTData(maxHz, maxPow, sumHist);//最大周波数とその大きさを送る

                    //色変えタイミング
                    if (colorChangeTime1 == 0)
                    {
                        if (radioButton1.Checked == true)//最大周波数にチェック
                        {
                            f3.setColorByMaxHz(maxHz);//最大周波数でHLS値を設定
                        }
                        else if(radioButton2.Checked == true)//ヒストグラム合計にチェック
                        {
                            f3.setColorBySumHistgram(sumHist);//ヒストグラムの合計値でHLS値を設定
                        }
                        else
                        {
                            f3.setColorByMaxHz(maxHz);//最大周波数でHLS値を設定
                        }
                            
                        f3.convertingHLSToRGB();//HLSをRGBに変換

                        colorChangeTime1 = COLOR_TIMER;
                    }
                    else
                    {
                        colorChangeTime1--;
                    }
                    
                    f3.setFFTHist(fftNum);//FFTした値を送信

                    //フォームが閉じられていたらしない
                    if(f3.IsDisposed == true) { isForm3Open = false; return; }
                    f3.refresh();//描画の更新
                }

                //上窓表示画面に書き込み
                if (isForm4Open == true)
                {

                    //色変えタイミング
                    if (colorChangeTime2 == 0)
                    {
                        f4.RGBA = f3.RGBA;

                        colorChangeTime2 = COLOR_TIMER;
                    }
                    else
                    {
                        colorChangeTime2--;
                    }

                    //フォームが閉じられていたらしない
                    if (f4.IsDisposed == true) { isForm4Open = false; return; }
                    f4.refresh();//描画の更新
                }



                maxPow = 0;
                maxIndex = 0;
                maxHz = 0;
                sumHist = 0;
            }


        }

        
    }
    
}

namespace ProjectionMapping
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.plotView2 = new OxyPlot.WindowsForms.PlotView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.upLocationX = new System.Windows.Forms.TextBox();
            this.upLocationY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bottomLocationX = new System.Windows.Forms.TextBox();
            this.bottomLocationY = new System.Windows.Forms.TextBox();
            this.leftLocationY = new System.Windows.Forms.TextBox();
            this.leftLocationX = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rightLocationY = new System.Windows.Forms.TextBox();
            this.rightLocationX = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.threholtTxtBox = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 86);
            this.button1.TabIndex = 0;
            this.button1.Text = "ライントレース";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 35);
            this.button2.TabIndex = 1;
            this.button2.Text = "バー表示\r\n(下画面)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // plotView1
            // 
            this.plotView1.Location = new System.Drawing.Point(12, 53);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(1309, 741);
            this.plotView1.TabIndex = 2;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotView1.Click += new System.EventHandler(this.plotView1_Click);
            // 
            // plotView2
            // 
            this.plotView2.Location = new System.Drawing.Point(12, 347);
            this.plotView2.Name = "plotView2";
            this.plotView2.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView2.Size = new System.Drawing.Size(328, 130);
            this.plotView2.TabIndex = 3;
            this.plotView2.Text = "plotView2";
            this.plotView2.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView2.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView2.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotView2.Click += new System.EventHandler(this.plotView2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "波形";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "周波数ヒストグラム";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 492);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 517);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "label5";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 16);
            this.radioButton1.TabIndex = 9;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "最大周波数";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 39);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(135, 16);
            this.radioButton2.TabIndex = 10;
            this.radioButton2.Text = "周波数ヒストグラム合計";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(425, 62);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 85);
            this.button3.TabIndex = 12;
            this.button3.Text = "上画面表示";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(531, 64);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 83);
            this.button4.TabIndex = 13;
            this.button4.Text = "左画面表示";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(643, 64);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(89, 83);
            this.button5.TabIndex = 14;
            this.button5.Text = "右画面表示";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(265, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 58);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "色を変える要素";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Location = new System.Drawing.Point(265, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 59);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "バーのモード";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 40);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(123, 16);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "最大周波数で増える";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(89, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "最初から全部";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // upLocationX
            // 
            this.upLocationX.Location = new System.Drawing.Point(498, 282);
            this.upLocationX.Name = "upLocationX";
            this.upLocationX.Size = new System.Drawing.Size(88, 19);
            this.upLocationX.TabIndex = 17;
            this.upLocationX.Text = "0";
            this.upLocationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // upLocationY
            // 
            this.upLocationY.Location = new System.Drawing.Point(592, 282);
            this.upLocationY.Name = "upLocationY";
            this.upLocationY.Size = new System.Drawing.Size(88, 19);
            this.upLocationY.TabIndex = 18;
            this.upLocationY.Text = "0";
            this.upLocationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(496, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 24);
            this.label6.TabIndex = 19;
            this.label6.Text = "上画面の座標\r\nX　　　　　　　　　　　Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(496, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 24);
            this.label7.TabIndex = 20;
            this.label7.Text = "下画面の座標\r\nX　　　　　　　　　　　Y";
            // 
            // bottomLocationX
            // 
            this.bottomLocationX.Location = new System.Drawing.Point(498, 331);
            this.bottomLocationX.Name = "bottomLocationX";
            this.bottomLocationX.Size = new System.Drawing.Size(88, 19);
            this.bottomLocationX.TabIndex = 21;
            this.bottomLocationX.Text = "0";
            this.bottomLocationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // bottomLocationY
            // 
            this.bottomLocationY.Location = new System.Drawing.Point(592, 331);
            this.bottomLocationY.Name = "bottomLocationY";
            this.bottomLocationY.Size = new System.Drawing.Size(88, 19);
            this.bottomLocationY.TabIndex = 22;
            this.bottomLocationY.Text = "0";
            this.bottomLocationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // leftLocationY
            // 
            this.leftLocationY.Location = new System.Drawing.Point(592, 380);
            this.leftLocationY.Name = "leftLocationY";
            this.leftLocationY.Size = new System.Drawing.Size(88, 19);
            this.leftLocationY.TabIndex = 25;
            this.leftLocationY.Text = "0";
            this.leftLocationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // leftLocationX
            // 
            this.leftLocationX.Location = new System.Drawing.Point(498, 380);
            this.leftLocationX.Name = "leftLocationX";
            this.leftLocationX.Size = new System.Drawing.Size(88, 19);
            this.leftLocationX.TabIndex = 24;
            this.leftLocationX.Text = "0";
            this.leftLocationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(496, 353);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 24);
            this.label8.TabIndex = 23;
            this.label8.Text = "左画面の座標\r\nX　　　　　　　　　　　Y";
            // 
            // rightLocationY
            // 
            this.rightLocationY.Location = new System.Drawing.Point(592, 429);
            this.rightLocationY.Name = "rightLocationY";
            this.rightLocationY.Size = new System.Drawing.Size(88, 19);
            this.rightLocationY.TabIndex = 28;
            this.rightLocationY.Text = "0";
            this.rightLocationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // rightLocationX
            // 
            this.rightLocationX.Location = new System.Drawing.Point(498, 429);
            this.rightLocationX.Name = "rightLocationX";
            this.rightLocationX.Size = new System.Drawing.Size(88, 19);
            this.rightLocationX.TabIndex = 27;
            this.rightLocationX.Text = "0";
            this.rightLocationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumber);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(496, 402);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 24);
            this.label9.TabIndex = 26;
            this.label9.Text = "右画面の座標\r\nX　　　　　　　　　　　Y";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(496, 175);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(140, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "色変えのタイミングのしきい値";
            // 
            // threholtTxtBox
            // 
            this.threholtTxtBox.Location = new System.Drawing.Point(498, 190);
            this.threholtTxtBox.Name = "threholtTxtBox";
            this.threholtTxtBox.Size = new System.Drawing.Size(88, 19);
            this.threholtTxtBox.TabIndex = 30;
            this.threholtTxtBox.Text = "0";
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.button6.Location = new System.Drawing.Point(592, 190);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(61, 30);
            this.button6.TabIndex = 31;
            this.button6.Text = "決定";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(496, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "今：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 806);
            this.Controls.Add(this.plotView1);
            this.Controls.Add(this.plotView2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.threholtTxtBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rightLocationY);
            this.Controls.Add(this.rightLocationX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.leftLocationY);
            this.Controls.Add(this.leftLocationX);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bottomLocationY);
            this.Controls.Add(this.bottomLocationX);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.upLocationY);
            this.Controls.Add(this.upLocationX);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private OxyPlot.WindowsForms.PlotView plotView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.TextBox upLocationX;
        private System.Windows.Forms.TextBox upLocationY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox bottomLocationX;
        private System.Windows.Forms.TextBox bottomLocationY;
        private System.Windows.Forms.TextBox leftLocationY;
        private System.Windows.Forms.TextBox leftLocationX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rightLocationY;
        private System.Windows.Forms.TextBox rightLocationX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox threholtTxtBox;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label11;
    }
}


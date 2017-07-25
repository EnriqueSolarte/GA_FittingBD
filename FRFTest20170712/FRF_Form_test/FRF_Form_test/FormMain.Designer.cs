namespace FRF_Form_test
{
    partial class FormMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_mag = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_phs = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBoxMag_1 = new System.Windows.Forms.TextBox();
            this.textBoxMag_2 = new System.Windows.Forms.TextBox();
            this.textBoxMag_3 = new System.Windows.Forms.TextBox();
            this.textBoxFreq_1 = new System.Windows.Forms.TextBox();
            this.textBoxFreq_2 = new System.Windows.Forms.TextBox();
            this.textBoxFreq_3 = new System.Windows.Forms.TextBox();
            this.textBoxFreqMin_1 = new System.Windows.Forms.TextBox();
            this.textBoxFreqMin_2 = new System.Windows.Forms.TextBox();
            this.textBoxFreqMin_3 = new System.Windows.Forms.TextBox();
            this.textBoxFreqMax_1 = new System.Windows.Forms.TextBox();
            this.textBoxFreqMax_2 = new System.Windows.Forms.TextBox();
            this.textBoxFreqMax_3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonCreateFiittingCurve = new System.Windows.Forms.Button();
            this.buttonOptimizationOptions = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart_mag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_phs)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_mag
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_mag.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_mag.Legends.Add(legend1);
            this.chart_mag.Location = new System.Drawing.Point(2, 2);
            this.chart_mag.Margin = new System.Windows.Forms.Padding(2);
            this.chart_mag.Name = "chart_mag";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.chart_mag.Series.Add(series1);
            this.chart_mag.Series.Add(series2);
            this.chart_mag.Size = new System.Drawing.Size(697, 306);
            this.chart_mag.TabIndex = 0;
            this.chart_mag.Text = "11";
            this.chart_mag.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.InformationTipEvent);
            // 
            // chart_phs
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_phs.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_phs.Legends.Add(legend2);
            this.chart_phs.Location = new System.Drawing.Point(2, 312);
            this.chart_phs.Margin = new System.Windows.Forms.Padding(2);
            this.chart_phs.Name = "chart_phs";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series2";
            this.chart_phs.Series.Add(series3);
            this.chart_phs.Series.Add(series4);
            this.chart_phs.Size = new System.Drawing.Size(697, 306);
            this.chart_phs.TabIndex = 1;
            this.chart_phs.Text = "chart_phs";
            this.chart_phs.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.InformationTipEvent);
            // 
            // textBoxMag_1
            // 
            this.textBoxMag_1.Location = new System.Drawing.Point(54, 667);
            this.textBoxMag_1.Name = "textBoxMag_1";
            this.textBoxMag_1.Size = new System.Drawing.Size(100, 22);
            this.textBoxMag_1.TabIndex = 2;
            this.textBoxMag_1.Text = "-2";
            // 
            // textBoxMag_2
            // 
            this.textBoxMag_2.Location = new System.Drawing.Point(54, 695);
            this.textBoxMag_2.Name = "textBoxMag_2";
            this.textBoxMag_2.Size = new System.Drawing.Size(100, 22);
            this.textBoxMag_2.TabIndex = 2;
            this.textBoxMag_2.Text = "-3";
            // 
            // textBoxMag_3
            // 
            this.textBoxMag_3.Location = new System.Drawing.Point(54, 723);
            this.textBoxMag_3.Name = "textBoxMag_3";
            this.textBoxMag_3.Size = new System.Drawing.Size(100, 22);
            this.textBoxMag_3.TabIndex = 2;
            this.textBoxMag_3.Text = "-6.4";
            // 
            // textBoxFreq_1
            // 
            this.textBoxFreq_1.Location = new System.Drawing.Point(160, 667);
            this.textBoxFreq_1.Name = "textBoxFreq_1";
            this.textBoxFreq_1.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreq_1.TabIndex = 2;
            this.textBoxFreq_1.Text = "60";
            // 
            // textBoxFreq_2
            // 
            this.textBoxFreq_2.Location = new System.Drawing.Point(160, 695);
            this.textBoxFreq_2.Name = "textBoxFreq_2";
            this.textBoxFreq_2.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreq_2.TabIndex = 2;
            this.textBoxFreq_2.Text = "125";
            // 
            // textBoxFreq_3
            // 
            this.textBoxFreq_3.Location = new System.Drawing.Point(160, 723);
            this.textBoxFreq_3.Name = "textBoxFreq_3";
            this.textBoxFreq_3.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreq_3.TabIndex = 2;
            this.textBoxFreq_3.Text = "335";
            // 
            // textBoxFreqMin_1
            // 
            this.textBoxFreqMin_1.Location = new System.Drawing.Point(304, 667);
            this.textBoxFreqMin_1.Name = "textBoxFreqMin_1";
            this.textBoxFreqMin_1.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreqMin_1.TabIndex = 2;
            this.textBoxFreqMin_1.Text = "55";
            // 
            // textBoxFreqMin_2
            // 
            this.textBoxFreqMin_2.Location = new System.Drawing.Point(304, 695);
            this.textBoxFreqMin_2.Name = "textBoxFreqMin_2";
            this.textBoxFreqMin_2.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreqMin_2.TabIndex = 2;
            this.textBoxFreqMin_2.Text = "115";
            // 
            // textBoxFreqMin_3
            // 
            this.textBoxFreqMin_3.Location = new System.Drawing.Point(304, 723);
            this.textBoxFreqMin_3.Name = "textBoxFreqMin_3";
            this.textBoxFreqMin_3.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreqMin_3.TabIndex = 2;
            this.textBoxFreqMin_3.Text = "290";
            // 
            // textBoxFreqMax_1
            // 
            this.textBoxFreqMax_1.Location = new System.Drawing.Point(420, 667);
            this.textBoxFreqMax_1.Name = "textBoxFreqMax_1";
            this.textBoxFreqMax_1.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreqMax_1.TabIndex = 2;
            this.textBoxFreqMax_1.Text = "60";
            // 
            // textBoxFreqMax_2
            // 
            this.textBoxFreqMax_2.Location = new System.Drawing.Point(420, 695);
            this.textBoxFreqMax_2.Name = "textBoxFreqMax_2";
            this.textBoxFreqMax_2.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreqMax_2.TabIndex = 2;
            this.textBoxFreqMax_2.Text = "220";
            // 
            // textBoxFreqMax_3
            // 
            this.textBoxFreqMax_3.Location = new System.Drawing.Point(420, 723);
            this.textBoxFreqMax_3.Name = "textBoxFreqMax_3";
            this.textBoxFreqMax_3.Size = new System.Drawing.Size(100, 22);
            this.textBoxFreqMax_3.TabIndex = 2;
            this.textBoxFreqMax_3.Text = "580";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 652);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Magnitud (dB)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 652);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Frequency";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(332, 652);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Minimum";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(448, 652);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Maximum";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(371, 636);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Frequency Range";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(123, 636);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "Resonant Peak";
            // 
            // buttonCreateFiittingCurve
            // 
            this.buttonCreateFiittingCurve.Location = new System.Drawing.Point(553, 678);
            this.buttonCreateFiittingCurve.Name = "buttonCreateFiittingCurve";
            this.buttonCreateFiittingCurve.Size = new System.Drawing.Size(110, 23);
            this.buttonCreateFiittingCurve.TabIndex = 5;
            this.buttonCreateFiittingCurve.Text = "Fitting Bode Curve";
            this.buttonCreateFiittingCurve.UseVisualStyleBackColor = true;
            this.buttonCreateFiittingCurve.Click += new System.EventHandler(this.FittingCurveBtnEvent);
            // 
            // buttonOptimizationOptions
            // 
            this.buttonOptimizationOptions.Enabled = false;
            this.buttonOptimizationOptions.Location = new System.Drawing.Point(553, 706);
            this.buttonOptimizationOptions.Name = "buttonOptimizationOptions";
            this.buttonOptimizationOptions.Size = new System.Drawing.Size(110, 23);
            this.buttonOptimizationOptions.TabIndex = 5;
            this.buttonOptimizationOptions.Text = "Genetic Algor Op";
            this.buttonOptimizationOptions.UseVisualStyleBackColor = true;
            this.buttonOptimizationOptions.Click += new System.EventHandler(this.GeneticAlgorithmOptionsBntEvent);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 782);
            this.Controls.Add(this.buttonOptimizationOptions);
            this.Controls.Add(this.buttonCreateFiittingCurve);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFreqMax_3);
            this.Controls.Add(this.textBoxFreqMin_3);
            this.Controls.Add(this.textBoxFreq_3);
            this.Controls.Add(this.textBoxMag_3);
            this.Controls.Add(this.textBoxFreqMax_2);
            this.Controls.Add(this.textBoxFreqMin_2);
            this.Controls.Add(this.textBoxFreq_2);
            this.Controls.Add(this.textBoxMag_2);
            this.Controls.Add(this.textBoxFreqMax_1);
            this.Controls.Add(this.textBoxFreqMin_1);
            this.Controls.Add(this.textBoxFreq_1);
            this.Controls.Add(this.textBoxMag_1);
            this.Controls.Add(this.chart_phs);
            this.Controls.Add(this.chart_mag);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart_mag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_phs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_mag;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_phs;
        private System.Windows.Forms.TextBox textBoxMag_1;
        private System.Windows.Forms.TextBox textBoxMag_2;
        private System.Windows.Forms.TextBox textBoxMag_3;
        private System.Windows.Forms.TextBox textBoxFreq_1;
        private System.Windows.Forms.TextBox textBoxFreq_2;
        private System.Windows.Forms.TextBox textBoxFreq_3;
        private System.Windows.Forms.TextBox textBoxFreqMin_1;
        private System.Windows.Forms.TextBox textBoxFreqMin_2;
        private System.Windows.Forms.TextBox textBoxFreqMin_3;
        private System.Windows.Forms.TextBox textBoxFreqMax_1;
        private System.Windows.Forms.TextBox textBoxFreqMax_2;
        private System.Windows.Forms.TextBox textBoxFreqMax_3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonCreateFiittingCurve;
        private System.Windows.Forms.Button buttonOptimizationOptions;
    }
}


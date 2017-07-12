using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;


using Mechatronics.MathToolBox;
using Mechatronics.MSMObject;
using Mechatronics.Analysis;

namespace FRF_Form_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Create Structure Nature Modes Object
            List<Mode> VLoopModes = new List<Mode>();
            CreateModes(VLoopModes);

            //Set experiment Control Parameters 
            Parameters P = new Parameters();
            SetParameters(P);
            // setting C controller 

            //create Object
            Mechatronics.Analysis.VelocityResponse VR = new VelocityResponse();


            //read FRF File from csv file, which is created by servoguide
            //this data is considered as referance.
            FRF[] VClose_ref =
                VR.ReadServoGuide_FRFdata_csv("Frequency_Response_Axis-1_1_-_1000Hz.csv");
            FRF aaa = new FRF();
            

            VR.P = P;
            VR.VLoopModes = VLoopModes;

            FRF[] Close = VR.SolveCloseLoopResponse();

            FRF[] Open = VR.SolveOpenLoopResponse();

            

            // draw referance FRF in chart
            DrawLine(VClose_ref, 0);

            // draw simulated FRF in chart
            DrawLine(Close, 1);

        }





        void SetParameters(Parameters P)
        {//value is from *.prm file (ServoGuide Parameter file)
            P.FANUCs.HRVGain = 300;
            P.FANUCs.VelocityLoopGain = 100;
            P.FANUCs.No2043_KVI_Standard = 198;
            P.FANUCs.No2044_KVP_Standard = -1775;
            P.FANUCs.No2043_KVI_Setting = 198;
            P.FANUCs.No2044_KVP_Setting = -1775;


            P.Jm = 0.012;
            P.Kt = 1.2;
            P.dTzoh = 0.001m;
            P.Tc = 0;
            P.JL = 0.00546;
            P.IsHighCycle = true;
            P.IsFullCloseLoop = true;

            P.ConvertFUNUCParamters();

        }
        void CreateModes(List<Mode> VLoopModes)
        {

            Mode mode;

            mode = new Mode();
            mode.Freq = 55;
            mode.Zeta = 0.1;
            mode.Mass = 0.3;
            VLoopModes.Add(mode);
            

            mode = new Mode();
            mode.Freq = 120;
            mode.Zeta = 0.07;
            mode.Mass = 0.1;
            VLoopModes.Add(mode);

            mode = new Mode();
            mode.Freq = 315;
            mode.Zeta = 0.1;
            mode.Mass = 0.05;
            VLoopModes.Add(mode);


        }

        void DrawLine(FRF[] FRFData, int Channel)
        {
            //X AXIS in log scale
            chart_mag.ChartAreas[0].AxisX.IsLogarithmic = true;
            chart_phs.ChartAreas[0].AxisX.IsLogarithmic = true;

            //reset
            chart_mag.Series[Channel].Points.Clear();
            chart_phs.Series[Channel].Points.Clear();

            for (int i = 0; i < FRFData.Length; i++)
            {
                chart_mag.Series[Channel].Points.AddXY(FRFData[i].Freq, Mechatronics.MathToolBox.TL.DB(FRFData[i].Mag));
                chart_phs.Series[Channel].Points.AddXY(FRFData[i].Freq, FRFData[i].Phs);

            }

        }


    }
}

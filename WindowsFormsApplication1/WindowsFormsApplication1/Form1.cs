using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Condiciones iniciales-------------------------//
            int N = (int)numericUpDown1.Value;
            double T = 0;
            double Tpll = 0;
            double[] Tci = new double[20000];
            double Gt = 0;
            double[] Sto = new double[20000];
            double[] St = new double[20000];
            double TF = 1000000;
            int aux = 0;
            var rnd = new Random(DateTime.Now.Millisecond);
            double R1;
            double R2;
            double X1 =0;
            double Y1;
            double fx1;
            double ILL;
            double Pto;
            int flag1 =0;

            while (aux < 20000)
            { Sto[aux] = -100; St[aux] = -100; Tci[aux] = -100; aux++; }
            aux = 0;
            while (aux < N)
            { Sto[aux] = 0; St[aux] = 0; Tci[aux] = 0; aux++; }

            //comienzo de la simulacion--------------------//
            while (T < TF)
            {
                T = Tpll;
                //Genero iLL-----------------------------------------//
                fx1 = 0; Y1 = 1;
                while (fx1<Y1){ 
                Double M = 3.1;

                double rDouble1 = rnd.NextDouble() * 1;
                R1 =Math.Truncate(rDouble1 * Math.Pow(10, 2)) / Math.Pow(10, 2);
                rDouble1 = rnd.NextDouble() * 1;
                R2 = Math.Truncate(rDouble1 * Math.Pow(10, 2)) / Math.Pow(10, 2);
                X1 = 40 * R1;
                Y1 = M * R2;
                fx1 = (2 * X1) / ((Math.Pow(X1, 4) / 625) + (2 * Math.Pow(X1, 2) / 25) + 1);
                }

                ILL = X1;
                //.......................................................//
                   
            Tpll = T + ILL;
                int ind = 0;

               
                //busco tci menor
               for(int j = 1; j <N; j++) {
                    if (Tci[j]< Tci[ind]) { ind = j; }
                }
                if (Tci[0] < 0) {  flag1 =1; }
                //-----------------------------------------------------//

                //GeneroTT
                double rDouble = rnd.NextDouble() * 1;
                R1 = Math.Truncate(rDouble * Math.Pow(10, 2)) / Math.Pow(10, 2);
                X1 = 30 * R1 + 15;
                double TT = X1;

                Tci[ind] = Tci[ind] + TT;
                St[ind] = St[ind] + (50 * TT/60);
                //genero random
                rDouble = rnd.NextDouble() * 1;

                R1 = Math.Truncate(rDouble * Math.Pow(10, 2)) / Math.Pow(10, 2);

                if (R1 < 0.7)
                {
                    //genero TR
                    fx1 = 0; Y1 = 1;
                    while (fx1 < Y1)
                    {
                        Double M = 1;

                        double rDouble1 = rnd.NextDouble() * 1;
                        R1 = Math.Truncate(rDouble1 * Math.Pow(10, 2)) / Math.Pow(10, 2);
                        rDouble1 = rnd.NextDouble() * 1;
                        R2 = Math.Truncate(rDouble1 * Math.Pow(10, 2)) / Math.Pow(10, 2);
                        X1 = 2 * R1 +1;
                        Y1 = M * R2;
                        fx1 =  (-(Math.Pow(X1, 2)) + (6 * X1)  -8 );
                    }


                    double TR = X1 ;



                    if (T >= Tci[ind])
                    {
                        Sto[ind] = Sto[ind] + (T - Tci[ind]);
                        Tci[ind] = T + TR; 
                    }
                    else {
                        Tci[ind] = Tci[ind] + TR; 
                    }
                    St[ind] = St[ind] + (50 * TR / 60);
                    Gt = Gt + 60 * TR/60; 
                }
                else
                {
                    if (T >= Tci[ind])
                    {
                        Sto[ind] = Sto[ind] + (T - Tci[ind]);
                        Tci[ind] = T + 60;

                    }
                    else
                    {
                        Tci[ind] = T + 60;
                    }
                    St[ind] = St[ind] + 50;
                    Gt = Gt + 150;

                }


            }
            int i = 0;
           double sum = 0;
            while (St[i] > 0)
            {
                sum = sum + St[i]; i++;
            }
            Gt = Gt - sum;
            i = 0;
             sum = 0;
            while (Sto[i] > 0)
            {
                sum = sum + Sto[i]; i++;
            }
            Pto = ((sum / TF) / N)*100;
            if (flag1 == 1)
            { Gt = 0; Pto = 0; }
            textBox3.Text = Pto.ToString();
            
            textBox2.Text = (((8*20*60 * Gt) / TF)-(500*N)).ToString();       }
    }


    }


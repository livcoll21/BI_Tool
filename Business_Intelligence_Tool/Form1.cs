using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Business_Intelligence_Tool
{
    public partial class Form1 : Form
    {
        ///////////////////////////////////////////////////////////////////////////

        // GLOBALS

        // Global data for storing data and for plotting lines
        int year;
        string[] fileData = new string[25];
        double[] plotLines = new double[12];
        double[] plotLinesFC = new double[12]; // data for Funky Chicken
        double[] plotLinesBB = new double[12]; // data for Barry's Burgers
        double[] plotLinesII = new double[12]; // data for Izzys Ice Cream
        double[] plotLinesPP = new double[12]; // data for Pizza Petes
        double[] plotLinesKK = new double[12]; // data for Kevs Kebabs

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        ///////////////////////////////////////////////////////////////////////////

        // BUTTON READ FILE, BUILD DATA, PRESENT DATA (All in one)

        private void btnRead_Click(object sender, EventArgs e)
        {
            // Local path to bin file

            string fileName = "mySalesData.csv";

            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(fileName);

            // Reads all data from file into internal array

            int len = 25;

            for (int i = 0; i < len; i++)
            {
                fileData[i] = objReader.ReadLine();
            }

            objReader.Close();

            ///////////////////////////////////////////////////////////////////////////

            // LINE CHART  

            string salesData;
            string[] plotData;

            if (textBox1.Text.Length != 0)
            {

                year = int.Parse(textBox1.Text);

                for (int i = 0; i < 25; i++) // step through whole array looking for years
                {
                    salesData = fileData[i];
                    plotData = salesData.Split(',');
                    if (int.Parse(plotData[1]) == (year))
                    {

                        // build data for Funky Chicken
                        if (plotData[0] == "Funky Chicken")
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                plotLinesFC[j] = double.Parse(plotData[j + 2]);
                            }
                        }

                        // build data for Barry's Burgers
                        if (plotData[0] == "Barrys Burgers")
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                plotLinesBB[j] = double.Parse(plotData[j + 2]);
                            }
                        }

                        // build data for Izzys Ice Cream
                        if (plotData[0] == "Izzys Ice cream")
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                plotLinesII[j] = double.Parse(plotData[j + 2]);
                            }
                        }

                        // build data for Pizza Petes
                        if (plotData[0] == "Pizza Petes")
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                plotLinesPP[j] = double.Parse(plotData[j + 2]);
                            }
                        }

                        // build data for Kevs Kababs
                        if (plotData[0] == "Kevs Kebabs")
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                plotLinesKK[j] = double.Parse(plotData[j + 2]);
                            }
                        }
                    }
                    
                }

                // adds some chart labels for months

                string[] months = new string[12] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

                CustomLabel customlabel;

                Axis axisX = chart2.ChartAreas[0].AxisX;
                double axelLabelPos = 0.5;
                for (int i = 0; i < 12; i++)
                {
                    customlabel = axisX.CustomLabels.Add(axelLabelPos, axelLabelPos + 1, months[i]);
                    axelLabelPos++;
                }

                chart2.Series["Funky Chicken"].Points.DataBindY(plotLinesFC);
                chart2.Series["Barry's Burgers"].Points.DataBindY(plotLinesBB);
                chart2.Series["Izzys Ice Cream"].Points.DataBindY(plotLinesII);
                chart2.Series["Pizza Petes"].Points.DataBindY(plotLinesPP);
                chart2.Series["Kevs Kebabs"].Points.DataBindY(plotLinesKK);
            }

            ///////////////////////////////////////////////////////////////////////////

            // BAR CHART


            // sets the titles and year totals array
            string[] titles = new string[5] { "Funky Chicken", "Barrys Burgers", "Izzys Ice Cream", "Pizza Pete", "Kevs Kebabs" };
            double[] yearTotals = new double[5];

            CustomLabel customlabel1;
            Axis barChartAxisX = chart1.ChartAreas[0].AxisX;

            double axelLabelPos1 = 0.5;

            for (int i = 0; i < 5; i++)
            {
                customlabel1 = barChartAxisX.CustomLabels.Add(axelLabelPos1, axelLabelPos1 + 1, titles[i]);
                axelLabelPos1++;
            }

            // calculate year yearTotals here

            for (int j = 0; j < 12; j++)
            {
                yearTotals[0] = yearTotals[0] + plotLinesFC[j];
                yearTotals[1] = yearTotals[1] + plotLinesBB[j];
                yearTotals[2] = yearTotals[2] + plotLinesII[j];
                yearTotals[3] = yearTotals[3] + plotLinesPP[j];
                yearTotals[4] = yearTotals[4] + plotLinesKK[j];
            }
            chart1.Series["Series1"].Points.DataBindY(yearTotals);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

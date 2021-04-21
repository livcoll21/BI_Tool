using System;
using System.Windows.Forms;

public partial class Form1 : Form
{
    //global data for storing data and for plotting lines
    int year;
    string[] file_data = new string[10];
    double[] plotLines = new double[12];
    double[] plotLines_fc = new double[12];
    double[] plotLines_bb = new double[12];


    private void button1_Click(object sender, EventArgs e)
    {
        //local path to bin file!"
        int counter = 0;
        string line;
        string file_name = "myDemoSalesData.csv";
        System.IO.StreamReader objReader;
        objReader = new System.IO.StreamReader(file_name);
        // read all data from file into internal array
        while ((line = objReader.ReadLine()) != null)
        {
            file_data[counter] = line;
            counter++;
        }
        MessageBox.Show(counter + " records read from data file");
        objReader.Close();
    }


    private void button2_Click(object sender, EventArgs e)
    {
        string sales_data;
        string[] plot_data;
        year = int.Parse(textBox1.Text);
        for (int i = 0; i < 10; i++) // step through whole array looking for years
        {
            sales_data = file_data[i];
            plot_data = sales_data.Split(',');
            if (int.Parse(plot_data[1]) == (year))
            {
                //build data for Funky Chicken
                if (plot_data[0] == "Funky Chicken")
                {
                    for (int j = 0; j < 12; j++)
                    {
                        plotLines_fc[j] = double.Parse(plot_data[j + 2]);
                    }
                }
                //build data for Barrys Burgers
                if (plot_data[0] == "Barrys Burgers")
                {
                    for (int j = 0; j < 12; j++)
                    {
                        plotLines_bb[j] = double.Parse(plot_data[j + 2]);
                    }
                }
                //need to add other companies plot data here in assignment 2
            }
        }
        // add some chart labels for Months
        string[] months = new string[12] { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
"Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        CustomLabel customlabel;
        Axis axisX = chart2.ChartAreas[0].AxisX;
        double axelLabelPos = 0.5;
        for (int i = 0; i < 12; i++)
        {
            customlabel = axisX.CustomLabels.Add(axelLabelPos, axelLabelPos + 1, months[i]);
            axelLabelPos = axelLabelPos + 1.0;
        }
        // add lines to chart
        chart2.Series["Funky Chicken"].Points.DataBindY(plotLines_fc);
        chart2.Series["Barrys Burgers"].Points.DataBindY(plotLines_bb);
    }

    private void button3_Click(object sender, EventArgs e)
    {
        string[] titles = new string[5] { "Funky Chicken", "Barrys Burgers", "", "", "" };
        double[] year_totals = new double[5];
        CustomLabel customlabel;
        Axis axisX = chart1.ChartAreas[0].AxisX;
        double axelLabelPos = 0.5;
        for (int i = 0; i < 2; i++)
        {
            customlabel = axisX.CustomLabels.Add(axelLabelPos, axelLabelPos + 1, titles[i]);
            axelLabelPos = axelLabelPos + 1.0;
        }
        //calculate year year_totals here
        for (int j = 0; j < 12; j++)
        {
            year_totals[0] = year_totals[0] + plotLines_fc[j];
            year_totals[1] = year_totals[1] + plotLines_bb[j];
            // need to add other years for the assignment
        }
        //Used for debugging : MessageBox.Show(year_totals[1] + "fc year total");
        chart1.Series["Series1"].Points.DataBindY(year_totals);
    }

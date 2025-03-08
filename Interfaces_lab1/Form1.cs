using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Diagnostics;
using System.Drawing.Configuration;

namespace Interfaces_lab1
{
    public partial class Form1 : Form
    {
        private int[] rad = { 0, 20, 40, 60, 100, 150, 200, 250, 300, 350};
        private int[] sizes = {8, 10, 12, 15, 20, 30, 50, 70, 100};
        private int count_rad = -1;
        private int count_size = -1;
        private int count_attempts = 0;
        private float sum_time = 0f;
        private Random random = new Random();
        private Stopwatch timer = new Stopwatch();
        private float record_time = 0f;
        private bool check_cursor = false;
        private bool cursor_moved = false;
        private System.Drawing.Point current_cursor_position;
        private int exp_number = 0;

        public Form1()
        {
            InitializeComponent();
            this.MouseMove += Form1_MouseMove;        
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (check_cursor)
            {
                if (Cursor.Position != current_cursor_position)
                {
                    cursor_moved = true;
                    check_cursor = false;

                    timer.Reset();
                    timer.Start();
                }
            }
        }

        /// <summary>
        /// Makes the test button elliptical for the 1st experiment (unchengeable size)
        /// </summary>
        /// <param name="button">The test button</param>
        private void MakeButtonRound_test1(Button button)
        {
            button.Width = 9;
            button.Height = 18;

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, button.Width, button.Height);

            button.Region = new Region(path);
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.Yellow;
        }

        /// <summary>
        /// Spawns the test button
        /// </summary>
        /// <param name="button">The test button</param>
        private void Spawn_buttonMain(Button button)
        {
            switch(exp_number)
            {
                case 1:
                    {
                        double angle = random.NextDouble() * (Math.PI / 2);
                        int newX = (int)(rad[count_rad] * Math.Cos(angle)); 
                        int newY = (int)(rad[count_rad] * Math.Sin(angle)); 
                        buttonMain.Location = new Point(newX, newY);

                        break;
                    }
                case 2:
                    {
                        double angle = random.NextDouble() * (Math.PI / 2);
                        int newX = (int)(500 * Math.Cos(angle)); 
                        int newY = (int)(500 * Math.Sin(angle));

                        buttonMain.Width = sizes[count_size];
                        buttonMain.Height = 2 * buttonMain.Width;
                        buttonMain.Location = new Point(newX, newY);

                        break;
                    }
                case 3:
                    {
                        double angle = random.NextDouble() * (Math.PI / 2);
                        int newX = (int)(rad[count_rad] * Math.Cos(angle));
                        int newY = (int)(rad[count_rad] * Math.Sin(angle));

                        buttonMain.Width = sizes[count_size];
                        buttonMain.Height = 2 * buttonMain.Width;
                        buttonMain.Location = new Point(newX, newY);

                        break;
                    }
            }
        }

        private void buttonTest1_Click(object sender, EventArgs e)
        {
            exp_number = 1;

            buttonTest1.Visible = false;
            buttonTest2.Visible = true;
            buttonTest3.Visible = false;
            buttonStart.Visible = true;
            labelAvg_time.Visible = true;

            buttonStart.Focus();

            buttonMain.Location = new Point(0, 0);          
        }

        private void buttonStartClick(object sender, EventArgs e)
        {
            Cursor.Position = this.PointToScreen(new System.Drawing.Point(0, 0));
            current_cursor_position = Cursor.Position;
            buttonMain.Visible = true;
            buttonStart.Text = "Next step";
            
            switch(exp_number)
            {
                case 1:
                    {
                        if (count_rad != rad.Length-1) count_rad++;
                        else count_rad = 0;

                        check_cursor = true;
                        MakeButtonRound_test1(buttonMain);
                        Spawn_buttonMain(buttonMain);

                        break;
                    }
                case 2:
                    {
                        if (count_size != sizes.Length-1) count_size++;
                        else count_size = 0;

                        check_cursor = true;
                        MakeButtonRound_test2(buttonMain);
                        Spawn_buttonMain(buttonMain);

                        break;
                    }
                case 3:
                    {
                        {
                            if (count_rad + 1 != rad.Length)
                            {
                                count_rad++;
                                if (count_size < 0) count_size++;
                            } 
                            else
                            {
                                count_rad = 0;
                                if (count_size + 1 != sizes.Length) count_size++;
                                else count_size = 0;
                            }
                            MakeButtonRound_test2(buttonMain);
                            Spawn_buttonMain(buttonMain);

                            break;
                        }
                    }
                default: break;
            }
            count_attempts = 0;
        }

        private void buttonMain_Click(object sender, EventArgs e)
        {
            timer.Stop();

            record_time = timer.ElapsedMilliseconds;
            sum_time += record_time;

            if(exp_number == 1 || exp_number == 2)
            {
                if (count_attempts != 4)
                {
                    count_attempts++;
                    labelAvg_time.Text = "Время: " + (record_time);
                    check_cursor = true;
                    cursor_moved = false;
                }
                else
                {
                    count_attempts = 0;
                    float avg_time = sum_time / 5;

                    if(exp_number == 1)
                        WriteToFile(rad[count_rad], buttonMain.Width, avg_time);
                    else
                        WriteToFile(500, buttonMain.Width, avg_time);


                    labelAvg_time.Text = "Среднее время за 5 попыток: " + avg_time;
                    sum_time = 0f;
                }
            }
            else
            {
                if (count_attempts != 2)
                {
                    count_attempts++;
                    labelAvg_time.Text = "Время: " + (record_time);
                    check_cursor = true;
                    cursor_moved = false;
                }
                else
                {
                    count_attempts = 0;
                    float avg_time = sum_time / 3;

                    WriteToFile(rad[count_rad], buttonMain.Width, avg_time);

                    labelAvg_time.Text = "Среднее время за 3 попытки: " + avg_time;
                    sum_time = 0f;
                }
            }
            timer.Reset();
            record_time = 0f;
            Cursor.Position = this.PointToScreen(new System.Drawing.Point(0, 0));
            
            Spawn_buttonMain(buttonMain);
            
            buttonStart.Focus();         
        }

        private void buttonTest2_Click(object sender, EventArgs e)
        {
            exp_number = 2;

            buttonTest1.Visible = false;
            buttonTest2.Visible = false;
            buttonTest3.Visible = true;
            buttonStart.Text = "Start";
            buttonStart.Visible = true;
            labelAvg_time.Visible = true;

            buttonStart.Focus();

            buttonMain.Location = new Point(500, 0);
        }

        private void buttonTest3_Click(object sender, EventArgs e)
        {
            exp_number = 3;

            buttonTest1.Visible = false;
            buttonTest2.Visible = false;
            buttonTest3.Visible = false;
            buttonStart.Text = "Start";
            buttonStart.Visible = true;
            labelAvg_time.Visible = true;

            buttonStart.Focus();

            buttonMain.Location = new Point(0, 0);
        }

        /// <summary>
        /// Makes the test button elliptical for the 2nd and 3rd experiment (changeable size)
        /// </summary>
        /// <param name="button">The test button</param>
        private void MakeButtonRound_test2(Button button)
        {
            if (count_size != sizes.Length)
                button.Width = sizes[count_size];
            else button.Width = sizes[0];

            button.Height = 2 * button.Width;

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, button.Width, button.Height);

            button.Region = new Region(path);
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.Yellow;
        }

        /// <summary>
        /// Writes results to txt and csv files
        /// </summary>
        /// <param name="radius">S parameter</param>
        /// <param name="width">D parameter</param>
        /// <param name="avg_time">AVG button click time</param>
        private void WriteToFile(int radius, int width, float avg_time)
        {
            string filename = "";
            switch(exp_number)
            {
                case 1:
                    filename = "experiment 1";

                    break;
                case 2:
                    filename = "experiment 2";

                    break;
                case 3:
                    filename = "experiment 3";

                    break;
            }
            try
            {
                StreamWriter writer = File.AppendText(filename + ".txt");
                writer.WriteLine($"S: {radius}, D: {width}, Average time: {avg_time}");
                writer.Close();

                using (StreamWriter csvwriter = new StreamWriter(filename + ".csv", true, System.Text.Encoding.UTF8))
                {
                    if(exp_number == 3)
                        csvwriter.WriteLine($"{radius};{width};{radius / width};{avg_time}");
                    else
                        csvwriter.WriteLine($"{radius};{width};{avg_time}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error occured when writing to a file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
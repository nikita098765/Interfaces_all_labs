﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;

namespace Interfaces_lab2
{
    public partial class Form1 : Form
    {
        private Stopwatch timer = new Stopwatch();
        private Random random = new Random();

        private int exp_number = 1;
        private int button_count = 2;
        private int buttonClicks_count = 0;
        private float sum_time = 0f;
        private float record_time = 0f;
        private List<Button> buttons;
        private bool add_button = false;
        private bool change_color = false;
        private bool change_font = false;
        private bool test_ended = false;
        private List<Color> colors;
        private int color_index = 0;
        private List<Font> fonts;
        private int font_index = 0;

        public Form1()
        {
            InitializeComponent();

            buttonStart.Focus();

            buttons = new List<Button>();
            colors = new List<Color>();
            fonts = new List<Font>();

            buttons.Add(button1);
            buttons.Add(button2);
            buttons.Add(button3);
            buttons.Add(button4);
            buttons.Add(button5);
            buttons.Add(button6);
            buttons.Add(button7);
            buttons.Add(button8);
            buttons.Add(button9);

            colors.Add(Color.White);
            colors.Add(Color.Green);
            colors.Add(Color.Blue);
            colors.Add(Color.Red);

            fonts.Add(new Font("Arial", 12, FontStyle.Bold));
            fonts.Add(new Font("Arial", 12, FontStyle.Italic));
            fonts.Add(new Font("Arial", 12, FontStyle.Underline));

        }

        /// <summary>
        /// Makes a new button visible
        /// </summary>
        private void MakeButtonVisible()
        {
            for(int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Visible == false)
                {
                    buttons[i].Visible = true;
                    button_count++;
                    break;
                }
            }
        }

        private void MakeButtonsInvisible()
        {
            for (int i = 2; i < buttons.Count; i++)
            {
                buttons[i].Visible = false;
            }
            button_count = 2;
        }

        /// <summary>
        /// Assigns random numbers from [1;*count of buttons*] to buttons
        /// </summary>
        private void AssignNumberToButton()
        {
            var numbers = Enumerable.Range(1, button_count).ToList();   //generating a list of numbers [1; button_count]
            var shuffled_numbers = numbers.OrderBy(x => random.Next()).ToList();    //shuffling numbers

            for(int i = 0; i < button_count; i++)
            {
                buttons[i].Text = shuffled_numbers[i].ToString();   //assigning numbers to buttons
            }
        }

        /// <summary>
        /// Writes result to csv file
        /// </summary>
        /// <param name="avg_time">AVG button click time</param>
        private void WriteToFile(float avg_time)
        {
            int count = button_count;
            string filename = "";

            switch(exp_number)
            {
                case 1:
                    filename = "experiment1";
                    break;
                case 2:
                    filename = "experiment2";
                    break;
                case 3:
                    filename = "experiment3";
                    break;
            }
            try
            {
                using (StreamWriter csvwriter = new StreamWriter(filename + ".csv", true, System.Text.Encoding.UTF8))
                {
                    switch(exp_number)
                    {
                        case 1:
                            csvwriter.WriteLine($"{count};{avg_time}");
                            break;
                        case 2:
                            csvwriter.WriteLine($"{count};{colors[color_index]};{avg_time}");
                            break;
                        case 3:
                            csvwriter.WriteLine($"{count};{fonts[font_index]};{avg_time}");
                            break;
                    }
                    
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error occured when writing to a file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Measures time spent pressing buttons and checks if a new button needs to be added
        /// </summary>
        private void TimeMeasure()
        {
            timer.Stop();
            record_time = timer.ElapsedMilliseconds;
            labelTime.Text = $"Time: {record_time}ms";
            sum_time += record_time;

            if (buttonClicks_count == 5)
            {
                if (exp_number == 1 & button_count == 9)
                    test_ended = true;
                if(exp_number == 2 & button_count == 9)
                {
                    change_color = true;
                    if (colors[color_index] == colors[colors.Count-1])
                    {
                        change_color = false;
                        test_ended = true;
                    }
                }
                else if(exp_number == 3 & button_count == 9)
                {
                    change_font = true;
                    if (fonts[font_index] == fonts[fonts.Count - 1])
                    {
                        change_font = false;
                        test_ended = true;
                    }
                }
                add_button = true;
                float avg_time = sum_time / 5;
                labelLastAvgTime.Text = $"Last AVG time: {avg_time}ms";
                WriteToFile(avg_time);
                sum_time = 0;
                buttonClicks_count = 0;    
            }
        }

        /// <summary>
        /// Calculates the Y coordinate for cursor positioning
        /// </summary>
        /// <returns>Y coordinate</returns>
        private int FindYForCursor()
        {
            int last_visible_index = 0;
            for(int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Visible == true) last_visible_index = i; //finding the last visible button
                else break;
            }

            int lower_y = buttons[last_visible_index].Location.Y + buttons[last_visible_index].Height;  //Y of the button's lower point
            int Y = buttons[0].Location.Y + ((lower_y - buttons[0].Location.Y) / 2);

            return Y;
        }

        private void ChangeFontColor()
        {
            for(int i = 0; i < button_count; i++)
            {
                buttons[i].ForeColor = Color.Yellow;
            }

            int button_index = random.Next(0, button_count);

            //ИСКЛЮЧЕНИЕ ПРИ ЗАВЕРШЕНИИ 2 ТЕСТА
            buttons[button_index].ForeColor = colors[color_index];
        }

        private void ChangeFontOutline()
        {
            for(int i = 0; i < button_count; i++)
            {
                buttons[i].Font = new Font("Arial", 12, FontStyle.Regular);
            }

            int button_index = random.Next(0, button_count);

            buttons[button_index].Font = fonts[font_index];
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if(test_ended)
            {
                switch (exp_number)
                {
                    case 1:
                        MessageBox.Show("Experiment 1 ended", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //buttonExp2.Visible = true;
                        break;
                    case 2:
                        MessageBox.Show("Experiment 2 ended", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //buttonExp2.Visible = true;
                        break;
                    case 3:
                        MessageBox.Show("Experiment 3 ended", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //buttonExp2.Visible = true;
                        break;
                }

            }

            if(add_button)
            {
                MakeButtonVisible();
                add_button = false;
            }

            if (exp_number == 1)
            {
                AssignNumberToButton();
                labelNumber.Text = random.Next(1, button_count + 1).ToString();
            }

            if(exp_number == 2)
                ChangeFontColor();
            if(change_color)
            {
                change_color = false;
                color_index++;
                MakeButtonsInvisible();
                ChangeFontColor();                 
            }

            if (exp_number == 3)
                ChangeFontOutline();
            if(change_font)
            {
                change_font = false;
                font_index++;
                MakeButtonsInvisible();
                ChangeFontOutline();
            }

            timer.Reset();
            int Y = FindYForCursor();
            Cursor.Position = this.PointToScreen(new System.Drawing.Point(451, Y));
            timer.Start();
        }

        private void buttonExp2_Click(object sender, EventArgs e)
        {
            exp_number = 2;

            MakeButtonsInvisible();

            labelNumber.Visible = false;

            buttonStart.Focus();
        }

        private void buttonExp3_Click(object sender, EventArgs e)
        {
            exp_number = 3;

            MakeButtonsInvisible();

            labelNumber.Visible = false;

            buttonStart.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button1.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if(button1.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button1.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button2.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if (button2.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button2.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button3.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if (button3.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button3.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button4.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if (button4.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button4.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button5.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if (button5.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button5.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button6.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if (button6.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button6.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button7.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if (button7.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button7.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button8.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if (button8.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button8.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            switch (exp_number)
            {
                case 1:
                    if (int.Parse(button9.Text.ToString()) == int.Parse(labelNumber.Text.ToString()))
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 2:
                    if (button9.ForeColor == colors[color_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;
                case 3:
                    if (button9.Font == fonts[font_index])
                    {
                        buttonClicks_count++;
                        TimeMeasure();
                    }
                    break;

            }

            buttonStart.Focus();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();

        //Integer values for addition
        int addend1;
        int addend2;
        //Integer values for subtraction.
        int minuend;
        int subtrahend;
        //Time remaining.
        int timeLeft;
        //Integer for multiplication.
        int multiplicand;
        int multiplier;
        //Integer for division.
        int dividend;
        int divisor;
        bool hardDifficulty = false;

        public Form1()
        {
            InitializeComponent();
            normalToolStripMenuItem.Checked = true;
            Restart.Hide();
        }
        //Starts quiz.
        public void StartQuiz()
        {
            if (!hardDifficulty)
            {
                //Addition problem.
                addend1 = randomizer.Next(1, 101);
                addend2 = randomizer.Next(1, 101);
                plusLeftLabel.Text = addend1.ToString();
                plusRightLabel.Text = addend2.ToString();

                //Changes the NumericUpDown control box to 0.
                sum.Value = 0;

                //subtraction problem.
                minuend = randomizer.Next(1, 51);
                subtrahend = randomizer.Next(1, minuend);
                minusLeftLabel.Text = minuend.ToString();
                minusRightLabel.Text = subtrahend.ToString();
                difference.Value = 0;

                //Multiplication problem.
                multiplicand = randomizer.Next(2, 11);
                multiplier = randomizer.Next(2, 11);
                timesLeftLabel.Text = multiplicand.ToString();
                timesRightLabel.Text = multiplier.ToString();
                product.Value = 0;

                //Division problem.
                divisor = randomizer.Next(2, 11);
                int temporaryQuotient = randomizer.Next(2, 11);
                dividend = divisor * temporaryQuotient;
                dividedLeftLabel.Text = dividend.ToString();
                dividedRightLabel.Text = divisor.ToString();
                quotient.Value = 0;

                //This will start the timer.            
                timeLeft = 30;
                progressBar1.Maximum = 30;
                timeLabel.Text = "30 seconds";
                timer1.Start();
            }
            else
            {
                //Addition problem.
                addend1 = randomizer.Next(1, 501);
                addend2 = randomizer.Next(1, 501);
                plusLeftLabel.Text = addend1.ToString();
                plusRightLabel.Text = addend2.ToString();

                //Changes the NumericUpDown control box to 0.
                sum.Value = 0;

                //subtraction problem.
                minuend = randomizer.Next(1, 201);
                subtrahend = randomizer.Next(1, minuend);
                minusLeftLabel.Text = minuend.ToString();
                minusRightLabel.Text = subtrahend.ToString();
                difference.Value = 0;

                //Multiplication problem.
                multiplicand = randomizer.Next(2, 22);
                multiplier = randomizer.Next(2, 22);
                timesLeftLabel.Text = multiplicand.ToString();
                timesRightLabel.Text = multiplier.ToString();
                product.Value = 0;

                //Division problem.
                divisor = randomizer.Next(2, 22);
                int temporaryQuotient = randomizer.Next(2, 22);
                dividend = divisor * temporaryQuotient;
                dividedLeftLabel.Text = dividend.ToString();
                dividedRightLabel.Text = divisor.ToString();
                quotient.Value = 0;

                //This will start the timer.            
                timeLeft = 60;
                progressBar1.Maximum = 60;
                timeLabel.Text = "60 seconds";
                timer1.Start();
            }

            progressBar1.Value = timeLeft;
            timeLabel.BackColor = Color.White;
        }
        private void lockReset()
        {
            Restart.Enabled = false;
            timer2.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //Start the quiz.
            StartQuiz();
            startButton.Enabled = false;
            Restart.Show();
            label9.Hide();
            label12.Show();
            
            sum.Enabled = true;
            difference.Enabled = true;
            product.Enabled = true;
            quotient.Enabled = true;

            lockReset();            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user got the answer right.
                timer1.Stop();
                label13.Show();
                label12.Hide();
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                //time -1
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
                //Makes the progress bar work               
                progressBar1.PerformStep();
                //when time is 5 or below the time box will turn red, once the time runs out the box returns to white.
                if (timeLeft <= 5)
                    timeLabel.BackColor = Color.Red;
                else
                    timeLabel.BackColor = Color.White;
            }
            else
            {
                //if the user runs out of time it will show a message.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                //shows the correct answers if the user runs out of time.
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                
                sum.Enabled = false;
                difference.Enabled = false;
                product.Enabled = false;
                quotient.Enabled = false;
            }
        }
        //checks the answers to see if the user is correct.
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                  && (minuend - subtrahend == difference.Value)
                  && (multiplicand * multiplier == product.Value)
                  && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }
        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
        //Hidden button to reveal all answers.
        private void hidden_Click(object sender, EventArgs e)
        {
            developerToolStripMenuItem.Visible = !developerToolStripMenuItem.Visible;         
        }
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hardDifficulty = false;
            normalToolStripMenuItem.Checked = !normalToolStripMenuItem.Checked;
            startButton_Click(sender, e);
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hardDifficulty = true;
            hardToolStripMenuItem.Checked = !hardToolStripMenuItem.Checked;
            startButton_Click(sender, e);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //starts quiz using menu bar.
            StartQuiz();
            startToolStripMenuItem.Enabled = false;
        }
        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //string url = "www.google.co.uk";
            //Process.Start(url);
            MessageBox.Show("Created by Lewis Cook ©2017");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Changes the answer boxes to 0 and restarts the quiz.
            sum.Value = 0;
            difference.Value = 0;
            product.Value = 0;
            quotient.Value = 0;

            StartQuiz();
            lockReset();
            label13.Hide();
            label12.Show();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            Restart.Enabled = true;
            timer2.Stop();
        }
        private void answerAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (addend1 != 0 || minuend != 0 || multiplicand != 0 || dividend != 0)
            {
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
            }
        }
        private void forceRestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartQuiz();
            label13.Hide();
            label12.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
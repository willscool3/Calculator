using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        #region Private Bits

        decimal previousNumber = 0;
        bool hasCalc = false;
        bool hasInput = false;

        enum Modifiers { Addition, Subtraction, Multiply, Divide };

        Modifiers CurrentModifier = Modifiers.Addition;

        #endregion

        #region Constructor & loaders

        public Calculator()
        {
            InitializeComponent();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Misc Buttons

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_actionsPerformed.Clear();
            txt_numberDisplay.Text = "";
            hasCalc = false;
            hasInput = false;
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            int len = txt_numberDisplay.Text.Length;
            if (len == 0)
            {
                return;
            }
            txt_numberDisplay.Text = txt_numberDisplay.Text.Substring(0, len - 1);

            int lenActions = txt_actionsPerformed.Text.Length;
            if (lenActions == 0)
            {
                return;
            }
            txt_actionsPerformed.Text = txt_actionsPerformed.Text.Substring(0, lenActions - 1); 
        }

        private void btn_Equals_Click(object sender, EventArgs e)
        {
            txt_actionsPerformed.AppendText(txt_numberDisplay.Text);
            txt_actionsPerformed.AppendText("=");
            DoCalc();
            hasCalc = true;
        }

        #endregion

        #region Modifiers

        private void btn_Plus_Click(object sender, EventArgs e)
        {
            txt_Modifier.Text = "+";
            CurrentModifier = Modifiers.Addition;
            DoCalc();
            txt_actionsPerformed.AppendText("+");
            txt_numberDisplay.Text = "";
        }

        private void btn_Minus_Click(object sender, EventArgs e)
        {
            txt_Modifier.Text = "-";
            CurrentModifier = Modifiers.Subtraction;

            DoCalc();

            txt_numberDisplay.Text = "";
        }

        private void btn_Times_Click(object sender, EventArgs e)
        {
            txt_Modifier.Text = "*";
            txt_actionsPerformed.AppendText("*");
            CurrentModifier = Modifiers.Multiply;

            DoCalc();

            txt_numberDisplay.Text = "";
        }

        private void btn_Divide_Click(object sender, EventArgs e)
        {
            txt_Modifier.Text = "/";
            txt_actionsPerformed.AppendText("/");
            CurrentModifier = Modifiers.Divide;
            DoCalc();
            txt_numberDisplay.Text = "";
        }

        #endregion

        #region Helpers

        private void DoCalc()
        {
            decimal newNumber = 0;

            if (decimal.TryParse(txt_numberDisplay.Text, out newNumber) == false)
            {
                MessageBox.Show("bad numbers!");
            }

            if (hasInput == false)
            {
                previousNumber = newNumber;
            }
            else
            {
                switch (CurrentModifier)
                {
                    case Modifiers.Addition:
                        {
                            if (!hasCalc == true)
                            {
                                previousNumber += newNumber;
                            }
                        }; break;
                    case Modifiers.Subtraction:
                        {
                            if (!hasCalc == true)
                            {
                                previousNumber -= newNumber;
                            }
                        }; break;
                    case Modifiers.Multiply:
                        {
                            if (!hasCalc == true)
                            {
                                previousNumber *= newNumber;
                            }
                        }; break;
                    case Modifiers.Divide:
                        {
                            if (!hasCalc == true)
                            {
                                previousNumber /= newNumber;
                            }
                        }; break;
                }
            }

            txt_numberDisplay.Text = previousNumber.ToString();  
            txt_actionsPerformed.AppendText(previousNumber.ToString());
            hasInput = true;
        }

        #endregion

        #region NumberButtons and decimal point
        private void NumberButton_Clicked(object sender, EventArgs e)
        {
            var but = sender as Button;

            if (but.Text == ".")
            {
                if (txt_numberDisplay.Text.Contains('.')) return;
            }

            if (hasCalc == false)
            {
                txt_numberDisplay.Text = txt_numberDisplay.Text.Insert(txt_numberDisplay.SelectionStart, but.Text);
            }
            else
            {
                txt_actionsPerformed.Text = "";
                txt_numberDisplay.Text = txt_numberDisplay.Text.Insert(txt_numberDisplay.SelectionStart, but.Text);
                hasCalc = false;
            }
            
            txt_numberDisplay.SelectionStart = txt_numberDisplay.Text.Length;
        }
        #endregion

        private void txt_numberDisplay_TextChanged(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpLikeCalc
{
    public enum Operation
    {
        None,
        Addition,
        Subtraction,
        Division,
        Multiplication
    }
    public partial class Calculator : Form
    {
        private string _firstValue;
        private Operation _currentOperation = Operation.None;
        private string _secondValue;
        private bool _isTheResultOnTheScreen = false;
        
        public Calculator()
        {
            InitializeComponent();
            tbScreen.Text = "0";
        }

        

        private void tbScreen_TextChanged(object sender, EventArgs e)
        {

        }

        private void OnBtnNumberClick(object sender, EventArgs e)
        {
            var clickedValue = (sender as Button).Text;

            if (tbScreen.Text == "0" && clickedValue != ",")
                tbScreen.Text = String.Empty;

            if (_isTheResultOnTheScreen)
            {
                _isTheResultOnTheScreen = false;
                tbScreen.Text = string.Empty;

                if (clickedValue == ",")
                    clickedValue = "0,";
            }

            tbScreen.Text += clickedValue;
            SetResultBtnState(true);


            if (_currentOperation != Operation.None)            
                _secondValue += clickedValue;
            else           
                SetOperationBtnState(true);

            if (tbScreen.Text.Length == 10)
                SetNumbersBtnState(false);

        }

        private void OnBtnOperationClick(object sender, EventArgs e)
        {
            _firstValue = tbScreen.Text;
            var operation = (sender as Button).Text;

            _currentOperation = operation switch
            {
                "+" => Operation.Addition,
                "-" => Operation.Subtraction,
                "x" => Operation.Multiplication,
                "/" => Operation.Division,
                _ => Operation.None,
            };

            tbScreen.Text += $"{operation}";

            if (_isTheResultOnTheScreen)
                _isTheResultOnTheScreen = false;
            tbScreen.Text = "0";
            SetNumbersBtnState(true);
            SetOperationBtnState(false);
            SetResultBtnState(false);
        }

        private void OnBtnClearClick(object sender, EventArgs e)
        {
            tbScreen.Text = "0";
            _firstValue = string.Empty;
            _secondValue = string.Empty;
            _currentOperation = Operation.None;
        }

        private void OnBtnResultClick(object sender, EventArgs e)
        {
            if (_currentOperation == Operation.None)
                return;

            var firstNumber = double.Parse(_firstValue);
            var secondNumber = double.Parse(_secondValue);

            var result = Calculate(firstNumber, secondNumber);

            tbScreen.Text = result.ToString();
            _secondValue = string.Empty;
            _currentOperation = Operation.None;
            _isTheResultOnTheScreen = true;
            SetOperationBtnState(true);
            SetResultBtnState(true);
            SetNumbersBtnState(true);
        }
        private double Calculate(double firstNumber, double secondNumber)
        {
            switch (_currentOperation)
            {
                case Operation.None:
                    return firstNumber;
                    
                case Operation.Addition:
                    return  firstNumber + secondNumber;
                    
                case Operation.Subtraction:
                    return firstNumber - secondNumber;
                    
                case Operation.Division:
                    if (secondNumber == 0)
                    {
                        MessageBox.Show("Nie można dzielić przez zero!");
                        return  0;
                    }

                    return firstNumber / secondNumber;
                    
                case Operation.Multiplication:
                    return firstNumber * secondNumber;                   
            }
            return 0;
        }
        private void SetOperationBtnState(bool value)
        {            
            btnAdd.Enabled = value;
            btnMultiply.Enabled = value;
            btnDivision.Enabled = value;
            btnSubstract.Enabled = value;
        }
        private void SetResultBtnState(bool value)
        {
            btnEquals.Enabled = value;            
        }
        private void SetNumbersBtnState(bool value)
        {
            btn0.Enabled = value;
            btn1.Enabled = value;
            btn2.Enabled = value;
            btn3.Enabled = value;
            btn4.Enabled = value;
            btn5.Enabled = value;
            btn6.Enabled = value;
            btn7.Enabled = value;
            btn8.Enabled = value;
            btn9.Enabled = value;
        }

    }
}

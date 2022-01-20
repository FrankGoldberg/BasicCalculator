using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCalculator
{
    /// <summary>
    /// A Basic Calculator
    /// </summary>
    public partial class Form1 : Form
    {
        
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        #endregion
        private void ButtonsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        #region Clearing Methods
        /// <summary>
        /// Clears the user input text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CEButton_Click(object sender, EventArgs e)
        {
            // Clears the box from the user input text box
            this.UserInputText.Text = string.Empty;

            FocusInputText();
        }

        private void CButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void DelButton_Click(object sender, EventArgs e)
        {
            DeleteTextValue();

            //Focus user input text
            FocusInputText();
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("/");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void SevenButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("7");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void EightButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("8");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void NineButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("9");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void TimesButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("*");
            //Focuses the user input text
            this.UserInputText.Focus();

        }

        private void FourButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("4");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void FiveButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("5");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void SixButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("6");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("-");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void OneButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("1");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void TwoButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("2");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void ThreeButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("3");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("+");
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            CalculateEquation();
        }
        /// <summary>
        /// Calculates the given equation and outputs the answer to the user label
        /// </summary>
        private void CalculateEquation()
        {
            
            //Focuses the user input text
            this.UserInputText.Focus();

            this.CalculatorResultText.Text = ParseOperation();

        }

        /// <summary>
        /// Parses the user equation and calculates the result
        /// </summary>
        /// <returns></returns>
        private string ParseOperation()
        {
            try
            {
                //Get the users equation input
                var input = this.UserInputText.Text;

                //Remove all the spaces
                input = input.Replace(" ", "");

                //Create a top lvl operation
                var operation = new Operation();
                var leftSide = true;

                //Loop through each character of the input
                //Starting from the left work to the right
                for (int i = 0; i < input.Length; i++)
                {
                    //TODO Handle order priority
                    // 4 + 5 * 3
                    // it should calculate first 5 * 3 then, 4 + the result, so 4 + 15
                    // Check if the current character is a number
                    var myString = "0123456789.";
                    if (myString.Any(c => input[i] == c))
                    {
                        if (leftSide)
                            operation.LeftSide = AddNumberPart(operation.LeftSide, input[i]);
                        else
                            operation.RightSide = AddNumberPart(operation.RightSide, input[i]);
                    }
                    // If it is an operator + - * / set operator type
                    else if ("+-*/.".Any(c => input[i] == c))
                    {
                        if (!leftSide)
                        {
                            //Get the operator type
                            var operatorType = GetOperationType(input[i]);

                            //Check if have actual number
                            if (operation.RightSide.Length == 0)
                            {
                                //Check the operator is not a minus ( as they could be creating a negative number)
                                if (operatorType != OperationType.Minus)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified withoutan left side number");

                                //If we got here, the operator type is a minus, and there is no right number currently, so add the minus to the number
                                operation.RightSide += input[i];
                            }
                            else
                            {
                                //calculate previous equation and set to the left side
                                operation.LeftSide = CalcualteOperation(operation);

                                //Set new operator
                                operation.OperationType = operatorType;

                                //Clear the previous right number
                                operation.RightSide = string.Empty;

                            }
                        }
                        else
                        {
                            //Get the operator type
                            var operatorType = GetOperationType(input[i]);

                            //Check if have actual number
                            if (operation.LeftSide.Length == 0)
                            {
                                //Check the operator is not a minus ( as they could be creating a negative number)
                                if (operatorType != OperationType.Minus)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified withoutan left side number");

                                //If we got here, the operator type is a minus, and there is no left number currently, so add the minus to the number
                                operation.LeftSide += input[i];
                            }
                            else
                            {
                                // if we get here, we have a left number and now an operator, so we want to move to the right side

                                // Set the operation type
                                operation.OperationType = operatorType;

                                // Move to the right side
                                leftSide = false;

                            }
                        }
                    }
                 }

                //if we are done parsing, that were no exceptions 
                //Calculate the current operation
                return CalcualteOperation(operation);

            }
            catch (Exception ex)
            {
                return $"Invalid Equation. {ex.Message}";
            }
            
        }
        /// <summary>
        /// Calcualtes an <see cref="Operation"/> and retuns the result
        /// </summary>
        /// <param name="operation">The operation to calcualte</param>
        private string CalcualteOperation(Operation operation)
        {
            // Store the values of the string representation
            double left = 0;
            double right = 0;

            //check if we have a valid left side number
            if (string.IsNullOrEmpty(operation.LeftSide) || !double.TryParse(operation.LeftSide, out left))
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.LeftSide}");
            

            //check if we have a valid right side number
            if (string.IsNullOrEmpty(operation.RightSide) || !double.TryParse(operation.RightSide, out right))
                throw new InvalidOperationException($"Right side of the operation was not a number. {operation.RightSide}");
            try
            {
                switch (operation.OperationType)
                {
                    case OperationType.Add:
                        return (left + right).ToString();
                    case OperationType.Minus:
                        return (left - right).ToString();
                    case OperationType.Divide:
                        return (left / right).ToString();
                    case OperationType.Multiply:
                        return (left * right).ToString();
                    default:
                        throw new InvalidOperationException($"Unknown operation type when calculating operation. {operation.OperationType}");
                }
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Failed to calculate operation {operation.LeftSide} {operation.OperationType} {operation.RightSide}. {ex.Message}");
            }

        }

        /// <summary>
        /// Attampts to add the character to the current number, checking for valid characters as it goes
        /// </summary>
        /// <param name="currentNumber">the current number string</param>
        /// <param name="currentCharacter">the new character to append to the string</param>
        /// <returns></returns>
        private string AddNumberPart(string currentNumber, char newCharacter)
        {
            //Checking if there is already a . in the number
            if (newCharacter == '.' && currentNumber.Contains('.'))
                throw new InvalidOperationException($"Number{currentNumber}already contains a .and another cannot be added");

            return currentNumber + newCharacter;
        }

        private void DotButton_Click(object sender, EventArgs e)
        {
            InsertTextValue(".");
            //Focuses the user input text
            this.UserInputText.Focus();
        }

        private void ZeroButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("0");
            //Focuses the user input text
            this.UserInputText.Focus();
        }


        private void button16_Click(object sender, EventArgs e)
        {

        }

        #region Private Helpers
        /// <summary>
        /// Focuses the user input text
        /// </summary>
        private void FocusInputText()
        {
            this.UserInputText.Focus();
        }
        /// <summary>
        /// Deletes the character to the right of the selection start of the user input text box
        /// </summary>


        private void InsertTextValue(string value)
        {
            var selectionStart = this.UserInputText.SelectionStart;
            this.UserInputText.Text = UserInputText.Text.Insert(this.UserInputText.SelectionStart, value);
            this.UserInputText.SelectionStart = selectionStart + value.Length;
            this.UserInputText.SelectionLength = 0;
        }

        private void DeleteTextValue()
        {
            //if we dont have a value to delete, return. 
            // if (this.UserInputText.Text.Length <= this.UserInputText.SelectionStart + 1) 
            //  return;
            if (UserInputText.SelectionStart == 0)
                return;
            //Remember Selection Start
            var selectionStart = this.UserInputText.SelectionStart;
            
            this.UserInputText.Text = this.UserInputText.Text.Remove(this.UserInputText.SelectionStart -1, 1);
            //Restore the selection start
            this.UserInputText.SelectionStart = selectionStart;
            //Set selection length to zero
            this.UserInputText.SelectionLength = 0;

        }
        /// <summary>
        /// Accepts a character and returns the known <see cref="OperationType"/>
        /// </summary>
        /// <param name="character">The character to parse</param>
        /// <returns></returns>
        private OperationType GetOperationType (char character)
        {
            switch (character)
            {
                case '+':
                    return OperationType.Add;
                case '-':
                    return OperationType.Minus;
                case '/':
                    return OperationType.Divide;
                case '*':
                    return OperationType.Multiply;
                default:
                    throw new InvalidOperationException($"Unknow operator type { character } ");
            }
            
        }


        #endregion

        
    }
}

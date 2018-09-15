////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Values2.cs
//
// summary:	Implements the values 2 class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to set values for the equations. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace NetworkArithmeticGame
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The values 2. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class Values2
    {
        /// <summary>   The first number. </summary>
        public int FirstNumber;
        /// <summary>   The operator. </summary>
        public string Operator;
        /// <summary>   The second number. </summary>
        public int SecondNumber;
        /// <summary>   The answer. </summary>
        public int Answer;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="firstNumber">  The first number. </param>
        /// <param name="secondNumber"> The second number. </param>
        /// <param name="operation">    The operation. </param>
        /// <param name="answer">       The answer. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Values2(int firstNumber, int secondNumber, string operation, int answer)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
            Operator = operation;
            Answer = answer;
        }
    }
}

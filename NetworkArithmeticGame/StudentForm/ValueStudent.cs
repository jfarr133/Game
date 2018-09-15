using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to set values for the equations to be used by the student. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace StudentForm
{
    [Serializable]
    class ValueStudent
    {
        [JsonProperty("firstNumber")]
        public ushort FirstNumber { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("secondNumber")]
        public ushort SecondNumber { get; set; }

        [JsonProperty("answer")]
        public ushort Answer { get; set; }

        [JsonProperty("answerCorrect")]
        public bool AnswerCorrect { get; set; }

        public ValueStudent() { }

        public ValueStudent(ushort firstNumber, ushort secondNumber, string operation, ushort answer, bool answerCorrect)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
            Operation = operation;
            Answer = answer;
            AnswerCorrect = answerCorrect;
        }
    }
}

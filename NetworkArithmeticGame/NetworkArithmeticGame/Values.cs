using System;
using Newtonsoft.Json;
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
    [Serializable]
    class Values
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

        public Values() { }

        public Values(ushort firstNumber, ushort secondNumber, string operation, ushort answer, bool answerCorrect)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
            Operation = operation;
            Answer = answer;
            AnswerCorrect = answerCorrect;
        }

        public Values(byte[] data)
        {
            FirstNumber = BitConverter.ToUInt16(data, 0);
            int symbolLength = BitConverter.ToInt32(data, 3);
            Operation = Encoding.ASCII.GetString(data, 4, symbolLength);
            SecondNumber = BitConverter.ToUInt16(data, 1);
            Answer = BitConverter.ToUInt16(data, 2);
            AnswerCorrect = BitConverter.ToBoolean(data, 5);
        }

        public byte[] convertToByteArray()
        {
            List<byte> byteList = new List<byte>();
            byteList.AddRange(BitConverter.GetBytes(FirstNumber));
            byteList.AddRange(BitConverter.GetBytes(Operation.Length));
            byteList.AddRange(BitConverter.GetBytes(SecondNumber));
            byteList.AddRange(BitConverter.GetBytes(Answer));
            byteList.AddRange(BitConverter.GetBytes(AnswerCorrect));
            return byteList.ToArray();
        }
    }
}

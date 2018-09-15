using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArithmeticGame
{
    class LinkListNode
    {
        Values numberValue;

        public LinkListNode previous;
        public LinkListNode next;

        public LinkListNode(Values value)
        {
            this.numberValue = value;
        }

        public Values GetMyValue()
        {
            return this.numberValue;
        }
        public void SetMyValue(Values value)
        {
            this.numberValue = value;
        }

        public void SetNext(LinkListNode aNode)
        {
            this.next = aNode;
        }

        public LinkListNode getNext()
        {
            return this.next;
        }

        public void SetPrevious(LinkListNode aNode)
        {
            this.previous = aNode;
        }

        public LinkListNode GetPrevious()
        {
            return this.previous;
        }

        public string NodeToString()
        {
            return numberValue.FirstNumber.ToString() + numberValue.Operation 
                + numberValue.SecondNumber + "=" + numberValue.Answer;
        }
    }
}

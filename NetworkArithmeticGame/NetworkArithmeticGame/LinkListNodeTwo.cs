using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArithmeticGame
{
    class LinkListNodeTwo
    {
        public LinkListNode HeadNode;
        public LinkListNode CurrentNode;
        public LinkListNode TailNode;

        public static int count = 0;

        public LinkListNodeTwo() { }

        public LinkListNodeTwo(LinkListNode node)
        {
            HeadNode = node;
            CurrentNode = node;
            TailNode = node;
            count++;
        }

        public LinkListNode getHeadNode() { return HeadNode; }
        public LinkListNode getCurrentNode() { return CurrentNode; }
        public LinkListNode getTailNode() { return TailNode; }

        public void setCurrentNode(LinkListNode node) { CurrentNode = node; }
        public void setHeadNode(LinkListNode node) { HeadNode = node; }
        public void setTailNode(LinkListNode node) { TailNode = node; }

        public void AddValuesNode(LinkListNode node)
        {
            if ((HeadNode == null) && (CurrentNode == null) && (TailNode == null))
            {
                // this firstNode in the list
                HeadNode = node;
                CurrentNode = node;
                TailNode = node;
                count++;
            }
            else
            {
                CurrentNode = node;
                HeadNode.SetPrevious(node);
                CurrentNode.SetNext(HeadNode);
                setHeadNode(CurrentNode);
                count++;
            }
        }
    }
}

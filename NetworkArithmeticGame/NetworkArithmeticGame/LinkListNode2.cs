////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	LinkListNode2.cs
//
// summary:	Implements the link list node 2 class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to create and get the values for the linked list and binary search. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace NetworkArithmeticGame
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A link list node 2. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class LinkListNode2
    {
        /// <summary>   The current node. </summary>
        static LinkListNode CurrentNode = null;
        /// <summary>   The head node. </summary>
        static LinkListNode HeadNode = null;
        /// <summary>   The tail node. </summary>
        static LinkListNode TailNode = null;

        /// <summary>   Number of. </summary>
        static int count = 0;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public LinkListNode2()
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="node"> The node. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public LinkListNode2(LinkListNode node)
        {
            HeadNode = node;
            CurrentNode = node;
            TailNode = node;
            count++;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets head node. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <returns>   The head node. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public LinkListNode getHeadNode() { return HeadNode; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets current node. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <returns>   The current node. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public LinkListNode getCurrentNode() { return CurrentNode; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets tail node. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <returns>   The tail node. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public LinkListNode getTailNode() { return TailNode; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets current node. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="node"> The node. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void setCurrentNode(LinkListNode node) { CurrentNode = node; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets head node. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="node"> The node. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void setHeadNode(LinkListNode node) { HeadNode = node; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets tail node. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="node"> The node. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void setTailNode(LinkListNode node) { TailNode = node; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds the values node. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="node"> The node. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddValuesNode(LinkListNode node)
        {
            if ((HeadNode == null) && (CurrentNode == null) && (TailNode == null))
            {
                HeadNode = node;
                CurrentNode = node;
                TailNode = node;
                count++;
            }
            else
            {
                CurrentNode = node;
                HeadNode.setPrevious(node);
                CurrentNode.setNext(HeadNode);
                setHeadNode(CurrentNode);
                count++;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Binary search. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="searchValue">  The search value. </param>
        ///
        /// <returns>   An int. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int binarySearch(int searchValue)
        {
            this.SortList();
            LinkListNode current = HeadNode;
            ArrayList myTempList = new ArrayList();
            for (LinkListNode i = current; i != null; i = i.getNext())
            {
                myTempList.Add(i.getValue());
            }
            return myTempList.BinarySearch(searchValue);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sort list. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void SortList()
        {
            LinkListNode current = HeadNode;
            for (LinkListNode i = current; i.getNext() != null; i = i.getNext())
            {
                for (LinkListNode j = i.getNext(); j != null; j = j.getNext())
                {
                    if (i.getValue() > j.getValue())
                    {
                        int Temp = j.getValue();
                        j.setMyValue(i.getValue());
                        i.setMyValue(Temp);
                    }
                }
            }
        }
    }
}

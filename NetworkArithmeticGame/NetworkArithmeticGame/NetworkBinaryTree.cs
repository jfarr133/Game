////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	NetworkBinaryTree.cs
//
// summary:	Implements the network binary tree class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to get and print the values for the binary tree. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace NetworkArithmeticGame
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A network binary tree. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class NetworkBinaryTree
    {
        /// <summary>   The top. </summary>
        public NetworkBinaryTreeNode top;
        /// <summary>   The print string. </summary>
        private static string printStr = "";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Print pre order. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="tree"> The tree. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string printPreOrder(NetworkBinaryTree tree)
        {
            printStr = "";
            PreOrder(tree.top);
            return printStr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Pre order. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="Root"> The root. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void PreOrder(NetworkBinaryTreeNode Root)
        {
            if (Root == null)
            {
                return;
            }
            else
            {
                printStr += Root.NodeToString();
                PreOrder(Root.left);
                PreOrder(Root.right);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Print in order. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="tree"> The tree. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string printInOrder(NetworkBinaryTree tree)
        {
            printStr = "";
            InOrder(tree.top);
            return printStr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   In order. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="Root"> The root. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void InOrder(NetworkBinaryTreeNode Root)
        {
            if (Root == null)
            {
                return;
            }
            else
            {
                InOrder(Root.left);
                if (!printStr.Contains(Root.NodeToString()))
                {
                    printStr += Root.NodeToString();
                }
                InOrder(Root.right);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Print post order. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="tree"> The tree. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string printPostOrder(NetworkBinaryTree tree)
        {
            printStr = "";
            PostOrder(tree.top);
            return printStr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Posts an order. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="Root"> The root. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void PostOrder(NetworkBinaryTreeNode Root)
        {
            if (Root == null)
            {
                return;
            }
            PreOrder(Root.left);
            PreOrder(Root.right);
            printStr += Root.NodeToString();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="answerVal">    The answer value. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public NetworkBinaryTree(Values2 answerVal)
        {
            top = new NetworkBinaryTreeNode(answerVal);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public NetworkBinaryTree()
        {
            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds answerVal2. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="answerVal2">   The answer Value 2 to add. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Add(Values2 answerVal2)
        {
            if (top == null)
            {
                top = new NetworkBinaryTreeNode(answerVal2);
                return;
            }
            NetworkBinaryTreeNode currentNode = top;
            bool inserted = false;

            do
            {
                if (answerVal2.Answer < currentNode.getMath.Answer)
                {
                    if (currentNode.left == null)
                    {
                        currentNode.left = new NetworkBinaryTreeNode(answerVal2);
                        inserted = true;
                    }
                    else
                    {
                        currentNode = currentNode.left;
                    }

                    if (answerVal2.Answer >= currentNode.getMath.Answer)
                    {
                        if (currentNode.right == null)
                        {
                            currentNode.right = new NetworkBinaryTreeNode(answerVal2);
                            inserted = true;
                        }
                        else
                        {
                            currentNode = currentNode.right;
                        }
                    }
                }
            } while (!inserted);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Print tree. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="root"> The root. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string PrintTree(NetworkBinaryTreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(root.getMath.Answer.ToString());
            sb.Append("(");
            sb.Append(root.getMath.FirstNumber.ToString());
            sb.Append(root.getMath.Operator.ToString());
            sb.Append(root.getMath.SecondNumber.ToString());
            sb.Append(")");
            return sb.ToString();
        }
    }
}

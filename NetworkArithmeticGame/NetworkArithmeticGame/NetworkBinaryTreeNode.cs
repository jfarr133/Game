////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	NetworkBinaryTreeNode.cs
//
// summary:	Implements the network binary tree node class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to convert the binary tree to a string. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace NetworkArithmeticGame
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A network binary tree node. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class NetworkBinaryTreeNode
    {
        /// <summary>   The get mathematics. </summary>
        public Values2 getMath;
        /// <summary>   The left. </summary>
        public NetworkBinaryTreeNode left;
        /// <summary>   The right. </summary>
        public NetworkBinaryTreeNode right;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="val">  The value. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public NetworkBinaryTreeNode(Values2 val)
        {
            getMath = val;
            left = null;
            right = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Node to string. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string NodeToString()
        {
            return getMath.Answer.ToString() + "(" + getMath.FirstNumber.ToString() + getMath.Operator + getMath.SecondNumber.ToString() + "), ";
        }
    }
}

﻿/*
 * Samuel Develey and Thomas Rey
 * 22.11.2021
 * ETML
 * Code managing a stack
*/

using System;

namespace P_P_Algo_labyrinthe_ThomasRey_SamuelDeveley
{
    class Stack
    {
        // Properties
        private int[] _myStack;     // Create the stack
        private int _counter = 0;  // Look for where is the element in the stack

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Initelize the size of hte stack</param>
        public Stack(int size)
        {
            _myStack = new int[size];
        }

        /// <summary>
        /// Push a value in the stack
        /// </summary>
        /// <param name="value">Value to put in the stack</param>
        /// <returns></returns>
        public bool Push(int value)
        {
            if(_counter == _myStack.Length)
            {
                return false;
            }

            else
            {
                _myStack[_counter] = value;
                _counter++;
                return true;
            }
        }

        /// <summary>
        /// Show the top value of the stack
        /// </summary>
        /// <returns></returns>
        public int Top()
        {
            return _myStack[_counter - 1];
        }

        /// <summary>
        /// Check if the stack is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if(_counter == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Show all element of the stack
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string text = "";

            for(int i = 0; i != _myStack.Length; i++)
            {
                text += _myStack[_myStack.Length - (i + 1)];
            }
            return text;
        }
    }
}

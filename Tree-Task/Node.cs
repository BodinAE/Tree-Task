using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Task
{
    internal class Node
    {
        public int Data { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public int Height { get; set; }
        public int BalanceFactor { get; set; }

        public Node(int data, int height)
        {
            this.Data = data;
            this.Height = height;
        }
        public Node(int data)
        {
            this.Data = data;
            this.Height = 0;
        }

        public void FixBalance()
        {
            FixHeight();
            switch ( Left != null, Right != null)
            {
                case (false, false) :
                    BalanceFactor = 0;
                    break;
                case (false, true) :
                    BalanceFactor = 0 - Right.Height;
                    break;
                case (true, false) :
                    BalanceFactor = Left.Height;
                    break;
                case (true, true) :
                    BalanceFactor = Left.Height - Right.Height;
                    break;
            }

        }

        public int FixHeight()                              //Broken
        {
            int rightheight = 0, leftheight = 0;
            switch (Left != null, Right != null)
            {
                case (false, false):
                    Height = 0; break;
                case (false, true):
                    rightheight = Right.FixHeight(); break;
                case (true, false):
                    leftheight = Left.FixHeight(); break;
                case (true, true):
                    rightheight = Right.FixHeight();
                    leftheight = Left.FixHeight(); break;
            }

            if (rightheight > leftheight)
            {
                Height = rightheight + 1;
            }
            else
            {
                Height = leftheight + 1;
            }
            return Height;
        }

        public override string ToString()
        {
            return Convert.ToString(this.Data);
        }
    }
}

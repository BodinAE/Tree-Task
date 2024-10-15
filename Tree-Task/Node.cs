using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Task
{
    internal class Node
    {
        int Data { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        int Height { get; set; }
        int BalanceFactor { get; set; }

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
        public int Get()
        {
            return Data;
        }
        public void SetHeight(int height)
        {
            this.Height = height;
        }
        public int GetHeight()
        {
            return Height;
        }
        public int GetBalance()
        {
            return BalanceFactor;
        }
        public void FixBalance()
        {
            FixHeight();
            if (Left == null)
            {
                BalanceFactor = 0 - Right.GetHeight();
            }
            else if (Right == null)
            {
                BalanceFactor = Left.GetHeight();
            }
            else
            {
                BalanceFactor = Left.GetHeight() - Right.GetHeight();
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

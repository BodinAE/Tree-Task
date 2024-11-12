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

        //public Node(int data, int height)
        //{
        //    this.Data = data;
        //    this.Height = height;
        //}
        public Node(int data)
        {
            this.Data = data;
            this.Height = 0;
        }

        public void CheckBalance()
        {
            switch ( Left != null, Right != null)
            {
                case (false, false) :
                    BalanceFactor = 0;
                    break;
                case (false, true) :
                    BalanceFactor = Right.Height;
                    break;
                case (true, false) :
                    BalanceFactor = -Left.Height;
                    break;
                case (true, true) :
                    BalanceFactor = Right.Height - Left.Height;
                    break;
            }

        }

        public void CheckHeight()                              
        {
            switch (Left != null, Right != null)
            {
                case (false, false):
                    Height = 0;
                    break;
                case (false, true):
                    Height = Right.Height + 1;
                    break;
                case (true, false):
                    Height = Left.Height + 1;
                    break;
                case (true, true):
                    if (Left.Height > Right.Height)
                    {
                        Height = Left.Height + 1;
                    }
                    else
                    {
                        Height = Right.Height + 1;
                    }
                    break;
            }
        }

        public override string ToString()
        {
            return Convert.ToString(this.Data);
        }
    }
}

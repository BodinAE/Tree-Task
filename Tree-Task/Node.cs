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
        public void CheckBalance()
        {
            if (Left == null)
            {
                BalanceFactor = 0 - Right.GetBalance();
            }
            else if (Right == null)
            {
                BalanceFactor = Left.GetBalance();
            }
            else
            {
                BalanceFactor = Left.GetBalance() - Right.GetBalance();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Task
{
    internal class Tree
    {
        public Node? Head { get; set; }

        public Tree(int data)
        {
            this.Head = new Node(data);
        }

        public Tree() { }

        //public void Insert(int data)
        //{
        //    Node a = new Node(data);
        //    int a_height = 0;
        //    Node currnode = Head;
        //    while (currnode != null)
        //    {
        //        if (data > currnode.Get())
        //        {
        //            if (currnode.Right != null)
        //            {
        //                currnode = currnode.Right;
        //            }
        //            else
        //            {
        //                currnode.Right = a;
        //                currnode = null;
        //            }
        //        }
        //        else
        //        {
        //            if (currnode.Left != null)
        //            {
        //                currnode = currnode.Left;
        //            }
        //            else
        //            {
        //                currnode.Left = a;
        //                currnode = null;
        //            }
        //        }
        //    }
        //}

        public Node? Find(int data)                 //returns node with matching data,  or null node in apprpriate place if exact match not found
        {
            Node? currnode = this.Head;
            while (currnode != null)
            {
                if (currnode.Get() == data)
                {
                    break;
                }
                else if (currnode.Get() >= data)
                {
                    currnode = currnode.Right;
                }
                else
                {
                    currnode = currnode.Left;
                }
            }
            return currnode;
        }

        public void Insert(int data)
        {
            Node? p = Find(data);
            if (p == null)
            {
                p = new Node(data);
                Head.FixBalance();
            }
            else
            {
                Console.WriteLine("Node already exists");
            }
        }

        //public void Remove(int data) 
        //{
        //    Node? target = Find(data);
        //    if (target == null)
        //    {
        //        Console.WriteLine($"{data} does not exist");
        //    }
        //    else
        //    {

        //    }
        //}
        public Node Remove(Node? p, int targetNode)
        {
            if (p.Get() == targetNode)
            {
                Node temp;
                switch ((p.Left == null, p.Right == null))
                {
                    case (false, false):
                        return null;
                    case (true, false):
                        temp = p.Left;
                        p.Left = null;
                        return temp;
                    case (false, true):
                        temp = p.Right;
                        p.Right = null;
                        return temp;
                    case (true, true):
                        temp = FindMin(p.Right);
                        temp.Left = p.Left;
                        temp.Right = p.Right;
                        Remove(temp.Right, temp.Get());
                        return temp;
                }
            }
            else if (p.Get() < targetNode)
            {
                p.Left = Remove(p.Left, targetNode);
            }
            else
            {
                p.Right = Remove(p.Right, targetNode);
            }
            Head.FixBalance();
            return p;
        }

        public Node? FindMin(Node? p)       //Finds minimum node in tree with p node as root
        {
            if (p.Left != null)
            {
                return FindMin(p.Left);
            }
            return p;
        }

        private void RemoveMin(Node p)
        {
            if (p.Left == null) { return; }
            while (p.Left.Left != null)
            {
                p = p.Left;
            }
            p.Left = null;
        }

        public Node Balance(Node p)
        {
            switch (p.GetBalance())
            {
                case -2:
                    if (p.Left.GetBalance() > 0)
                    {
                        p.Left = LeftRotation(p.Left);
                    }
                    p = RightRotation(p.Right);
                    break;
                case 2:
                    if (p.Right.GetBalance() < 0)
                    {
                        p.Right = RightRotation(p.Right);
                    }
                    p = LeftRotation(p.Left);
                    break;
                default:
                    break;
            }
            Head.FixBalance();
            return p;
        }

        private Node RightRotation(Node p)
        {
            Node? L = p.Left;
            p.Left = L.Right;
            L.Right = p;
            p.FixBalance();
            return p;
        }

        private Node LeftRotation(Node p)
        {
            Node? R = p.Right;
            p.Right = R.Left;
            R.Left = p;
            p.FixBalance();
            return p;
        }

        public void Print()
        {
            string[] lines = new string[Head.GetHeight()];

        }
    }
}

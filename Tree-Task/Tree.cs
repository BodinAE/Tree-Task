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

        private Stack<Node> ChangedNodes { get; set; }

        public Tree(int data)
        {
            this.Head = new Node(data);
            ChangedNodes = new Stack<Node>();
        }

        public Tree() { }

        //public void Insert(int data)
        //{
        //    Node a = new Node(data);
        //    int a_height = 0;
        //    Node currnode = Head;
        //    while (currnode != null)
        //    {
        //        if (data > currnode.Data)
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
        public Node? Find(int data)
        {
            Node? currnode = this.Head;                             //if tree is empty returns Head  
            while (currnode != null)
            {
                if (currnode.Data == data)                        
                {
                    return currnode;                                //if matching node found, returns it
                }
                else
                {
                    if (currnode.Data < data)                     
                    {
                        currnode = currnode.Right;
                    }
                    else
                    {
                        currnode = currnode.Left;
                    }
                }
            }
            Console.WriteLine("Matching node not found");
            return currnode;                                        //returns null if matching node not found
        }

        private Node? InsFind(int data)                             //Find method used only for Insert
        {
            Node? currnode = this.Head;                             //if tree is empty returns Head  
            while (currnode != null)
            {
                if (currnode.Data == data)                         //if matching node found, returns it
                {
                    break;
                }                                                   
                else if (currnode.Data < data)                     //if matching node not found, returns node above the appropriate place to insert it
                {
                    if (currnode.Right == null) return currnode;
                    else currnode = currnode.Right;
                }
                else
                {   
                    if (currnode.Left == null) return currnode;
                    else currnode = currnode.Left;
                }
            }
            return currnode;
        }

        public void Insert (int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
            }
            else
            {
                InsertRec(data, Head);
            }
            ChangedNodes.Clear();
        }
        private void InsertRec(int data, Node? currnode)                                //adds a node to the tree in the right place and fixes balance and height
            //!!!
            //redo with recursion, add touched nodes to stack and then balance them
            //!!!
        {
            if (currnode == null)
            {
                currnode = new Node(data);
                if (data < ChangedNodes.Peek().Data)
                {
                    ChangedNodes.Peek().Left = currnode;
                }
                else
                {
                    ChangedNodes.Peek().Right = currnode;
                }
                return;
            }
            if (currnode.Data == data)
            {
                return;
            }
            if (data < currnode.Data)
            {
                InsertRec(data, currnode.Left);
            }
            else
            {
                InsertRec(data, currnode.Right);
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
        public Node Remove(Node? parentnode, int targetNode)                 //Removes node in parentnode tree through recursion, should be called parentnode = Remove(parentnode, targetnode)
        {
            if (parentnode.Data == targetNode)
            {
                Node temp;
                switch ((parentnode.Left != null, parentnode.Right != null))
                {
                    case (false, false):
                        return null;
                    case (true, false):
                        temp = parentnode.Left;
                        parentnode.Left = null;
                        Head.FixBalance();
                        //Balance(parentnode);
                        return temp;
                    case (false, true):
                        temp = parentnode.Right;
                        parentnode.Right = null;
                        Head.FixBalance();
                        //Balance(parentnode);
                        return temp;
                    case (true, true):
                        temp = FindMin(parentnode.Right);
                        RemoveMin(parentnode.Right);
                        parentnode.Right = Remove(parentnode.Right, temp.Data);
                        temp.Left = parentnode.Left;
                        temp.Right = parentnode.Right;
                        Head.FixBalance();
                        //Balance(parentnode);
                        return temp;
                }
            }
            else if (parentnode.Data > targetNode)
            {
                parentnode.Left = Remove(parentnode.Left, targetNode);
            }
            else
            {
                parentnode.Right = Remove(parentnode.Right, targetNode);
            }
            
            return parentnode;
        }

        public Node? FindMin(Node? p)       //Finds minimum node in tree with p node as root
        {
            if (p.Left != null)
            {
                return FindMin(p.Left);
            }
            return p;
        }

        private void RemoveMin(Node p)      //used for node removal
        {
            if (p.Left == null) 
            {
                p.Left = null;
                return; 
            }
            while (p.Left.Left != null)
            {
                p = p.Left;
            }
            p.Left = null;
        }

        public Node Balance(Node p)
        {
            switch (p.BalanceFactor)
            {
                case -2:
                    if (p.Left.BalanceFactor > 0)
                    {
                        p.Left = LeftRotation(p.Left);
                    }
                    p = RightRotation(p.Right);
                    break;
                case 2:
                    if (p.Right.BalanceFactor < 0)
                    {
                        p.Right = RightRotation(p.Right);
                    }
                    p = LeftRotation(p.Left);
                    break;
                default:
                    return p;
            }
            Head.FixBalance();
            return p;
        }

        public Node RightRotation(Node p)
        {
            Node? L = p.Left;
            p.Left = L.Right;
            L.Right = p;
            p.FixBalance();
            Balance(p);
            return p;
        }

        public Node LeftRotation(Node p)
        {
            Node? R = p.Right;
            p.Right = R.Left;
            R.Left = p;
            p.FixBalance();
            Balance(p);
            return p;
        }

        public static void Print(Node? p, int depth = 0)
        {
            //string[] lines = new string[Head.Height];
            if (p == null) return;
            Console.WriteLine($"{new String('.', depth)} {p.Data}");
            if (p.Left != null)
            {
                Console.Write("Left");
                Print(p.Left, depth + 1);
            }
            if (p.Right != null)
            {
                Console.Write("Rite");
                Print(p.Right, depth + 1);
            }
        }
    }
}

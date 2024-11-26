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

        private Stack<Node> TouchedNodes { get; set; }

        public Tree(int data)
        {
            this.Head = new Node(data);
            TouchedNodes = new Stack<Node>();
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

        //private Node? InsFind(int data)                             //Find method used only for Insert
        //{
        //    Node? currnode = this.Head;                             //if tree is empty returns Head  
        //    while (currnode != null)
        //    {
        //        if (currnode.Data == data)                         //if matching node found, returns it
        //        {
        //            break;
        //        }                                                   
        //        else if (currnode.Data < data)                     //if matching node not found, returns node above the appropriate place to insert it
        //        {
        //            if (currnode.Right == null) return currnode;
        //            else currnode = currnode.Right;
        //        }
        //        else
        //        {   
        //            if (currnode.Left == null) return currnode;
        //            else currnode = currnode.Left;
        //        }
        //    }
        //    return currnode;
        //}

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
            TouchedNodes.Clear();
        }
        private void InsertRec(int data, Node? currnode)                                //
        {
            if (currnode == null)                                                       //if node is null, assumes it's the correct placement and creates new node
            {
                currnode = new Node(data);
                if (data < TouchedNodes.Peek().Data)                                    //connects the new node to it's parent
                    TouchedNodes.Peek().Left = currnode;
                else
                    TouchedNodes.Peek().Right = currnode;
                return;
            }
            if (currnode.Data == data)                                                  //does not add duplicates of nodes
            {
                Console.WriteLine("Node already exists");
                return;
            }
            TouchedNodes.Push(currnode);                    
            if (data < currnode.Data)                                                   
                InsertRec(data, currnode.Left);
            else if (data > currnode.Data)
                InsertRec(data, currnode.Right);
            Balance(currnode);
            return;
        }

        public void Remove(int target)
        {
            if (Head == null)
            {
                Console.WriteLine("Head empty");
            }
            else
            {
                RemoveRec(target, Head);
            }
            TouchedNodes.Clear();
        }
        public void RemoveRec(int targetNode, Node? currnode)                 //Removes node in parentnode tree through recursion, should be called parentnode = Remove(parentnode, targetnode)
        {
           //if only one connection, replace node with it
           //if two, replace node with min from right
           //if none, remove node
            if (currnode.Data == targetNode)
            {
                switch(currnode.Left, currnode.Right)
                {
                    case (false, false):
                        if (targetNode < TouchedNodes.Peek().Data)                                    //connects the new node to it's parent
                            TouchedNodes.Peek().Left = null;
                        else
                            TouchedNodes.Peek().Right = null;
                        break;
                    case (true, false):
                        TouchedNodes.Peek() = currnode.Left;
                        break;
                    case (false, true):
                        TouchedNodes.Peek() = currnode.Right;
                        break;
                    case (true, true):
                        TouchedNodes.Peek() = FindMin(currnode.Right);
                        break;
                }
            }
            else if (currnode == null)
            {
                Console.WriteLine("no node")
            }
            TouchedNodes.Push(currnode);
            if (targetNode < currnode.Data)
            {
                RemoveRec(targetNode, currnode.Left)
            }
            else if (targetNode > currnode.Right) 
            { 
                RemoveRec(targetNode, currnode.Right)
            }
            Balance(currnode);
            return;
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

        private void Balance(Node p)
        {
            Node parentnode = TouchedNodes.Pop();
            if (p.Data < parentnode.Data)
                parentnode.Left = parentnode.Left.BalanceNode();
            else
                parentnode.Right = parentnode.Right.BalanceNode();
            return;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tree_Task
{
    internal class Tree
    {
        public Node? Head { get; set; }

        private Stack<Node> TouchedNodes { get; set; }

        public Tree(int data)
        {
            this.Head = new Node(data);
        }

        public Tree() { }

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


        public void Insert (int data)
        {
            TouchedNodes = new Stack<Node>();
            if (Head == null)
            {
                Head = new Node(data);
            }
            else
            {
                InsertRec(data, Head);
            }
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
                Balance(currnode);
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
            TouchedNodes = new Stack<Node>();
            if (Head == null)
            {
                Console.WriteLine("Head empty");
            }
            else
            {
                RemoveRec(target, Head);
            }
        }
        public void RemoveRec(int targetNode, Node? currnode)                 //Removes node in parentnode tree through recursion, should be called parentnode = Remove(parentnode, targetnode)
        {
           //if only one connection, replace node with it
           //if two, replace node with min from right
           //if none, remove node
            if (currnode.Data == targetNode)
            {
                if (currnode != Head)
                { 
                    Node parentnode = TouchedNodes.Peek();
                    switch (currnode.Left != null, currnode.Right != null)
                    {
                        case (false, false):
                            if (targetNode < parentnode.Data)                                    //connects the new node to it's parent
                                parentnode.Left = null;
                            else
                                parentnode.Right = null;
                            break;

                        case (true, false):
                            if (targetNode < parentnode.Data)                                    //connects the new node to it's parent
                                parentnode.Left = currnode.Left;
                            else
                                parentnode.Right = currnode.Left; 
                            break;

                        case (false, true):
                            if (targetNode < parentnode.Data)                                    //connects the new node to it's parent
                                parentnode.Left = currnode.Right;
                            else
                                parentnode.Right = currnode.Right;
                            break;

                        case (true, true):
                            Node min = FindMin(currnode.Right);
                            Remove(min.Data);
                            min.Left = currnode.Left;
                            min.Right = currnode.Right;
                            if (targetNode < parentnode.Data)                                    //connects the new node to it's parent
                                parentnode.Left = min;
                            else
                                parentnode.Right = min;
                            break;
                    }
                    if (TouchedNodes.Count > 0)
                        TouchedNodes.Pop();
                }
                else
                {
                    switch (currnode.Left != null, currnode.Right != null)
                    {
                        case (false, false):
                            Head = null;
                            break;
                        case (true, false):
                            Head = currnode.Left;
                            break;
                        case (false, true):
                            Head = currnode.Right;
                            break;
                        case (true, true):
                            Node min = FindMin(currnode.Right);
                            Remove(min.Data);
                            min.Left = currnode.Left;
                            min.Right = currnode.Right;
                            Head = min;
                            
                            break;
                    }
                }
                currnode.Left = null;
                currnode.Right = null;               
                return;
            }
            else if (currnode == null)
            {
                Console.WriteLine("no node");
            }
            else TouchedNodes.Push(currnode);
            if (targetNode < currnode.Data)
            {
                RemoveRec(targetNode, currnode.Left);
            }
            else if (targetNode > currnode.Data) 
            {
                RemoveRec(targetNode, currnode.Right);
            }
            //if (currnode != Head) TouchedNodes.Pop();
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

        private void Balance(Node p)
        {
            if (p == Head)
            {
                Head = Head.BalanceNode();
            }
            else
            {
                Node parentnode = TouchedNodes.Pop();
                if ((p.Data < parentnode.Data) && (parentnode.Left != null))
                    parentnode.Left = parentnode.Left.BalanceNode();
                else if (parentnode.Right != null)
                    parentnode.Right = parentnode.Right.BalanceNode();
                return;
            }
        }
        public static void Print(Tree tree) 
        {
            Print(tree.Head);
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
    
        public static Tree operator +(Tree tree, int n)
        {
            var otpt = new Tree();
            otpt.Insert(Sum(tree.Head, n));

            int Sum(Node node, int x)
            {
                var sum = node.Data +  x;
                if (node.Left != null) 
                    otpt.Insert(Sum(node.Left, x));
                if (node.Right != null)
                    otpt.Insert(Sum(node.Right, x));
                return sum;
            }
            
            return otpt;
        }

        public static Tree operator +(Tree tree1, Tree tree2)
        {
            var otpt = new Tree();
            Ins(tree1.Head);
            Ins(tree2.Head);
            void Ins(Node node)
            {
                otpt.Insert(node.Data);
                if (node.Left != null)
                    Ins(node.Left);
                if (node.Right != null)
                    Ins(node.Right);
                return;
            }

            return otpt;
        }
    }
}

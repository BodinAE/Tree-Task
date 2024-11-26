namespace Tree_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tree test = new Tree();
            //test.Head = new Node(7);
            //test.Head.Left = new Node(6);
            //test.Head.Right = new Node(8);
            //test.Head.Left.Left = new Node(5);
            test.Insert(7);
            test.Insert(6);
            test.Insert(8);
            test.Insert(5);
            test.Insert(10);
            test.Insert(9);
            test.Insert(13);
            test.Insert(1);
            test.Insert(2);
           // test.Insert(14);
            Tree.Print(test.Head);
            Console.WriteLine(test.Find(10));


            //test.Remove(7);
            //Tree.Print(test.Head);

            //test.Remove(2);
            //Tree.Print(test.Head);

            //test.Remove(13);
            //Tree.Print(test.Head);
            //var n = test.Find(5);
            //n = test.RightRotation(n);
            //Tree.Print(test.Head);
        }
    }
}

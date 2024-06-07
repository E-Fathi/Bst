using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree bt = new BinaryTree();
            BinaryTree bt1 = new BinaryTree();
            BinaryTree bt2 = new BinaryTree();
            MaxHeap max = new MaxHeap();
            int MAX = 1000;
            int[] arr = new int[MAX];

            bt.Add(25);
            bt.Add(70);
            bt.Add(50);
            bt.Add(13);
            bt.Add(30);
            bt.Add(35);
            bt.Add(33);
            bt.Add(65);
            bt.Add(60);
            bt.Add(62);
            bt.Add(68);
            bt.Add(61);
            bt.Add(64);
            bt1.InsertNode(1);
            bt1.InsertNode(3);
            bt1.InsertNode(2);
            bt1.InsertNode(6);
            bt1.InsertNode(7);
            bt1.InsertNode(4);
            bt2.Root = bt2.mergeTrees(bt.Root, bt1.Root);
            Console.Write("Merged Trees");
            bt2.InOrder();
            //bt1.Delete(3);
            Node p = bt1.Root.Right;
            bt1.Successor(p);
            bt1.Before(p);
            //bt.PostOrder();
            //bt.PreOrder();
          //  bt.InOrder();
            bt.Min();
            bt.Max();
            while (bt1.Root != null)
            {
                int m = bt1.MaxValue(bt1.Root);
                arr[0] = m;
                int n = max.insertNode(arr, 1, m);
                max.print(arr, 1);
                bt1.Delete(bt1.MaxValue(bt1.Root));
            }
            Console.ReadKey();
        }
    }
}
class Node
{
    public Node Left;
    public Node Right;
    public Node Parent;
    public int data;
    public Node(int value = 0)
    {
        this.data = value;
        this.Left = null;
        this.Right = null;
        this.Parent = null;
    }
}


class BinaryTree
{

    public Node Root;
    public bool Add(int value)
    {
        Node before = null;
        Node after = this.Root;
        while (after != null)
        {
            before = after;
            if (value < after.data)
                after = after.Left;
            else if (value > after.data)
                after = after.Right;
            else
            {
                Console.WriteLine("Same Value Exist");
                return false;
            }
        }
        Node newNode = new Node();
        newNode.data = value;
        if (this.Root == null)//Tree is Empty
            this.Root = newNode;
        else
        {
            if (value < before.data)
                before.Left = newNode;
            else
                before.Right = newNode;
        }
        return true;
    }
    public void InsertNode(int data)
    {
        Node newNode = new Node(data);
        if (Root == null) //First node insertion
            Root = newNode;
        else
        {
            Node current = Root;
            while (true)
            {
                Node tempParent = current;
                if (Convert.ToInt32(newNode.data) < Convert.ToInt32(current.data))
                {
                    current = current.Left;
                    if (current == null)
                    {
                        tempParent.Left = newNode;
                        newNode.Parent = tempParent;
                        return;
                    }
                }
                else
                {
                    current = current.Right;
                    if (current == null)
                    {
                        tempParent.Right = newNode;
                        newNode.Parent = tempParent;
                        return;
                    }
                }
            }
        }
    }
    public void PreOrder()
    {
        Console.Write("PreOrder Traversal :"); TraversePreOrder(this.Root);
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------");
    }
    void TraversePreOrder(Node parent)
    {
        if (parent != null)
        {
            Console.Write(parent.data + " ");
            TraversePreOrder(parent.Left);
            TraversePreOrder(parent.Right);
        }
    }
    public void InOrder()
    {
        Console.Write("InOrder Traversal :"); TraverseInOrder(this.Root);
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------");
    }
    void TraverseInOrder(Node parent)
    {
        if (parent != null)
        {
            TraverseInOrder(parent.Left);
            Console.Write(parent.data + " ");
            TraverseInOrder(parent.Right);
        }

    }
    public void PostOrder()
    {
        Console.Write("PostOrder Traversal :"); TraversePostOrder(this.Root);
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------");
    }
    void TraversePostOrder(Node parent)
    {
        if (parent != null)
        {
            TraversePostOrder(parent.Left);
            TraversePostOrder(parent.Right);
            Console.Write(parent.data + " ");
        }
    }
    public void Delete(int value)
    {
        this.Root = DeleteIterative(this.Root, value);
    }
    public static Node DeleteIterative(Node root, int data)
    {
        Node curr = root;
        Node prev = null;
        // Check if the key is 
        // present in the BST.
        // the variable prev points to
        // the parent of the key to be deleted.
        while (curr != null && curr.data != data)
        {
            prev = curr;
            if (data < curr.data)
                curr = curr.Left;

            else
                curr = curr.Right;

        }

        if (curr == null)
        {
            Console.WriteLine("Key " + data + " not found in the provided BST.");
            return root;
        }

        // Check if the node to be
        // deleted has one child.
        if (curr.Left == null || curr.Right == null)
        {
            // newCurr will replace
            // the node to be deleted.
            Node newCurr;

            // if the left child does not exist.
            if (curr.Left == null)
                newCurr = curr.Right;

            else
                newCurr = curr.Left;

            // check if the node to
            // be deleted is the root.
            if (prev == null)
                return newCurr;

            // check if the node to be deleted
            // is prev's left or right child
            // and then replace this with newCurr
            if (curr == prev.Left)
                prev.Left = newCurr;
           
            else
                prev.Right = newCurr;
            

            // free memory of the
            // node to be deleted.
            curr = null;
        }

        // node to be deleted has
        // two children.
        else
        {
            Node p = null;
            // Compute the inorder successor
            Node temp = curr.Right;
            while (temp.Left != null)
            {
                p = temp;
                temp = temp.Left;
            }
            // check if the parent of the inorder
            // successor is the curr or not(i.e. curr=
            // the node which has the same data as
            // the given data by the user to be
            // deleted). if it isn't, then make the
            // the left child of its parent equal to
            // the inorder successor'd right child.
            if (p != null)
            {
                p.Left = temp.Right;
            }

            // if the inorder successor was the
            // curr 
            // then make the right child of the node
            // to be deleted equal to the right child of
            // the inorder successor.
            else
            {
                curr.Right = temp.Right;
            }

            curr.data = temp.data;
            temp = null;
        }

        return root;
    }
    public void Min()
    {
        Console.WriteLine("Min Node is :" + MinValue(this.Root));
        Console.WriteLine("--------------------------------------------");
    }
    // Methode returns Minimum data in Tree
    public int MinValue(Node node)
    {
        int min = node.data;

        while (node.Left != null)
        {
            min = node.Left.data;
            node = node.Left;
        }

        return min;
    }
    // Methode returns Minimum Node in Tree
    Node Minimum(Node node)
    {
        while (node.Left != null)
            node = node.Left;

        return node;
    }
    public void Max()
    {
        Console.WriteLine("Max Node is:" + MaxValue(this.Root));
        Console.WriteLine("--------------------------------------------");
    }
    // Methode returns Maximum data in Tree
    public int MaxValue(Node node)
    {
        int max = node.data;
        while (node.Right != null)
        {
            max = node.Right.data;
            node = node.Right;
        }
        return max;
    }
    // Methode returns Maximum Node in Tree
    Node Maximum(Node node)
    {
        while (node.Right != null)
            node = node.Right;
        return node;
    }
    //Methode to find Tree Successor
    public void Successor(Node n)
    {
        int suc = TreeSuccessor(this.Root, n);
        if (suc != 0)
            Console.WriteLine("Successor of" + " " + n.data + " " + "is" + " " + suc);
        Console.WriteLine("--------------------------------------------");
    }
    int TreeSuccessor(Node root, Node n)
    {
        if (n.Right != null)
        {
            return MinValue(n.Right);
        }
        Node p = n.Parent;
        while (p != null && n == p.Right)
        {
            n = p;
            p = p.Parent;
        }
        return p.data;
    }
    // Method to find a node before an specific node 
    public void Before(Node n)
    {
        int bfr = NodeBefore(this.Root, n);
        if (bfr != 0)
            Console.WriteLine("Node before " + " " + n.data + " " + "is" + " " + bfr);
        Console.WriteLine("--------------------------------------------");
    }
    int NodeBefore(Node root, Node n)
    {
        if (n.Left != null)
        {
            return MaxValue(n.Left);
        }
        Node p = n.Parent;
        while (p != null && n == p.Left)
        {
            n = p;
            p = p.Parent;
        }
        return p.data;
    }
    // A Utility Method that stores inorder traversal of a tree 
    public virtual List<int> StoreInorderUtil(Node node, List<int> list)
    {
        if (node == null)
            return list;
        StoreInorderUtil(node.Left, list);
        list.Add(node.data);
        StoreInorderUtil(node.Right, list);
        return list;
    }
    // Method that stores inorder traversal of a tree 
    public virtual List<int> StoreInorder(Node node)
    {
        List<int> list1 = new List<int>();
        List<int> list2 = StoreInorderUtil(node, list1);
        return list2;
    }
    // Method that merges two ArrayLists into one.  
    public virtual List<int> merge(List<int> list1, List<int> list2, int m, int n)
    {
        // list3 will contain the merge of list1 and list2 
        List<int> list3 = new List<int>();
        int i = 0;
        int j = 0;

        //Traversing through both ArrayLists 
        while (i < m && j < n)
        {
            // Smaller one goes into list3 
            if (list1[i] < list2[j])
            {
                list3.Add(list1[i]);
                i++;
            }
            else
            {
                list3.Add(list2[j]);
                j++;
            }
        }
        while (i < m)
        {
            list3.Add(list1[i]);
            i++;
        }
        while (j < n)
        {
            list3.Add(list2[j]);
            j++;
        }
        return list3;
    }

    // Method that converts an ArrayList to a BST 
    public virtual Node ArrToBST(List<int> list, int start, int end)
    {
        if (start > end)
        {
            return null;
        }

        // Get the middle element and make it root      
        int mid = (start + end) / 2;
        Node node = new Node(list[mid]);

        /* Recursively construct the left subtree and make it 
        left child of root */
        node.Left = ArrToBST(list, start, mid - 1);

        /* Recursively construct the right subtree and make it 
        right child of root */
        node.Right = ArrToBST(list, mid + 1, end);

        return node;
    }
    public virtual Node mergeTrees(Node node1, Node node2)
    {
        //Stores Inorder of tree1 to list1 
        List<int> list1 = StoreInorder(node1);

        //Stores Inorder of tree2 to list2 
        List<int> list2 = StoreInorder(node2);

        // Merges both list1 and list2 into list3 
        List<int> list3 = merge(list1, list2, list1.Count, list2.Count);

        //Converts the merged list into result BST 
        Node node = ArrToBST(list3, 0, list3.Count - 1);
        return node;
    }
}
class MaxHeap
{
    // Function to heapify ith node in a Heap of size n 
    public void heapify(int[] arr, int n, int i)
    {
        // Find parent
        int parent = (i - 1) / 2;

        if (arr[parent] > 0)
        {
            // For Max-Heap
            // If current node is greater than its parent
            // Swap both of them and call heapify again
            // for the parent
            if (arr[i] > arr[parent])
            {

                // swap arr[i] and arr[parent]
                int temp = arr[i];
                arr[i] = arr[parent];
                arr[parent] = temp;

                // Recursively heapify the parent node
                heapify(arr, n, parent);
            }
        }
    }

    // Function to insert a new node to the heap.
    public int insertNode(int[] arr, int n, int Key)
    {
        // Increase the size of Heap by 1
        n = n + 1;

        // Insert the element at end of Heap
        arr[n - 1] = Key;

        // Heapify the new node
        heapify(arr, n, n - 1);

        // return new size of Heap
        return n;
    }

    /* A utility function to print array of size n */
    public void printArray(int[] arr, int n)
    {
        Console.Write("Max heap elements:");
        for (int i = 0; i < n; ++i)
            Console.Write(arr[i] + " ");

        Console.WriteLine("");
        Console.WriteLine("------------------------");
    }
    public void print(int[] arr, int n)
    {
        for (int i = 0; i < n; ++i)
            Console.Write(arr[i] + " ");

    }
}


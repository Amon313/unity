using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class to print the Diameter */
class BinaryTree
{
    Node root;

    /* Method to calculate the diameter and return it to main */
    int diameter(Node root)
    {
        /* base case if tree is empty */
        if (root == null)
            return 0;

        /* get the height of left and right sub trees */
        int lheight = height(root.left);
        int rheight = height(root.right);

        /* get the diameter of left and right subtrees */
        int ldiameter = diameter(root.left);
        int rdiameter = diameter(root.right);

        /* Return max of following three
          1) Diameter of left subtree
         2) Diameter of right subtree
         3) Height of left subtree + height of right subtree + 1 */
      /*  return Math.Max(lheight + rheight + 1,
                        Math.Max(ldiameter, rdiameter));*/
      if(lheight + rheight + 1 > ldiameter && lheight + rheight + 1 > rdiameter)
        {
            return lheight + rheight + 1;
        }
        else
        {
            return (ldiameter > rdiameter ? ldiameter : rdiameter);
        }
        //return lheight + rheight + 1;

    }

    /* A wrapper over diameter(Node root) */
    int diameter()
    {
        return diameter(root);
    }

    /*The function Compute the "height" of a tree. Height is the
      number f nodes along the longest path from the root node
      down to the farthest leaf node.*/
    static int height(Node node)
    {
        /* base case tree is empty */
        if (node == null)
            return 0;

        /* If tree is not empty then height = 1 + max of left
           height and right heights */
        return 1 + (height(node.left) > height(node.right) ? height(node.left) : height(node.right));
    }

    static string PreorderTraversal(Node root)
    {
        if (root == null)
            return "";

        //Console.WriteLine(root.data); ;

        if (root.left != null)
        {
            //Console.WriteLine(root.left.data);
            PreorderTraversal(root.left);
        }
        if (root.right != null)
        {
            //Console.WriteLine(root.right.data);
            PreorderTraversal(root.right);
        }
        return "";
    }

    static string InorderTraversal(Node root)
    {
        if (root == null)
            return "";

        if (root.left != null)
        {
            InorderTraversal(root.left);
            //Console.WriteLine(root.data);
        }
        else if (root.left == null)
        {
            //Console.WriteLine(root.data);
        }
        if (root.right != null)
        {
            //Console.WriteLine(root.right.data);
            InorderTraversal(root.right);
        }
        return "";
    }

    static string PostorderTraversal(Node root)
    {
        if (root == null)
            return "";

        if (root.left != null)
        {
            PostorderTraversal(root.left);
        }
        if (root.right != null)
        {
            //Console.WriteLine(root.right.data);
            PostorderTraversal(root.right);
            //Console.WriteLine(root.data);
        }
        else
            //Console.WriteLine(root.data);
        if (root.left == null && root.right == null)
        {
            //Console.WriteLine(root.data);
        }
        return "";
    }
/*
    public static void Main(String[] args)
    {
        //var a = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();
        //int TotalNodes = a[0], rootValue = a[1];
        *//*creating a binary tree and entering the nodes*//*
        BinaryTree tree = new BinaryTree();
        //tree.root = new Node(rootValue.ToString());

        tree.root = new Node("12");
        tree.root.left = new Node("7");
        tree.root.right = new Node("17");
        tree.root.left.left = new Node("1");
        tree.root.left.right = new Node("5");

        string nodeloc = null;
        string nodeinputval = null;
        //for (int input = 1; input <= TotalNodes - 1; input++)
        //{
        //     nodeloc = Console.ReadLine().Trim();
        //     nodeinputval = Console.ReadLine().Trim();
        //    if(!string.IsNullOrWhiteSpace(nodeinputval))
        //     TraverseAndAdd(nodeloc.ToList<char>(), tree.root, nodeinputval);
        //}
        //Console.Write(tree.diameter());
        //PreorderTraversal(tree.root);
        PostorderTraversal(tree.root);
        Console.Read();
    }*/
}


public class Node
{
    public string data;
    public Node left, right;

    public Node(string item)
    {
        data = item;
        left = right = null;
    }
}
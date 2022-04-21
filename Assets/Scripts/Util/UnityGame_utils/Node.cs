using System;
using System.Collections.Generic;

namespace UnityUtils.alpha
{
    public class Node
    {
        //string NodeContainer { get; set; }
        private Node child;
        public Node Child { 
            get 
            {
                return child;
            }
            set
            {
                if (child != null)
                {
                    Siblings.Remove(child);
                    child = null;
                }
                child = value;
                Siblings.Add(child);
            }
        }

        public List<Node> Children
        {
            get;
            private set;
        }
        int Degree { get; }
        int Depth { get; }

        object Forest { get; set; }

        int Height { get; }

        bool InternalMode {
            get { return child != null; }
        }
        public bool LeafNode {
            get
            {
                return (child == null);
            }
        }   

        public List<Node> Siblings = new List<Node>();
        
        public bool RootNode {
            get
            {
                foreach (Node sibling in Siblings)
                { 
                    if (sibling.child == this) return false;
                }
                return true;
            }
        }

        //Child: A child node is a node extending from another node.For example, a computer with internet access could be considered a child node of a node representing the internet.The inverse relationship is that of a parent node.If node C is a child of node A, then A is the parent node of C.
        //Degree: the degree of a node is the number of children of the node.
        //Depth: the depth of node A is the length of the path from A to the root node.The root node is said to have depth 0.

        //Edge: the connection between nodes.

        //Forest: a set of trees.

        //Height: the height of node A is the length of the longest path through children to a leaf node.

        //Internal node: a node with at least one child.
        //Leaf node: a node with no children.
        //Root node: a node distinguished from the rest of the tree nodes. Usually, it is depicted as the highest node of the tree.

        //Sibling nodes: these are nodes connected to the same parent node.
    }

}

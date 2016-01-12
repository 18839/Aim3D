using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>
{

    public bool walkable;//can you walk on this node or not
    public Vector3 worldPos;//the position of the node, kinda importand

    public int gridX;//xpos of the current node
    public int gridY;//ypos of the current node

    public int gCost;//the ammount of units its from the starting point
    public int hCost;//the ammount of units its from the target

    public Node parent;

    int heapIndex;

    public Node(bool nodeWalkable, Vector3 nodeWorldPos, int nodeGridX, int nodeGridY)//this is to asign these value's to a node
    {
        walkable = nodeWalkable;
        worldPos = nodeWorldPos;
        gridX = nodeGridX;
        gridY = nodeGridY;
    }

    public int fCost//the ammount of units that you would need to go if you draw a line from start and a line from target to tis nod and followed those lines. always f=g+h
    {
        get//no need to alter it elsewhere, only able to get it
        {
            return gCost + hCost;
        }
    }
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}

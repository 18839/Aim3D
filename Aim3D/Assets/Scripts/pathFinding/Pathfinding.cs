using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

    public Transform seeker, target;

    Grid grid;//get grid script

    
    void Awake()
    {
        grid = GetComponent<Grid>();//gets the grit info
    }

    void update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)//here we get 2 positions from the game and check on wich node they are, we do that with NodeFromWorldPoint wich we have in the grid script
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);//this is going to be the startng point f the path
        Node targetNode = grid.NodeFromWorldPoint(targetPos);//this wil be the enemy base or player etc, just the target

        List<Node> openSet = new List<Node>();//list of nodes that havent been checked yet
        HashSet<Node> closedSet = new HashSet<Node>();//a list for the nodes that have been checked

        openSet.Add(startNode);//the verry first node that wil be checked, this is the starting position
        while (openSet.Count > 0)//as long as there are nodes to be checked, go on :)
        {
            Node currentNode = openSet[0];//first to be checked a
            for(int i =1; i< openSet.Count; i++)//loop thru all of the nodes in openset
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)//if the node in openset has a fcost smaller than the current node it becomes the current node or if the fcost is equal we go to the one with the lowest hcost becouse thatone is closer to the target 
                {
                    currentNode = openSet[i];//current node is checked node in openset
                }
                openSet.Remove(currentNode);//'this node is checked and gets removed from the openset and send to the closedset where all node who are already checked are
                closedSet.Add(currentNode);
                if (currentNode == targetNode)//if the current nod is the target node, then we are done
                {
                    RetracePath(startNode , targetNode);
                    return;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))//here we ask for the neighbour nodes of the current node wich we got in the grid script
                {
                    if(!neighbour.walkable || closedSet.Contains(neighbour))//if you cant walk on the neighbout node and its already checked(in closedset list) then skip
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);//check what the new cost is to walk to the neighbour
                    if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;//set gcost
                        neighbour.hCost = getDistance(neighbour, targetNode);//set hcost
                        neighbour.parent = currentNode;//set parent node(middle node)

                        if(!openSet.Contains(neighbour))//if the neighbour isnt in openset the add it so it get checked
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)//function to.. retrace its path
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
    }

    int getDistance(Node nodeA, Node nodeB)//here i calculate the distance between 2 nodes(node A and B)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);//here i check the x distance
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);//here i check the y distance
        //the biggest number - the smaller 1 gives the ammount of horizontal steps that you have to do, like y=7 x = 2 means 5 (5*10)steps horizontal and 2 steps diagonal(2*14)
        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);//the ammount of units you need to walk to get to the other node
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);//the ammount of units you need to walk to get to the other node
        }
    }
}

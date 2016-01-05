using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public bool displayGridGizmos;
    public LayerMask unwalkableMask;//the layermask that the obstacles will have where you cant walk.
    public Vector2 gridSize;//a vector 2 that decides the size of the grid
    public float nodeRadius;//the radius of the node
    Node[,] grid;//a 2d array of nodes that wil make the grid

    float nodeDiameter;//the diameter of the nodes
    int gridSizeX, gridSizeY;//the ammount if nodes that wil be in a xline and a y line in the grid(magic trick: do x*y and you get all the nodes in the grid! :D)

    void Start()
    {
        nodeDiameter = nodeRadius * 2;//radius = from midle to end, diameter is from end to end
        gridSizeX = Mathf.RoundToInt(gridSize.x/nodeDiameter);//takes the grid and divides it by the diameter of a node to find the ammount if nodes, rounded to a int becouse we cant have hal nodes here
        gridSizeY = Mathf.RoundToInt(gridSize.y/nodeDiameter);//takes the grid and divides it by the diameter of a node to find the ammount if nodes, rounded to a int becouse we cant have hal nodes here
        CreateGrid();
    }
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            CreateGrid();
        }
    }
    public int maxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];//here we draw the grid to check what is walkable
        Vector3 worldBottomLeft = transform.position/*middle*/- Vector3.right * gridSize.x / 2/*left middle*/- Vector3.forward * gridSize.y / 2;//now we have the bottom left of the grid
        for(int x = 0; x < gridSizeX; x++)//2 for loops that go thru every node in the grid
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);//actual thing that has the node position
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwalkableMask));//checks for each worldpoint(node) if it is walkable or not
                grid[x, y] = new Node(walkable,worldPoint, x, y);//creates final grid with the info if a node is walkable or not
            }
        }
    }

    public List<Node> GetNeighbours(Node node)//list with the neighbours of the node, cant be a array becouse the ammount of neighbours may varry
    {
        List<Node> neighbours = new List<Node>();//
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)//loop who will search a 3by3 block(except for the middle node)
            {
                if (x == 0 && y == 0)//if it is on the current node it wil continue(skip the rest of this code) 
                    continue;

                int checkX = node.gridX + x;//the xpos of the neighbour node
                int checkY = node.gridY + y;//the ypos of the neighbour node

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)//if its in the grid add it to the neighbours list
                {
                    neighbours.Add(grid[checkX, checkY]);//here add it to the neighbour list
                }
            }
        }
        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)//this method gets a position from the world(wich becomes the target)
    {
        float percentX = (worldPosition.x + gridSize.x / 2) / gridSize.x;//the place of the target wil be a percentage, if it is of the left it wil be 0, middle 0,5 and right 1.
        float percentY = (worldPosition.z + gridSize.y / 2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);//these two mathf make sure that if for somereason the target is outside the grid it wont give error etc
        percentY = Mathf.Clamp01(percentY);//the pos is always between 1 and 0. 

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);// the actual positions that wil be given to the grid
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    public List<Node> path;
    void OnDrawGizmos()//a function to draw the gizmo's this wont be visable ingame but in the editor to show me stuff works
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));//here i draw a wirecube the size of the grid itself. why a vector 3 for a vector 2 value? the game is 3d so the y wil go up instead of forward wich is why we put the vector2 y valye on the vector3 z.

        if (grid != null && displayGridGizmos)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}

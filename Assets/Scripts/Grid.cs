using UnityEngine;
using System.Collections;


// NOTE keep in mind when using a 3d vector that our characters are moving to the x,z axis  not the y. The height of the characters does't change
public class Grid : MonoBehaviour
{
    [SerializeField] LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    [SerializeField] float nodeRadius;
    public Node[,] grid = null;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    [SerializeField] Transform player;

    private void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        if (nodeRadius == 0f)
        {
            Debug.LogWarning("Diameter cannot be zero");
            return;
        }

        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldButtomLeft = transform.position - (Vector3.right * gridWorldSize.x / 2) - (Vector3.forward * gridWorldSize.y / 2);

        //create grid
        for (int x = 0; x < gridSizeX; ++x)
        {
            for (int y = 0; y < gridSizeY; ++y)
            {
                // add the radius to get the center of every point
                Vector3 worldPoint = worldButtomLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                                                     + Vector3.forward * (y * nodeDiameter + nodeRadius);
                grid[x, y] = new Node(worldPoint, false);

            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float bottomX = gridWorldSize.x / 2;
        float bottomY = gridWorldSize.y / 2;


        int x = (int)((bottomX + worldPosition.x) / nodeDiameter);
        int y = (int)((bottomY + worldPosition.z) / nodeDiameter);

        if(x >= gridSizeX || y >= gridSizeY)
        {
            string exceptionError = "Request position is out of bounds,x " + x + " ,y" + y +
                                    "\n GridSizeX "+ gridSizeX + " GridSizeY " + gridSizeY + " buttomY "+ bottomY;
            throw new System.IndexOutOfRangeException(exceptionError);
        }

        return grid[x, y];
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));


        if (grid != null)
        {
            Node playerNode = NodeFromWorldPosition(player.position);
            foreach (Node node in grid)
            {
                Gizmos.color = Color.blue;

                if (playerNode == node)
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawCube(node.worldPoint, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}

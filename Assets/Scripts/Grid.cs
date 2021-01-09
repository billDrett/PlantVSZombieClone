using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    [SerializeField] LayerMask unwalkableMask;
    [SerializeField] Vector2 gridWorldSize;
    [SerializeField] float nodeRadius;
    [SerializeField] Node[,] grid = null;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    [SerializeField] Transform player;

    // Use this for initialization
    void Start()
    {
       CreateGrid();
    }

    void CreateGrid()
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

        Vector3 worldButtonLeft = transform.position - (Vector3.right * gridWorldSize.x / 2) - (Vector3.forward * gridWorldSize.y / 2);

        //create grid
        for (int x = 0; x < gridSizeX; ++x)
        {
            for (int y = 0; y < gridSizeY; ++y)
            {
                // add the radius to get the center of every point
                Vector3 worldPoint = worldButtonLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                                                     + Vector3.forward * (y * nodeDiameter + nodeRadius);
                grid[x, y] = new Node(worldPoint, false);

            }
        }
    }

    Node NodeFromWorldPosition(Vector3 worldPosition)
    {

        //TODO find a way to make the logic here simpler. Using a for loop is not fast enough
        //for big greeds but it's easy to understand compare to this code of 6 lines full of math
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    // Update is called once per frame
    void Update()
    {
        CreateGrid();
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

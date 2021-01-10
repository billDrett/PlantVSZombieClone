using UnityEngine;
using System.Collections;

//public class GameManager : MonoBehaviour
//{
//    Grid grid;


//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawWireCube(transform.position, new Vector3(grid.gridWorldSize.x, 1, grid.gridWorldSize.y));


//        if (grid != null)
//        {
//            Node playerNode = grid.NodeFromWorldPosition(player.position);
//            foreach (Node node in grid)
//            {
//                Gizmos.color = Color.blue;

//                if (playerNode == node)
//                {
//                    Gizmos.color = Color.green;
//                }
//                Gizmos.DrawCube(node.worldPoint, Vector3.one * (nodeDiameter - 0.1f));
//            }
//        }
//    }
//}

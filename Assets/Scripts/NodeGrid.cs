using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    private const int gridSize = 20;
    private const float cellSize = 1.0f;
    public readonly Node[,] Grid = new Node[gridSize,gridSize];

    
    private void Start()
    {
        Generate();
    }

    private void OnDrawGizmos()
    {

        foreach (var node in Grid)
        {
            if (node == null)
                continue;
            
            Gizmos.DrawCube(node.Position, Vector3.one * cellSize * 0.5f);

            foreach (var neighbour in node.Neighbours)
            {
                Gizmos.DrawLine(node.Position, neighbour);
            }
        }

    }


    private void Generate()
    {
        for (var x = 0; x < gridSize; x++)
        {
            for (var z = 0; z < gridSize; z++)
            {
                Grid[x, z] = new Node(cellSize)
                {
                    Position = new Vector3(x,0,z),
                    G = 0,
                    H = 0
                };
            }
        }
        
        for (var x = 0; x < gridSize; x++)
        {
            for (var z = 0; z < gridSize; z++)
            {
                Grid[x, z].AddNeighbours();
               
                
                
                
            }
        }
    }



}

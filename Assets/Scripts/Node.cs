using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool Passable;
    
    public float G;
    public float H;

    public float F;
    
    public Vector3 Position;

    public readonly List<Vector3> Neighbours;

    private readonly float cellSize;
    
    public Node(float cellSize)
    {
        this.cellSize = cellSize;
        Neighbours = new List<Vector3>();
        Passable = true;
    }

    public void AddNeighbours()
    {
        Neighbours.Clear();

        var adjacent = new Vector3[]
        {
            // Horizontal Vertical
            new Vector3(1, 0, 0) + Position,
            new Vector3(0, 0, 1) + Position,
            new Vector3(-1, 0, 0) + Position,
            new Vector3(0, 0, -1) + Position,
            
            // Diagonals
            new Vector3(1, 0, 1) + Position,
            new Vector3(1, 0, -1) + Position,
            new Vector3(-1, 0, 1) + Position,
            new Vector3(-1, 0, -1) + Position,
        };


        foreach (var direction in adjacent)
        {
            var neighbour = Position + new Vector3(direction.x * cellSize, 
                                                         direction.y * cellSize, 
                                                         direction.z * cellSize);

            // This should not be required
            if (!Neighbours.Contains(neighbour))
            {
                Neighbours.Add(neighbour);
            }
        }
        
        
    }

    public void CalculateHCost()
    {
        
    }

    public void CalculateGCost()
    {
        
    }
    
    public override int GetHashCode()
    {
        return F.GetHashCode() + G.GetHashCode() + H.GetHashCode() + Position.GetHashCode();
    }
}

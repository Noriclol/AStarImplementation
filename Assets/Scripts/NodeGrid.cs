using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    
    [Header("Prefabs")]
    [SerializeField]
    private GameObject nodePrefab;

    
    
    [Header("Materials")]
    [SerializeField] 
    private Material mPassable;

    [SerializeField] 
    private Material mObstructed;


    
    
    private const int gridSize = 20;
    private const float cellSize = 1.0f;
    public readonly Node[,] Grid = new Node[gridSize,gridSize];

    [Header("PrefabInstances")]
    [SerializeField] 
    private List<GameObject> prefabReferences = new List<GameObject>();

    public event Action GridLoaded = delegate {}; 



    private void Start()
    {
        Generate();
        PopulatePrefabs();
        GridLoaded.Invoke();
    }

    // private void OnDrawGizmos()
    // {
    //
    //     foreach (var node in Grid)
    //     {
    //         if (node == null)
    //             continue;
    //         
    //         Gizmos.DrawCube(node.Position, Vector3.one * cellSize * 0.5f);
    //
    //         foreach (var neighbour in node.Neighbours)
    //         {
    //             Gizmos.DrawLine(node.Position, neighbour);
    //         }
    //     }
    //
    // }


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

    private void PopulatePrefabs()
    {
        foreach (var node in Grid)
        {
            var newNode = Instantiate(nodePrefab, node.Position, Quaternion.identity);
            
            // If node is passable
            if (node.Passable)
            {
                newNode.GetComponent<MeshRenderer>().material = mPassable;
            }
            else
            {
                newNode.GetComponent<MeshRenderer>().material = mObstructed;

            }
            prefabReferences.Add(newNode);
        }
    }

    public void PopulateNeighbours()
    {
        
    }

    [CanBeNull]
    public Node GetNode(Vector3 pos)
    {
        return Grid[(int)pos.x, (int)pos.z];
        // foreach (var node in Grid)
        // {
        //     if (node.Position == pos)
        //     {
        //         return node;
        //     }
        // }
        // return null;
    }  
    
    [CanBeNull]
    public GameObject GetPrefab(Vector3 pos)
    {
        foreach (var prefab in prefabReferences)
        {
            if (prefab.transform.position == pos)
            {
                return prefab;
            }
        }
        return null;
    }  
    
    [CanBeNull]
    public GameObject GetPrefab(Node nodeInput)
    {
        foreach (var prefab in prefabReferences)
        {
            if (prefab.transform.position == nodeInput.Position)
            {
                return prefab;
            }
        }
        return null;
    } 
    

}

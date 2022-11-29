using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Algorithm : MonoBehaviour
{
    
    
    // Run Input
    
    [Header("Fields")]
    
    [SerializeField]
    private NodeGrid grid;

    [SerializeField]
    private Vector3 startPos = new Vector3(1, 0 ,1);
    
    [SerializeField]
    private Vector3 endPos = new Vector3(5, 0 ,10);



    [Header("Materials")]
    
    [SerializeField] 
    private Material mStart;

    [SerializeField] 
    private Material mEnd;
    
    [SerializeField] 
    private Material mPath;
    

    private void Start()
    {
        grid.GridLoaded += Run;
    }



    public void Run()
    {
        //spawn Actor on StartPos

        var Path = AStar(startPos, endPos);

        foreach (var node in Path)
        {
            var nodePrefab = grid.GetPrefab(node);
            nodePrefab.GetComponent<MeshRenderer>().material = mPath;
        }

        var startNode = grid.GetNode(startPos);
        grid.GetPrefab(startNode).GetComponent<MeshRenderer>().material = mStart;

        var endNode = grid.GetNode(startPos);
        grid.GetPrefab(endNode).GetComponent<MeshRenderer>().material = mEnd;
        

    }
    

    public List<Node> AStar(Vector3 start, Vector3 end)
    {

        //grab refs for start and End
        Node nStart = grid.GetNode(start);
        Node nEnd = grid.GetNode(end);
        
        
        List<Node> open = new List<Node>();
    

        List<Node> closed = new List<Node>();
        
        
        
        //add StartNode to OpenList
        open.Add(nStart);


        while (open.Count > 0)
        {
            CheckCheapestNeighbors(open[0]);
        }

        // Return Path of nodes from Start to Target
        return new List<Node>();
    }


    private List<Node> CheckCheapestNeighbors(Node n)
    {
        
        
        
        List<float> neighbourCosts = new List<float>();

        for (int i = 0; i < 8; i++)
        {
            //Get node from coords
            var node = grid.GetNode(n.Neighbours[i]);
            
            // if Node Obstructed then skip adding it to list
            if (!node.Passable)
                continue;
            
            neighbourCosts.Add(node.F);
        }

        
        List<Node> neighbours = new List<Node>();

        for (int i = 0; i < 8; i++)
        {
            neighbours.Add(grid.GetNode(n.Neighbours[i]));

        }
        return neighbours;
    }
    
    

}

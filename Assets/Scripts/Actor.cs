using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Vector3 Position;
    
    private ActorStates state = ActorStates.Starting;
    
    
    
    
}


public enum ActorStates
{
    Starting,
    Waiting,
    Moving,
    Finished
}
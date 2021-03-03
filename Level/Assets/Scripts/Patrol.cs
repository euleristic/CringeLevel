using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    CharacterController controller;
    Vector3 gravity;
    Vector3 velocity;
    public List<Vector3> checkPoints;
    short currentCheckPointIndex;
    PlayerMovement player;
    Vector3 lastKnownLocation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentCheckPointIndex = 0;
    }

    void Update()
    {
        if (checkPoints.Count != 0)
        {

        }
    }
}

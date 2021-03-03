using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] float patrolSpeed;
    [SerializeField] float chaseSpeed;
    [SerializeField] float viewAngle;
    CharacterController controller;
    Vector3 gravity;
    Vector3 velocity;
    public List<Vector3> checkPoints;
    int currentCheckPointIndex;
    Vector3 lastKnownLocation;
    bool chasing;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentCheckPointIndex = 0;
        chasing = false;
    }

    void Update()
    {
        if (!chasing && checkPoints.Count != 0)
        {
            velocity = (checkPoints[currentCheckPointIndex] - transform.position).normalized * patrolSpeed;
            if ((checkPoints[currentCheckPointIndex] - transform.position).sqrMagnitude < 0.5f)
            {
                currentCheckPointIndex++;
                currentCheckPointIndex %= checkPoints.Count;
            }
        }
        if (chasing && player != null)
        {
            velocity = (lastKnownLocation - transform.position).normalized * chaseSpeed;
            if ((lastKnownLocation - transform.position).sqrMagnitude < 0.1f)
                chasing = false;
        }
        transform.forward = velocity.normalized;
        controller.Move(velocity * Time.deltaTime);

        //Search for player
        if (player != null)
        {
            
            Vector3 playerRelative = player.transform.position - transform.position;
            float angleToPlayer = Mathf.Abs(Vector3.Angle(playerRelative, transform.forward));
            print(angleToPlayer);
            if (!Physics.Raycast(transform.position, playerRelative, playerRelative.magnitude) 
                && angleToPlayer < viewAngle)
            {
                chasing = true;
                lastKnownLocation = player.transform.position;
            }
        }
    }
}

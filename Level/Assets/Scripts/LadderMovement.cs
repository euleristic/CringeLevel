﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    bool isInside = false;
    float climbingSpeed = 2.5f;
    PlayerMovement playerMovementComponent;
    [SerializeField] AudioSource ladderAudio;

    private void Awake()
    {
        playerMovementComponent = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("It works");
        //Debug.Log(other.transform.gameObject.name);
        if(other.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("It works");
            isInside = true;
            playerMovementComponent.enabled = false;
            ladderAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isInside = false;
            playerMovementComponent.enabled = true;
            ladderAudio.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside && Input.GetKey(KeyCode.W))
        {
            //Debug.Log("It works");
            if (!ladderAudio.isPlaying)
                ladderAudio.Play();
            this.transform.position += Vector3.up * climbingSpeed * Time.deltaTime;
        }
        if (isInside && Input.GetKey(KeyCode.S))
        {
            if (!ladderAudio.isPlaying)
                ladderAudio.Play();
            this.transform.position -= Vector3.up * climbingSpeed * Time.deltaTime;
        }
        if(isInside && (Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.W))) && ladderAudio.isPlaying)
        {
            ladderAudio.Stop();
        }
    }
}

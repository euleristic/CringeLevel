using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncrouchable : MonoBehaviour
{
    public bool canUnCrouch = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        canUnCrouch = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) return;
        canUnCrouch = true;
    }
}

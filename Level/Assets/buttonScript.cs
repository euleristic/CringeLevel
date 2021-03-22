using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    getCaught getcaught;
    public bool pressedButton = false;
    private bool repetitionLock = false;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && !repetitionLock)
        {
            repetitionLock = true;
            pressedButton = true;
            GetComponent<AudioSource>().Play();
        }
    }
}

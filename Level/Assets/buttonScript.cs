using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    getCaught getcaught;
    public bool pressedButton = false;
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pressedButton = true;
        }
    }
}

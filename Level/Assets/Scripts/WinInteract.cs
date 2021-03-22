using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinInteract : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E)) SceneManager.LoadScene("Victory");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectDisc : MonoBehaviour
{
    [SerializeField] FloppyDisc disc;

    private void OnMouseOver()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            disc.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}

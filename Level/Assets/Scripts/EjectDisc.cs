using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectDisc : MonoBehaviour
{
    [SerializeField] FloppyDisc disc;
    [SerializeField] AudioSource source;
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            source.Play();
            disc.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
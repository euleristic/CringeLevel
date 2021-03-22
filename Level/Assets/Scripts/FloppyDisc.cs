using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyDisc : MonoBehaviour
{
    [SerializeField] Vector3 goalVector;
    [SerializeField] AudioSource source;
    public bool collected = false;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, goalVector, 0.5f);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (source != null) source.Play();
            collected = true;
            gameObject.SetActive(false);
        }
    }
}

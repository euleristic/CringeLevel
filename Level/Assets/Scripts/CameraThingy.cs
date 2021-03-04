using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThingy : MonoBehaviour
{
    [SerializeField] Rect rect;
    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.rect = rect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

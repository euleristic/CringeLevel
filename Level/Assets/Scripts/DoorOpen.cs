using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] float openAngle;
    [SerializeField] float openingSpeed;
    [SerializeField] Transform parentTrans;
    Vector3 openVector;
    private bool open;

    private void Start()
    {
        open = false;
        openVector = new Vector3(0.0f, openAngle);
    }

    private void Update()
    {
        if (open && parentTrans.eulerAngles.y != openAngle)
        {
            parentTrans.eulerAngles = Vector3.Lerp(parentTrans.eulerAngles, openVector, openingSpeed);
            if (Mathf.Abs(parentTrans.eulerAngles.y - openAngle) < 1.0f)
                parentTrans.eulerAngles = openVector;
        }
        if (!open && parentTrans.eulerAngles.y != 0.0f)
        {
            parentTrans.eulerAngles = Vector3.Lerp(parentTrans.eulerAngles, Vector3.zero, openingSpeed);
            if (Mathf.Abs(parentTrans.eulerAngles.y) < 1.0f)
                parentTrans.eulerAngles = Vector3.zero;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E)) open = !open;
    }
}

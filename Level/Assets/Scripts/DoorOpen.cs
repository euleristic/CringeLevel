using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] float openAngle;
    [SerializeField] float openingSpeed;
    [SerializeField] Transform parentTrans;
    Vector3 currentEulerAngles;
    Vector3 closedVector;
    Vector3 openVector;
    private bool open;

    private void Start()
    {
        open = false;
        currentEulerAngles = closedVector = parentTrans.eulerAngles;
        openVector = new Vector3(0.0f, openAngle + closedVector.y);
    }

    private void Update()
    {
        if (open && currentEulerAngles != openVector)
        {
            currentEulerAngles = Vector3.Lerp(currentEulerAngles, openVector, openingSpeed);
            if ((currentEulerAngles - openVector).sqrMagnitude < 1.0f)
                currentEulerAngles = openVector;
            parentTrans.eulerAngles = currentEulerAngles;
        }
        if (!open && currentEulerAngles != closedVector)
        {
            currentEulerAngles = Vector3.Lerp(currentEulerAngles, closedVector, openingSpeed);
            if ((currentEulerAngles - closedVector).sqrMagnitude < 1.0f)
                currentEulerAngles = closedVector;
            parentTrans.eulerAngles = currentEulerAngles;
        }

    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E)) open = !open;
    }
}

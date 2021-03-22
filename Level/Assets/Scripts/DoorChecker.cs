using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        DoorOpen door = other.gameObject.GetComponent<DoorOpen>();
        if (door != null && !door.open)
        {
            door.open = true;
            door.source.Play();
        }
    }


}

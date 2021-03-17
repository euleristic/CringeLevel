using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStatic : MonoBehaviour
{
    LineRenderer _lr;
    Vector3 _beamStartPoint; //Start and end positions of beam itself, used for line renderer
    Vector3 _beamEndPoint;

    [SerializeField] Transform _beamTransform; //The transform component of beam Endpoint


    void Awake()
    {
        _beamStartPoint = GetComponent<Transform>().position;
        _lr = GetComponent<LineRenderer>();

        _lr.positionCount = 2;
        _beamEndPoint = _beamTransform.position;
    }

    void Start()
    { 
        _lr.SetPosition(0, _beamStartPoint);
        _lr.SetPosition(1, _beamEndPoint);
    }

    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(_beamStartPoint, (_beamEndPoint - _beamStartPoint), out hit);
        //Debug.DrawLine(_beamStartPoint, hit.point);
        //Debug.Log(hit.transform.gameObject.name);
        if (hit.collider && hit.collider.gameObject.tag == "Player")
        {
            SceneReboot.RebootScene();
        }
    }
}

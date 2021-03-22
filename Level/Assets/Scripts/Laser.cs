using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer _lr;
    Vector3 _path; //A vector indicating lazer's movement direction, its normal is intented to be used
    Vector3 _beamStartPoint; //Start and end positions of beam itself, used for line renderer
    Vector3 _beamEndPoint;
    Vector3 _laserStartPoint; //Start and end position of laser's movement, used for reference and are not updated
    Vector3 _laserEndPoint;

    [SerializeField] float _speed;
    [SerializeField] float _direction; //variable indicating whether to go towards endpoint or back from it
    [SerializeField] Transform _beamTransform; //The transform component of beam Endpoint
    [SerializeField] Transform _laserTransform; //The transform component of laser Destination


    void Awake()
    {
        _laserStartPoint = GetComponent<Transform>().position;
        _beamStartPoint = GetComponent<Transform>().position;
        _lr = GetComponent<LineRenderer>();

        _lr.positionCount = 2;
        _laserEndPoint = _laserTransform.position;
        _beamEndPoint = _beamTransform.position;
        _path = new Vector3(_laserEndPoint.x - _laserStartPoint.x, _laserEndPoint.y - _laserStartPoint.y, _laserEndPoint.z - _laserStartPoint.z);
        _beamStartPoint += _path.normalized;
        _beamEndPoint += _path.normalized;
        _direction = 1;
    }

    void Start()
    {
        //Debug.Log(_path.normalized);
        //Debug.Log(Vector3.SqrMagnitude(_laserStartPoint - _beamStartPoint));
        DisableMeshes();

        _lr.SetPosition(0, _beamStartPoint);
        _lr.SetPosition(1, _beamEndPoint);
    }

    void Update()
    {
        if(Vector3.SqrMagnitude(_laserEndPoint - _beamStartPoint) < 0.001 || Vector3.SqrMagnitude(_laserStartPoint - _beamStartPoint) < 0.001)
        {
            _direction *= -1;
        }
        
        _beamStartPoint += (_path.normalized * _speed * _direction * Time.deltaTime); 
        _beamEndPoint += (_path.normalized * _speed * _direction * Time.deltaTime);
       
        _lr.SetPosition(0, _beamStartPoint);
        _lr.SetPosition(1, _beamEndPoint);

        RaycastHit hit;
        Physics.Raycast(_beamStartPoint, (_beamEndPoint - _beamStartPoint), out hit);
        //Debug.DrawLine(_beamStartPoint, hit.point);
        if (hit.collider && hit.collider.gameObject.tag == "Player")
        {
            SceneReboot.RebootScene();
        }

    }

    void DisableMeshes()
    {
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}

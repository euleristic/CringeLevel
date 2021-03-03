using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] float turnAngle;
    [SerializeField] float turnSpeed;
    [SerializeField] float viewAngle;
    [SerializeField] List<Patrol> AlertList;
    [SerializeField] PlayerMovement player;
    [SerializeField] Transform cam;
    float currentAngle;
    void Start()
    {
        currentAngle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = Mathf.Sin(turnSpeed * Time.time) * turnAngle;
        transform.rotation = Quaternion.Euler(0.0f, currentAngle, 0.0f);
        if (player != null)
        {
            Vector3 playerRelative = player.transform.position - cam.transform.position;
            float angleToPlayer = Mathf.Abs(Vector3.Angle(playerRelative, cam.transform.forward));
            print(angleToPlayer);
            if (!Physics.Raycast(cam.transform.position, playerRelative, playerRelative.magnitude)
                && angleToPlayer < viewAngle)
            {
                SceneManager.LoadScene("GameOver");
                foreach (Patrol guard in AlertList)
                {
                    guard.Alert(player.transform.position);
                }
            }
        }
    }
}

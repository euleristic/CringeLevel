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
    [SerializeField] Renderer playerRenderer;
    [SerializeField] Camera cam;
    Camera playerCam;
    float currentAngle;
    void Start()
    {
        currentAngle = 0.0f;
        playerCam = Camera.main;
    }

    void Update()
    {
        currentAngle = Mathf.Sin(turnSpeed * Time.time) * turnAngle;
        transform.rotation = Quaternion.Euler(0.0f, currentAngle, 0.0f);
        if (player != null)
        {
            Vector3 playerOnScreen = cam.WorldToViewportPoint(player.transform.position);
            if (!(playerOnScreen.x > 0f && playerOnScreen.x < 1f
                && playerOnScreen.y > 0f && playerOnScreen.y < 1f
                && playerOnScreen.z > 0f)) return;
            if (!Physics.Raycast(cam.transform.position + cam.transform.forward,
                playerCam.transform.position - cam.transform.position, out RaycastHit hit,
                (playerCam.transform.position - cam.transform.position).magnitude)) return;
            if (!hit.collider.CompareTag("Player")) return;
            foreach (Patrol guard in AlertList)
            {
                guard.Alert(player.transform.position);
            }
        }
    }
}

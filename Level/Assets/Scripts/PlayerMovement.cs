using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeedAcc;
    [SerializeField] float moveSpeedMax;
    [SerializeField] float frictionDivisor;
    [SerializeField] float jumpStrength;
    [SerializeField] float mouseSpeed;
    [SerializeField] bool invertMouseY;

    CharacterController controller;
    Vector3 gravity;
    Vector3 velocity;
    Camera cam;
    float camVerticalRotation;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gravity = Physics.gravity;
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            velocity.y = 0.0f;
            velocity.x += Input.GetAxis("Horizontal") * moveSpeedAcc;
            velocity.z += Input.GetAxis("Vertical") * moveSpeedAcc;
            velocity /= frictionDivisor;
            velocity.y += Input.GetAxis("Jump") * jumpStrength;
        }
        Vector2 xy = Vector2.ClampMagnitude(new Vector2(velocity.x, velocity.z), moveSpeedMax);
        velocity = new Vector3(xy.x, velocity.y, xy.y);
        
        velocity += gravity * Time.deltaTime;

        controller.Move(transform.rotation * velocity * Time.deltaTime);

        camVerticalRotation += Input.GetAxis("Mouse Y") * mouseSpeed * (invertMouseY ? 1.0f : -1.0f);
        camVerticalRotation = Mathf.Clamp(camVerticalRotation, -90.0f, 90.0f);
        transform.Rotate(transform.up, Input.GetAxis("Mouse X") * mouseSpeed);
        cam.transform.localRotation = Quaternion.Euler(camVerticalRotation, 0.0f, 0.0f);

    }
}

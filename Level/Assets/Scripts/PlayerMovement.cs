using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerOptions options;
    [SerializeField] float moveSpeedAcc;
    [SerializeField] float moveSpeedMax;
    [SerializeField] float frictionDivisor;
    [SerializeField] float jumpStrength;
    [SerializeField] float mouseSpeed;
    [SerializeField] bool invertMouseY;
    [SerializeField] float crouchFactor;
    [SerializeField] float crouchMoveSpeedFactor;
    [SerializeField] float crouchSpeed;

    CharacterController controller;
    Vector3 gravity;
    Vector3 velocity;
    Vector3 crouchScale;
    Camera cam;
    float camVerticalRotation;
    bool crouching;
    void Start()
    {
        if (!UpdateOptions())
            Debug.Log("Could not update options");
        controller = GetComponent<CharacterController>();
        gravity = Physics.gravity;
        cam = Camera.main;
        camVerticalRotation = 0.0f;
        crouching = false;
        crouchScale = new Vector3(1.0f, crouchFactor, 1.0f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) crouching = !crouching;
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
        
        
        Vector2 xz = Vector2.ClampMagnitude(new Vector2(velocity.x, velocity.z), 
            moveSpeedMax * (crouching ? crouchMoveSpeedFactor : 1.0f));
        velocity = new Vector3(xz.x, velocity.y, xz.y);

        velocity += gravity * Time.deltaTime;

        controller.Move(transform.rotation * velocity * Time.deltaTime);

        camVerticalRotation += Input.GetAxis("Mouse Y") * mouseSpeed * (invertMouseY ? 1.0f : -1.0f);
        camVerticalRotation = Mathf.Clamp(camVerticalRotation, -90.0f, 90.0f);
        transform.Rotate(transform.up, Input.GetAxis("Mouse X") * mouseSpeed);
        cam.transform.localRotation = Quaternion.Euler(camVerticalRotation, 0.0f, 0.0f);

        if (crouching && transform.localScale != crouchScale)
        {
            //Lerping manually to keep track
            Vector3 scaleLeft = crouchScale - transform.localScale;
            if (scaleLeft.sqrMagnitude < 0.001f)
            {
                transform.localScale = crouchScale;
                transform.position += scaleLeft / 2.0f;
            }
            else
            {
                transform.localScale += scaleLeft * crouchSpeed;
                transform.position += scaleLeft * crouchSpeed / 2.0f;
            }
        }
        if (!crouching && transform.localScale != Vector3.one)
        {
            //Lerping manually to keep track
            Vector3 scaleLeft = Vector3.one - transform.localScale;
            if (scaleLeft.sqrMagnitude < 0.01f)
            {
                transform.localScale = Vector3.one;
                transform.position += scaleLeft / 2.0f;
            }
            else
            {
                transform.localScale += scaleLeft * crouchSpeed;
                transform.position += scaleLeft * crouchSpeed / 2.0f;
            }
        }
    }
    
    public bool UpdateOptions()
    {
        if (options != null)
        {
            float result = 0.0f;
            if (options.GetValue("cameraInvert", ref result))
                invertMouseY = result == 1.0f;
            else return false;
            if (!options.GetValue("mouseSpeed", ref mouseSpeed))
                return false;
        }
        else return false;
        return true;
    }    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Uncrouchable uncrouchable;
    [SerializeField] PlayerOptions options;
    [SerializeField] Transform capsule;
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
    Camera cam;
    float playerHeight;
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
        playerHeight = controller.height;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && uncrouchable.canUnCrouch) crouching = !crouching;
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            Vector2 inputVector;
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            inputVector = Vector2.ClampMagnitude(inputVector, 1.0f);
            velocity.y = 0.0f;
            velocity.x += inputVector.x * moveSpeedAcc;
            velocity.z += inputVector.y * moveSpeedAcc;
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

        if (crouching && controller.height != crouchFactor * playerHeight)
        {
            //Lerping manually to keep track
            float scaleLeft = crouchFactor * playerHeight - controller.height;
            if (Mathf.Abs(scaleLeft) < 0.001f)
            {
                controller.height = crouchFactor * playerHeight;
                controller.center += new Vector3(0f, scaleLeft / 2.0f);
                cam.transform.localPosition += new Vector3(0f, scaleLeft);
            }
            else
            {
                controller.height += scaleLeft * crouchSpeed;
                controller.center += new Vector3(0f, scaleLeft * crouchSpeed / 2.0f);
                cam.transform.localPosition += new Vector3(0f, scaleLeft * crouchSpeed);
            }
            capsule.localPosition = controller.center;
            capsule.localScale = new Vector3(1f, controller.height / playerHeight, 1f);
        }
        if (!crouching && controller.height != playerHeight)
        {
            //Lerping manually to keep track
            float scaleLeft = playerHeight - controller.height;
            if (Mathf.Abs(scaleLeft) < 0.01f)
            {
                controller.height = playerHeight;
                controller.center += new Vector3(0f, scaleLeft / 2.0f);
                cam.transform.localPosition += new Vector3(0f, scaleLeft);
            }
            else
            {
                controller.height += scaleLeft * crouchSpeed;
                controller.center += new Vector3(0f, scaleLeft * crouchSpeed / 2.0f);
                cam.transform.localPosition += new Vector3(0f, scaleLeft * crouchSpeed);
            }
            capsule.localPosition = controller.center;
            capsule.localScale = new Vector3(1f, controller.height / playerHeight, 1f);
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
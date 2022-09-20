using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //player body reference
    public Transform playerBody;


    
    

    //ground checks & jumping
    public float groundDrag;
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;
    public KeyCode jumpKey = KeyCode.Space;

    //input vars
    public float mouseSensitivity = 100f;

    private float horizontalInput;
    private float verticalInput;

    private float xRotation = 0;

    public float moveSpeed = 10f;

    //MemberStats
    public MemberStats ms;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void FixedUpdate()
    {
        if(ms.movementUsed < 100)
        {
            MovePlayer();
        }
        
    }
    void Update()
    {
        //look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
        
        //movement
        MyInput();
        SpeedControl();

        //Ground checking
        grounded = Physics.Raycast(playerBody.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if(grounded)
        {
            playerBody.GetComponent<Rigidbody>().drag = groundDrag;
        }
        else
        {
            playerBody.GetComponent<Rigidbody>().drag = 0;
        }

    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump == true && grounded == true && ms.movementUsed < 100)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void MovePlayer()
    {
        Vector3 moveDirection = playerBody.forward * verticalInput + playerBody.right * horizontalInput;

        if(moveDirection != Vector3.zero)
        {
            ms.movementUsed += 0.5f;
        }

        if(grounded)
        {
            playerBody.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            
        }
        else if(!grounded)
        {
            playerBody.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerBody.GetComponent<Rigidbody>().velocity.x, 0f, playerBody.GetComponent<Rigidbody>().velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            playerBody.GetComponent<Rigidbody>().velocity = new Vector3(limitedVel.x, playerBody.GetComponent<Rigidbody>().velocity.y, limitedVel.z);
        }

    }
    private void Jump()
    {
        ms.movementUsed += 5f;

        playerBody.GetComponent<Rigidbody>().velocity = new Vector3(playerBody.GetComponent<Rigidbody>().velocity.x, 0f, playerBody.GetComponent<Rigidbody>().velocity.z);

        playerBody.GetComponent<Rigidbody>().AddForce(playerBody.transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// for horizontal movement use
    // PlayerInputManager.Instance.movement.x
// for vertical movement use
    // PlayerInputManager.Instance.movement.y
// for rotation use
    // PlayerInputManager.Instance.rotation

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed;

        public float groundDrag;

        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;

        //[Header("KeyBinds")]
        //public KeyCode jumpKey = KeyCode.Space;
        
        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask whatIsGround;
        bool grounded;

        public Transform orientation;

        float horizontalInput;
        float verticalInput;

        Vector3 moveDirection;

        Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            readyToJump = true;
        }

        private void FixedUpdate(){
            MovePlayer();
            Debug.Log(PlayerInputManager.Instance.jump);
        }

        private void MyInput(){
            horizontalInput = PlayerInputManager.Instance.movement.x;
            verticalInput = PlayerInputManager.Instance.movement.y;

            if(PlayerInputManager.Instance.jump && readyToJump && grounded){
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void MovePlayer(){
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            // on ground
            if(grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        private void SpeedControl(){
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //limit velocity if needed
            if(flatVel.magnitude > moveSpeed){
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();
            SpeedControl();

            //handle drag
            if (grounded){
                rb.drag = groundDrag;
            }
            else{
                rb.drag = 0;
            }
        }

        private void Jump(){
            //reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump(){
            readyToJump = true;
        }

    }
}
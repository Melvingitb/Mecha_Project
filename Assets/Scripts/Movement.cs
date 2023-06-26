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
        }

        private void FixedUpdate(){
            MovePlayer();
        }

        private void MyInput(){
            horizontalInput = PlayerInputManager.Instance.movement.x;
            verticalInput = PlayerInputManager.Instance.movement.y;
        }

        private void MovePlayer(){
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        // Update is called once per frame
        void Update()
        {
            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();

            //handle drag
            if (grounded){
                rb.drag = groundDrag;
            }
            else{
                rb.drag = 0;
            }
        }

    }
}
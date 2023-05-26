using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 
// Singleton that deals all the movement values
// 

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance = null;

        // Input Controller
        private PlayerController input;
        private InputAction moveAction;
        private InputAction rotateAction;

        // Basic movement
        [HideInInspector] public Vector2 movement;
        [HideInInspector] public Vector2 rotation;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(this.gameObject);
            }

            // Initialize Input
            input = new PlayerController();
            input.Enable();
            // Place other actions here
            moveAction = input.Player.Movement;
            rotateAction = input.Player.Rotation;

            // Movement
            moveAction.performed += ctx => {
                // Reads value
                movement = ctx.ReadValue<Vector2>();
            };
            // Stops movement when released
            moveAction.canceled += ctx => movement = Vector2.zero;

            // Rotation
            rotateAction.performed += ctx => {
                // Reads value
                rotation = ctx.ReadValue<Vector2>();
            };
            // Stops movement when released
            rotateAction.canceled += ctx => rotation = Vector2.zero;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnEnable()
        {
            moveAction.Enable();
            rotateAction.Enable();
        }
        void OnDisable()
        {
            moveAction.Disable();
            rotateAction.Disable();
        }
    }
}
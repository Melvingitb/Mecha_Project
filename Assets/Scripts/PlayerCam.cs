using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace Player{
    public class PlayerCam : MonoBehaviour
    {
        public float sensX;
        public float sensY;

        public Transform orientation;
        //public Transform cameraPosition;
        //public InputAction cameraControl;

        float xRotation;
        float yRotation;
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            //get mouse input
            float mouseX = PlayerInputManager.Instance.rotation.x * Time.deltaTime * sensX;
            float mouseY = PlayerInputManager.Instance.rotation.y * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
            //transform.position = cameraPosition.position;
        }
    }
}
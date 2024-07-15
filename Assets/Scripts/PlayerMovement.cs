using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector2 input;
    private CharacterController characterController;
    private Transform cam;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        characterController.Move(transform.forward * input.magnitude * speed * Time.deltaTime);
        characterController.Move(Vector3.down * 9.81f * Time.deltaTime);
    }

    public void PlayerMove(InputAction.CallbackContext value)
    {
        input = value.ReadValue<Vector2>();
    }

    void RotatePlayer()
    {
        Vector3 forward = cam.TransformDirection(Vector3.forward);
        Vector3 right = cam.TransformDirection(Vector3.right);

        Vector3 targetDirection = input.x * right + input.y * forward;

        if (input != Vector2.zero && targetDirection.magnitude >0.1f) 
        {
            Quaternion freeRotation = Quaternion.LookRotation(targetDirection.normalized);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, freeRotation.eulerAngles.y, transform.eulerAngles.z)), 10 * Time.deltaTime);
        }
    }
}

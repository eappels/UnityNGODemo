using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    private CharacterController controller;
    private float speed = 2.0F;
    private float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        if (!IsOwner)
            return;
        Camera.main.GetComponent<CamScript>().target = transform;
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<ColorSync>().ChangeColor();
        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
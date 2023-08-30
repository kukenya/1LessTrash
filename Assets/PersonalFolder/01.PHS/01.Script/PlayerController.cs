using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir = Vector2.zero;
    Vector2 lookDir = Vector2.zero;
    private float xRotation = 0f;

    CharacterController characterController;
    public float speed = 5;
    public float runSpeed = 10;
    public float gravity = 5f;
    public Vector3 sumVector, xVector, zVector;

    public float upDownRange = 90;
    public float mouseSensitivity = 10f;


    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float mouseX = lookDir.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookDir.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        //if (moveDir.x != 0 || moveDir.y != 0)
        //{
        //    playerStatus.isMoving = true;
        //}
        //else
        //{
        //    playerStatus.isMoving = false;
        //}
        xVector = transform.forward * speed * Time.deltaTime * moveDir.y;
        zVector = transform.right * speed * Time.deltaTime * moveDir.x;
        sumVector = xVector + zVector;
        sumVector.y -= gravity * Time.deltaTime;

        if (canMove)
        {
            characterController.Move(sumVector);
        }
    }
}

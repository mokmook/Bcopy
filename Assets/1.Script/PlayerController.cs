using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어 세팅")]
     [SerializeField]private float walkSpeed=40;
     [SerializeField]private float runSpeed=50;
    private float applySpeed;
    private bool isRun=false;

    [SerializeField] private float jumpForce;
    private bool isGround=true;
    [Header("카메라 관련")]
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float cameraRotationLimit;
    private float curCameraRotationX = 0f;

    [SerializeField] Camera playerCamera;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
    }

    void Update()
    {
        TryRun();
        Move();
        CameraRotation();
        CharacterRotation();
    }
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal=transform.right*moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized*applySpeed;

        rb.MovePosition(transform.position+velocity*Time.deltaTime);

    }
    private void CameraRotation()
    {
        float xRotation=Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;
        curCameraRotationX -= cameraRotationX;
        curCameraRotationX=Mathf.Clamp(curCameraRotationX,-cameraRotationLimit,cameraRotationLimit);

        playerCamera.transform.localEulerAngles = new Vector3(curCameraRotationX, 0f, 0f);
    }
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY=new Vector3(0f,yRotation,0f)*lookSensitivity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(characterRotationY));
    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            isRun = true;
        else
            isRun = false;
        applySpeed = isRun ? runSpeed : walkSpeed;
    }
}

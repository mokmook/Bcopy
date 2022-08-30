using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어 세팅")]
     [SerializeField]private float walkSpeed=40;
     [SerializeField]private float runSpeed=50;
    [SerializeField] private float sitDownSpeed = 25;
    private float applySpeed;
    private bool isWalk = false;
    private bool isRun=false;
    private bool isSitDown=false;

    private Vector3 lastPos;
    [SerializeField] private float jumpForce;
    private bool isGround=true;

    private CapsuleCollider playerCollider;

    [Header("카메라 관련")]
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float cameraRotationLimit;
    private float curCameraRotationX = 0f;

    [SerializeField]
    private float SitDownPosY;
    private float originPosY;
    private float applySitDownPosY;


    [SerializeField] Camera playerCamera;
    private Rigidbody rb;
    GunController theGunController;
    private Crosshair theCrosshair;
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = playerCamera.transform.localPosition.y;
        theGunController = FindObjectOfType<GunController>();
        theCrosshair = FindObjectOfType<Crosshair>();
    }

    void Update()
    {
        IsGround();
        TryJump();
        TrySitDown();
        TryRun();
        Move();
        MoveCheck();
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
    private void MoveCheck()
    {
        if (!isRun&&!isSitDown)
        {
            isWalk =Vector3.Distance(lastPos,transform.position) >0.02f  ? true : false;
            theCrosshair.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }              
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
        {
            isRun = true;
            theGunController.CancelFindSight();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
            isRun = false;       
        applySpeed = isRun ? runSpeed : walkSpeed;
        theCrosshair.RunningAnimation(isRun);
    }
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)           
            rb.velocity = transform.up * jumpForce;        
    }
    private void TrySitDown()
    {
        theCrosshair.CrouchingAnimation(isSitDown);
        if (Input.GetKeyDown(KeyCode.C))
            isSitDown = !isSitDown;
        if (Input.GetKey(KeyCode.LeftControl))       
            isSitDown = true;
        else
            isSitDown = false;

        applySpeed = isSitDown ? sitDownSpeed : walkSpeed;
        applySitDownPosY = isSitDown ? SitDownPosY : originPosY;

        StartCoroutine("SitDown");
    }
    IEnumerator SitDown()
    {
        float posY = playerCamera.transform.localPosition.y;
        int count = 0;

        while (posY!=applySitDownPosY)
        {
            count++;
            posY = Mathf.Lerp(posY,applySitDownPosY,.3f);
            playerCamera.transform.localPosition = new Vector3(0f, posY, 0f);
            if (count > 15)
                break;
            yield return null;
        }
        playerCamera.transform.localPosition = new Vector3(0f, applySitDownPosY, 0f);
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y+0.3f);
        theCrosshair.RunningAnimation(!isGround);
    }
}

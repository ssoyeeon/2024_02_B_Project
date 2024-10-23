using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�÷��̾��� ������ �ӵ��� �����ϴ� ����
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;      //�̵� �ӵ� 
    public float jumpForce = 5.0f;      //���� ��
    public float rotationSpeed = 10f;      //ȸ�� ��

    //ī�޶� ���� ���� 
    [Header("Camera Setting")]
    public Camera firstPersonCamera;        //1��Ī ī�޶�
    public Camera thirdPersonCamera;        //3��Ī ī�޶�

    public float cameraDistance = 5.0f;     //ī�޶� �Ÿ�
    public float minDistance = 1.0f;        //
    public float maxDistance = 10.0f;

    private float CurrentX = 0.0f;          //���� ȸ�� ����
    private float CurrentY = 45.0f;         //���� ȸ�� ����
    public float mouseSenesitiviy = 100.0f;    //���콺 ����

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;    



    public float radius = 5.0f;             //3��Ī ī�޶�� �÷��̾� ���� �Ÿ�
    public float minRadius = 1.0f;          //ī�޶� �ּ� �Ÿ�
    public float maxRadius = 10.0f;         //ī�޶� �ִ� �Ÿ� 

    public float yMinLimit = -90;            //ī�޶� ���� ȸ�� �ּҰ�
    public float yMaxLimit = 90;            //ī�޶� ���� ȸ�� �ִ밢

    private float theta = 0.0f;                 //ī�޶��� ����ä� ȸ�� ����
    private float phi = 0.0f;                   //ī�޶��� ���� ȸ�� ����
    private float targetVerticalRotion = 0;     //��ǥ ���� ȸ�� ����
    private float verticalRoationSpeed = 240f;  //���� ȸ�� �ӵ�

    public float mouseSenesitivity = 2f;    //���콺 ����

    //���� ������
    public bool isFirstPerson = true;  //1��Ī ������� ���� 
    //private bool isGrounded;            //�÷��̾ ���� �ִ��� ���� 
    private Rigidbody rigidbody;        //�÷��̾� ������ٵ� 

    public float fallingThreshold = -0.1f;  //�������°����� ������ ���� �ӵ� �Ӱ谪

    [Header("Ground Check Setting")]
    public float groundCheckDistance = 0.3f;        //���� �ֳ� ���� ���� �Ÿ�
    public float slopedLimit = 45f;                 //��� ������ �ִ� ��� ����
    public const int groundCheckPoints = 5;         //���� üũ ����Ʈ �� 

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;       //���콺 Ŀ���� ��װ� �����.
        SetupCameras();
        SetAcitiveCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }
        HandleRotation();
        HandleCameraToggle();
    }

    private void FixedUpdate()
    {

        HandleMovement();
    }
    void HandleCameraToggle()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson; //ī�޶� ��� ��ȯ 
            SetAcitiveCamera();
        }
    }

    void SetAcitiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);  //1��Ī ī�޶� Ȱ��ȭ ���� 
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);  //3��Ī ī�޶� Ȱ��ȭ ����
    }

    public void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitiviy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitiviy * Time.deltaTime;

        if(isFirstPerson)
        {
            transform.rotation = Quaternion.Euler(0.0f, CurrentX, 0.0f);
            firstPersonCamera.transform.localRotation = Quaternion.Euler(CurrentY, 0.0f, 0.0f);

        }
        else
        {
            CurrentX += mouseX;
            CurrentY -= mouseY;

            CurrentY = Mathf.Clamp(CurrentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

            Vector3 dir = new Vector3(0,0, - cameraDistance);
            Quaternion rotation = Quaternion.Euler(CurrentY, CurrentX, 0.0f);
            thirdPersonCamera.transform.position = transform.position + rotation * dir;
            thirdPersonCamera.transform.LookAt(transform.position);

            cameraDistance = Mathf.Clamp(cameraDistance - Input.GetAxis("Mouse ScrollWheel") * 5, minDistance, maxDistance);
        }
    }

    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0f, 0.6f, 0f);  //1��Ī ī�޶� ��ġ
        firstPersonCamera.transform.localRotation = Quaternion.identity;        //1��Ī ī�޶� ȸ�� �ʱ�ȭ
    }

    public void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement;
        if(!isFirstPerson)  //3��Ī ��� �� �� ī�޶� �������� �̵� ó��
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;    //ī�޶� �� ���� 
            cameraForward.y = 0f;    //���� ���� ����
            cameraForward.Normalize();  //���� ���� ����ȭ (0-1)������ ������ ������ش�.

            Vector3 cameraRight = thirdPersonCamera.transform.right;        //ī�޶� ������ ����
            cameraRight.y = 0f;
            cameraRight.Normalize();

            //�̵� ���� �Ի�
            movement = cameraForward * moveVertical + cameraRight * moveHorizontal;
        }
        else
        {
            //ĳ���� �������� �̵�
            movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        }

        //�̵� �������� ȸ��
        if (movement.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        rigidbody.MovePosition(rigidbody.position +movement * moveSpeed * Time.deltaTime);
    }

    //�÷��̾� ������ ó���ϴ� �Լ�
    void HandleJump()
    {
        //���� ��ư�� ������ ���� ���� �� 
        if(isGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //�������� ���� ���� ���� 
        }

   }

    public bool isFalling()
    {
        return rigidbody.velocity.y < fallingThreshold && !isGrounded();
    }
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 2.0f);
    }

    public float GetVerticalVelocity() //�÷��̾��� Y�� �ӵ� Ȯ�� 
    {
        return rigidbody.velocity.y;
    }    

}

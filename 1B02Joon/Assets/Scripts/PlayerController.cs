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
    private bool isFirstPerson = true;  //1��Ī ������� ���� 
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
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;    //���콺 �¿� �Է�
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;    //���콺 ���� �Է�

        //���� ȸ�� 
        theta += mouseX;    //���콺 �Է°� �߰�
        theta = Mathf.Repeat(theta, 360f);  //���� ���� 360�� ���� �ʵ��� ����

        //���� ȸ��
        targetVerticalRotion -= mouseY;
        targetVerticalRotion = Mathf.Clamp(targetVerticalRotion, yMinLimit, yMaxLimit); //���� ȸ�� ����
        phi = Mathf.MoveTowards(phi, targetVerticalRotion, verticalRoationSpeed * Time.deltaTime);

        if (isFirstPerson)
        {
            transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);       //�÷��̾� ȸ��(ĳ���Ͱ� �������θ� ȸ��)
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);  //1��Ī ī�޶� ���� ȸ��
        }
        else
        {
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
            thirdPersonCamera.transform.LookAt(transform);  //ī�޶� �׻� �÷��̾ �ٶ󺸵��� ����

            //���콺 ��ũ���� �̿��Ͽ� ī�޶� �� ����
            radius = Mathf.Clamp(radius - Input.GetAxis("Mouse ScrollWheel") * 5, minRadius, maxRadius);
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

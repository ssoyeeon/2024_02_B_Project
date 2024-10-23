using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어의 움직임 속도를 설정하는 변수
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;      //이동 속도 
    public float jumpForce = 5.0f;      //점프 힘
    public float rotationSpeed = 10f;      //회전 힘

    //카메라 설정 변수 
    [Header("Camera Setting")]
    public Camera firstPersonCamera;        //1인칭 카메라
    public Camera thirdPersonCamera;        //3인칭 카메라

    public float cameraDistance = 5.0f;     //카메라 거리
    public float minDistance = 1.0f;        //
    public float maxDistance = 10.0f;

    private float CurrentX = 0.0f;          //수평 회전 각도
    private float CurrentY = 45.0f;         //수직 회전 각도
    public float mouseSenesitiviy = 100.0f;    //마우스 감도

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;    



    public float radius = 5.0f;             //3인칭 카메라와 플레이어 간의 거리
    public float minRadius = 1.0f;          //카메라 최소 거리
    public float maxRadius = 10.0f;         //카메라 최대 거리 

    public float yMinLimit = -90;            //카메라 수직 회전 최소각
    public float yMaxLimit = 90;            //카메라 수직 회전 최대각

    private float theta = 0.0f;                 //카메라의 수펴ㅓㅇ 회전 각도
    private float phi = 0.0f;                   //카메라의 수직 회전 각도
    private float targetVerticalRotion = 0;     //목표 수직 회전 각도
    private float verticalRoationSpeed = 240f;  //수직 회전 속도

    public float mouseSenesitivity = 2f;    //마우스 감도

    //내부 변수들
    public bool isFirstPerson = true;  //1인칭 모드인지 여부 
    //private bool isGrounded;            //플레이어가 땅에 있는지 여부 
    private Rigidbody rigidbody;        //플레이어 리지드바디 

    public float fallingThreshold = -0.1f;  //떨어지는것으로 간주할 수직 속도 임계값

    [Header("Ground Check Setting")]
    public float groundCheckDistance = 0.3f;        //땅에 있나 없나 측정 거리
    public float slopedLimit = 45f;                 //등반 가능한 최대 경사 각도
    public const int groundCheckPoints = 5;         //지면 체크 포인트 수 

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;       //마우스 커서를 잠그고 숨긴다.
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
            isFirstPerson = !isFirstPerson; //카메라 모드 전환 
            SetAcitiveCamera();
        }
    }

    void SetAcitiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);  //1인칭 카메라 활성화 여부 
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);  //3인칭 카메라 활성화 여부
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
        firstPersonCamera.transform.localPosition = new Vector3(0f, 0.6f, 0f);  //1인칭 카메라 위치
        firstPersonCamera.transform.localRotation = Quaternion.identity;        //1인칭 카메라 회전 초기화
    }

    public void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement;
        if(!isFirstPerson)  //3인칭 모드 일 때 카메라 방향으로 이동 처리
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;    //카메라 앞 방향 
            cameraForward.y = 0f;    //수직 방향 제거
            cameraForward.Normalize();  //방향 벡터 정규화 (0-1)사이의 값으로 만들어준다.

            Vector3 cameraRight = thirdPersonCamera.transform.right;        //카메라 오른쪽 방향
            cameraRight.y = 0f;
            cameraRight.Normalize();

            //이동 벡터 게산
            movement = cameraForward * moveVertical + cameraRight * moveHorizontal;
        }
        else
        {
            //캐릭터 기준으로 이동
            movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        }

        //이동 방향으로 회전
        if (movement.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        rigidbody.MovePosition(rigidbody.position +movement * moveSpeed * Time.deltaTime);
    }

    //플레이어 점프를 처리하는 함수
    void HandleJump()
    {
        //점프 버튼을 누르고 땅에 있을 때 
        if(isGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //위쪽으로 힘을 가해 점프 
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

    public float GetVerticalVelocity() //플레이어의 Y축 속도 확인 
    {
        return rigidbody.velocity.y;
    }    

}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public InputAction jumpAction;       // 점프 액션

    public float jumpForce = 8f;         // 점프 힘
    public float gravity = 9.81f * 2f;   // 중력 가속도

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        jumpAction.Enable();
        jumpAction.performed += OnJumpPerformed; // 점프 액션 이벤트 연결
    }

    private void OnDisable()
    {
        jumpAction.Disable();
        jumpAction.performed -= OnJumpPerformed; // 점프 액션 이벤트 연결 해제
    }

    private void Update()
    {
        // 땅 체크
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 땅에 닿았을 때 Y축 초기화
        }

        // 중력 적용
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Jump action detected"); // 이벤트 호출 확인
        if (isGrounded)
        {
            velocity.y = jumpForce;
            Debug.Log("Jump key pressed and executed"); // 점프 실행 확인
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}

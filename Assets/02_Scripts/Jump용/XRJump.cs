using UnityEngine;
using UnityEngine.InputSystem;

public class XRJump : MonoBehaviour
{
    public float jumpForce = 10f;       // 첫 번째 점프 힘
    public float secondJumpForce = 2f; // 두 번째 점프 힘
    public float diveForce = -20f;     // Ground 방향으로 빠르게 내려오는 힘
    public float doubleTapTime = 0.3f; // 두 번 누르는 간격 제한

    private Rigidbody rb;
    public int maxJumps = 2;            // 최대 점프 가능 횟수
    private int currentJumps;           // 현재 사용한 점프 횟수

    private float lastJumpTime;         // 마지막 점프 키 입력 시간
    private bool isDiving;              // Dive 상태 여부

    [SerializeField] private InputActionProperty jumpAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentJumps = maxJumps; // 시작 시 최대 점프 횟수로 초기화
        lastJumpTime = -doubleTapTime;  // 초기값 설정
        jumpAction.action.Enable();
        jumpAction.action.performed += OnJump;
    }

    void OnDestroy()
    {
        jumpAction.action.performed -= OnJump;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        float currentTime = Time.time;

        // 빠르게 두 번 눌렀는지 확인
        if (currentTime - lastJumpTime <= doubleTapTime && !isGrounded())
        {
            Dive();
            return;
        }

        // 점프 실행
        if (currentJumps > 0 && !isDiving)
        {
            Jump();
        }

        lastJumpTime = currentTime; // 마지막 입력 시간 갱신
    }

    private void Jump()
    {
        if (rb != null)
        {
            float force;

            if (currentJumps == maxJumps)
            {
                force = jumpForce; // 첫 번째 점프 힘
            }
            else
            {
                force = secondJumpForce; // 두 번째 점프 힘
                rb.linearVelocity = Vector3.zero; // 속도 초기화
            }

            rb.AddForce(Vector3.up * force, ForceMode.Impulse); // 힘 적용
            currentJumps--; // 점프 횟수 감소
            Debug.Log($"점프! 남은 점프 횟수: {currentJumps}, 적용된 힘: {force}");
        }
    }

    private void Dive()
    {
        if (rb != null)
        {
            isDiving = true; // Dive 상태 설정
            rb.linearVelocity = Vector3.zero; // 기존 속도 제거
            rb.AddForce(Vector3.down * Mathf.Abs(diveForce), ForceMode.Impulse); // Ground 방향으로 힘 적용
            Debug.Log("빠르게 내려오기 실행!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 땅에 닿으면 점프 횟수 초기화 및 Dive 상태 해제
        currentJumps = maxJumps;
        isDiving = false;
        Debug.Log("땅에 닿음: " + collision.gameObject.name);
    }

    private bool isGrounded()
    {
        // 캐릭터가 땅에 닿아 있는지 확인
        return currentJumps == maxJumps;
    }
}

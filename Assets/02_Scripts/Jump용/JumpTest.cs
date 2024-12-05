using UnityEngine;
using UnityEngine.InputSystem;

public class JumpTest : MonoBehaviour
{
    public InputAction inputAction;     // 점프 액션

    public float jumpForce = 8f;        // 점프 힘
    public float gravity = 9.81f * 2f;  // 중력 가속도

    private CharacterController characterController;
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 velocity;

    private void Awake()
    {
        inputAction = new InputAction();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스페이스");
            // PressJump();

        }

        // isGrounded = characterController.isGrounded;

        // 중력 적용
        // velocity.y -= gravity * Time.deltaTime;
        // characterController.Move(velocity * Time.deltaTime);

    }

    private void PressJump()
    {
        Debug.Log("Jump");
        if (isGrounded)
        {
            velocity.y = jumpForce;
        }

    }


}

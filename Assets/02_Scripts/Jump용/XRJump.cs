using UnityEngine;
using UnityEngine.InputSystem;

public class XRJump : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody rb;
    public bool isGrounded;

    [SerializeField] private InputActionProperty jumpAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Debugging: 액션 상태 확인
        Debug.Log("Jump Action 활성화 전: " + jumpAction.action.enabled);

        jumpAction.action.Enable();
        Debug.Log("Jump Action 활성화 후: " + jumpAction.action.enabled);
        jumpAction.action.performed += OnJump;
    }

    void OnDestroy()
    {
        jumpAction.action.performed -= OnJump;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("여기");
        if (isGrounded)
        {
            Debug.Log("A");
            Jump();
        }
    }

    private void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        Debug.Log("땅에 닿음:" + collision.gameObject.name);
    }
}

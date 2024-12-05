using UnityEngine;

public class SpaceBar : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody rb;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            Debug.Log("마우스버튼");
            CubeJump();
        }
    }

    void CubeJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        Debug.Log("땅에 닿음:" + collision.gameObject.name);
    }
}

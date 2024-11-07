using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public float moveSpeed = 2f; // 이동 속도

    private void Update()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}

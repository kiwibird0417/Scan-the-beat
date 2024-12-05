using UnityEngine;

public class Obstaclesssssssss : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float lifeTime = 10f; // 방해물이 살아있는 시간 (초)

    private float spawnTime; // 생성된 시점의 시간 기록

    void Start()
    {
        // 생성된 시점의 시간을 기록
        spawnTime = Time.time;

        // 중력 효과를 무시
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    void Update()
    {
        // 방해물 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // 생성된 지 지정된 시간이 지났으면 삭제
        if (Time.time - spawnTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}

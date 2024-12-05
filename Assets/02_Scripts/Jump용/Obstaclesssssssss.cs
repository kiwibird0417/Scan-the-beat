using UnityEngine;

public class Obstaclesssssssss : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float destroyDistance = 20f; // 화면을 벗어난 방해물 삭제 기준 거리

    private Transform player; // 플레이어 위치 참조

    void Start()
    {
        // 플레이어 오브젝트 참조
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
        }

    }

    void Update()
    {
        // 플레이어가 없으면 이동 및 삭제 로직 실행 중단
        if (player == null) return;

        // 중력 효과를 무시
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }

        // 방해물 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // 방해물이 플레이어와 멀리 떨어졌을 경우 삭제
        if (Vector3.Distance(transform.position, player.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}

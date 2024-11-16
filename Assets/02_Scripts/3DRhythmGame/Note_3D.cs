using UnityEngine;

public class Note_3D : MonoBehaviour
{
    public float noteSpeed = 10f; // Z축 이동 속도

    void Update()
    {
        // Z축 방향으로 이동
        transform.position += Vector3.back * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        // 노트를 비활성화
        gameObject.SetActive(false);
    }

    // 미스 박스와 충돌한 경우 노트를 비활성화하고 풀로 반환
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MissBox"))
        {
            HideNote(); // 노트 숨기기 (비활성화)
            ObjectPool_3D.instance.ReturnNote(gameObject); // 풀에 반환
        }
    }
}

using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            // 게임 오버 처리 추가 가능 (예: 씬 리로드, UI 표시 등)
            GameOver();
        }
    }

    void GameOver()
    {
        // 게임 시간을 정지
        Time.timeScale = 0;

        // UI 또는 추가 처리 가능
        Debug.Log("게임 정지. Game Over 처리 완료!");
    }
}

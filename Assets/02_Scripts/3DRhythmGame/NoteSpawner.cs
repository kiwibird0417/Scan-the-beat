using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // 노트를 생성할 위치
    [SerializeField] private int bpm; // 초당 비트 수 (BPM)
    private float spawnTimer = 0f;

    [SerializeField] private MusicManager musicManager;

    void Update()
    {
        if (musicManager.IsMusicPlaying())
        {
            spawnTimer += Time.deltaTime;

            // BPM에 맞춰 노트 생성 주기 계산 (60초 / BPM = 각 비트 간격)
            if (spawnTimer >= 60f / bpm)
            {
                SpawnNote();
                spawnTimer -= 60f / bpm; // 주기만큼 시간 차감
            }
        }
    }

    void SpawnNote()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // ObjectPool에서 Note를 가져옴
        GameObject note = ObjectPool_3D.instance.GetNote();

        // 노트 위치 설정
        note.transform.position = spawnPoints[randomIndex].position;
        note.SetActive(true); // 활성화하여 표시
    }
}

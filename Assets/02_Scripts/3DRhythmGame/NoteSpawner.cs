using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // 노트를 생성할 위치들
    [SerializeField] private Transform endPoint;      // 노트가 도달할 최종 위치
    [SerializeField] private int bpm;                // 초당 비트 수 (BPM)
    [SerializeField] private float curveHeight = 2f; // V자 곡선 높이
    [SerializeField] private MusicManager musicManager;

    private float spawnTimer = 0f;

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

        // 노트 위치 초기화
        note.transform.position = spawnPoints[randomIndex].position;
        note.SetActive(true); // 활성화하여 표시

        // V자 경로 설정
        Note_3D noteScript = note.GetComponent<Note_3D>();
        if (noteScript != null)
        {
            Vector3 startPoint = spawnPoints[randomIndex].position;

            // 노트의 경로 설정
            noteScript.InitializePath(startPoint, endPoint.position, curveHeight);
        }
    }
}

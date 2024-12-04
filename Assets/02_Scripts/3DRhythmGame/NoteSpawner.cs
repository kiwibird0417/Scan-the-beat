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

        // 랜덤으로 노트 유형 결정
        string noteType = Random.Range(0, 2) == 0 ? "Left" : "Right";

        // ObjectPool에서 Note를 가져옴
        GameObject note = ObjectPool_3D.instance.GetNote(noteType);

        if (note != null)
        {
            // 노트 위치 설정
            note.transform.position = spawnPoints[randomIndex].position;
            note.SetActive(true);
        }
        else
        {
            Debug.LogError($"Failed to spawn {noteType} note. Pool might be empty.");
        }
    }

}

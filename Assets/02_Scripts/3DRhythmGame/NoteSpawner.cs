using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private RhythmMap rhythmMap;  // 기록된 RhythmMap 참조
    [SerializeField] private Transform[] spawnPoints; // 노트를 생성할 위치
    [SerializeField] private MusicManager musicManager; // MusicManager 참조

    private float spawnTimer = 0f;  // 게임 시간 추적용 타이머
    private int noteIndex = 0;  // 생성할 노트를 추적하는 인덱스

    void Update()
    {
        if (musicManager.IsMusicPlaying())
        {
            spawnTimer += Time.deltaTime;  // 게임 시간이 흐를 때마다 증가

            // RhythmMap에 기록된 노트를 타이밍에 맞춰 생성
            if (noteIndex < rhythmMap.notes.Length)
            {
                // 노트 생성 시간과 현재 시간 비교
                if (spawnTimer >= rhythmMap.notes[noteIndex].time)
                {
                    SpawnNote(rhythmMap.notes[noteIndex]);  // 노트를 생성
                    noteIndex++;  // 다음 노트로 인덱스 증가
                }
            }
        }
    }

    // 노트를 생성하는 메서드
    void SpawnNote(Rhythm_Note note)
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);  // 랜덤한 위치 선택
        string noteType = note.noteType;  // 노트 유형 (Left / Right)

        // ObjectPool에서 노트를 가져옴
        GameObject noteObject = ObjectPool_3D.instance.GetNote(noteType);

        if (noteObject != null)
        {
            noteObject.transform.position = spawnPoints[randomIndex].position;
            noteObject.SetActive(true);
        }
        else
        {
            Debug.LogError($"Failed to spawn {noteType} note. Pool might be empty.");
        }
    }
}

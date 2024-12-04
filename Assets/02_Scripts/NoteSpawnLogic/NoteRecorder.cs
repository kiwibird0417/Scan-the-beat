using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;  // 에디터 API를 사용할 수 있도록 추가
#endif

public class NoteRecorder : MonoBehaviour
{
    [SerializeField] private RhythmMap rhythmMap;  // 기록할 RhythmMap 참조
    private float timeSinceStart = 0f;  // 게임이 시작된 이후의 시간

    void Update()
    {
        timeSinceStart += Time.deltaTime;  // 시간 업데이트

        // 마우스 클릭 감지 (왼쪽 클릭 -> Left 노트, 오른쪽 클릭 -> Right 노트)
        if (Input.GetMouseButtonDown(0))  // 왼쪽 마우스 클릭
        {
            RecordNote("Left");
        }
        else if (Input.GetMouseButtonDown(1))  // 오른쪽 마우스 클릭
        {
            RecordNote("Right");
        }
    }

    void RecordNote(string noteType)
    {
        if (rhythmMap == null)
        {
            // 새로운 RhythmMap이 없다면, 새로 생성하는 방법을 고려할 수 있습니다.
            Debug.LogWarning("RhythmMap is missing. Please assign it in the Inspector.");
            return;
        }

        // 기존 리듬 맵에 노트를 추가
        Rhythm_Note newNote = new Rhythm_Note
        {
            time = timeSinceStart,  // 현재 시간 기록
            noteType = noteType     // 노트 유형
        };

        // 노트 배열 크기 확장
        System.Array.Resize(ref rhythmMap.notes, rhythmMap.notes.Length + 1);
        rhythmMap.notes[rhythmMap.notes.Length - 1] = newNote;

        Debug.Log($"Recorded {noteType} note at time {timeSinceStart:F2}");
    }

    [ContextMenu("Save Rhythm Map")]
    void SaveRhythmMap()
    {
#if UNITY_EDITOR
        // ScriptableObject를 저장하는 코드 (에디터에서만 작동)
        EditorUtility.SetDirty(rhythmMap);
        AssetDatabase.SaveAssets();
        Debug.Log("Rhythm Map saved.");
#endif
    }
}

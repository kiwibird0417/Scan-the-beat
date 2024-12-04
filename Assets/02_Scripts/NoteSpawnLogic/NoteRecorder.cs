using UnityEditor;
using UnityEngine;

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
        // 노트를 기록
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
        // ScriptableObject를 저장하는 코드
        EditorUtility.SetDirty(rhythmMap);
        AssetDatabase.SaveAssets();
        Debug.Log("Rhythm Map saved.");
    }
}

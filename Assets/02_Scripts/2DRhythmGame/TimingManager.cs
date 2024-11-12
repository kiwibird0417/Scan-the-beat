using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();
    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxes = null;

    EffectManager theEffect;

    // 콤보 수를 저장할 변수
    private int comboCount = 0;
    // 타이밍에 맞은 노트 개수를 저장하여 All Perfect 확인
    private int perfectCount = 0;
    private int totalNotes = 0;

    // TMP 텍스트 UI 참조
    [SerializeField] TMP_Text comboText = null;  // 콤보 텍스트
    [SerializeField] TMP_Text resultText = null;  // 타이밍 결과 텍스트
    [SerializeField] TMP_Text endMessageText = null;  // 곡 끝날 때 "All Perfect!" 표시용 텍스트

    private bool isSongEnded = false; // 노래 종료 여부

    void Start()
    {
        theEffect = FindAnyObjectByType<EffectManager>();

        //타이밍 박스 설정
        timingBoxes = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxes[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                               Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    // GetCurrentNote 메서드 추가
    public Note GetCurrentNote()
    {
        // boxNoteList에서 첫 번째 노트를 반환 (현재 타이밍에 맞는 노트로 가정)
        if (boxNoteList.Count > 0)
        {
            return boxNoteList[0].GetComponent<Note>(); // 첫 번째 노트를 반환
        }

        return null; // 노트가 없으면 null 반환
    }

    public void CheckTiming(int triggerType)
    {
        // 노래가 끝났다면 타이밍 체크를 중지
        if (isSongEnded)
        {
            return;
        }

        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x = 0; x < timingBoxes.Length; x++)
            {
                if (timingBoxes[x].x <= t_notePosX && t_notePosX <= timingBoxes[x].y)
                {
                    bool isPerfect = (x == 0); // Perfect 타이밍 (x == 0)
                    bool isGoodOrBad = (x == 2 || x == 3); // Good 또는 Bad 타이밍
                    Note currentNote = boxNoteList[i].GetComponent<Note>();

                    if (currentNote.GetNoteType() == triggerType)
                    {
                        // 해당 트리거와 맞는 노트 처리
                        boxNoteList[i].GetComponent<Note>().HideNote();

                        // "Perfect"인 경우
                        if (isPerfect)
                        {
                            comboCount++;
                            perfectCount++;
                            resultText.text = "Perfect!";
                            Debug.Log("Perfect! Combo: " + comboCount);
                        }
                        // "Good" 또는 "Bad"인 경우 콤보 리셋
                        else if (isGoodOrBad)
                        {
                            comboCount = 0;  // 콤보 리셋
                            resultText.text = (x == 2) ? "Good!" : "Bad!";
                            Debug.Log("Good/Bad! Combo Reset");
                        }

                        // 이펙트 처리
                        if (x < timingBoxes.Length - 1)
                        {
                            theEffect.NoteHitEffect();
                        }

                        // 콤보 텍스트 업데이트
                        comboText.text = "Combo: " + comboCount;

                        return;
                    }
                }
            }
        }

        // 타이밍에 맞지 않으면 콤보 초기화
        Debug.Log("Miss");
        comboCount = 0;
        resultText.text = "Miss!";
        comboText.text = "Combo: " + comboCount;
    }

    public void MissNote()
    {
        // Miss 처리 (타이밍에 맞지 않는 노트)
        Debug.Log("Missed!");
        comboCount = 0;
        resultText.text = "Miss!";
        comboText.text = "Combo: " + comboCount;
    }

    // 곡이 끝난 후, All Perfect 확인
    public void CheckEndOfSong()
    {
        isSongEnded = true; // 노래 종료 표시
        if (perfectCount == totalNotes)
        {
            endMessageText.text = "All Perfect!";
            Debug.Log("All Perfect!");
        }
    }

    // 노트가 생성될 때마다 타이밍 맞는 노트 개수 증가
    public void NoteAdded()
    {
        totalNotes++;
    }
}

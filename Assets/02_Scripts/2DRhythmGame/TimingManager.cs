using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    [SerializeField] TMP_Text resultText; // 타이밍 결과를 표시할 TMP 텍스트
    [SerializeField] float displayDuration = 1f; // 텍스트 표시 시간

    Vector2[] timingBoxes = null;
    EffectManager theEffect;
    float displayTime;

    void Start()
    {
        theEffect = FindAnyObjectByType<EffectManager>();

        // 타이밍 박스 설정
        timingBoxes = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxes[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                               Center.localPosition.x + timingRect[i].rect.width / 2);
        }

        resultText.text = ""; // 초기 상태에선 빈 텍스트
    }

    void Update()
    {
        if (Time.time >= displayTime)
        {
            resultText.text = ""; // 일정 시간이 지나면 텍스트를 비움
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x = 0; x < timingBoxes.Length; x++)
            {
                if (timingBoxes[x].x <= t_notePosX && t_notePosX <= timingBoxes[x].y)
                {
                    boxNoteList[i].GetComponent<Note>().HideNote();

                    // 이펙트 효과
                    if (x < timingBoxes.Length - 1)
                    {
                        theEffect.NoteHitEffect();
                    }

                    // 타이밍에 따른 결과 텍스트 설정
                    switch (x)
                    {
                        case 0:
                            resultText.text = "Perfect";
                            break;
                        case 1:
                            resultText.text = "Great";
                            break;
                        case 2:
                            resultText.text = "Good";
                            break;
                        case 3:
                            resultText.text = "Bad";
                            break;
                    }

                    displayTime = Time.time + displayDuration; // 텍스트 표시 시간 설정

                    boxNoteList.RemoveAt(i);
                    Debug.Log("Hit " + x);
                    return;
                }
            }
        }

        resultText.text = "Miss";
        displayTime = Time.time + displayDuration;
        Debug.Log("Miss");
    }
}

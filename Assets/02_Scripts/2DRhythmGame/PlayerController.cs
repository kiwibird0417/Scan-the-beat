using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference triggerR;  // 오른쪽 트리거
    public InputActionReference triggerL;  // 왼쪽 트리거
    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindAnyObjectByType<TimingManager>();

        triggerR.action.Enable();
        triggerR.action.performed += context => CheckJudgement(1); // Right trigger (빨강)

        triggerL.action.Enable();
        triggerL.action.performed += context => CheckJudgement(2); // Left trigger (파랑)
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 기본 판정: Space 입력 시
            CheckJudgement(0); // 예시로 Space 입력 시 기본 판정
        }
    }

    void CheckJudgement(int triggerType)
    {
        Note currentNote = theTimingManager.GetCurrentNote(); // 판정할 노트를 가져오는 메서드
        if (currentNote == null) return;

        int noteType = currentNote.GetNoteType();
        if ((noteType == 1 && triggerType == 1) || (noteType == 2 && triggerType == 2))
        {
            theTimingManager.CheckTiming(triggerType); // 성공 판정
        }
        else
        {
            // 실패 처리
            Debug.Log("Missed!");
            theTimingManager.MissNote();  // Miss 처리
        }
    }

    void OnDestroy()
    {
        triggerR.action.performed -= context => CheckJudgement(1);
        triggerL.action.performed -= context => CheckJudgement(2);
    }
}

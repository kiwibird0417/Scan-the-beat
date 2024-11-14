using UnityEngine;
using UnityEngine.InputSystem;

public class VRHoldTest : MonoBehaviour
{
    public InputActionReference triggerL;
    public InputActionReference triggerR;

    private Hold holdL;
    private Hold holdR;

    void Start()
    {
        // Hold 클래스 인스턴스 생성
        holdL = new Hold(triggerL);
        holdR = new Hold(triggerR);

        // Hold 클래스 활성화
        holdL.Enable();
        holdR.Enable();
    }

    void Update()
    {
        // 트리거 L 버튼이 눌려져 있는 상태 확인
        if (holdL.IsHeld)
        {
            Debug.Log("왼쪽 트리거 버튼이 눌려져 있다");
        }

        // 트리거 R 버튼이 눌려져 있는 상태 확인
        if (holdR.IsHeld)
        {
            Debug.Log("오른쪽 트리거 버튼이 눌려져 있다");
        }
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        holdL.Disable();
        holdR.Disable();
    }
}



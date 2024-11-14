using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerInteractable : MonoBehaviour
{
    public InputActionReference triggerL;
    public InputActionReference triggerR;

    private bool isDestroyed = false; // 이미 삭제되었는지 확인하는 플래그


    public void OnDestroy()
    {
        // 이벤트 구독 해제
        triggerL.action.performed -= OnTriggerL;
        triggerR.action.performed -= OnTriggerR;
    }

    private void OnTriggerL(InputAction.CallbackContext context)
    {
        Event_Trigger_L();
    }

    private void OnTriggerR(InputAction.CallbackContext context)
    {
        Event_Trigger_R();
    }

    public void Display()
    {
        triggerL.action.Enable();
        triggerL.action.performed += OnTriggerL;

        triggerR.action.Enable();
        triggerR.action.performed += OnTriggerR;
    }

    public void Event_Trigger_L()
    {
        if (isDestroyed) return; // 이미 삭제되었다면 실행하지 않음

        Debug.Log("왼쪽 트리거 버튼을 눌렀다");
        DestroySelf();
    }

    public void Event_Trigger_R()
    {
        if (isDestroyed) return; // 이미 삭제되었다면 실행하지 않음

        Debug.Log("오른쪽 트리거 버튼을 눌렀다");
        DestroySelf();
    }

    private void DestroySelf()
    {
        isDestroyed = true; // 객체가 삭제됨을 표시
        Destroy(this.gameObject); // targetObject 삭제
        /*
    if (targetObject != null)
    {
        isDestroyed = true; // 객체가 삭제됨을 표시
        Destroy(GameObject); // targetObject 삭제
    }
    else
    {
        Debug.LogWarning("Target object is not assigned!");
    }
    */
    }
}

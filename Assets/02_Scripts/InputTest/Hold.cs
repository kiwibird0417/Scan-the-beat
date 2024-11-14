using UnityEngine;
using UnityEngine.InputSystem;

public class Hold : MonoBehaviour
{
    private InputActionReference actionReference;
    public bool IsHeld { get; private set; }

    public Hold(InputActionReference actionReference)
    {
        this.actionReference = actionReference;
        this.actionReference.action.performed += OnActionPerformed;
        this.actionReference.action.canceled += OnActionCanceled;
    }

    public void Enable()
    {
        actionReference.action.Enable();
    }

    public void Disable()
    {
        actionReference.action.Disable();
        actionReference.action.performed -= OnActionPerformed;
        actionReference.action.canceled -= OnActionCanceled;
    }

    public void OnActionPerformed(InputAction.CallbackContext context)
    {
        if (!IsHeld) // 버튼이 처음 눌린 상태에서만 로그 출력
        {
            IsHeld = true; // 버튼이 눌려있는 상태로 설정
            Debug.Log("버튼이 눌렸다");
        }
    }

    public void OnActionCanceled(InputAction.CallbackContext context)
    {
        if (IsHeld) // 버튼이 떼어질 때만 로그 출력
        {
            IsHeld = false; // 버튼이 눌려있는 상태 해제
            Debug.Log("버튼에서 손을 뗐다");
        }
    }
}
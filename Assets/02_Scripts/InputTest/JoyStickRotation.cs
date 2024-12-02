using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class JoyStickRotation : MonoBehaviour
{
    public InputActionReference LeftJoystick; // 조이스틱 입력을 연결
    public InputActionReference RightJoystick;
    public Transform cubeTransform; // 회전할 큐브의 Transform

    private bool isRotating = false; // 회전 중인지 체크

    void Start()
    {
        LeftJoystick.action.Enable();
        LeftJoystick.action.performed += OnJoystickInput;

        RightJoystick.action.Enable();
        RightJoystick.action.performed += OnJoystickInput;
    }

    void OnDestroy()
    {
        LeftJoystick.action.performed -= OnJoystickInput;
        RightJoystick.action.performed -= OnJoystickInput;
    }

    private void OnJoystickInput(InputAction.CallbackContext context)
    {
        Debug.Log("돌고 있다.");

        if (isRotating) return; // 이미 회전 중이면 입력 무시

        Vector2 joystickInput = context.ReadValue<Vector2>();

        if (joystickInput.x > 0.5f) // 오른쪽으로 밀었을 때
        {
            RotateCube(Vector3.up * 90);
        }
        else if (joystickInput.x < -0.5f) // 왼쪽으로 밀었을 때
        {
            RotateCube(Vector3.down * 90);
        }
    }

    private void RotateCube(Vector3 rotation)
    {
        isRotating = true;

        cubeTransform.DORotate(cubeTransform.eulerAngles + rotation, 0.5f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => isRotating = false); // 회전 완료 후 상태 초기화
    }
}

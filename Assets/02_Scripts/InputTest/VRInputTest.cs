using UnityEngine;
using UnityEngine.InputSystem;

public class VRInputTest : MonoBehaviour
{
    // 버튼 입력을 인스펙터에서 할당할 수 있도록 InputActionReference 변수를 선언
    public InputActionReference buttonA;
    public InputActionReference buttonB;
    public InputActionReference buttonX;
    public InputActionReference buttonY;
    public InputActionReference joystick;
    public InputActionReference grip;
    public InputActionReference trigger;

    void Start()
    {
        // 각 입력에 대한 리스너 등록
        buttonA.action.Enable();
        buttonA.action.performed += context => Debug.Log("A 버튼을 눌렀다");

        buttonB.action.Enable();
        buttonB.action.performed += context => Debug.Log("B 버튼을 눌렀다");

        buttonX.action.Enable();
        buttonX.action.performed += context => Debug.Log("X 버튼을 눌렀다");

        buttonY.action.Enable();
        buttonY.action.performed += context => Debug.Log("Y 버튼을 눌렀다");

        joystick.action.Enable();
        joystick.action.performed += context =>
        {
            Vector2 joystickInput = context.ReadValue<Vector2>();
            Debug.Log($"조이스틱 움직임: {joystickInput}");
        };

        grip.action.Enable();
        grip.action.performed += context => Debug.Log("그랩 버튼을 눌렀다");

        trigger.action.Enable();
        trigger.action.performed += context => Debug.Log("트리거 버튼을 눌렀다");
    }

    void OnDestroy()
    {
        // 리스너 해제
        buttonA.action.performed -= context => Debug.Log("A 버튼을 눌렀다");
        buttonB.action.performed -= context => Debug.Log("B 버튼을 눌렀다");
        buttonX.action.performed -= context => Debug.Log("X 버튼을 눌렀다");
        buttonY.action.performed -= context => Debug.Log("Y 버튼을 눌렀다");
        joystick.action.performed -= context =>
        {
            Vector2 joystickInput = context.ReadValue<Vector2>();
            Debug.Log($"조이스틱 움직임: {joystickInput}");
        };
        grip.action.performed -= context => Debug.Log("그랩 버튼을 눌렀다");
        trigger.action.performed -= context => Debug.Log("트리거 버튼을 눌렀다");
    }
}

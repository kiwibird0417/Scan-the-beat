using MaskTransitions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChangeSceneByInput : MonoBehaviour
{
    public float totalTransitionTime;

    // 입력 액션 배열로 정리
    public InputActionReference[] inputActions; // 인스펙터에서 모든 액션을 배열로 추가

    private void Start()
    {
        // 모든 입력 액션 활성화 및 리스너 등록
        foreach (var inputAction in inputActions)
        {
            inputAction.action.Enable();
            inputAction.action.performed += OnInputPerformed;
        }
    }

    private void OnDestroy()
    {
        // 모든 입력 액션 리스너 해제
        foreach (var inputAction in inputActions)
        {
            inputAction.action.performed -= OnInputPerformed;
        }
    }

    private void OnInputPerformed(InputAction.CallbackContext context)
    {
        if (context.action.name == "Joystick")
        {
            Vector2 joystickInput = context.ReadValue<Vector2>();
            if (joystickInput.magnitude > 0.5f) // 입력 크기가 충분히 클 때만 반응
            {
                ChangeScene();
            }
        }
        else
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        // 현재 씬의 인덱스를 가져옴
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 현재 씬의 인덱스를 1 증가시켜서 다음 씬으로 이동
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        TransitionManager.Instance.LoadLevel("Menu");


    }

    public void PlayTransition()
    {
        TransitionManager.Instance.PlayTransition(totalTransitionTime);
    }

    public void PlayStartOfTransition()
    {
        TransitionManager.Instance.PlayStartHalfTransition(totalTransitionTime / 2);
    }

    public void PlayEndOfTransition()
    {
        TransitionManager.Instance.PlayEndHalfTransition(totalTransitionTime / 2);
    }
}

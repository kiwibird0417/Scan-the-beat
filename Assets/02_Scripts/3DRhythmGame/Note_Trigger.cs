using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
/*
public class Note_Trigger : MonoBehaviour
{
    // InputActionReference for triggers
    public InputActionReference triggerL;
    public InputActionReference triggerR;

    // 판정 관련 플래그
    private bool isTriggered = false; // 이미 판정되었는지 확인
    private bool isHovered = false;   // Hover 상태 확인

    // 효과 파티클 프리팹을 인스펙터에서 연결
    [SerializeField] private GameObject hitEffectPrefab;

    // 콤보 매니저 참조
    private ComboManager comboManager;

    private void Start()
    {
        // ComboManager 참조 가져오기
        comboManager = FindObjectOfType<ComboManager>();
    }

    private void OnEnable()
    {
        // 오브젝트가 활성화될 때마다 상태 초기화
        isTriggered = false;
        isHovered = false;

        // 입력 이벤트 연결
        Display();
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        triggerL.action.performed -= OnTriggerL;
        triggerR.action.performed -= OnTriggerR;
    }

    public void Display()
    {
        // Trigger actions 활성화 및 이벤트 연결
        triggerL.action.Enable();
        triggerL.action.performed += OnTriggerL;

        triggerR.action.Enable();
        triggerR.action.performed += OnTriggerR;
    }

    public void OnTriggerL(InputAction.CallbackContext context)
    {
        if (isHovered) HandleTrigger(); // Hover 상태일 때만 처리
    }

    public void OnTriggerR(InputAction.CallbackContext context)
    {
        if (isHovered) HandleTrigger(); // Hover 상태일 때만 처리
    }

    public void HandleTrigger()
    {
        // 이미 판정된 노트라면 아무 것도 하지 않음
        if (isTriggered) return;

        // 판정이 되면 효과 파티클 생성
        if (hitEffectPrefab != null)
        {
            // 파티클 생성
            GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            // 일정 시간 후 파티클 삭제 (예: 1초 후)
            Destroy(hitEffect, 1f); // 1초 후에 자동으로 삭제
        }

        // 콤보 성공 처리
        if (comboManager != null)
        {
            comboManager.AddCombo(); // 콤보 증가
        }

        // 자신만 비활성화하고 풀로 반환
        ObjectPool_3D.instance.ReturnNote(gameObject); // ObjectPool에 반환

        // 판정 상태로 변경
        isTriggered = true;
    }

    public void OnHoverEntered()
    {
        isHovered = true; // Hover 상태 시작
    }

    public void OnHoverExited()
    {
        isHovered = false; // Hover 상태 종료
    }
}
*/
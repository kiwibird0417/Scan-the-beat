using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LeftController_NoteTrigger : MonoBehaviour
{
    // InputActionReference for triggers
    public InputActionReference triggerL;

    // 판정 관련 플래그
    private bool isTriggeredL = false; // 이미 판정되었는지 확인
    private bool isHoveredL = false;   // Hover 상태 확인

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
        isTriggeredL = false;
        isHoveredL = false;

        // 입력 이벤트 연결
        LeftAction();
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        triggerL.action.performed -= OnTriggerL;
    }

    public void LeftAction()
    {
        // Trigger actions 활성화 및 이벤트 연결
        triggerL.action.Enable();
        triggerL.action.performed += OnTriggerL;

    }

    public void OnTriggerL(InputAction.CallbackContext context)
    {
        if (isHoveredL) LeftHandleTrigger(); // Hover 상태일 때만 처리
    }

    public void LeftHandleTrigger()
    {
        // 이미 판정된 노트라면 아무 것도 하지 않음
        if (isTriggeredL) return;

        // 판정이 되면 효과 파티클 생성
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, 1f); // 1초 후에 자동으로 삭제
        }

        // 콤보 성공 처리
        if (comboManager != null)
        {
            comboManager.AddCombo(); // 콤보 증가
        }

        // 자신만 비활성화하고 풀로 반환
        LeftNote_3D noteComponent = GetComponent<LeftNote_3D>(); // Note 컴포넌트 가져오기
        if (noteComponent != null)
        {
            ObjectPool_3D.instance.ReturnNote(gameObject, noteComponent.NoteType); // 노트 유형 전달
        }
        else
        {
            Debug.LogError("Note component is missing on this object.");
        }

        // 판정 상태로 변경
        isTriggeredL = true;
    }

    public void OnHoverEntered()
    {
        isHoveredL = true; // Hover 상태 시작
    }

    public void OnHoverExited()
    {
        isHoveredL = false; // Hover 상태 종료
    }
}

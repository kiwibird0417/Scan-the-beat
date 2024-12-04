using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class RightController_NoteTrigger : MonoBehaviour
{
    public InputActionReference triggerR;

    // 판정 관련 플래그
    private bool isTriggerR = false; // 이미 판정되었는지 확인
    private bool isHoveredR = false;   // Hover 상태 확인

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
        isTriggerR = false;
        isHoveredR = false;

        // 입력 이벤트 연결
        RightAction();
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        triggerR.action.performed -= OnTriggerR;
    }

    public void RightAction()
    {
        triggerR.action.Enable();
        triggerR.action.performed += OnTriggerR;
    }


    public void OnTriggerR(InputAction.CallbackContext context)
    {
        if (isHoveredR) RightHandleTrigger();
    }


    public void RightHandleTrigger()
    {
        // 이미 판정된 노트라면 아무 것도 하지 않음
        if (isTriggerR) return;

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
        RightNote_3D noteComponent = GetComponent<RightNote_3D>(); // Note 컴포넌트 가져오기
        if (noteComponent != null)
        {
            ObjectPool_3D.instance.ReturnNote(gameObject, noteComponent.NoteType); // 노트 유형 전달
        }
        else
        {
            Debug.LogError("Note component is missing on this object.");
        }

        // 판정 상태로 변경
        isTriggerR = true;
    }


    public void OnHoverEntered()
    {
        isHoveredR = true; // Hover 상태 시작
    }

    public void OnHoverExited()
    {
        isHoveredR = false; // Hover 상태 종료
    }
}

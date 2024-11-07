using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CubeInteraction : MonoBehaviour
{
    private XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        // 선택되었을 때 이벤트 연결
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDestroy()
    {
        // 이벤트 해제 (메모리 누수 방지)
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 큐브를 제거하고 로그 출력
        Debug.Log("큐브를 없앴다");
        Destroy(gameObject);
    }
}


using UnityEngine;
using DG.Tweening;

public class BlinkEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Start()
    {
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.DOFade(0, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetAutoKill(true); // 애니메이션 종료 시 자동 정리

    }

    void OnDestroy()
    {
        // 이 컴포넌트가 파괴될 때 애니메이션 강제 종료
        DOTween.Kill(canvasGroup);
    }
}

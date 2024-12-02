using UnityEngine;
using DG.Tweening;

public class Note_3D : MonoBehaviour
{
    private Vector3[] pathPoints;   // 이동 경로
    public float moveDuration = 2f; // 이동 시간

    public void InitializePath(Vector3 startPoint, Vector3 endPoint, float curveHeight)
    {
        // V자 경로 계산
        Vector3 middlePoint1 = startPoint + new Vector3(-curveHeight, 0, -(endPoint.z - startPoint.z) / 2);
        Vector3 middlePoint2 = startPoint + new Vector3(curveHeight, 0, -(endPoint.z - startPoint.z) / 2);

        pathPoints = new Vector3[] { startPoint, middlePoint1, middlePoint2, endPoint };

        // DoTween을 이용해 경로 이동
        transform.DOPath(pathPoints, moveDuration, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() => HideNote());
    }

    private void HideNote()
    {
        // 노트를 비활성화
        gameObject.SetActive(false);

        // ObjectPool에 반환
        ObjectPool_3D.instance.ReturnNote(gameObject);
    }
}

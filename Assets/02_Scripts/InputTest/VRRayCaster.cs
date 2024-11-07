using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRRaycaster : MonoBehaviour
{
    public XRController controller;  // VR 컨트롤러
    public LineRenderer lineRenderer; // 광선 시각화(LineRenderer)
    public float maxRayDistance = 10f; // 광선 최대 거리

    private RaycastHit hitInfo;

    void Update()
    {
        // 광선 발사
        Ray ray = new Ray(controller.transform.position, controller.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.green);  // Debug용 시각화

        // Raycast로 충돌된 오브젝트를 찾기
        if (Physics.Raycast(ray, out hitInfo, maxRayDistance))
        {
            // LineRenderer를 통해 광선 시각화
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, hitInfo.point);

            // 만약 trigger 버튼을 눌렀을 때
            if (Input.GetButtonDown("Fire1"))  // Fire1은 일반적으로 트리거 버튼
            {
                // 큐브를 Destroy
                if (hitInfo.collider.CompareTag("Cube"))  // 큐브라면
                {
                    Debug.Log("큐브입니다!");
                    Destroy(hitInfo.collider.gameObject);  // 큐브 삭제
                }
            }
        }
        else
        {
            // 광선이 닿지 않으면 LineRenderer 비활성화
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ray.direction * maxRayDistance);
        }
    }
}

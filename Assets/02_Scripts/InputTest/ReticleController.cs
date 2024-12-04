using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public GameObject reticlePrefab; // 레티클 프리팹
    private GameObject reticleInstance;
    public LayerMask interactableLayer; // 닿을 수 있는 레이어 지정

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward); // 컨트롤러 방향으로 레이캐스트
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            // 레티클 표시
            if (reticleInstance == null)
            {
                reticleInstance = Instantiate(reticlePrefab);
            }

            reticleInstance.transform.position = hit.point;
            reticleInstance.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            // 레티클 숨김
            if (reticleInstance != null)
            {
                Destroy(reticleInstance);
            }
        }
    }
}

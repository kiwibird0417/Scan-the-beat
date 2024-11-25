using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GazeBlinkDetector : MonoBehaviour
{
    [SerializeField] private GameObject tempObject;

    public XRRayInteractor rayInteractor; // XR Ray Interactor 연결

    private float gazeTime = 0f;
    private bool isGazing = false;

    public float gazeThreshold = 5f;

    void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            isGazing = true;
            gazeTime += Time.deltaTime;

            if (gazeTime >= gazeThreshold)
            {
                Debug.Log("눈 감김 감지: 화면 전환 트리거");
                // 화면 전환 로직 추가

                tempObject.SetActive(true);
            }
        }
        else
        {
            isGazing = false;
            gazeTime = 0f;
        }
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RayDirectionChecker : MonoBehaviour
{
    public int directionType;
    //Type정의
    //0 : 왼쪽
    //1 : 오른쪽
    //2 : 위
    //3 : 아래

    private Vector3 enterLocalPosition;
    private Vector3 exitLocalPosition;

    private bool isEnterTrue;

    private void OnEnable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.firstHoverEntered.AddListener(OnFirstHoverEntered);
        interactable.lastHoverExited.AddListener(OnLastHoverExited);
    }

    private void OnDisable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.firstHoverEntered.RemoveListener(OnFirstHoverEntered);
        interactable.lastHoverExited.RemoveListener(OnLastHoverExited);
    }

    private void OnFirstHoverEntered(HoverEnterEventArgs args)
    {
        // 진입 위치를 오브젝트의 로컬 좌표계로 변환
        enterLocalPosition = transform.InverseTransformPoint(args.interactorObject.transform.position);


        #region 진입 방향에 대한 성공/실패 여부 판단
        //0 : 왼쪽에서 진입
        if (directionType == 0)
        {
            if (enterLocalPosition.x < 0)
            {
                Debug.Log("오른쪽 진입 성공");
                isEnterTrue = true;
            }
            else
            {
                Debug.Log("오른쪽 진입 실패");
                gameObject.SetActive(false);
                isEnterTrue = false;
            }
        }
        else if (directionType == 1) //1 : 오른쪽에서 진입
        {
            if (enterLocalPosition.x > 0)
            {
                Debug.Log("왼쪽 진입 성공");
                isEnterTrue = true;
            }
            else
            {
                Debug.Log("왼쪽 진입 실패");
                gameObject.SetActive(false);
                isEnterTrue = false;
            }
        }
        else if (directionType == 2) //2 : 아래쪽에서 진입 
        {
            if (enterLocalPosition.y < 0)
            {
                Debug.Log("아래쪽 진입 성공");
                isEnterTrue = true;
            }
            else
            {
                Debug.Log("아래쪽 진입 실패");
                gameObject.SetActive(false);
                isEnterTrue = false;
            }
        }
        else if (directionType == 3) //3 : 위쪽에서 진입
        {
            if (enterLocalPosition.y < 0)
            {
                Debug.Log("위쪽 진입 성공");
                isEnterTrue = true;
            }
            else
            {
                Debug.Log("위쪽 진입 실패");
                gameObject.SetActive(false);
                isEnterTrue = false;
            }
        }
    }
    #endregion

    private void OnLastHoverExited(HoverExitEventArgs args)
    {
        // 이탈 위치를 오브젝트의 로컬 좌표계로 변환
        exitLocalPosition = transform.InverseTransformPoint(args.interactorObject.transform.position);

        if (isEnterTrue)
        {
            // 왼쪽에서 진입, 오른쪽으로 나감
            if (directionType == 0)
            {
                if (exitLocalPosition.x < 0)
                {
                    Debug.Log("오른쪽 이탈 성공!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("실패!");
                    gameObject.SetActive(false);
                }
            }
            else if (directionType == 1)    //오른쪽에서 진입, 왼쪽으로 나감
            {
                if (exitLocalPosition.x > 0)
                {
                    Debug.Log("왼쪽 이탈 성공!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("실패!");
                    gameObject.SetActive(false);
                }
            }
            else if (directionType == 2)    //위에서 진입, 아래로 나감
            {
                if (exitLocalPosition.y < 0)
                {
                    Debug.Log("위쪽 이탈 성공!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("실패!");
                    gameObject.SetActive(false);
                }
            }
            else if (directionType == 3)    //아래에서 진입, 위로 나감
            {
                if (exitLocalPosition.y < 0)
                {
                    Debug.Log("아래쪽 이탈 성공!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("실패!");
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("실패!");
            gameObject.SetActive(false);
        }
    }
}
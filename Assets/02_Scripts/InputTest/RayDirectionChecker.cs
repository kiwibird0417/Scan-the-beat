using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.InputSystem;

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

    //--------------------------------------------------
    public InputActionReference triggerL;
    public InputActionReference triggerR;

    private Hold holdL;
    private Hold holdR;

    //--------------------------------------------------
    void Start()
    {
        // Hold 클래스 인스턴스 생성
        holdL = new Hold(triggerL);
        holdR = new Hold(triggerR);

        // Hold 클래스 활성화
        holdL.Enable();
        holdR.Enable();
    }
    //===========================================================
    /*
    void Update()
    {
        // 트리거 L 버튼이 눌려져 있는 상태 확인
        if (holdL.IsHeld)
        {
            Debug.Log("왼쪽 트리거 버튼이 눌려져 있다");
        }

        // 트리거 R 버튼이 눌려져 있는 상태 확인
        if (holdR.IsHeld)
        {
            Debug.Log("오른쪽 트리거 버튼이 눌려져 있다");
        }
    }
    */

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        holdL.Disable();
        holdR.Disable();
    }


    //===================================================================
    #region 들어오는 방향, 나가는 방향
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

        if (holdL.IsHeld || holdR.IsHeld)
        {
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
        else
        {
            Debug.Log("누르지를 않아서 실패다.");
            gameObject.SetActive(false);
        }
    }
    #endregion

    private void OnLastHoverExited(HoverExitEventArgs args)
    {
        // 이탈 위치를 오브젝트의 로컬 좌표계로 변환
        exitLocalPosition = transform.InverseTransformPoint(args.interactorObject.transform.position);

        if (holdL.IsHeld || holdR.IsHeld)
        {
            Debug.Log("버튼에서 손 안 떼서 실패!");
            gameObject.SetActive(false);
        }
        else
        {
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
    #endregion
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference trigger;
    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindAnyObjectByType<TimingManager>();

        trigger.action.Enable();
        trigger.action.performed += context => CheckJudgement();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //판정 체크.
            CheckJudgement();
        }
    }

    void CheckJudgement()
    {
        theTimingManager.CheckTiming();
    }


    void OnDestroy()
    {
        trigger.action.performed -= context => CheckJudgement();
    }

}

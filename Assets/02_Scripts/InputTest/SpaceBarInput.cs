using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceBarInput : MonoBehaviour
{
    /*
    public InputActionReference SpaceBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpaceBar.action.Enable();
        SpaceBar.action.performed += JumpLog;

    }

    void JumpLog(InputAction.CallbackContext context)
    {
        Debug.Log("Space Bar Pressed!");
    }

    void OnDestroy()
    {
        SpaceBar.action.performed -= JumpLog;
    }
    */

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Debug.Log("Space Bar Pressed!");
        }


    }
}

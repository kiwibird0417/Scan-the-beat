using System;
using UnityEditor.Build.Content;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    private bool obtained; // Note가 눌렸는지 여부를 추적하는 변수

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;


    void Start()
    {
        obtained = false; // 초기값을 false로 설정
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                obtained = true; // Note가 눌렸으므로 obtained를 true로 설정
                gameObject.SetActive(false); // Note 비활성화

                //FourLine_GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.y) > 2.94)
                {
                    Debug.Log("Hit");
                    FourLine_GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 2.7)
                {
                    Debug.Log("Good");
                    FourLine_GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    FourLine_GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            // obtained가 false일 때만 NoteMissed를 호출
            if (!obtained)
            {
                FourLine_GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }

    // 노트가 다시 활성화될 때 obtained 값을 초기화
    public void ResetNote()
    {
        obtained = false;
        canBePressed = false;

        // 미스 효과를 생성하고, 다시 활성화하기 위해 SetActive(true) 호출
        gameObject.SetActive(true); // 노트 객체를 다시 활성화
    }

}

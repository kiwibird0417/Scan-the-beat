using UnityEngine;


public class RightNote_3D : MonoBehaviour
{
    public float noteSpeed = 10f; // Z축 이동 속도
    private ComboManager comboManager;
    public string NoteType = "Right";


    void Start()
    {
        // ComboManager 찾기
        comboManager = FindObjectOfType<ComboManager>();
    }


    void Update()
    {
        // Z축 방향으로 이동
        transform.position += Vector3.back * noteSpeed * Time.deltaTime;
    }


    public void HideNote()
    {
        // 노트를 비활성화
        gameObject.SetActive(false);
    }


    // 미스 박스와 충돌한 경우 콤보 초기화 및 노트를 비활성화
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MissBox"))
        {
            if (comboManager != null)
            {
                Debug.Log("콤보 초기화");
                comboManager.ResetCombo(); // 콤보 초기화
            }


            Debug.Log("노트 비활성화");
            HideNote(); // 노트 숨기기 (비활성화)

            Debug.Log("오브젝트 풀링 반환");
            ObjectPool_3D.instance.ReturnNote(gameObject, NoteType); // 풀에 반환
        }
    }
}

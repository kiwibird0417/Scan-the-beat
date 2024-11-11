using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;  // 비트(박자) 단위로 설정된 bpm
    double currentTime = 0d;

    [SerializeField] Transform tfNoteAppear = null;  // 노트 생성 위치
    [SerializeField] GameObject goNote = null;  // 생성할 노트 프리팹

    TimingManager theTimingManager;  // 타이밍 매니저 참조

    private bool canCreateNotes = true;  // 노트 생성 가능 여부를 나타내는 변수

    void Start()
    {
        theTimingManager = GetComponent<TimingManager>();  // 타이밍 매니저 초기화
    }

    void Update()
    {
        // 노트 생성이 가능한 경우에만 실행
        if (canCreateNotes)
        {
            currentTime += Time.deltaTime;

            // BPM에 맞춰 노트를 생성
            if (currentTime >= 60d / bpm)
            {
                // 오브젝트 풀에서 노트 가져오기
                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                t_note.transform.position = tfNoteAppear.position;  // 노트 생성 위치 설정

                // 원래 크기를 유지하도록 설정 (스케일을 1로)
                t_note.transform.localScale = Vector3.one;

                t_note.SetActive(true);  // 노트 활성화
                theTimingManager.boxNoteList.Add(t_note);  // 타이밍 매니저에 노트 추가

                currentTime -= 60d / bpm;  // 주기 설정
            }
        }
    }

    // 노트 생성 중지 함수
    public void StopNoteCreation()
    {
        canCreateNotes = false;  // 노트 생성 중지
    }

    // 노트가 범위를 벗어나면 제거하는 함수
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            // 노트가 범위를 벗어나면 타이밍 매니저에서 제거
            theTimingManager.boxNoteList.Remove(collision.gameObject);

            // 오브젝트 풀에 노트를 다시 추가
            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);

            // 노트를 비활성화 (없애지 않고)
            collision.gameObject.SetActive(false);
        }
    }

    // 노트 생성 재개 함수
    public void ResumeNoteCreation()
    {
        canCreateNotes = true;  // 노트 생성 재개
    }
}

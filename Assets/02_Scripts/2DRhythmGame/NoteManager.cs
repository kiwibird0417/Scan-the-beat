using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    [SerializeField] Transform tfNoteAppear = null; // 생성 위치를 지정하는 Transform
    [SerializeField] GameObject goNote = null; // 생성할 프리팹

    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 60d / bpm)
        {
            // 프리팹 생성
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform, false);

            theTimingManager.boxNoteList.Add(t_note);

            // 위치 재설정
            t_note.transform.position = tfNoteAppear.position; // tfNoteAppear의 위치를 강제로 적용

            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}

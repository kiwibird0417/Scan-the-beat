using UnityEngine;
using TMPro;

public class CenterFlame : MonoBehaviour
{
    AudioSource myAudio;
    bool musicStart = false;

    // NoteManager에 음악이 끝났음을 전달하기 위한 참조
    [SerializeField] NoteManager noteManager;
    [SerializeField] TMP_Text resultText = null;  // 타이밍 결과 텍스트

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.loop = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                myAudio.Play();
                musicStart = true;
            }
        }
    }

    private void Update()
    {
        // 음악이 끝나면 NoteManager에 종료 신호를 보냄
        if (!myAudio.isPlaying && musicStart)
        {
            musicStart = false;
            noteManager.StopNoteCreation(); // 음악 종료 후 노트 생성 중지

            resultText.text = "Music Ended";
            Debug.Log("Music Ended!");

            // 음악 볼륨을 0으로 설정하여 소리 없애기
            myAudio.volume = 0;

            // 스크립트를 비활성화하여 재생 중지 후 더 이상 실행되지 않도록 설정
            enabled = false;
        }
    }
}

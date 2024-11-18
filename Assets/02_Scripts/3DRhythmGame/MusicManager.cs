using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    private bool isPlaying = false;

    // UI 관련 변수
    [SerializeField] private TMP_Text resultText;

    // ComboManager를 참조
    [SerializeField] private ComboManager comboManager;

    public InputActionReference buttonA;

    void Start()
    {
        // 각 입력에 대한 리스너 등록
        buttonA.action.Enable();
        buttonA.action.performed += context => PlayMusic();
    }

    void OnDestroy()
    {
        // 리스너 해제
        buttonA.action.performed -= context => PlayMusic();
    }

    void Update()
    {
        // 스페이스바가 눌렸을 때 음악 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayMusic();
        }

        // 노래가 끝났는지 확인
        if (isPlaying && !musicSource.isPlaying && musicSource.time >= musicSource.clip.length)
        {
            OnMusicEnd();
        }
    }

    public void PlayMusic()
    {
        if (!isPlaying)
        {
            musicSource.Play();
            isPlaying = true;
        }
    }

    public void StopMusic()
    {
        if (isPlaying)
        {
            musicSource.Stop();
            isPlaying = false;
        }
    }

    public bool IsMusicPlaying()
    {
        return isPlaying && musicSource.isPlaying;
    }

    private void OnMusicEnd()
    {
        // 음악 종료 처리
        isPlaying = false;
        ShowResult(); // 결과 표시
    }

    private void ShowResult()
    {
        // 콤보 수 가져오기
        int currentCombo = comboManager.GetCombo();

        // 퍼포먼스 평가
        float comboPercentage = (float)currentCombo / comboManager.maxCombo * 100;

        if (comboPercentage == 100)
        {
            resultText.text = "Perfect!";
        }
        else if (comboPercentage >= 80)
        {
            resultText.text = "Great!";
        }
        else if (comboPercentage >= 50)
        {
            resultText.text = "Good!";
        }
        else
        {
            resultText.text = "You can do better next time!";
        }

        // 결과 텍스트 활성화
        resultText.gameObject.SetActive(true);
    }
}

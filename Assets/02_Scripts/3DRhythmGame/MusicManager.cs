using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    private bool isPlaying = false;

    // UI 관련 변수
    [SerializeField] private TMP_Text resultText;

    // ComboManager를 참조
    [SerializeField] private ComboManager comboManager;

    // MusicData ScriptableObject 참조
    [SerializeField] private MusicData musicData;

    public InputActionReference buttonA;

    void Start()
    {
        // 음악, Skybox 및 Fog 색상 설정
        if (musicData != null)
        {
            musicSource.clip = musicData.audioClip;
            RenderSettings.skybox = musicData.skyboxMaterial;
            RenderSettings.fogColor = musicData.fogColor; // Fog 색상 설정
        }

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

    // BPM을 외부에서 사용할 수 있게 공개하는 프로퍼티
    public int GetBPM()
    {
        return musicData != null ? musicData.bpm : 120; // 기본값 120
    }
}

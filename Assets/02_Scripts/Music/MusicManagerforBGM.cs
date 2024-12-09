using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerforBGM : MonoBehaviour
{
    private static MusicManagerforBGM instance;
    private AudioSource audioSource;

    // 로비/메인씬 음악 클립
    public AudioClip lobbyAndMainMusic;

    private string currentSceneName;
    private float savedMusicTime = 0f;

    private void Awake()
    {
        // Singleton 패턴 적용
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newSceneName = scene.name;

        // 씬 변경 확인
        if (newSceneName != currentSceneName)
        {
            currentSceneName = newSceneName;

            if (newSceneName == "BasicGame")
            {
                // 메인 게임 씬에서는 음악 정지
                savedMusicTime = audioSource.time; // 현재 재생 시간 저장
                audioSource.Pause();
            }
            else if (newSceneName == "MainTitle" || newSceneName == "Menu")
            {
                // 로비/메인씬 음악 재개
                PlayMusic(lobbyAndMainMusic, savedMusicTime);
            }
        }
    }

    public void PlayMusic(AudioClip clip, float startTime = 0f)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.time = startTime;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

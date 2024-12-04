using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using MaskTransitions;

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

    [SerializeField] private float transitionTime = 1f; // 트랜지션 지속 시간

    void Start()
    {
        if (musicData != null)
        {
            musicSource.clip = musicData.audioClip;
            RenderSettings.skybox = musicData.skyboxMaterial;
            RenderSettings.fogColor = musicData.fogColor;
        }

        StartCoroutine(PlayMusicAfterDelay(3f));
    }

    void Update()
    {
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

    private void OnMusicEnd()
    {
        isPlaying = false;
        ShowResult();

        // 1초 후에 트랜지션 시작 및 씬 전환
        StartCoroutine(TransitionToMenuAfterDelay(1f));
    }

    private void ShowResult()
    {
        int currentCombo = comboManager.GetCombo();
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

        resultText.gameObject.SetActive(true);
    }

    private IEnumerator PlayMusicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayMusic();
    }

    private IEnumerator TransitionToMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        TransitionManager.Instance.LoadLevel("Menu");
    }

    public bool IsMusicPlaying()
    {
        return musicSource != null && musicSource.isPlaying;
    }

}

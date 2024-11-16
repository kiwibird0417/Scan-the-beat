using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    private bool isPlaying = false;

    void Update()
    {
        // 스페이스바가 눌렸을 때 음악 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayMusic();
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
}

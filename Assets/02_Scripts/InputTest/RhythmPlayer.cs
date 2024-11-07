using UnityEngine;

public class RhythmPlayer : MonoBehaviour
{
    public AudioSource audioSource;  // AudioSource 컴포넌트
    public AudioClip[] sfxClips;     // SFX 파일 배열 (5개 파일)

    // 특정 번호에 해당하는 SFX를 재생하는 함수
    public void PlaySFX(int sfxNumber)
    {
        if (sfxNumber >= 1 && sfxNumber <= 5)
        {
            audioSource.clip = sfxClips[sfxNumber - 1];  // 번호에 맞는 SFX를 선택
            audioSource.Play();  // SFX 재생
        }
        else
        {
            Debug.LogWarning("SFX 번호는 1에서 5까지여야 합니다.");
        }
    }
}

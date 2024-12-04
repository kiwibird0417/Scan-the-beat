using UnityEngine;

[CreateAssetMenu(fileName = "NewMusicData", menuName = "Music/Create New Music Data")]
public class MusicData : ScriptableObject
{
    public AudioClip audioClip;        // 음악 파일
    public int bpm;                    // 곡의 BPM
    public Material skyboxMaterial;    // Skybox에 사용할 Material
    public Color fogColor;             // Fog 색상
}

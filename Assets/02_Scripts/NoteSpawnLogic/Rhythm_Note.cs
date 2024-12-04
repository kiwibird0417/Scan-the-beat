using UnityEngine;

[System.Serializable]
public class Rhythm_Note
{
    public float time;  // 노트가 등장하는 시간
    public string noteType;  // 노트 유형 (Left, Right)
}

[CreateAssetMenu(fileName = "New Rhythm Map", menuName = "Rhythm Game/Rhythm Map")]
public class RhythmMap : ScriptableObject
{
    public Rhythm_Note[] notes;  // 노트 배열
}

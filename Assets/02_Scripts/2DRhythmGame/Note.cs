using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;
    public Color color1 = Color.red;
    public Color color2 = Color.blue;

    private UnityEngine.UI.Image noteImage;

    void OnEnable()
    {
        if (noteImage == null)
        {
            noteImage = GetComponent<UnityEngine.UI.Image>();
        }

        noteImage.color = Random.value > 0.5f ? color1 : color2;
        noteImage.enabled = true;
    }

    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    public int GetNoteType()
    {
        // Red = 1, Blue = 2
        if (noteImage.color == color1) return 1;
        if (noteImage.color == color2) return 2;
        return 0; // Default or no color match
    }
}

using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public int maxCombo;
    private int currentCombo;
    public TMPro.TextMeshProUGUI comboText;

    void Start()
    {
        currentCombo = 0;
        UpdateComboText();
    }

    public void AddCombo()
    {
        currentCombo++;
        UpdateComboText();
    }

    public void ResetCombo()
    {
        currentCombo = 0; // 콤보 수 초기화
        UpdateComboText();
    }

    private void UpdateComboText()
    {
        if (comboText != null)
        {
            comboText.text = $"Combo: {currentCombo}";
        }
    }

    // 콤보 수 반환
    public int GetCombo()
    {
        return currentCombo;
    }
}

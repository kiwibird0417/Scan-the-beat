using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material skybox1;  // Skybox 1
    public Material skybox2;  // Skybox 2
    public Material skybox3;  // Skybox 3

    // Fog 색상
    public Color fogColor1 = new Color(0.5f, 0.5f, 0.5f); // 기본 Fog 색상
    public Color fogColor2 = new Color(0.8f, 0.8f, 0.8f); // 다른 색상
    public Color fogColor3 = new Color(0.2f, 0.2f, 0.2f); // 또 다른 색상

    // 설정에 따라 skybox와 fog 색상 변경
    public void ChangeSkyboxAndFog(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0:
                RenderSettings.skybox = skybox1;
                RenderSettings.fogColor = fogColor1;
                break;
            case 1:
                RenderSettings.skybox = skybox2;
                RenderSettings.fogColor = fogColor2;
                break;
            case 2:
                RenderSettings.skybox = skybox3;
                RenderSettings.fogColor = fogColor3;
                break;
            default:
                RenderSettings.skybox = skybox1;  // 기본값
                RenderSettings.fogColor = fogColor1;
                break;
        }

        // Fog 활성화 여부 설정
        RenderSettings.fog = true;
    }
}

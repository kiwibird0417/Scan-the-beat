using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 버튼을 사용하기 위해 필요

public class SceneTransition : MonoBehaviour
{
    // 이 함수는 버튼 클릭 시 호출됩니다.
    public void OnButtonClick()
    {
        // 현재 씬의 인덱스를 가져옴
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 현재 씬의 인덱스를 1 증가시켜서 다음 씬으로 이동
        LoadNextScene(currentSceneIndex + 1);
    }

    // 씬 전환 함수
    public void LoadNextScene(int nextSceneIndex)
    {
        // 다음 씬을 로드
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("마지막 씬입니다!");
        }
    }
}

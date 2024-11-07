using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public GameObject targetPrefab; // 타겟 프리팹
    public int poolSize = 20; // 풀 크기
    private Queue<GameObject> targetPool = new Queue<GameObject>(); // 풀을 관리할 큐

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // 타겟 오브젝트를 풀에 미리 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject target = Instantiate(targetPrefab);
            target.SetActive(false); // 비활성화된 상태로 생성
            targetPool.Enqueue(target);
        }
    }

    // 풀에서 오브젝트 가져오기
    public GameObject GetTarget()
    {
        if (targetPool.Count > 0)
        {
            GameObject target = targetPool.Dequeue();
            target.SetActive(true); // 오브젝트 활성화
            return target;
        }
        return null; // 풀에 더 이상 오브젝트가 없을 경우 null 반환
    }

    // 오브젝트를 풀로 반환하기
    public void ReturnTarget(GameObject target)
    {
        target.SetActive(false);
        targetPool.Enqueue(target);
    }
}

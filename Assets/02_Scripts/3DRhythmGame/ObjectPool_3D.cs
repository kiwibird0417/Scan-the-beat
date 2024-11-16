using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_3D : MonoBehaviour
{
    public static ObjectPool_3D instance;

    [SerializeField] private GameObject notePrefab;  // 노트 프리팹
    private Queue<GameObject> notePool = new Queue<GameObject>();
    private int poolSize = 10;  // 풀의 크기 제한 (예시로 10개로 설정)

    private void Awake()
    {
        instance = this;
        InitializePool();
    }

    // 풀 초기화: 미리 오브젝트를 생성하고 비활성화하여 풀에 저장
    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject note = Instantiate(notePrefab);
            note.SetActive(false);  // 풀에 저장될 오브젝트는 비활성화 상태
            notePool.Enqueue(note);
        }
    }

    // 오브젝트를 풀에서 가져옴
    public GameObject GetNote()
    {
        if (notePool.Count > 0)
        {
            GameObject note = notePool.Dequeue();  // 풀에서 오브젝트 꺼내기
            note.SetActive(true);  // 오브젝트 활성화
            return note;
        }
        else
        {
            Debug.LogWarning("Object pool is empty, returning null.");
            return null;  // 풀에 오브젝트가 없을 때는 null 반환
        }
    }

    // 오브젝트를 풀에 반환
    public void ReturnNote(GameObject note)
    {
        note.SetActive(false);  // 오브젝트 비활성화
        notePool.Enqueue(note);  // 풀에 반환
    }
}

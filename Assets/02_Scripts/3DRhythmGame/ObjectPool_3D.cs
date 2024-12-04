using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_3D : MonoBehaviour
{
    public static ObjectPool_3D instance;

    [SerializeField] private GameObject leftNotePrefab;  // 왼쪽 노트 프리팹
    [SerializeField] private GameObject rightNotePrefab; // 오른쪽 노트 프리팹
    private Queue<GameObject> leftNotePool = new Queue<GameObject>();
    private Queue<GameObject> rightNotePool = new Queue<GameObject>();

    private int poolSize = 10; // 각 풀의 크기

    private void Awake()
    {
        instance = this;
        InitializePool(leftNotePrefab, leftNotePool);
        InitializePool(rightNotePrefab, rightNotePool);
    }

    // 특정 유형의 풀 초기화
    private void InitializePool(GameObject prefab, Queue<GameObject> pool)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject note = Instantiate(prefab);
            note.SetActive(false);
            pool.Enqueue(note);
        }
    }

    // 노트를 풀에서 가져옴
    public GameObject GetNote(string noteType)
    {
        Queue<GameObject> selectedPool = GetPoolByType(noteType);

        if (selectedPool != null && selectedPool.Count > 0)
        {
            GameObject note = selectedPool.Dequeue();
            note.SetActive(true);
            return note;
        }
        else
        {
            Debug.LogWarning($"{noteType} pool is empty.");
            return null;
        }
    }

    // 노트를 반환
    public void ReturnNote(GameObject note, string noteType)
    {
        note.SetActive(false);

        Queue<GameObject> selectedPool = GetPoolByType(noteType);
        if (selectedPool != null)
        {
            selectedPool.Enqueue(note);
        }
        else
        {
            Debug.LogError($"Invalid note type: {noteType}");
        }
    }


    // 노트 유형에 따라 풀을 선택
    private Queue<GameObject> GetPoolByType(string noteType)
    {
        if (noteType == "Left") return leftNotePool;
        if (noteType == "Right") return rightNotePool;
        Debug.LogError($"Invalid note type: {noteType}");
        return null;
    }
}

using UnityEngine;

public class Spawnerer : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // 방해물 프리팹 배열
    public float spawnIntervalMin = 1f; // 최소 생성 주기
    public float spawnIntervalMax = 3f; // 최대 생성 주기
    public Transform spawnPoint; // 방해물이 생성될 위치
    public float spawnRangeX = 2f; // X축 생성 범위

    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            SetNextSpawnTime();
        }
    }

    void SpawnObstacle()
    {
        // 랜덤한 방해물 선택
        GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        // 랜덤한 X축 위치 설정
        Vector3 spawnPosition = spawnPoint.position + new Vector3(0f, 0f, 0f);
        // 방해물 생성
        Instantiate(obstacle, spawnPosition, Quaternion.identity);
    }

    void SetNextSpawnTime()
    {
        // 랜덤한 생성 주기 설정
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
    }
}

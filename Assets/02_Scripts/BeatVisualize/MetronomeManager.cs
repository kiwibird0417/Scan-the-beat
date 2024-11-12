using UnityEngine;

public class MetronomeManager : MonoBehaviour
{
    public GameObject[] cubes;         // 4개의 큐브 오브젝트
    public float bpm = 81f;            // BPM 값
    public int beatsPerMeasure = 4;    // 박자 수 (4/4 박자)
    public float maxScale = 1.2f;      // 큐브가 커지는 최대 크기
    public float scaleSpeed = 5f;      // 큐브가 커졌다 작아지는 속도
    public float initialScale = 0.5f;  // 초기 크기
    public AudioSource musicSource;    // 음악을 재생할 AudioSource
    public AudioSource metronomeSFX;   // 메트로놈 효과음을 재생할 AudioSource

    private float beatInterval;        // 한 박자의 간격
    private float timer = 0f;          // 타이머
    private int currentBeat = 0;       // 현재 박자 인덱스
    private bool musicStarted = false; // 음악이 시작되었는지 확인하는 플래그

    void Start()
    {
        UpdateInterval();

        // 모든 큐브의 초기 크기를 설정
        foreach (var cube in cubes)
        {
            cube.transform.localScale = Vector3.one * initialScale;
        }
    }

    void Update()
    {
        // 타이머를 누적하여 진행
        timer += Time.deltaTime;

        // 박자 간격에 맞춰 큐브 애니메이션과 음악을 처리
        if (timer >= beatInterval)
        {
            AnimateCube(currentBeat);  // 큐브 크기 애니메이션
            PlayMetronomeSFX();        // 메트로놈 SFX 재생

            // 첫 박자가 실행되었을 때 음악을 재생
            if (currentBeat == 0 && !musicStarted)
            {
                musicSource.Play();
                musicStarted = true;
            }

            // 타이머를 리셋 (누적하지 않고 일정한 간격으로 박자를 맞춤)
            timer -= beatInterval;

            // 박자 인덱스를 업데이트 (루프)
            currentBeat = (currentBeat + 1) % beatsPerMeasure;
        }

        // 큐브 크기 애니메이션을 매 박자마다 부드럽게 처리
        for (int i = 0; i < cubes.Length; i++)
        {
            if (i != currentBeat)
            {
                cubes[i].transform.localScale = Vector3.Lerp(cubes[i].transform.localScale, Vector3.one * initialScale, Time.deltaTime * scaleSpeed);
            }
        }
    }

    // 큐브 크기 변화를 트리거
    private void AnimateCube(int index)
    {
        if (index < cubes.Length)
        {
            cubes[index].transform.localScale = Vector3.one * maxScale;
        }
    }

    // 메트로놈 SFX를 박자에 맞춰 재생
    private void PlayMetronomeSFX()
    {
        if (metronomeSFX != null && !metronomeSFX.isPlaying) // SFX가 재생 중이 아니면
        {
            metronomeSFX.Play();
        }
    }

    // BPM이 변경될 때마다 간격을 업데이트
    public void SetBPM(float newBPM)
    {
        bpm = newBPM;
        UpdateInterval();
    }

    // 박자를 설정할 수 있는 메서드
    public void SetBeatsPerMeasure(int beats)
    {
        beatsPerMeasure = beats;
    }

    private void UpdateInterval()
    {
        beatInterval = 60f / bpm; // 1분(60초)을 BPM 값으로 나누어 한 박자의 시간 간격을 계산
    }
}
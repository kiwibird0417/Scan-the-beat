using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime = 1f;

    void Start()
    {

    }


    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}

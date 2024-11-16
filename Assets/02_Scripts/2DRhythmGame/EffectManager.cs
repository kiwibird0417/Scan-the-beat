using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;

    string hit = "Hit";

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }
}

using UnityEngine;

public class FlexibleAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    public void Play(float destroyTime)
    {
        src.Play();
        src.pitch = Random.Range(0.8f, 1.2f);
        Destroy(gameObject, destroyTime);
    }
}

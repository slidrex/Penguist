using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    public void PlayStep() => src.Play();
}
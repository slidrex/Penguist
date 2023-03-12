using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] public AudioSource src;
    public void PlayStep() 
    {
        if(src.clip != null)
            src.Play();
    }
}

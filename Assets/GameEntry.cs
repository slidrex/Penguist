using UnityEngine;

public class GameEntry : MonoBehaviour
{
    [SerializeField] private AudioClip snowStepClip;
    private void Awake()
    {
        FindObjectOfType<FootSteps>().src.clip = snowStepClip;
    }
}

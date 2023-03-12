using UnityEngine;
using System.Collections;
using Cinemachine;
public class Some : MonoBehaviour
{
    [SerializeField] private Animator fallingBottle;
    [SerializeField] private GameObject ice;
    [SerializeField] private GameObject effect;
    private CinemachineBasicMultiChannelPerlin cam;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.AddRule(Entity.Rule.DisableMovement);
        cam = FindObjectOfType<CinemachineBasicMultiChannelPerlin>();
        StartCoroutine(Beg());
    }

    private IEnumerator Beg()
    {
        yield return new WaitForSeconds(2);
        cam.m_AmplitudeGain = 4;
        cam.m_FrequencyGain = 4;
        yield return new WaitForSeconds(0.4f);
        cam.m_AmplitudeGain = 0;
        cam.m_FrequencyGain = 0;
        fallingBottle.SetTrigger("fall");
        yield return new WaitForSeconds(1);
        Destroy(Instantiate(effect, cam.transform.position, Quaternion.identity), 2);
        player.RemoveRule(Entity.Rule.DisableMovement);
        ice.SetActive(false);
    }
}

using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.UI;
public class Begin : MonoBehaviour
{
    [SerializeField] private GameObject frost;
    [SerializeField] private Text countdown;
    private CinemachineBasicMultiChannelPerlin cam;
    private float c;

    private void Start()
    {
        cam = FindObjectOfType<CinemachineBasicMultiChannelPerlin>();
        c = int.Parse(countdown.text);
        StartCoroutine(Beg());
    }
    private void Update()
    {
        if(c > 0)
        {
            countdown.text = "" + Mathf.Ceil(c -= Time.deltaTime);
            cam.m_AmplitudeGain += Time.deltaTime / 2;
            cam.m_FrequencyGain += Time.deltaTime / 2;
        }
    }
    private IEnumerator Beg()
    {
        yield return new WaitForSeconds(10.5f);
        Instantiate(frost, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        // next scene
    }
}

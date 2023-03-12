using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
    [SerializeField] private GameObject final;
    [SerializeField] private GameObject fire;
    [SerializeField] private WeatherController weatherController;
    [SerializeField] private GameObject frost;
    [SerializeField] private Text countdown;
    [SerializeField] private GameObject fireIcon;
    [SerializeField] private GameObject frostIcon;
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
            cam.m_AmplitudeGain += Time.deltaTime / 3;
            cam.m_FrequencyGain += Time.deltaTime / 3;
        }
    }
    private IEnumerator Beg()
    {
        yield return new WaitForSeconds(15.5f);
        if(weatherController.IsDestroyed() == true)
        {
            Instantiate(fire, transform.position, Quaternion.identity);
            fireIcon.SetActive(true);
            frostIcon.SetActive(false);
        }
        else
        {
            Instantiate(frost, transform.position, Quaternion.identity);
            fireIcon.SetActive(false);
            frostIcon.SetActive(true);
        }
        yield return new WaitForSeconds(2);
        cam.m_AmplitudeGain=0;
        cam.m_FrequencyGain=0;
        GameObject f = Instantiate(final, FindObjectOfType<Camera>().transform.position, Quaternion.identity, FindObjectOfType<Canvas>().transform);
        f.transform.localPosition = new Vector3(0, 0, 0);
        FindObjectOfType<Player>().AddRule(Entity.Rule.DisableMovement);
        FindObjectOfType<Player>().AddRule(Entity.Rule.DisableInteraction);
    }
}

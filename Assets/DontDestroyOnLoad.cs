using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static GameObject firstInst;
    private void Awake()
    {
        if(firstInst == null)
            firstInst = gameObject;
        else if (gameObject != firstInst)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}

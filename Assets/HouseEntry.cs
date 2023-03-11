using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEntry : HoldingInteractableObject
{
    [SerializeField] private int buildIndex;
    public override string InteractString => "Войти";
    protected override void OnInteractSuccess()
    {
        SceneManager.LoadScene(buildIndex);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEntry : HoldingInteractableObject
{
    public override string InteractString => "Войти";
    protected override void OnInteractSuccess()
    {
        SceneManager.LoadScene(3);
    }
}

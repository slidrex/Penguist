using UnityEngine.SceneManagement;
using UnityEngine;

public class Retry : MonoBehaviour
{
    public void Again()
    {
        Destroy(gameObject);
        Player player = FindObjectOfType<Player>();
        player.RemoveRule(Entity.Rule.DisableMovement);
        player.RemoveRule(Entity.Rule.DisableInteraction);
        SceneManager.LoadScene(2);
    }
}

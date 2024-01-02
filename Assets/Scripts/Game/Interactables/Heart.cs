using UnityEngine;

public class Heart : MonoBehaviour, IPlayerInteractable
{
    public void OnPlayerContact(PlayerController playerController)
    {
        GameManager.Instance.PlayerGainedLife();
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Coin : MonoBehaviour, IPlayerInteractable
{
    public void OnPlayerContact(PlayerController playerController)
    {
        GameManager.Instance.CollectedCoins += 1;
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Hazard : MonoBehaviour
{
    private int playerLayer;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            GameManager.Instance.PlayerLostLife();
        }
    }
}

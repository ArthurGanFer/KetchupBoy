using UnityEngine;

public class Exit : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private Animator animator;

    private readonly int isClosedParamHash = Animator.StringToHash("IsClosed");
    private readonly int isOpenedParamHash = Animator.StringToHash("IsOpen");

    private bool unlocked = false;
    public bool Unlocked
    {
        get
        {
            return unlocked;
        }
        set
        {
            unlocked = value;
            if (unlocked)
            {
                animator.SetTrigger(isOpenedParamHash);
            }
            else
            {
                animator.SetTrigger(isClosedParamHash);
            }
        }
    }

    public void OnPlayerContact(PlayerController playerController)
    {
        if (unlocked)
        {
            GameManager.Instance.LevelFinished();
            GameManager.Instance.LevelFinished();
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnLevelCompleted += Unlock;
        GameManager.Instance.OnLevelCompleted += PlaySoundSFX;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelCompleted -= Unlock;
        GameManager.Instance.OnLevelCompleted -= PlaySoundSFX;
    }

    private void Unlock()
    {
        Unlocked = true;
    }

    private void PlaySoundSFX()
    {
        Debug.Log("Pretend this is a cool sound effect.");
    }
}

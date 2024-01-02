using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator fadeAnimator;

    private int totalLives = 3;
    public int TotalLives
    {
        get
        {
            return totalLives;
        }
        private set
        {
            totalLives = value;
            OnTotalHeartsChanged?.Invoke();
        }
    }

    public delegate void OnTotalHeartsChange();
    public OnTotalHeartsChange OnTotalHeartsChanged;

    private bool levelComplete = false;
    public bool LevelComplete
    {
        get
        {
            return levelComplete;
        }
        set
        {
            levelComplete = value;
            if (levelComplete)
            {
                OnLevelCompleted?.Invoke();
            }
        }
    }

    public delegate void OnLevelComplete();
    public OnLevelComplete OnLevelCompleted;

    private int requiredCoins;
    public int RequiredCoins
    {
        get
        {
            return requiredCoins;
        }
        private set
        {
            requiredCoins = value;
            OnRequiredCoinsChanged?.Invoke();
        }
    }

    public delegate void OnRequiredCoinsChange();
    public OnRequiredCoinsChange OnRequiredCoinsChanged;

    private int collectedCoins = 0;
    public int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }
        set
        {
            collectedCoins = value;

            TotalCoins += 1;

            if (collectedCoins == requiredCoins)
            {
                LevelComplete = true;
            }
        }
    }

    private int totalCoins = 0;
    public int TotalCoins {
        get { return totalCoins; } 
        private set 
        { 
            totalCoins = value;
            OnTotalCoinsChanged?.Invoke();
        }
    }

    public delegate void OnTotalCoinsChange();
    public OnTotalCoinsChange OnTotalCoinsChanged;

    private Vector3 resetPosition;
    private Quaternion resetRotation;

    public delegate void OnPlayerReset();
    public OnPlayerReset OnPlayerWasReset;

    public delegate void OnLevelFinish();
    public OnLevelFinish OnLevelFinished;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeAnimator = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
        fadeAnimator.Play("FadeIn");

        if (SceneManager.GetActiveScene().name.StartsWith("Level"))
        {
            FindPlayerAndSetSpawn();

            requiredCoins = FindObjectsOfType<Coin>().Length;
            collectedCoins = 0;
            TotalCoins = 0;

            OnTotalCoinsChanged?.Invoke();
            OnPlayerWasReset?.Invoke();
            OnRequiredCoinsChanged?.Invoke();

            SetPlayerActiveState(false);
            StartCoroutine(ActivatePlayer());
        }
    }

    private IEnumerator ActivatePlayer()
    {
        yield return new WaitForSeconds(4);
        SetPlayerActiveState(true);
    }

    private void FindPlayerAndSetSpawn()
    {
        playerController = FindObjectOfType<PlayerController>();
        Assert.IsNotNull(playerController);

        Transform playerTransform = playerController.transform;

        resetPosition = playerTransform.position;
        resetRotation = playerTransform.rotation;
    }

    private void SetPlayerActiveState(bool active)
    {
        playerController.enabled = active;

        Rigidbody2D body = playerController.GetComponent<Rigidbody2D>();
        body.isKinematic = !active;

        if (!active)
        {
            
            body.velocity = Vector2.zero;
            body.angularVelocity = 0;
        }
    }

    public void PlayerLostLife()
    {
        TotalLives = Mathf.Clamp(TotalLives - 1, 0, 3);

        if (TotalLives == 0)
        {
            LoadSceneIndex(SceneManager.sceneCountInBuildSettings - 1);
        }

        ResetPlayer();
    }

    public void PlayerGainedLife()
    {
        TotalLives = Mathf.Clamp(TotalLives + 1, 0, 3);
    }

    private void ResetPlayer()
    {
        playerController.transform.SetPositionAndRotation(resetPosition, resetRotation);
        Rigidbody2D body = playerController.GetComponent<Rigidbody2D>();
        if (body)
        {
            body.velocity = Vector2.zero;
            body.angularVelocity = 0;
        }

        OnPlayerWasReset?.Invoke();
    }

    public void LevelFinished()
    {
        SetPlayerActiveState(false);
        OnLevelFinished?.Invoke();
    }

    public void LoadSceneIndex(int index)
    {
        fadeAnimator.Play("FadeOut");
        StartCoroutine(LoadSceneDelayed(index, 0));
    }

    public void LoadNextSceneIndex(float delay = 0)
    {
        fadeAnimator.Play("FadeOut");
        delay += fadeAnimator.GetCurrentAnimatorStateInfo(0).length;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadSceneDelayed(nextSceneIndex, delay));
    }

    private IEnumerator LoadSceneDelayed(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

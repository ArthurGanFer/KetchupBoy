using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator levelTextAnimator;
    [SerializeField] private TMP_Text firstText;
    [SerializeField] private TMP_Text secondText;

    [SerializeField] private TMP_Text coinCountText;
    [SerializeField] private Image[] heartIcons;

    private int maxHearts = 0;
    private int currentHearts = 0;

    private void OnEnable()
    {
        GameManager.Instance.OnTotalCoinsChanged += UpdateCoinText;
        GameManager.Instance.OnTotalHeartsChanged += UpdateHeartIcons;
        GameManager.Instance.OnLevelFinished += UpdateLevelText;
        GameManager.Instance.OnRequiredCoinsChanged += UpdateInstructionText;

        maxHearts = heartIcons.Length;
        currentHearts = GameManager.Instance.TotalLives;
    }

    private void UpdateCoinText()
    {
        coinCountText.text = GameManager.Instance.TotalCoins.ToString("D2") + "/" + GameManager.Instance.RequiredCoins.ToString("D2");
    }

    private void UpdateInstructionText()
    {
        firstText.text = "Collect " + GameManager.Instance.RequiredCoins + " Coins";
    }

    private void UpdateHeartIcons()
    {
        int heartsRemaining = GameManager.Instance.TotalLives;
        int heartIndex;

        bool activeState = heartsRemaining >= currentHearts;

        if (activeState) heartIndex = maxHearts - heartsRemaining;
        
        else heartIndex = maxHearts - currentHearts;
        

        heartIcons[heartIndex].gameObject.SetActive(activeState);
        currentHearts = heartsRemaining;
    }

    private void UpdateLevelText()
    {
        firstText.text = "Level";
        secondText.text = "Complete";
        levelTextAnimator.Play("DropIn", 0, 0f);

        float animationLength = levelTextAnimator.GetCurrentAnimatorStateInfo(0).length;
        GameManager.Instance.LoadNextSceneIndex(animationLength);
    }

    public void StartGameUI()
    {
        GameManager.Instance.LoadNextSceneIndex();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTotalCoinsChanged -= UpdateCoinText;
        GameManager.Instance.OnTotalHeartsChanged -= UpdateHeartIcons;
        GameManager.Instance.OnLevelFinished -= UpdateLevelText;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonManager : MonoBehaviour
{

    public void PressStart()
    {
        GameManager.Instance.LoadNextSceneIndex();
    }

    public void PressMainMenu()
    {
        GameManager.Instance.LoadSceneIndex(0);
    }

    public void PressExit()
    {
        GameManager.Instance.ExitGame();
    }

}

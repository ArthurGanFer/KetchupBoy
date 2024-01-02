using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretArea : MonoBehaviour
{
    private readonly int isOpenParamHash = Animator.StringToHash("IsOpen");
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CloseArea();
    }

    public void OpenArea()
    {
        animator.SetBool(isOpenParamHash, true);
    }

    public void CloseArea()
    {
        animator.SetBool(isOpenParamHash, false);
    }

}

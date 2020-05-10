using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public Animator warningTextAnimator;
    public string warningTrigger;
    private int hashedWarningTrigger;
    private void Start()
    {
        if(_instance == null)
        {
            _instance = this;
            hashedWarningTrigger = Animator.StringToHash(warningTrigger);
        }
    }

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void OnReset()
    {
        GameManager.Instance.ResetPathing();
    }

    public void TriggerWaterWarning()
    {
        warningTextAnimator.SetTrigger(hashedWarningTrigger);
    }
}

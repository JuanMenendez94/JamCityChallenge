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
            //we hash the trigger name for better performance. It's faster to read an int over a string.
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

    /*Resets Game instance*/
    public void OnReset()
    {
        GameManager.Instance.ResetPathing();
    }

    /*Shows non walkable node message*/
    public void TriggerNonWalkableWarning()
    {
        if(warningTextAnimator!= null)
        {
            warningTextAnimator.SetTrigger(hashedWarningTrigger);
        }
       
    }
}

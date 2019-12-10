using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using UnityEngine.UI;

public class CheckGesture : MonoBehaviour
{
    private bool allHandTrue;
    [SerializeField] private CheckMatch leftMatch;
    [SerializeField] private CheckMatch rightMatch;
    [SerializeField] private Text timeText;
    private float curTime;


    private void Update()
    {
        curTime += Time.deltaTime;
        if (leftMatch.GestureDone() && rightMatch.GestureDone())
        {
            timeText.text = curTime.ToString();
            leftMatch.CalculateDelta();
            rightMatch.CalculateDelta();
        }
        if (leftMatch.thisHandTrue && rightMatch.thisHandTrue)
        {
            leftMatch.SetPlay();
            rightMatch.SetPlay();
            leftMatch.RecMovement();
            rightMatch.RecMovement();
        }
        else
        {
            leftMatch.SetStop();
            rightMatch.SetStop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;

public class RecordingLogic : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button pauseButton;
    [SerializeField] float startingTime = 3f;
    [SerializeField] GameObject countdownPanel;
    [SerializeField] GameObject askNamePanel;
    [SerializeField] GameObject fantomHandRight;
    [SerializeField] GameObject fantomHandLeft;
    [SerializeField] Text countdownText;
    [SerializeField] InputField inputField;
    [SerializeField] HandMover leftHandMov;
    [SerializeField] HandMover rightHandMov;
    [SerializeField] GetTransform leftGetTrans;
    [SerializeField] GetTransform rightGetTrans;

    private bool isRecordingStart = false;
    private bool recording = false;
    private float countdownTime = 0f;
    private bool namePresent = false;

    private void Awake()
    {
        countdownTime = startingTime;

        fantomHandLeft.SetActive(false);
        fantomHandRight.SetActive(false);
        askNamePanel.SetActive(false);
        countdownPanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isRecordingStart)
        {
            countdownTime -= Time.deltaTime;
            countdownText.text = ((int)countdownTime).ToString();
        }
        if (recording)
        {
            Debug.Log("IsRecording");
        }
    }

    public void ActivateRecording()
    {
        countdownTime = startingTime;
        isRecordingStart = true;
        StartCoroutine(StartCountdown());
    }

    public void StopRecording()
    {
        StartCoroutine(PausePlaying());
    }

    private IEnumerator PausePlaying()
    {
        recording = false;
        leftGetTrans.StopRecording();
        rightGetTrans.StopRecording();
        leftHandMov.SetNewTransforms(leftGetTrans.GetRecPos(), leftGetTrans.GetRecRot());
        rightHandMov.SetNewTransforms(rightGetTrans.GetRecPos(), rightGetTrans.GetRecRot());
        fantomHandLeft.SetActive(true);
        fantomHandRight.SetActive(true);
        leftHandMov.StartPlay();
        rightHandMov.StartPlay();

        yield return new WaitUntil(() =>
        {
            return !leftHandMov.FirstCircule() && !rightHandMov.FirstCircule();
        });

        askNamePanel.SetActive(true);
        fantomHandLeft.SetActive(false);
        fantomHandRight.SetActive(false);
    }

    public void SaveGesture()
    {
        string fileName = GetFileName();
        if(fileName != "" && namePresent)
        {
            rightGetTrans.SaveGesture(fileName);
            leftGetTrans.SaveGesture(fileName);
            DeleteGesture();
        }
    }

    private string GetFileName()
    {
        Debug.Log(inputField.text + " Input field");
        return inputField.text;
    }

    public void DeleteGesture()
    {
        leftGetTrans.DeleteRecordedGest();
        rightGetTrans.DeleteRecordedGest();
        pauseButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
        askNamePanel.SetActive(false);
    }

    private IEnumerator StartCountdown()
    {
        countdownPanel.SetActive(true);
        yield return new WaitUntil(() =>
        {
            return countdownTime <= 0f;
        });
        recording = true;
        isRecordingStart = false;
        countdownPanel.SetActive(false);
        startButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        leftGetTrans.StartRecording();
        rightGetTrans.StartRecording();
    }
    public void SetName()
    {
        namePresent = true;
    }
}

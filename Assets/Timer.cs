using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    TextMeshProUGUI registeredTimerText;
    Image registeredTimerButton;

    float taskSec = 0;

    public void StartTime(GameObject registerTarget)
    {
        if (registeredTimerText != null) StopTime();

        registeredTimerText = registerTarget.transform.Find("Active/Timer").GetComponent<TextMeshProUGUI>();
        registerTarget.transform.Find("Active/Start").gameObject.SetActive(false);
        registerTarget.transform.Find("Active/Stop").gameObject.SetActive(true);
        taskSec = HMStoSec(registeredTimerText.text);
    }

    public void StopTime()
    {
       
        registeredTimerText.transform.parent.Find("Stop").gameObject.SetActive(false);
        registeredTimerText.transform.parent.transform.Find("Start").gameObject.SetActive(true);
        registeredTimerText = null;
        GetComponent<FileSaver>().SaveTask();
    }

    private void Update()
    {
        if (registeredTimerText == null) return;
        taskSec += Time.deltaTime;
        registeredTimerText.text = SecToHMS(taskSec);
        //
    }

    string SecToHMS(float sec)
    {
        float remainingSec = sec;
        int hour = Mathf.FloorToInt(remainingSec / 3600f);
        remainingSec -= 3600 * hour;
        int minute = Mathf.FloorToInt(remainingSec / 60);
        remainingSec -= 60 * minute;
        int intSec = Mathf.FloorToInt(remainingSec);
        int lessThanSec = Mathf.FloorToInt(remainingSec % 1 * 100);
        return $"{hour:d2}:{minute:d2}:{intSec:d2}.{lessThanSec:d2}";
    }

    float HMStoSec(string hms)
    {
        string[] targetString = hms.Split(":");
        int hour = int.Parse(targetString[0]);
        int minutes = int.Parse(targetString[1]);
        float sec = float.Parse(targetString[2]);
        return hour * 3600 + minutes * 60 + sec;
    }
}

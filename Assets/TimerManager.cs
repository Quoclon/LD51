using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public Image uiFill;
    public TextMeshProUGUI uiText;

    public float timer;
    public float timerMax;

    public bool timerRunning;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        RestartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning)
            return;

        uiText.text = $"{timer.ToString("F1")}";
        uiFill.fillAmount = Mathf.InverseLerp(0, timerMax, timer);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // DO STUFF -  Transition Scene, etc.
            RestartTimer();
        }
    }

    public void RestartTimer()
    {
        timer = timerMax;
        timerRunning = true;
    }
}

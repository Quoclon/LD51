using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public Image uiFill;
    public TextMeshProUGUI uiText;

    [Header("Timers")]
    public float timer;
    public float timerMax;
    public bool timerRunning;

    public bool roundOver;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartTimer(timerMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning)
            return;

        uiText.text = $"{timer.ToString("F0")}";
        uiFill.fillAmount = Mathf.InverseLerp(0, timerMax, timer);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            RoundOver();
        }
    }

    public void StartTimer(float _TimerMax)
    {
        timer = _TimerMax;
        timerRunning = true;
        roundOver = false;
    }

    public void RoundOver()
    {
        timerRunning = false;
        roundOver = true;
    }
}

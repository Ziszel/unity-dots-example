using System;
using System.Globalization;
using TMPro;
using Unity.Entities;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    [Header("Text defaults")] 
    [SerializeField] private string defaultText;

    private TMP_Text _tmpTimer;
    private TimerManagerSystem _timerManagerSystem;

    private void Start()
    {
        _tmpTimer = GetComponent<TMP_Text>();
        // Required since I cannot access SystemAPI from a Monobehaviour class
        _timerManagerSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<TimerManagerSystem>();
        
        _timerManagerSystem.OnTimeUpdate += UpdateText;
    }

    private void UpdateText(float currentTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        string currentTimeFormattedStr = timeSpan.ToString("hh':'mm':'ss", new CultureInfo("en-GB"));
        _tmpTimer.text = defaultText + currentTimeFormattedStr;
    }

    private void OnDisable()
    {
        _timerManagerSystem.OnTimeUpdate -= UpdateText;
    }
}

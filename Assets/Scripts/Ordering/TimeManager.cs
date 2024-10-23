using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public float dayDuration = 180f; // 3 minutes = 180 seconds
    private float currentTime;
    private int totalDays;
    public int validShot;
    public int totalShot;
    public bool isDayOver;
    public static TimeManager main;
    public TextMeshProUGUI dayText; // UI Text for displaying current day
    public TextMeshProUGUI timerText; // UI Text for displaying remaining time
    public TextMeshProUGUI revenueText;
    public TextMeshProUGUI orderCompleteText;
    public TextMeshProUGUI summaryDayText;
    public TextMeshProUGUI shootAccuracy;
    public GameObject summaryPanel; // A panel to show summary and wait for player input
    [SerializeField]private OrderManager orderManager;
    [SerializeField]private GameManager gameManager;
    void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }
    void Start()
    {
        totalDays = 1;
        currentTime = dayDuration;
        isDayOver = false;
        totalShot = 0;
        validShot = 0;
        UpdateDayText();
        summaryPanel.SetActive(false); // Hide the summary UI initially
    }

    void Update()
    {
        if (!isDayOver)
        {
            RunDayTimer();
        }
    }

    void RunDayTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            DisplayTime(currentTime);
        }
        else
        {
            EndOfDay();
        }
    }

    void EndOfDay()
    {
        isDayOver = true; // Mark that the day is over
        ShowSummary(); // Show the summary UI
        Time.timeScale = 0f; // Pause the game
    }

    void ShowSummary()
    {
        summaryDayText.text = Convert.ToString(totalDays);
        orderCompleteText.text = Convert.ToString(orderManager.orderCounter);
        revenueText.text = Convert.ToString(gameManager.revenue);
        if (totalShot > 0)  shootAccuracy.text = "Shoot Accuracy " + ((double)validShot / totalShot * 100).ToString("F2") + "%";  // "F2" ensures 2 decimal places
        else shootAccuracy.text = "Shoot Accuracy 0%";  // If no shots fired, display 0% accuracy
        summaryPanel.SetActive(true); // Show the summary panel
    }

    public void StartNextDay()
    {
        if (!isDayOver) return;
        isDayOver = false; // Reset the day status
        totalDays++; // Increment the day count
        UpdateDayText(); // Update the UI for the new day
        currentTime = dayDuration; // Reset the day timer
        orderManager.orderCounter = 0;
        gameManager.revenue = 0;
        totalShot = 0;
        validShot = 0;
        summaryPanel.SetActive(false); // Hide the summary panel
        Time.timeScale = 1f; // Resume the game
    }

    void UpdateDayText()
    {
        dayText.text = $"Day {totalDays}";
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Ensure the timer shows 00:00 at the end instead of stopping at 01.

        // Get minutes and seconds
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format the time string
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}

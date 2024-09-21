using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TimeManager : MonoBehaviour
{
    public float dayDuration = 180f; // 3 minutes = 180 seconds
    private float currentTime;
    private int totalDays;
    private bool isDayOver;

    public TextMeshProUGUI dayText; // UI Text for displaying current day
    public TextMeshProUGUI timerText; // UI Text for displaying remaining time
    public TextMeshProUGUI summaryText; // UI Text for displaying end-of-day summary
    public GameObject summaryPanel; // A panel to show summary and wait for player input
    [SerializeField]private OrderManager orderManager;

    void Start()
    {
        totalDays = 1;
        currentTime = dayDuration;
        isDayOver = false;
        UpdateDayText();
        summaryText.gameObject.SetActive(false); // Hide the summary UI initially
    }

    void Update()
    {
        if (!isDayOver)
        {
            RunDayTimer();
        }
        else
        {
            // Wait for player to press Enter to start the next day
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartNextDay();
            }
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
        orderManager.ClearOrderList();
        Time.timeScale = 0f; // Pause the game
    }

    void ShowSummary()
    {
        summaryText.text = $"Day {totalDays} Summary:\nEarnings: {CalculateEarnings()}";
        summaryText.gameObject.SetActive(true); // Show the summary panel
    }

    int CalculateEarnings()
    {
        // Example calculation for earnings (this can be replaced with actual logic)
        return totalDays * 10; // Example: earnings increase with each day
    }

    void StartNextDay()
    {
        isDayOver = false; // Reset the day status
        totalDays++; // Increment the day count
        UpdateDayText(); // Update the UI for the new day
        currentTime = dayDuration; // Reset the day timer
        summaryText.gameObject.SetActive(false); // Hide the summary panel
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

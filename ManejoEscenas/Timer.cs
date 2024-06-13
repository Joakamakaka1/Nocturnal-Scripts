using UnityEngine;
using TMPro;

public class Timer : Singleton<Timer>
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;

    public float ElapsedTime { get { return elapsedTime; } }

    private bool isTimerRunning = true;

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutos = Mathf.FloorToInt(elapsedTime / 60);
            int segundos = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    public void DetenerTimer()
    {
        isTimerRunning = false;
    }
}

using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI fpsText; // Assign in the inspector
    private float updateInterval = 0.5f;
    private float accum = 0;
    private int frames = 0;
    private float timeleft;

    void Start()
    {
        if (fpsText == null)
        {
            Debug.Log("Please assign a UI Text object in the inspector.");
            enabled = false; // Stop the script from running if the text object is not assigned
            return;
        }
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            float fps = accum / frames;
            string format = System.String.Format("{0:f0}", fps);
            fpsText.text = "FPS: " + format;

            if (timeleft < -updateInterval)
            {   
                timeleft = 0.0f;
            }

            timeleft += updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
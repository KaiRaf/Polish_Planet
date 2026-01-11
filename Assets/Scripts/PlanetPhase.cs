using UnityEngine;
using UnityEngine.UI;

public class PlanetPhase : MonoBehaviour
{
    public Text ProgressPercentage;
    private int counter = 0;

    public enum Phase
    {
        GarbageCleaning, ResourceRepairing, Completed 
    }

    public Phase currentPhase = Phase.GarbageCleaning; //starting phase
    [Header("Progress")]
    public Slider progressBar;
    [Header("Clean the trash")]
    public int phase1Clicks = 350;
    [Header("Repair the resources")]
    public int phase2Clicks = 1000;
                
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetPhase1();
    }

    public void OnPlanetClicked()
    {

        if (currentPhase == Phase.GarbageCleaning)
        {
            counter++;
            progressBar.value = counter;
            UpdateProgressText();
            if (counter >= phase1Clicks)
            {
                SetPhase2();
            }
        }
        else if (currentPhase == Phase.ResourceRepairing)
        {
            counter++;
            progressBar.value = counter;
            UpdateProgressText();
            if (counter >= phase2Clicks)
            {
                SetCompleted();
            }
        }
    }

    void SetPhase1()
    {
        currentPhase = Phase.GarbageCleaning;
        counter = 0;
        progressBar.value = 0;
        progressBar.maxValue = phase1Clicks;
        UpdateProgressText();
    }

    void SetPhase2()
    {
        currentPhase = Phase.ResourceRepairing;
        counter = 0;
        progressBar.value = 0;
        progressBar.maxValue = phase2Clicks;
        UpdateProgressText();
    }

    void SetCompleted()
    {
        currentPhase = Phase.Completed;
        progressBar.gameObject.SetActive(false);
    }

    void UpdateProgressText()
    {
        float percent = (progressBar.value / progressBar.maxValue) * 100f;
        ProgressPercentage.text = Mathf.RoundToInt(percent) + "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

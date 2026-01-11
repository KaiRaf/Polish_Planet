using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class game_manager_script : MonoBehaviour
{
    public static game_manager_script instance;
    private int clicks;
    private int currency; // bubbles
    private float progress; // phase 1 -> 2 = 200 clicks, phase 2 -> 3 = 1000 clicks 

    [SerializeField] private TextMeshProUGUI currency_counter;
    [SerializeField] private TextMeshProUGUI clicks_counter;
    [SerializeField] private Slider progress_bar;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        clicks = 0;
        currency = 0;
        progress = 0;
    }

    public void AddClick()
    {
        clicks++;

        if (clicks % 10 == 0)
        {
            currency++;
            currency_counter.text = $": {currency}";
        }
        Debug.Log("Clicks: " + clicks);

        clicks_counter.text = $": {clicks}";


        if (clicks % 100 == 0)
        {
            progress_bar.value++;
        }
    }
}

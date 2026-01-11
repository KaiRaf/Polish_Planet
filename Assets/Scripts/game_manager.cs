using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager_script : MonoBehaviour
{
    public static game_manager_script instance;
    private int clicks;
    private int currency; // bubbles
    private float progress; // phase 1 -> 2 = 200 clicks, phase 2 -> 3 = 1000 clicks 

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

        if (100 % clicks == 0)
        {
            currency++;
        }
        Debug.Log("Clicks: " + clicks);
    }
}

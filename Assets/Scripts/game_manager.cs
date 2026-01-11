using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class game_manager_script : MonoBehaviour
{
    public static game_manager_script instance;
    [SerializeField] private int clicks;
    [SerializeField] private int currency; // bubbles
    private float progress; // phase 1 -> 2 = 200 clicks, phase 2 -> 3 = 1000 clicks 
    [SerializeField] private int currencyAdd = 0;
    [SerializeField] private int currencyMult = 1;
    [SerializeField] private int passiveClicks = 0;

    [SerializeField] private double addCost = 100;
    [SerializeField] private double multCost = 100;
    [SerializeField] private double passiveCost = 100;
    [SerializeField] private double costMult = 1.1;

    [SerializeField] private TextMeshProUGUI currency_counter;
    [SerializeField] private TextMeshProUGUI clicks_counter;
    [SerializeField] private TextMeshProUGUI addCostText;

    [SerializeField] private TextMeshProUGUI multCostText;
    [SerializeField] private TextMeshProUGUI passiveCostText;

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

        addCostText.text = $"Buy Bonus Clicks: " + addCost;
        multCostText.text = $"Buy Click Mult: " + multCost;
        passiveCostText.text = $"Buy Passive Clicks: " + passiveCost;

        StartCoroutine(UpdatePerSecond());
    }

    private IEnumerator UpdatePerSecond()
    {
        while (true)
        {
            // Wait for one second before executing the following code
            yield return new WaitForSeconds(1f);

            currency += passiveClicks;
        }
    }

    public void AddClick()
    {
        clicks++;

        if (clicks % 10 == 0)
        {
            currency += (1 * currencyMult) + currencyAdd;
            currency_counter.text = $": {currency}";
        }
        Debug.Log("Clicks: " + clicks);

        clicks_counter.text = $": {clicks}";


        if (clicks % 100 == 0)
        {
            progress_bar.value++;
        }
    }

    public void addBonusClicks(int amount)
    {
        currencyAdd += amount;
    }

    public void addMultClicks(int amount)
    {
        currencyMult += amount;
    }

    public void addPassiveClicks(int amount)
    {
        passiveClicks += amount;
    }


    //BUYING
    public void BuyAdd()
    {
        if (currency >= addCost)
        {
            currency -= (int) addCost;
            addCost *= costMult;
            costMult *= 1.1;
            addBonusClicks(1 + (int) costMult);
            addCostText.text = $"Buy Bonus Clicks: " + addCost;
            audio_manager.instance.Play("buying_upgrade");
        } else
        {
            audio_manager.instance.Play("ui_select");
        }
    }

    public void BuyMult()
    {
        if (currency >= multCost)
        {
            currency -= (int) multCost;
            multCost *= costMult;
            costMult *= 1.1;
            addMultClicks(1 + (int) costMult);
            multCostText.text = $"Buy Click Mult: " + multCost;
            audio_manager.instance.Play("buying_upgrade");
        } else
        {
            audio_manager.instance.Play("ui_select");
        }
    }

    public void BuyPassive()
    {
        if (currency >= passiveCost)
        {
            currency -= (int) passiveCost;
            passiveCost *= costMult;
            costMult *= 1.1;
            addPassiveClicks(1 + (int) costMult);
            passiveCostText.text = $"Buy Passive Clicks: " + passiveCost;
            audio_manager.instance.Play("buying_upgrade");
        } else
        {
            audio_manager.instance.Play("ui_select");
        }
    }


}

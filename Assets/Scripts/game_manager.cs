using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using Unity.VisualScripting;
using System;

public class game_manager_script : MonoBehaviour
{
    public static game_manager_script instance;
    [SerializeField] private int clicks;
    [SerializeField] private int currency; // bubbles
    [SerializeField] private int tempClicks = 0;

    private float progress; // phase 1 -> 2 = 200 clicks, phase 2 -> 3 = 1000 clicks 
    [SerializeField] private int clickAdd = 0;
    [SerializeField] private int clickMult = 1;
    [SerializeField] private int passiveClicks = 0;

    [SerializeField] private double addCost = 50;
    [SerializeField] private double multCost = 50;
    [SerializeField] private double passiveCost = 50;
    [SerializeField] private double costMult = 1.25;

    [SerializeField] private TextMeshProUGUI currency_counter;
    [SerializeField] private TextMeshProUGUI clicks_counter;
    [SerializeField] private TextMeshProUGUI addCostText;

    [SerializeField] private TextMeshProUGUI multCostText;
    [SerializeField] private TextMeshProUGUI passiveCostText;

    [SerializeField] private Slider progress_bar;

    [SerializeField] private GameObject treeParent;
    [SerializeField] private GameObject trashParent;

    private List<GameObject> trees = new List<GameObject>();
    private List<GameObject> trashes = new List<GameObject>();

    private int randomTrash;
    private int randomTree;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1f;
    }

    public void Start()
    {
        clicks = 0;
        currency = 0;
        progress = 0;

        trees = GetImmediateChildren(trees, treeParent);
        trashes = GetImmediateChildren(trashes, trashParent);

        foreach (GameObject tree in trees)
        {
            int i = 0;
            Debug.Log("Tree" + ++i);
        }

        foreach (GameObject trash in trashes)
        {
            int i = 0;
            Debug.Log("Trash" + ++i);

        }

        treeParent.SetActive(true);
        trashParent.SetActive(true);

        for (int i = 0; i < treeParent.transform.childCount; ++i)
        {
            treeParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < trashParent.transform.childCount; ++i)
        {
            trashParent.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    List<GameObject> GetImmediateChildren(List<GameObject> list, GameObject parent)
    {
        list.Clear(); // Clear the list before repopulating
        foreach (Transform child in parent.transform) // Iterate through the parent's transform
        {
            list.Add(child.gameObject);
        }


        return list;
    }

    void Update()
    {
        // if (clicks == 100)
        // {
        //     treeParent.SetActive(false);
        //     trashParent.SetActive(false);
        // } else if (clicks >= 200)
        // {
        //     trashParent.SetActive(false);
        //     treeParent.SetActive(true);
        // }

        randomTrash = UnityEngine.Random.Range(0, trashParent.transform.childCount);
        randomTree = UnityEngine.Random.Range(0, treeParent.transform.childCount);

        if (clicks >= 10000000)
        {
            SceneManager.LoadScene("EndScene");
            addCostText.text = $"Buy Bonus Clicks: " + addCost + " O2";
            multCostText.text = $"Buy Click Mult: " + multCost + " O2";
            passiveCostText.text = $"Buy Passive Clicks: " + passiveCost + " O2";

            StartCoroutine(UpdatePerSecond());
        }
    }

    private IEnumerator UpdatePerSecond()
    {
        while (true)
        {
            // Wait for one second before executing the following code
            yield return new WaitForSeconds(1f);

            currency += passiveClicks;
            currency_counter.text = $": {currency}";
        }
    }

    public void AddClick()
    {
        clicks += (1 * clickMult) + clickAdd;
        tempClicks += (1 * clickMult) + clickAdd;

        if (tempClicks >= 10)
        {
            int remainder = tempClicks % 10;
            int divResult = tempClicks / 10;

            currency += divResult;
            tempClicks = remainder;
            currency_counter.text = $": {currency}";
        }

        Debug.Log("Clicks: " + clicks);

        clicks_counter.text = $": {clicks}";

        if (clicks % 10 == 0)
        {
            while (!trashParent.transform.GetChild(randomTrash).gameObject.activeSelf)
            {
                Destroy(trashParent.transform.GetChild(randomTrash).gameObject);
                break;
            }
            progress_bar.value++;
        }
        if (clicks >= 80 && clicks % 10 == 0)
        {
            while (!treeParent.transform.GetChild(randomTree).gameObject.activeSelf)
            {
                treeParent.transform.GetChild(randomTree).gameObject.SetActive(true);
                break;
            }

        }
    }

    public void addBonusClicks(int amount)
    {
        clickAdd += amount;
    }

    public void addMultClicks(int amount)
    {
        clickMult += amount;
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
            currency -= (int)addCost;
            addCost *= costMult;
            addCost = System.Math.Round(addCost, 2);
            costMult *= 1.1;
            addBonusClicks(1);
            addCostText.text = $"Buy Bonus Clicks: " + addCost + " O2";
            audio_manager.instance.Play("buying_upgrade");
        }
        else
        {
            audio_manager.instance.Play("ui_select");
        }
    }

    public void BuyMult()
    {
        if (currency >= multCost)
        {
            currency -= (int)multCost;
            multCost *= costMult;
            multCost = System.Math.Round(multCost, 2);
            costMult *= 1.1;
            addMultClicks(1);
            multCostText.text = $"Buy Click Mult: " + multCost + " O2";
            audio_manager.instance.Play("buying_upgrade");
        }
        else
        {
            audio_manager.instance.Play("ui_select");
        }
    }

    public void BuyPassive()
    {
        if (currency >= passiveCost)
        {
            currency -= (int)passiveCost;
            passiveCost *= costMult;
            passiveCost = System.Math.Round(passiveCost, 2);
            costMult *= 1.1;
            addPassiveClicks(1);
            passiveCostText.text = $"Buy Passive Clicks: " + passiveCost + " O2";
            audio_manager.instance.Play("buying_upgrade");
        }
        else
        {
            audio_manager.instance.Play("ui_select");
        }
    }


}

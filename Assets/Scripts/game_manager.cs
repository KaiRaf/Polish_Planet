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

    [SerializeField] private GameObject treeParent;
    [SerializeField] private GameObject trashParent;

    private List<GameObject> trees = new List<GameObject>();
    private List<GameObject> trashes = new List<GameObject>();

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

            foreach (GameObject tree in trees)
            {
                
            }
        }
    }
}
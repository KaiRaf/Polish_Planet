using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

        randomTrash = Random.Range(0, trashParent.transform.childCount);
        randomTree = Random.Range(0, treeParent.transform.childCount);

        if (clicks >= 250)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("EndScene");
        }
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

        if (clicks % 10 == 0)
        {
            while (!trashParent.transform.GetChild(randomTrash).gameObject.activeSelf)
            {
                Destroy(trashParent.transform.GetChild(randomTrash).gameObject);
                break;
            }
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
}

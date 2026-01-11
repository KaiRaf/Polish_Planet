using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_spawning : MonoBehaviour
{

    [SerializeField] private GameObject _treePrefab;

    public List<GameObject> nodes = new List<GameObject>();

    void Start()
    {
        GetImmediateChildren();
        PopulateTrees();
    }

    void GetImmediateChildren()
    {
        nodes.Clear(); // Clear the list before repopulating
        foreach (Transform childTransform in transform) // Iterate through the parent's transform
        {
            nodes.Add(childTransform.gameObject);
        }
    }

    void PopulateTrees()
    {
        foreach (GameObject node in nodes) // Iterate through the parent's transform
        {
            GameObject obj = Instantiate(_treePrefab, node.transform.position, node.transform.rotation);
            obj.transform.SetParent(node.transform);
        }
    }

}

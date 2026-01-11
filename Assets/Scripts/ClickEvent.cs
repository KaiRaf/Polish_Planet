using UnityEngine;

public class ClickEvent : MonoBehaviour
{

    public PlanetPhase planetPhase;

    void OnMouseDown()
    {
        planetPhase.OnPlanetClicked();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

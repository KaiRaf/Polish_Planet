using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrade_menu_manager : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToggleUpgradeMenu()
    {
        if (isActive)
        {
            isActive = false;
            HideUpgradeMenu();
        } else
        {
            isActive = true;
            ShowUpgradeMenu();
        }
    }

    void ShowUpgradeMenu()
    {
        //TODON QUIET MUSIC

        _upgradeMenu.SetActive(true);
        audio_manager.instance.Play("ui_select");
    }    

    void HideUpgradeMenu()
    {
        //TODON LOUDEN MUSIC

        _upgradeMenu.SetActive(false);
        audio_manager.instance.Play("ui_select");
    }  



}

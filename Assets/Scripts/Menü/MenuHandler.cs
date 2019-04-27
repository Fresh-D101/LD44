using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public enum MenuState
    {
        MainMenu,
        Options,
        LoadSave
    }

    [SerializeField] private MenuState m_menuState = MenuState.MainMenu;
    [Space]
    [SerializeField] private GameObject m_MainMenu = null;
    [SerializeField] private GameObject m_Options = null;
    [SerializeField] private GameObject m_LoadSave = null;

    public void ChangeMenuState(MenuState menuState)
    {
        m_menuState = menuState;

        CloseAllMenus();

        switch (m_menuState)
        {
            case MenuState.MainMenu:
                m_MainMenu.SetActive(true);
                break;

            case MenuState.Options:
                m_Options.SetActive(true);
                break;

            case MenuState.LoadSave:
                m_LoadSave.SetActive(true);
                break;

            default:
                break;
        }
    }

    public void GoToMainMenu()
    {
        CloseAllMenus();

        m_MainMenu.SetActive(true);
    }

    public void GoToOptions()
    {
        CloseAllMenus();

        m_Options.SetActive(true);
    }

    public void GoToLoadSave()
    {
        CloseAllMenus();

        m_LoadSave.SetActive(true);
    }

    private void CloseAllMenus()
    {
        m_MainMenu.SetActive(false);
        m_Options.SetActive(false);
        m_LoadSave.SetActive(false);
    }
}

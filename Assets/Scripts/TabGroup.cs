using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TabGroup : MonoBehaviour
{
    public List<TabButtom> tabButtons;
    public Color tabIdle = Color.black;
    public Color tabActive;
    public Color tabHover;
    public TabButtom selectButtom;
    public List<GameObject> objectToSwap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Subscribe(TabButtom button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButtom>();
        }
        tabButtons.Add(button);
    }
    public void OnTabEnter(TabButtom button)
    {
        ResetTabs();
        if (selectButtom == null || button != selectButtom)
        {
            button.background.color = tabHover;
        }
    }
    public void OnTabExit(TabButtom button)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButtom button)
    {

        selectButtom = button;
        ResetTabs();

        // Desactivar el Event Trigger del botón seleccionado by Chelo
        if (button.TryGetComponent<EventTrigger>(out EventTrigger trigger))
        {
            trigger.enabled = false;
        }

        button.background.color = tabActive;
        
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectToSwap.Count; i++)
        {
            if (i == index)
            {
                objectToSwap[i].SetActive(true);
            }
            else
            {
                objectToSwap[i].SetActive(false);
            }
        }
    }
    public void ResetTabs()
    {
        foreach (TabButtom button in tabButtons)
        {
            // Reactivar el Event Trigger en todos los que NO sean el seleccionado by Chelo
            if (button.TryGetComponent<EventTrigger>(out EventTrigger trigger))
            {
                // Si es el seleccionado, queda apagado. Si no, se enciende.
                trigger.enabled = (selectButtom == null || button != selectButtom);
            }

            if (selectButtom != null && button == selectButtom) { continue; }
            button.background.color = Color.black;
        }
    }
}

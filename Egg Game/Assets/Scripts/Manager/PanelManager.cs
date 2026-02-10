using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    private Dictionary<string, Panel> _panelList = new Dictionary<string, Panel>();
    public override void Awake()
    {
        base.Awake();
        var existPanels = GetComponentsInChildren<Panel>();
        foreach (var panel in existPanels)
        {
            _panelList[panel.name] = panel;
        }
    }
    public Panel GetPanel(string panelName)
    {
        if (_panelList.ContainsKey(panelName))
        {
            Panel panel = _panelList[panelName];

            if (panel == null)
            {
                _panelList.Remove(panelName);
            }
            else
            {
                return panel;
            }
        }
        Panel loadedPanel = Resources.Load<Panel>(GameConfig.PANEL_PATH + panelName);
        Panel newPanel = Instantiate(loadedPanel, transform);
        newPanel.transform.SetAsLastSibling();
        newPanel.gameObject.SetActive(false);

        _panelList[panelName] = newPanel;
        return newPanel;
    }
    public void OpenPanel(string panelName)
    {
        Panel panel = GetPanel(panelName);
        panel.Open();
        Debug.Log("Open Panel:" + panelName);

    }
    public void ClosePanel(string panelName)
    {
        Panel panel = GetPanel(panelName);
        panel.Close();
    }
    public void CloseAllPanel()
    {
        foreach (var panel in _panelList.Values)
        {
            panel.Close();
        }
    }

    public void OnPanelDestroyed(Panel panel)
    {
        if (_panelList.ContainsValue(panel))
        {
            string keyToRemove = null;

            foreach (var kvp in _panelList)
            {
                if (kvp.Value == panel)
                {
                    keyToRemove = kvp.Key;
                    break;
                }
            }

            if (keyToRemove != null)
            {
                _panelList[keyToRemove] = null;
            }
        }
    }

}

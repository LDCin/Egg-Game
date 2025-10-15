using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    private Dictionary<string, Panel> panelList = new Dictionary<string, Panel>();
    public override void Awake()
    {
        base.Awake();
        var existPanels = GetComponentsInChildren<Panel>();
        foreach (var panel in existPanels)
        {
            panelList[panel.name] = panel;
        }
    }
    private bool IsAvailablePanel(string panelName)
    {
        return panelList.ContainsKey(panelName);
    }
    public Panel GetPanel(string panelName)
    {
        if (IsAvailablePanel(panelName))
        {
            return panelList[panelName];
        }

        Panel panel = Resources.Load<Panel>(GameConfig.PANEL_PATH + panelName);
        Panel newPanel = Instantiate(panel, transform);
        newPanel.transform.SetAsLastSibling();
        newPanel.gameObject.SetActive(false);

        panelList[panelName] = newPanel;
        return newPanel;
    }
    public void OpenPanel(string panelName)
    {
        Panel panel = GetPanel(panelName);
        panel.Open();
    }
    public void ClosePanel(string panelName)
    {
        Panel panel = GetPanel(panelName);
        panel.Close();
    }
    public void CloseAllPanel()
    {
        foreach (var panel in panelList.Values)
        {
            panel.Close();
        }
    }
}

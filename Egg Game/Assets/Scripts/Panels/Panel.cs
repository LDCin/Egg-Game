using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] bool _destroyOnClose = false;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        if (_destroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
       }
    }
    private void OnDestroy()
    {
        PanelManager.Instance?.OnPanelDestroyed(this);
    }
}

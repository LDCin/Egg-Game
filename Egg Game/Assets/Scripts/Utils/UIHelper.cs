using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHelper : MonoBehaviour
{
    public static UIHelper Instance;
    private void Awake()
    {
        Instance = this;
    }

    public bool IsPointerOverUI() // chi ho tro co raycast vao diem nao tren man hinh hay khong thoi.
    {
        EventSystem eventSystem = EventSystem.current;

        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        eventSystem.RaycastAll(eventData, results);

        if (results.Count > 0) Debug.Log("True");
        return results.Count > 0;
    }
}
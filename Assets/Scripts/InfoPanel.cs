using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public TextMeshProUGUI DaysOpenText;
    public TextMeshProUGUI Balance;
    public TextMeshProUGUI NewCustomersText;

    public float StartY;
    public float EndY;
    public float Speed;

    private float LastWeekMoney;
    private int LastWeekCustomers;

    private void Start()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, StartY);
    }

    public void ShowInfoPanel(Report report)
    {
        DaysOpenText.text = "Days open: " + report.days.ToString();
        Balance.text = "This weeks' balance: " + (report.money - LastWeekMoney).ToString();
        NewCustomersText.text = "New satisfied customers: " + (report.customers - LastWeekCustomers).ToString();

        LastWeekMoney = report.money;
        LastWeekCustomers = report.customers;

        StopAllCoroutines();
        StartCoroutine(MoveIn());
    }

    public void HideInfoPanel()
    {
        StopAllCoroutines();
        StartCoroutine(MoveOut());
    }

    private IEnumerator MoveIn()
    {
        var recT = GetComponent<RectTransform>();

        while(recT.anchoredPosition.y < EndY)
        {
            recT.anchoredPosition = new Vector2(recT.anchoredPosition.x, recT.anchoredPosition.y + Time.deltaTime * Speed);
            yield return null;
        }

        recT.anchoredPosition = new Vector2(recT.anchoredPosition.x, EndY);
    }


    private IEnumerator MoveOut()
    {
        var recT = GetComponent<RectTransform>();

        while (recT.anchoredPosition.y > StartY)
        {
            recT.anchoredPosition = new Vector2(recT.anchoredPosition.x, recT.anchoredPosition.y - Time.deltaTime * Speed);
            yield return null;
        }

        recT.anchoredPosition = new Vector2(recT.anchoredPosition.x, StartY);
    }
}

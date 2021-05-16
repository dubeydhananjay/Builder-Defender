using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    private TMPro.TextMeshProUGUI tooltipText;
    private RectTransform background;
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform canvasRectTransform;
    private ToolTipTimer toolTipTimer;
    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        tooltipText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        background = transform.Find("background").GetComponent<RectTransform>();
        rectTransform.SetAsLastSibling();
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();

        if (toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if (toolTipTimer.timer <= 0)
                Hide();
        }
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + background.rect.width > canvasRectTransform.rect.width)
            anchoredPosition.x = canvasRectTransform.rect.width - background.rect.width;
        if (anchoredPosition.y + background.rect.height > canvasRectTransform.rect.height)
            anchoredPosition.y = canvasRectTransform.rect.height - background.rect.height;

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetToolTipText(string text)
    {
        tooltipText.text = text;
        tooltipText.ForceMeshUpdate();
        Vector2 textSize = tooltipText.GetRenderedValues(false);
        Vector2 padding = new Vector2(16, 16);
        background.sizeDelta = textSize + padding;
    }

    public void Show(string text, ToolTipTimer toolTipTimer = null)
    {
        this.toolTipTimer = toolTipTimer;
        gameObject.SetActive(true);
        SetToolTipText(text);
        HandleFollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class ToolTipTimer
    {
        public float timer;
    }

}

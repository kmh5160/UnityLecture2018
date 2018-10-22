using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RestartGameUIButtonHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public RectTransform root;

    public void OnPointerClick(PointerEventData eventData)
    {
        root.GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(() =>
        {
            root.gameObject.SetActive(false);
            GameFlow.Instance.StartCoroutine(GameFlow.Instance.RestartGame());
            GameFlow.Instance.fsm.SetTrigger("RestartGame");
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(0.8f, 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1.0f, 0.1f);
    }
}

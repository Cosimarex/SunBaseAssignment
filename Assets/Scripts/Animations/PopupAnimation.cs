using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupAnimation : MonoBehaviour
{
    public float animationDuration = 0.5f; // Duration of the animation

    private void OnEnable()
    {
        // Reset scale and play scale-up animation
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);
    }

    public void ClosePopup()
    {
        // Scale-down animation and deactivate popup
        transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}

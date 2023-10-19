using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[Serializable]
public class SlotEffect
{
    public Vector3 Force = Vector3.one;
    public float Duration = 1f;
}

public class ResourceSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private SlotEffect _clickEffect;
    [SerializeField] private SlotEffect _initialEffect;

    [Inject] private Craft _craft;

    public ResourceUnit Unit { get; private set; }

    public void Initialize(ResourceUnit resource)
    {
        Unit = resource;
        _image.sprite = resource.Icon;

        _image.transform.localScale = Vector3.zero;
        _image.transform.DOKill(true);
        _image.transform.DOScale(Vector3.one, _initialEffect.Duration);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _craft.TakePlace(Unit);

        _image.transform.DOKill(true);
        _image.transform.DOScale(_clickEffect.Force, _clickEffect.Duration).SetLoops(2, LoopType.Yoyo);
    }
}
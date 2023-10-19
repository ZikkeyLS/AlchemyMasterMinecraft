using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private SlotEffect _statusEffect;

    public ResourceUnit Unit { get; private set; }

    private Craft _craft;

    public void Success()
    {
        _image.color = Color.green;

        _image.transform.DOKill(true);
        _image.transform.DOScale(Vector3.zero, _statusEffect.Duration).OnComplete(() => Clear());
    }

    public void Error()
    {
        _image.color = Color.red;

        _image.transform.DOKill(true);
        _image.transform.DOPunchRotation(_statusEffect.Force, _statusEffect.Duration).OnComplete(() => Clear());
    }

    public void Initialize(Craft craft, ResourceUnit resource)
    {
        _craft = craft;

        Unit = resource;
        _image.color = Color.white;
        _image.sprite = resource.Icon;
    }

    public void Clear()
    {
        Unit = null;
        _craft.SetLock(false);
        _image.transform.localScale = Vector3.one;
        _image.sprite = null;
        _image.color = new Color(0, 0, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_craft != null && _craft.GetLock() == false)
            Clear();
    }
}
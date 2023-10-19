using UnityEngine;
using Zenject;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _collectionStatus;
     
    [Inject] private ResourcePool _resourcePool;

    private void OnEnable()
    {
        _resourcePool.OnAdded.AddListener(UpdateText);
    }

    private void OnDisable()
    {
        _resourcePool.OnAdded.RemoveListener(UpdateText);
    }

    public void UpdateText()
    {
        _collectionStatus.text = $"{_resourcePool.CurrentResourceCount}/{_resourcePool.GlobalResourceCount}";
    }
}

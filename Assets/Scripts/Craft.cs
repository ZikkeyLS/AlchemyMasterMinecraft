using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Craft : MonoBehaviour
{
    [SerializeField] private CraftSlot[] _containers = new CraftSlot[2];
    [SerializeField] private CraftUnit[] _crafts;

    public readonly UnityEvent<ResourceUnit> CraftCallback = new UnityEvent<ResourceUnit>();

    [Inject] private ResourcePool _resources;

    private bool _lock = false;
    public bool GetLock() => _lock;
    public void SetLock(bool value) => _lock = value;

    public ResourceUnit[] TryCraft()
    {
        List<ResourceUnit> items = new List<ResourceUnit>();

        foreach (CraftUnit craft in _crafts)
            foreach (ResourceUnit result in craft.Results)
            {
                if (_resources.HasResource(result))
                    continue;

                bool firstCase = craft.Items[0] == _containers[0].Unit && craft.Items[1] == _containers[1].Unit;
                bool secondCase = craft.Items[1] == _containers[0].Unit && craft.Items[0] == _containers[1].Unit;

                if (firstCase || secondCase)
                {
                    CraftCallback.Invoke(result);
                    items.Add(result);
                }
            }

        return items.ToArray();
    }

    public void TakePlace(ResourceUnit unit)
    {
        if (_lock)
            return;

        bool final = _containers[0].Unit != null;

        _containers[final ? 1 : 0].Initialize(this, unit);

        if (final == false)
            return;

        SetLock(true);

        ResourceUnit[] results = TryCraft();

        foreach (ResourceUnit result in results)
            _resources.AddResource(result);


        if (results.Length == 0)
        {
            foreach (CraftSlot slot in _containers)
                slot.Error();
        }
        else
        {
            foreach (CraftSlot slot in _containers)
                slot.Success();
        }
    }
}


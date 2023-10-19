using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private ResourceSlot _resourcePrefab;

    [SerializeField] private ResourceUnit[] _allResources;
    [SerializeField] private ResourceUnit[] _initialResources;

    [Inject] private DiContainer _container;

    public UnityEvent OnAdded { get; private set; } = new UnityEvent();


    private List<ResourceUnit> _units = new List<ResourceUnit>();
    public bool HasResource(ResourceUnit unit) => _units.Contains(unit);

    public int GlobalResourceCount => _allResources.Length;
    public int CurrentResourceCount => _units.Count;

    private void Start()
    {
        foreach (ResourceUnit initialUnit in _initialResources)
            AddResource(initialUnit);
    }

    public void AddResource(ResourceUnit unit)
    {
        ResourceSlot resource = _container.InstantiatePrefabForComponent<ResourceSlot>(_resourcePrefab, _root);
        resource.Initialize(unit);
        _units.Add(unit);
        OnAdded.Invoke();
    }
}

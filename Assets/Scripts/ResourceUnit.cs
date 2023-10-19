using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Game/Resource")]
public class ResourceUnit : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
}

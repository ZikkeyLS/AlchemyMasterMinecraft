using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "Game/Craft")]
public class CraftUnit : ScriptableObject
{
    public ResourceUnit[] Items = new ResourceUnit[2];
    public ResourceUnit[] Results;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite icon;
    [TextArea(5, 5)]
    public string description;
    public Transform prefab;
}

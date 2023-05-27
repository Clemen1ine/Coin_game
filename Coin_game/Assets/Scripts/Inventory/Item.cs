using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/item")]
public class Item : ScriptableObject
{
    public Tilemap tile;
    public Sprite image;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    public bool stackable = true;
    
}

public enum ItemType
{
    SmoleCoin,
    BigCoin
}

public enum ActionType
{
    Being,
    Dig
}
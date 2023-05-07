using UnityEngine;

public class HexTilePropertiesInt32 : MonoBehaviour, IHexTileProperties<int>
{
    public DictInt32 properties = new DictInt32();

    public int this[string propertyName] {
        get {
            return properties[propertyName];
        }
        set {
            properties[propertyName] = value;
        }
    }
}

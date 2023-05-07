using UnityEngine;

public class HexTilePropertiesBoolean : MonoBehaviour, IHexTileProperties<bool>
{
    public DictBoolean properties = new DictBoolean();

    public bool this[string propertyName] {
        get {
            return properties[propertyName];
        }
        set {
            properties[propertyName] = value;
        }
    }
}

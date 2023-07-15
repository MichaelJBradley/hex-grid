using UnityEngine;

public class HexTilePropertiesSingle : MonoBehaviour, IHexTileProperties<float>
{
    public DictSingle properties = new DictSingle();

    public float this[string propertyName] {
        get {
            return properties[propertyName];
        }
        set {
            properties[propertyName] = value;
        }
    }
}

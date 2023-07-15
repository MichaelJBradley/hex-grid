using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[Serializable] public class DictString : SerializableDictionaryBase<string, string> { }

[CreateAssetMenu(fileName = "HexTileProperties", menuName = "ScriptableObjects/Hex Grid Samples/TileProperty")]
public class TilePropertyScriptableObject : ScriptableObject
{
    public Color color;

    public DictInt32 propsInt = new DictInt32();
    public DictBoolean propsBool = new DictBoolean();
}

using System;
using RotaryHeart.Lib.SerializableDictionary;

[Serializable] public class DictInt32 : SerializableDictionaryBase<string, int> { }
[Serializable] public class DictSingle : SerializableDictionaryBase<string, float> { }
[Serializable] public class DictBoolean : SerializableDictionaryBase<string, bool> { }

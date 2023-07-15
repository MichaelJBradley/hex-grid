using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HexTilePropertiesTest
    {
        [Test]
        public void HexTilePropertyBoolean_WithValues_SetsAndGetsCorrectly()
        {
            GameObject p = new GameObject();
            HexTilePropertiesBoolean props = p.AddComponent<HexTilePropertiesBoolean>();
            props["someProp"] = true;
            props["anotherProp"] = false;

            Assert.That(props["someProp"], Is.True);
            Assert.That(props["anotherProp"], Is.False);
        }

        [Test]
        public void HexTilePropertiesBoolean_WithNoValues_ThrowsKeyNotFoundException()
        {
            GameObject p = new GameObject();
            HexTilePropertiesBoolean props = p.AddComponent<HexTilePropertiesBoolean>();

            Assert.That(() => props["nonexistentProp"], Throws.TypeOf<KeyNotFoundException>());
        }

        [Test]
        public void HexTilePropertyInt32_WithValues_SetsAndGetsCorrectly()
        {
            GameObject p = new GameObject();
            HexTilePropertiesInt32 props = p.AddComponent<HexTilePropertiesInt32>();
            props["someProp"] = 123;
            props["anotherProp"] = 0;

            Assert.That(props["someProp"], Is.EqualTo(123));
            Assert.That(props["anotherProp"], Is.EqualTo(0));
        }

        [Test]
        public void HexTilePropertiesInt32_WithNoValues_ThrowsKeyNotFoundException()
        {
            GameObject p = new GameObject();
            HexTilePropertiesInt32 props = p.AddComponent<HexTilePropertiesInt32>();

            Assert.That(() => props["nonexistentProp"], Throws.TypeOf<KeyNotFoundException>());
        }

        [Test]
        public void HexTilePropertySingle_WithValues_SetsAndGetsCorrectly()
        {
            GameObject p = new GameObject();
            HexTilePropertiesSingle props = p.AddComponent<HexTilePropertiesSingle>();
            props["someProp"] = 0.45f;
            props["anotherProp"] = -1.863f;

            Assert.That(props["someProp"], Is.EqualTo(0.45f));
            Assert.That(props["anotherProp"], Is.EqualTo(-1.863f));
        }

        [Test]
        public void HexTilePropertiesSingle_WithNoValues_ThrowsKeyNotFoundException()
        {
            GameObject p = new GameObject();
            HexTilePropertiesSingle props = p.AddComponent<HexTilePropertiesSingle>();

            Assert.That(() => props["nonexistentProp"], Throws.TypeOf<KeyNotFoundException>());
        }
    }
}

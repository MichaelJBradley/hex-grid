using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HexUvTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GetUvMapping_ReceivesAValidEnum_ReturnsMapping()
        {
            HexUvTypes mappingType = HexUvTypes.SquareOutline;
            Vector2[] uvMapping = HexUv.GetUvMapping(mappingType);

            Assert.That(uvMapping, Is.EqualTo(HexUv.SquareOutline));
        }
    }
}

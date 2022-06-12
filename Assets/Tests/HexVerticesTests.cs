using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class HexVerticesTests
    {
        [Test]
        public void HexToWorld_WithFlatToppedZeroHex_CalculatesZeroVector3()
        {
            Hex h = new Hex();
            Vector3 expected = Vector3.zero;
            
            Vector3 actual = HexVertices.HexToWorld(HexVertices.FlatTopOrientation, h);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void HexToWorld_WithPointyToppedZeroHex_CalculatesZeroVector3()
        {
            Hex h = new Hex();
            Vector3 expected = Vector3.zero;
            
            Vector3 actual = HexVertices.HexToWorld(HexVertices.PointyTopOrientation, h);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void HexToWorld_WithFlatToppedNonZeroHex_CalculatesCorrectCenterPosition()
        {
            Hex h = new Hex(1, -3, 2);
            // Expected comes from the following matrix multiplication
            // [ 1, -3 ] * [ 1.5, sqrt(3) / 2]
            //             [ 0,   sqrt(3)    ]
            Vector3 expected = new Vector3(1.5f, 0.0f, -4.330f);

            Vector3 actual = HexVertices.HexToWorld(HexVertices.FlatTopOrientation, h);

            Assert.That(actual.x, Is.EqualTo(expected.x).Within(0.001f));
            Assert.That(actual.y, Is.EqualTo(0.0f));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(0.001f));
        }

        [Test]
        public void HexToWorld_WithPointyToppedNonZeroHex_CalculatesCorrectCenterPosition()
        {
            Hex h = new Hex(-4, 5, -1);
            // Expected comes from the following matrix multiplication
            // [ -4, 5 ] * [ sqrt(3),     0   ]
            //             [ sqrt(3) / 2, 1.5 ]
            Vector3 expected = new Vector3(-2.598f, 0.0f, 7.5f);

            Vector3 actual = HexVertices.HexToWorld(HexVertices.PointyTopOrientation, h);
            
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(0.001f));
            Assert.That(actual.y, Is.EqualTo(0.0f));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(0.001f));
        }

        [Test]
        public void WorldToHex_WithZeroVector3AndFlatToppedOrientation_CalculatesZeroFloatHex()
        {
            FloatHex expected = new FloatHex();

            FloatHex actual = HexVertices.WorldToHex(HexVertices.FlatTopOrientation, Vector3.zero);

            Assert.That(actual, Is.EqualTo(expected));
        }
        
        [Test]
        public void WorldToHex_WithZeroVector3AndPointyToppedOrientation_CalculatesZeroFloatHex()
        {
            FloatHex expected = new FloatHex();

            FloatHex actual = HexVertices.WorldToHex(HexVertices.PointyTopOrientation, Vector3.zero);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void WorldToHex_WithNonZeroVector3AndFlatToppedOrientation_CalculatesCorrectFloatHex()
        {
            Vector3 v = new Vector3(5.0f, 0.0f, -3.0f);
            // Expected comes from the following matrix multiplication, where the 2x2 matrix is the inverse of the
            // flat topped to-world matrix.
            // [ 5, -3 ] * [ 2 / 3, -1 / 3      ]
            //             [ 0,     sqrt(3) / 3 ]
            FloatHex expected = new FloatHex(3.3333f, -3.3987f);

            FloatHex actual = HexVertices.WorldToHex(HexVertices.FlatTopOrientation, v);

            Assert.That(actual.Q, Is.EqualTo(expected.Q).Within(0.001f));
            Assert.That(actual.R, Is.EqualTo(expected.R).Within(0.001f));
        }

        [Test]
        public void WorldToHex_WithNonZeroVector3AndPointyToppedOrientation_CalculatesCorrectFloatHex()
        {
            Vector3 v = new Vector3(-10.5f, 0.0f, 172.0f);
            // Expected comes from the following matrix multiplication, where the 2x2 matrix is the inverse of the
            // pointy topped to-world matrix.
            // [ -10.5, 172 ] * [ sqrt(3) / 3, 0     ]
            //                  [ -1 / 3,      2 / 3 ]
            FloatHex expected = new FloatHex(-63.3955f, 114.6667f);

            FloatHex actual = HexVertices.WorldToHex(HexVertices.PointyTopOrientation, v);

            Assert.That(actual.Q, Is.EqualTo(expected.Q).Within(0.001f));
            Assert.That(actual.R, Is.EqualTo(expected.R).Within(0.001f));
        }

        [Test]
        public void CalcCornerOffset_ForFlatToppedHexAtIndex0_EqualsVector3Right()
        {
            Vector3 expected = Vector3.right;

            Vector3 actual = HexVertices.CalcCornerOffset(HexVertices.FlatTopOrientation, 0);

            Assert.That(actual.x, Is.EqualTo(expected.x).Within(0.001f));
            Assert.That(actual.y, Is.EqualTo(0.0f));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(0.001f));
        }

        [Test]
        public void CalcCornerOffset_ForPointyToppedHexAtIndex4_EqualsVector3Back()
        {
            Vector3 expected = Vector3.back;

            Vector3 actual = HexVertices.CalcCornerOffset(HexVertices.PointyTopOrientation, 4);
            
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(0.001f));
            Assert.That(actual.y, Is.EqualTo(0.0f));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(0.001f));
        }

        [Test]
        public void GetVertices_ForFlatToppedZeroHex_EqualsPredefinedFlatTopHexVertices()
        {
            Hex h = new Hex();
            Vector3[] expected = HexVertices.FlatTop;

            Vector3[] actual = HexVertices.GetVertices(h, HexTypes.FlatTop);

            Assert.That(actual.Length, Is.EqualTo(expected.Length));
            for (uint i = 0; i < actual.Length; i++)
            {
                Assert.That(actual[i].x, Is.EqualTo(expected[i].x).Within(0.001f));
                Assert.That(actual[i].y, Is.EqualTo(0.0f));
                Assert.That(actual[i].z, Is.EqualTo(expected[i].z).Within(0.001f));
            }
        }

        [Test]
        public void GetVertices_ForPointyToppedZeroHex_EqualsPredefinedPointyTopHexVertices()
        {
            Hex h = new Hex();
            Vector3[] expected = HexVertices.PointyTop;

            Vector3[] actual = HexVertices.GetVertices(h, HexTypes.PointyTop);

            Assert.That(actual.Length, Is.EqualTo(expected.Length));
            for (uint i = 0; i < actual.Length; i++)
            {
                Assert.That(actual[i].x, Is.EqualTo(expected[i].x).Within(0.001f));
                Assert.That(actual[i].y, Is.EqualTo(0.0f));
                Assert.That(actual[i].z, Is.EqualTo(expected[i].z).Within(0.001f));
            }
        }
    }
}

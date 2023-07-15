using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HexTileTests
    {
        private readonly int[] flatTopTriangles = new int[] {0, 2, 1, 0, 3, 2, 0, 4, 3, 0, 5, 4, 0, 6, 5, 0, 1, 6};
        [Test]
        public void HexTile_WithMeshFilterAndVerticesAndTriangles_GeneratesMesh()
        {
            GameObject h = new GameObject();
            HexTile ht = h.AddComponent<HexTile>();
            // Set vertices for a Hex at (0, 0)
            ht.vertices = HexVertices.FlatTop;
            // Set triangles. Unity calculates triangles clockwise.
            ht.triangles = flatTopTriangles;

            Mesh m = ht.GenerateMesh();

            Assert.That(m, Is.Not.Null);
            Assert.That(m.vertices, Is.EqualTo(ht.vertices));
            Assert.That(m.triangles, Is.EqualTo(ht.triangles));
            Assert.That(m.uv, Is.EqualTo(HexUv.SquareOutline));
        }
        
        [UnityTest]
        public IEnumerator HexTile_WithMeshCollider_BelowFallingBall_Collides()
        {
            // Create the ball that will collide with the Hex.
            // Put it above the Hex, so it can fall onto it.
            GameObject ball = new GameObject();
            ball.transform.position = new Vector3(0, 4, 0);
            ball.AddComponent<SphereCollider>();
            Ball ballScript = ball.AddComponent<Ball>();
            Rigidbody ballRb = ball.AddComponent<Rigidbody>();
            ballRb.useGravity = true;
            
            // Create the Hex
            GameObject h = new GameObject();
            h.AddComponent<MeshCollider>();
            HexTile ht = h.AddComponent<HexTile>();
            ht.pos = new Hex(0, 0);
            ht.vertices = HexVertices.FlatTop;
            ht.triangles = flatTopTriangles;
            
            yield return new WaitForSeconds(1f);

            MeshCollider mc = h.GetComponent<MeshCollider>();
            Assert.That(mc, Is.Not.Null);
            Assert.That(ballScript.collision, Is.True);
        }
    
        [UnityTest]
        public IEnumerator HexTile_WithMeshCollider_AboveFallingBall_DoesNotCollide() {
            // Create the ball that will collide with the Hex.
            // Put it above the Hex, so it can fall onto it.
            GameObject ball = new GameObject();
            ball.transform.position = new Vector3(0, -4, 0);
            ball.AddComponent<SphereCollider>();
            Ball ballScript = ball.AddComponent<Ball>();
            Rigidbody ballRb = ball.AddComponent<Rigidbody>();
            ballRb.useGravity = true;
            
            // Create the Hex
            GameObject h = new GameObject();
            h.AddComponent<MeshCollider>();
            HexTile ht = h.AddComponent<HexTile>();
            ht.pos = new Hex(0, 0);
            ht.vertices = HexVertices.FlatTop;
            ht.triangles = flatTopTriangles;
            
            yield return new WaitForSeconds(1f);

            MeshCollider mc = h.GetComponent<MeshCollider>();
            Assert.That(mc, Is.Not.Null);
            Assert.That(ballScript.collision, Is.False);
        }

        [Test]
        public void HexTile_WithPropertyInt32_GetsComponent() {
            GameObject h = new GameObject();
            HexTile ht = h.AddComponent<HexTile>();
            HexTilePropertiesInt32 p = h.AddComponent<HexTilePropertiesInt32>();
            p["SomeProperty"] = 568;

            IHexTileProperties<int> actual = ht.PropertyInt32;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual["SomeProperty"], Is.EqualTo(568));
        }

        [Test]
        public void HexTile_WithoutPropertyInt32_ReturnsNull() {
            GameObject h = new GameObject();
            HexTile ht = h.AddComponent<HexTile>();

            IHexTileProperties<int> actual = ht.PropertyInt32;

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void HexTile_WithPropertySingle_GetsComponent() {
            GameObject h = new GameObject();
            HexTile ht = h.AddComponent<HexTile>();
            HexTilePropertiesSingle p = h.AddComponent<HexTilePropertiesSingle>();
            p["SomeProperty"] = 12.4f;

            IHexTileProperties<float> actual = ht.PropertySingle;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual["SomeProperty"], Is.EqualTo(12.4f));
        }

        [Test]
        public void HexTile_WithoutPropertySingle_ReturnsNull() {
            GameObject h = new GameObject();
            HexTile ht = h.AddComponent<HexTile>();

            IHexTileProperties<float> actual = ht.PropertySingle;

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void HexTile_WithPropertyBoolean_GetsComponent() {
            GameObject h = new GameObject();
            HexTile ht = h.AddComponent<HexTile>();
            HexTilePropertiesBoolean p = h.AddComponent<HexTilePropertiesBoolean>();
            p["SomeProperty"] = true;

            IHexTileProperties<bool> actual = ht.PropertyBoolean;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual["SomeProperty"], Is.True);
        }

        [Test]
        public void HexTile_WithoutPropertyBoolean_ReturnsNull() {
            GameObject h = new GameObject();
            HexTile ht = h.AddComponent<HexTile>();

            IHexTileProperties<bool> actual = ht.PropertyBoolean;

            Assert.That(actual, Is.Null);
        }
    }

    /// <summary>
    /// Ball is a test MonoBehavior used to detect collisions.
    /// </summary>
    class Ball : MonoBehaviour
    {
        public bool collision;

        void OnCollisionEnter(Collision col)
        {
            collision = true;
        }

        void OnCollisionExit(Collision col)
        {
            collision = false;
        }
    }
}

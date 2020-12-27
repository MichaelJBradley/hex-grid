using System;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    public class HexTileTests
    {
        [Test]
        public void HexTile_WithMeshFilterAndVerticesAndTriangles_GeneratesMesh()
        {
            GameObject g = new GameObject();
            HexTile h = g.AddComponent<HexTile>();
            // Set vertices for a Hex at (0, 0)
            h.vertices = HexVertices.FlatTop;
            // Set triangles. Unity calculates triangles clockwise.
            h.triangles = new int[] {0, 2, 1, 0, 3, 2, 0, 4, 3, 0, 5, 4, 0, 6, 5, 0, 1, 6};

            Mesh m = h.GenerateMesh();

            Assert.That(m, Is.Not.Null);
            Assert.That(m.vertices, Is.EqualTo(h.vertices));
            Assert.That(m.triangles, Is.EqualTo(h.triangles));
            Assert.That(m.uv, Is.EqualTo(HexUv.SquareOutline));
        }
    }
}

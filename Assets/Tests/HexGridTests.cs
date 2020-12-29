using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Grid;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HexGridTests
    {
        [UnityTest]
        public IEnumerator HexGrid_WithAHexagonalGridGeneratorAndRadiusOf10_GeneratesCorrectly()
        {
            GameObject g = new GameObject();
            TestHexagonalGridGenerator generator = g.AddComponent<TestHexagonalGridGenerator>();
            generator.gridRadius = 10;
            HexGrid grid = g.AddComponent<HexGrid>();

            Assert.That(grid.Loaded, Is.False);
            
            // Generation times may differ depending on which hardware the test is being run on.
            // 2 seconds should be a safe wait time without increasing the overall run time too much.
            yield return new WaitForSeconds(2.0f);

            Assert.That(grid.Loaded, Is.True);
            // Count should be (radius * 2 + 1) + ((((radius + 1 + (radius * 2)) / 2) * radius) * 2)
            // The first term calculates the middle row of hexes
            // The second term is the area of a trapezoid where the smaller base is 11, and the bigger base is 20. It
            // must be multiplied by 2 for both sides of the grid.
            Assert.That(grid.Hexes.Count, Is.EqualTo(331));
        }
    }

    class TestHexagonalGridGenerator : MonoBehaviour, IGridGenerator
    {
        public int gridRadius;
        public void Generate(Dictionary<Hex, HexTile> hexes)
        {
            for (int q = -gridRadius; q <= gridRadius; q++)
            {
                int r1 = (int)Mathf.Max(-gridRadius, -q - gridRadius);
                int r2 = (int)Mathf.Min(gridRadius, -q + gridRadius);
                for (int r = r1; r <= r2; r++)
                {
                    Hex pos = new Hex(q, r);
                    GameObject hex = new GameObject(pos.ToString());
                    hex.transform.parent = transform;

                    hex.AddComponent<MeshFilter>();
                    MeshRenderer renderer = hex.AddComponent<MeshRenderer>();
                    hex.AddComponent<MeshCollider>();

                    HexTile hexTile = hex.AddComponent<HexTile>();
                    hexTile.pos = pos;
                    hexTile.vertices = HexVertices.GetVertices(pos, HexTypes.PointyTop);
                    hexTile.center = hexTile.vertices[0];
                    hexTile.triangles = HexVertices.Triangles;

                    hexes[pos] = hexTile;
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class SingleMaterialDiamondShapedGridGenerator : MonoBehaviour, IGridGenerator
    {
        public Hex min;
        public Hex max;
        public HexTypes hexType;
        public bool useMeshes = true;
        public bool useColliders = true;

        public Material hexTileMaterial;
        
        public void Generate(Dictionary<Hex, HexTile> hexes)
        {
            if (hexes == null)
            {
                throw new NullReferenceException();
            }

            for (int q = min.Q; q <= max.Q; q++)
            {
                for (int r = min.R; r <= max.R; r++)
                {
                    // Create the position of the HexTile.
                    Hex pos = new Hex(q, r);
                    // Create the GameObject and set the parent
                    GameObject hex = new GameObject(pos.ToString());
                    hex.transform.parent = transform;

                    // Create meshes and colliders based on the script's settings.
                    if (useMeshes)
                    {
                        hex.AddComponent<MeshFilter>();
                        MeshRenderer renderer = hex.AddComponent<MeshRenderer>();
                        renderer.sharedMaterial = hexTileMaterial;
                    }
                    if (useColliders)
                    {
                        hex.AddComponent<MeshCollider>();
                    }

                    // Create the HexTile and set vertices only if meshes or colliders are used.
                    // No sense creating the vertices if neither of the components that use them exist.
                    HexTile hexTile = hex.AddComponent<HexTile>();
                    hexTile.pos = pos;
                    if (useMeshes || useColliders)
                    {
                        hexTile.vertices = HexVertices.GetVertices(pos, hexType);
                        // GetVertices always puts the center vertex at element 0.
                        hexTile.center = hexTile.vertices[0];
                        hexTile.triangles = HexVertices.Triangles;
                    }

                    hexes[pos] = hexTile;
                }
            }
        }
    }
}

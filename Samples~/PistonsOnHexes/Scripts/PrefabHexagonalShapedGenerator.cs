using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class PrefabHexagonalShapedGenerator : MonoBehaviour, IGridGenerator
{
    public GameObject hexPrefab;

    public int gridRadius = 5;

    public void Generate(Dictionary<Hex, HexTile> hexes)
    {
        // Don't continue if the hexes dictionary hasn't been initialized.
        if (hexes == null)
        {
            throw new NullReferenceException();
        }

        // Save the prefab's position so it can be restored later.
        Vector3 prefabPos = hexPrefab.transform.position;

        // Iterate across the q-axis of the hexagon.
        for (int q = -gridRadius; q <= gridRadius; q++)
        {
            // Define the width of the r-axis based on q.
            int r1 = (int)Mathf.Max(-gridRadius, -q - gridRadius);
            int r2 = (int)Mathf.Min(gridRadius, -q + gridRadius);
            // Iterate across the r-axis.
            for (int r = r1; r <= r2; r++)
            {
                // Define which Hex this is.
                Hex hex = new Hex(q, r);
                // Calculate the world coordinates.
                // Because the vertices are set on the prefab are for flat topped Hexes, the orientation must match. 
                Vector3 center = HexVertices.HexToWorld(HexVertices.FlatTopOrientation, hex);

                // Move the prefab to the correct position.
                // This alters the prefab, but it will be reset later.
                hexPrefab.transform.position = center;

                // Instantiate the prefab and name it as its position.
                GameObject worldHex = Instantiate(hexPrefab, transform);
                worldHex.name = hex.ToString();
                
                // Save the Hex in the Dictionary so it can be referenced later.
                hexes[hex] = worldHex.GetComponent<HexTile>();
            }
        }
        
        // Restore the prefab's original position
        hexPrefab.transform.position = prefabPos;
    }
}

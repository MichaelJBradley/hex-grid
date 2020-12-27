using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A list of Hex types supported by this package.
/// </summary>
public enum HexTypes
{
    FlatTop,
    PointyTop
}

/// <summary>
/// HexVertices defines Vector3[]'s representing the vertices of Hexes.
/// All vertices arrays start with the center vertex and have 0 offset.
/// </summary>
public class HexVertices
{
    /// <summary>
    /// The ratio to determine the z or x component of a vertex depending on
    /// whether the Hex is flat topped or pointy topped, respectively.
    /// It about equals 0.866025 and comes from sin(pi/3) or sin(60°).
    /// </summary>
    private static readonly float HalfSizeRatio = Mathf.Sqrt(3) / 2f;
    
    /// <summary>
    /// Vertices representing a flat topped Hex with a size of 1. The first
    /// vertex is the center at (0, 0, 0).
    /// </summary>
    public static readonly Vector3[] FlatTop = new Vector3[]
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(0.5f, 0, HalfSizeRatio),
        new Vector3(-0.5f, 0, HalfSizeRatio),
        new Vector3(-1f, 0, 0),
        new Vector3(-0.5f, 0, -HalfSizeRatio), 
        new Vector3(0.5f, 0, -HalfSizeRatio)
    };
    
    /// <summary>
    /// Vertices representing a pointy topped Hex with a size of 1. The first
    /// vertex is the center at (0, 0, 0).
    /// </summary>
    public static readonly Vector3[] PointyTop = new Vector3[]
    {
        new Vector3(0, 0, 0), 
        new Vector3(HalfSizeRatio, 0, 0.5f), 
        new Vector3(0, 0, 1f), 
        new Vector3(-HalfSizeRatio, 0, 0.5f), 
        new Vector3(-HalfSizeRatio, 0, -0.5f), 
        new Vector3(0, 0, -1f), 
        new Vector3(HalfSizeRatio, 0, -0.5f)
    };
}

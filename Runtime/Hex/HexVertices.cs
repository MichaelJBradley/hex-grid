﻿using System.ComponentModel;
using UnityEngine;

/// <summary>
/// Holds matrices and angle needed to convert Hex to world coordinates and vice
/// versa.
/// </summary>
public readonly struct Orientation
{
    /// <summary>
    /// The matrix to convert from Hex coordinates to world coordinates.
    /// It has a length of 2 where index 0 is used to calculate x and 1 to
    /// calculate z.
    /// The choice to use FloatHex[] is fairly arbitrary. There may be a better
    /// choice, but it made sense to multiply the Q's and R's of the Hexes
    /// together.
    /// </summary>
    public readonly FloatHex[] ToWorldMatrix;
    /// <summary>
    /// The matrix to convert from world coordinates to Hex coordinates.
    /// It has a length of 2 where index 0 is used to calculate Q and 1 to
    /// calculate R.
    /// The choice to use Vector3[] is fairly arbitrary. There may be a better
    /// choice, but it made sense to multiply the x's and z's of the Vector3s
    /// together.
    /// </summary>
    public readonly Vector3[] ToHexMatrix;
    /// <summary>
    /// The angle of the first corner of the Hex in radians.
    /// </summary>
    public readonly float StartAngle;

    public Orientation(FloatHex[] toWorldMatrix, Vector3[] toHexMatrix, float startAngle)
    {
        ToWorldMatrix = toWorldMatrix;
        ToHexMatrix = toHexMatrix;
        StartAngle = startAngle;
    }
}

/// <summary>
/// Defines data structures and methods to convert Hex to world coordinates and
/// vice versa.
/// </summary>
public static class HexVertices
{
    /// <summary>
    /// The number of vertices it takes to represent a Hex.
    /// </summary>
    public const uint NumVertices = 7;
    
    /// <summary>
    /// The ratio to determine the z or x component of a vertex depending on
    /// whether the Hex is flat topped or pointy topped, respectively.
    /// It about equals 0.866025 and comes from sin(pi/3) or sin(60°).
    /// </summary>
    private static readonly float HalfSizeRatio = Mathf.Sqrt(3) / 2f;
    
    /// <summary>
    /// Defines the vertex indices that define triangles for both FlatTop and
    /// PointyTop vertex arrays.
    /// </summary>
    public static readonly int[] Triangles = new int[] {0, 2, 1, 0, 3, 2, 0, 4, 3, 0, 5, 4, 0, 6, 5, 0, 1, 6};
    
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
    
    /// <summary>
    /// Defines the matrices used to convert a flat topped Hex's vertices
    /// from Hex coordinates to world and vice versa.
    /// Start angle is 0, because the first corner is at (1, 0, 0), which is 0
    /// degrees on the unit sphere.
    /// </summary>
    public static readonly Orientation FlatTopOrientation = new Orientation(
        new FloatHex[]{
            new FloatHex(3.0f / 2.0f, 0.0f),
            new FloatHex(Mathf.Sqrt(3.0f) / 2.0f, Mathf.Sqrt(3.0f))
        },
        new Vector3[]{
            new Vector3(2.0f / 3.0f, 0.0f, 0.0f),
            new Vector3(-1.0f / 3.0f, 0.0f, Mathf.Sqrt(3.0f) / 3.0f)
        },
        0.0f
    );
    
    /// <summary>
    /// Defines the matrices used to convert a pointy topped Hex's vertices
    /// from Hex coordinates to world and vice versa.
    /// Start angle is .5 rad (60 deg) because the first corner is at
    /// (sqrt(3) / 2, 0, 0.5), which is 60 degrees on the unit sphere.
    /// </summary>
    public static readonly Orientation PointyTopOrientation = new Orientation(
        new FloatHex[] {
            new FloatHex(Mathf.Sqrt(3.0f), Mathf.Sqrt(3.0f) / 2.0f),
            new FloatHex(0.0f, 3.0f / 2.0f)
        },
        new Vector3[] {
            new Vector3(Mathf.Sqrt(3.0f) / 3.0f, 0.0f, -1.0f / 3.0f),
            new Vector3(0.0f, 0.0f, 2.0f / 3.0f)
        },
        0.5f
    );
    
    /// <summary>
    /// Calculates a HexTile's vertices in world coordinates given a Hex
    /// position and type.
    /// The HexTile's center will always be at element 0.
    /// </summary>
    /// <param name="pos">The Hex to calculate.</param>
    /// <param name="hexType">The type of Hex to calculate. This determines
    /// at which angle the HexTiles will rotated.</param>
    /// <returns>A Vector3[] of length <c>NumVertices</c> containing the
    /// vertices required to draw the HexTile.</returns>
    /// <exception cref="InvalidEnumArgumentException">Attempting to use an
    /// unsupported Hex type.</exception>
    public static Vector3[] GetVertices(Hex pos, HexTypes hexType)
    {
        Orientation orientation;
        Vector3[] vertices;
        switch (hexType)
        {
            case HexTypes.FlatTop:
                orientation = FlatTopOrientation;
                break;
            case HexTypes.PointyTop:
                orientation = PointyTopOrientation;
                break;
            default:
                throw new InvalidEnumArgumentException("Unsupported Hex type " + hexType);
        }
        vertices = new Vector3[NumVertices];
        vertices[0] = HexToWorld(orientation, pos);
        for (uint i = 1; i < vertices.Length; i++)
        {
            // The first corner is 0 indexed, so subtract one from the index.
            vertices[i] = vertices[0] + CalcCornerOffset(orientation, i - 1);
        }

        return vertices;
    }

    /// <summary>
    /// Calculate the corner offset given the index of a corner.
    /// Corners are 0 indexed, start at Orientation.StartAngle, and go
    /// counterclockwise, increasing by .5 rad each time index increases by 1.
    /// </summary>
    /// <param name="orientation">The type of Hex to calculate. This determines
    /// at which angle the HexTiles will be rotated.</param>
    /// <param name="index">The index of the corner to calculate. For example,
    /// on a flat topped Hex, index 0 represents the far right corner and 4
    /// represents the bottom left corner.</param>
    /// <returns>A Vector3 representing the corner offset from the center of the
    /// Hex.</returns>
    public static Vector3 CalcCornerOffset(Orientation orientation, uint index)
    {
        // Calculate on which angle of the unit circle this corner of the Hex lies.
        float angle = (2.0f * Mathf.PI * (orientation.StartAngle + index)) / 6;
        return new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));
    }

    /// <summary>
    /// Calculates the Hex's center in world coordinates from a Hex given the
    /// Orientation.
    /// The resulting world coordinate is always at y = 0.
    /// </summary>
    /// <param name="orientation">The Orientation struct to use. Normally used
    /// with the predefined FlatTopOrientation or PointyTopOrientation
    /// structs.</param>
    /// <param name="pos">The Hex to calculate.</param>
    /// <returns>A Vector3 representing the world coordinate.</returns>
    public static Vector3 HexToWorld(Orientation orientation, Hex pos)
    {
        return new Vector3(
            (orientation.ToWorldMatrix[0].Q * pos.Q) + (orientation.ToWorldMatrix[0].R * pos.R),
            0.0f,
            (orientation.ToWorldMatrix[1].Q * pos.Q) + (orientation.ToWorldMatrix[1].R * pos.R)
        );
    }

    /// <summary>
    /// Calculates the FloatHex from the world coordinate given the
    /// Orientation.
    /// A FloatHex is returned because the given Vector3 might not always be at
    /// a Hex's exact center. Use the FloatHex.Round method to find the nearest
    /// Hex.
    /// </summary>
    /// <param name="orientation">The Orientation struct to use. Normally used
    /// with the predefined FlatTopOrientation or PointyTopOrientation
    /// structs.</param>
    /// <param name="pos">The Vector3 representing the world coordinate.</param>
    /// <returns>A FloatHex representation the world coordinate in Hex
    /// coordinates.</returns>
    public static FloatHex WorldToHex(Orientation orientation, Vector3 pos)
    {
        return new FloatHex(
            (orientation.ToHexMatrix[0].x * pos.x) + (orientation.ToHexMatrix[0].z * pos.z),
            (orientation.ToHexMatrix[1].x * pos.x) + (orientation.ToHexMatrix[1].z * pos.z));
    }
}

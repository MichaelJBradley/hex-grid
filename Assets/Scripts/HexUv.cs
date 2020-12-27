using System.ComponentModel;
using UnityEngine;

/// <summary>
/// A list of UV texture mappings.
/// Each mapping should have the same name as its associated Vector2[] in HexUv
/// for clarity.
/// </summary>
public enum HexUvTypes
{
    SquareOutline
}

/// <summary>
/// HexUv defines UV mappings for different textures to Hex vertices.
/// Each mapping should be the same length as the number of vertices in a Hex
/// (7).
/// The name of each mapping should be the same as its associated HexUvTypes
/// enum for clarity.
/// </summary>
public class HexUv
{
    /// <summary>
    /// SquareOutline maps a square outline texture to a Hex, drawing an outlined
    /// Hex.
    /// See the FilledOutline.png texture for an example of a "square outline".
    /// </summary>
    public static readonly Vector2[] SquareOutline = {
        new Vector2(0.5f, 0.5f),
        new Vector2(1f, 0.5f),
        new Vector2(1f, 1f), 
        new Vector2(0f, 1f), 
        new Vector2(0f, 0.5f), 
        new Vector2(0f, 0f),
        new Vector2(1f, 0f), 
    };

    /// <summary>
    /// A utility method to ease the retrieval of UV mappings. All default
    /// mappings should be present in this function.
    /// </summary>
    /// <param name="uvType">The UV mapping to get.</param>
    /// <returns>The UV mapping associated with the HexUvTypes enum.</returns>
    /// <exception cref="InvalidEnumArgumentException">Attempting to get an
    /// unsupported UV mapping. This should only ever be encountered when a
    /// HexUvTypes enum has not been added to this method, so it is probably
    /// an issue with the package itself.</exception>
    public static Vector2[] GetUvMapping(HexUvTypes uvType)
    {
        switch (uvType)
        {
            case HexUvTypes.SquareOutline:
                return SquareOutline;
            default:
                throw new InvalidEnumArgumentException("The HexUvType " + uvType + " is not currently supported.");
        }
    }
}

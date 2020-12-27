using UnityEngine;

public class HexTile : MonoBehaviour
{
    public Hex pos;
    public HexUvTypes uvType = HexUvTypes.SquareOutline;
    
    public Vector3[] vertices;
    public int[] triangles;
    public Mesh mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (!meshFilter)
        {
            Debug.LogWarning("HexTile at position " + pos + " has no MeshFilter. It will not be visible in " +
                             "the scene.");
        }
        else
        {
            Debug.Log("Mesh filter found. Generating mesh.");
            mesh = GenerateMesh();
            meshFilter.mesh = mesh;
        }
    }

    /// <summary>
    /// Creates a new Mesh with the this HexTile's vertices, triangles, and UV
    /// mapping.
    /// </summary>
    /// <returns>The newly created Mesh.</returns>
    public Mesh GenerateMesh()
    {
        Mesh ret = new Mesh();
        ret.vertices = vertices;
        ret.triangles = triangles;
        ret.RecalculateNormals();
        ret.uv = HexUv.GetUvMapping(uvType);
        
        return ret;
    }
}

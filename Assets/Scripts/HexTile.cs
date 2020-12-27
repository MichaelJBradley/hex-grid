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
        // Check whether a MeshFilter or MeshCollider exist on the Hex. If at least one of them does, then generate the
        // mesh.
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshFilter || meshCollider)
        {
            mesh = GenerateMesh();
        }
        
        // If the MeshFilter exists, assign the generated mesh.
        if (!meshFilter)
        {
            Debug.LogWarning("HexTile at position " + pos + " has no MeshFilter. It will not be visible in " +
                             "the scene.");
        }
        else
        {
            meshFilter.mesh = mesh;
        }

        // If the MeshCollider exists, assign the generated mesh.
        if (!meshCollider)
        {
            Debug.Log("HexTile at position " + pos + " has no MeshCollider.");
        }
        else
        {
            Debug.Log("MeshCollider found, adding Mesh.");
            meshCollider.sharedMesh = mesh;
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

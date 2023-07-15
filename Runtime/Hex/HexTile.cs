using UnityEngine;

public class HexTile : MonoBehaviour
{
    public Hex pos;
    public HexUvTypes uvType = HexUvTypes.SquareOutline;
    
    public Vector3[] vertices;
    public int[] triangles;
    public Mesh mesh;

    public Vector3 center;

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
        if (meshCollider) 
        {
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
    
    private IHexTileProperties<int> propertyInt32;

    /// <summary>
    /// A convenience property to get and set int properties.
    /// 
    /// If propertySingle is null, it loads the Component from the GameObject
    /// and saves a reference to it.
    /// </summary>
    public IHexTileProperties<int> PropertyInt32 {
        get
        {
            if (propertyInt32 == null) {
                propertyInt32 = GetComponent<IHexTileProperties<int>>();
            }
            return propertyInt32;
        }

        set
        {
            propertyInt32 = value;
        }
    }

    private IHexTileProperties<float> propertySingle;

    /// <summary>
    /// A convenience property to get and set float properties.
    /// 
    /// If propertySingle is null, it loads the Component from the GameObject
    /// and saves a reference to it.
    /// </summary>
    public IHexTileProperties<float> PropertySingle {
        get
        {
            if (propertySingle == null) {
                propertySingle = GetComponent<IHexTileProperties<float>>();
            }
            return propertySingle;
        }

        set
        {
            propertySingle = value;
        }
    }

    private IHexTileProperties<bool> propertyBoolean;

    /// <summary>
    /// A convenience property to get and set boolean properties. 
    /// 
    /// If propertyBoolean is null, it loads the Component from the GameObject
    /// and saves a reference to it.
    /// </summary>
    public IHexTileProperties<bool> PropertyBoolean {
        get
        {
            if (propertyBoolean == null) {
                propertyBoolean = GetComponent<IHexTileProperties<bool>>();
            }
            return propertyBoolean;
        }

        set
        {
            propertyBoolean = value;
        }
    }

    /// <summary>
    /// Convenience method to retrieve an IHexTileProperty component from this
    /// GameObject.
    ///
    /// There is no caching involved, this method is purely for convenience.
    /// </summary>
    /// <typeparam name="T">The type of property to retrieve.</typeparam>
    /// <returns>The retrieved IHexTileProperty</returns>
    public IHexTileProperties<T> GetProperty<T>() {
        return GetComponent<IHexTileProperties<T>>();
    }
}

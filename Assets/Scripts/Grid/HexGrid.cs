using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class HexGrid : MonoBehaviour
    {
        public Dictionary<Hex, HexTile> Hexes;

        // Start is called before the first frame update
        void Start()
        {
            Hexes = new Dictionary<Hex, HexTile>();
            IGridGenerator gridGenerator = GetComponent<IGridGenerator>();
            if (gridGenerator == null)
            {
                Debug.LogWarning("No script that implements IGridGenerator exists on " + name + ". No" +
                                 " HexTiles will be generated.");
            }
            else
            {
                gridGenerator.Generate(Hexes);
            }
        }
    }
}

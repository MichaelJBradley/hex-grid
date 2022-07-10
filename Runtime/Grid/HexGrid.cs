using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class HexGrid : MonoBehaviour
    {
        /// <summary>
        /// Holds all HexTiles with key as the HexTile's Hex position.
        /// Should be filled in by an IGridGenerator's Generate function.
        /// </summary>
        public Dictionary<Hex, HexTile> Hexes;

        /// <summary>
        /// Determines whether the HexGrid will generate at the start of the scene.
        /// </summary>
        public bool generateOnStart = true;

        private bool loaded;
        /// <summary>
        /// Flag to identify whether Hex grid has been generated.
        /// </summary>
        public bool Loaded => loaded;

        // Start is called before the first frame update
        void Start()
        {
            loaded = false;
            Hexes = new Dictionary<Hex, HexTile>();
            if (generateOnStart)
            {
                GenerateGrid();
            }
        }

        public void GenerateGrid() {
            // Lazy load in case the IGridGenerator will be added later.
            IGridGenerator gridGenerator = GetComponent<IGridGenerator>();
            if (gridGenerator == null)
            {
                Debug.LogWarning("No script that implements IGridGenerator exists on " + name + ". No" +
                                 " HexTiles will be generated.");
            }
            else 
            {
                gridGenerator.Generate(Hexes);
                loaded = true;
            }
        }
    }
}

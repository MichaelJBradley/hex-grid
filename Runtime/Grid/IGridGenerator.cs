using System.Collections.Generic;

namespace Grid 
{
    /// <summary>
    /// Exposes a method to generate HexGrids.
    /// 
    /// Generally implemented by a script that instantiates HexTiles in the
    /// scene.
    /// </summary>
    public interface IGridGenerator
    {
        /// <summary>
        /// Generates a HexGrid, and stores the resulting HexTiles in the
        /// Dictionary by Hex.
        /// </summary>
        /// <param name="grid">The Dictionary in which to store generated
        /// HexTiles.</param>
        public void Generate(Dictionary<Hex, HexTile> grid);
        // TODO: Apparently this can't be implemented because it's private. And it should have been
        // public to begin with.
    }
}

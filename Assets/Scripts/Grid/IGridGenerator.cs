using System.Collections.Generic;

namespace Grid 
{
    public interface IGridGenerator
    {
        void Generate(Dictionary<Hex, HexTile> grid);
    }
}

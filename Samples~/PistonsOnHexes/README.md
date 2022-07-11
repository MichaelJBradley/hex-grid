# Pistons on hexes

This sample generates a hexagonal grid from a HexTile prefab.

The `Hexagonal Grid` GameObject has three script components.

1. The `Hex Grid` script holds a Collection of HexTiles, and calls the `Generate` method on start.
1. The `Prefab Hexagonal Shaped Generator` script defines the `Generate` method. The `Generate` method uses the
   `Piston Hex Tile` prefab to create the Hex grid with a radius of 5 hexes.
1. The `Piston Mover` script moves the pistons defined by the `Pistons To Move` list up and down at a random speed. The
   `Pistons To Move` list uses the Hex position to determine which pistons to move.

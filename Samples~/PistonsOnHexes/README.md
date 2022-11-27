# Pistons on hexes

This sample generates a hexagonal grid from a HexTile prefab.

## Features

Pistons on hexes demonstrates how to generate a hexagonal grid, because it isn't as simple as writing a double for loop
iterating over the radius.

The basic idea is to iterate over an axis (in this case Q) from `-1 * grid_radius` to `grid_radius`. For
each iteration, calculate the the width of the grid along the other axis (in this case R). The width of the R axis
changes along the Q axis, where it is widest in the middle and smallest at each end.

It also demonstrates how to select `HexTile`s from the `HexGrid` by `Hex` position. At which point, the retrieving
script can perform any necessary actions.

## Scene

The `Hexagonal Grid` GameObject has three script components.

1. The `HexGrid` script holds a Collection of HexTiles, and calls the `Generate` method on start.
1. The `Prefab Hexagonal Shaped Generator` script defines the `Generate` method. The `Generate` method uses the
   `Piston Hex Tile` prefab to create the Hex grid with a radius of 5 hexes.
1. The `Piston Mover` script moves the pistons defined by the `Pistons To Move` list up and down at a random speed. The
   `Pistons To Move` list uses the Hex position to determine which pistons to move.

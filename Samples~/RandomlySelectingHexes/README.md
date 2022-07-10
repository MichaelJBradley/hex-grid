# Randomly selecting hexes

This sample generates a Hex grid in the shape of a parallelogram.

The `ParallelogrammaticGrid` GameObject has two script components.

1. The `Hex Grid` script holds a Collection of HexTiles, and calls the `Generate` method on start.
1. The `Single Material Diamond Shaped Grid Generator` defines the `Generate` method. It creates Hex tiles at runtime,
   rather than from a prefab. It defines a `Hex Type` field which can be used to change the Hex tiles' orientation.

The `Selector` GameObject has a reference to the `ParallelogrammaticGrid`, which it uses to select a single Hex tile,
and change its outline color.


# Randomly selecting hexes

This sample generates a Hex grid in the shape of a parallelogram.

## Features

Randomly selecting hexes demonstrates the most basic grid generation. That is, iterating over a length and width with a
double for loop.

When generating a grid this way, the loop iterates over the two axes, Q and R. The two axes bisect each other at a 60°
angle, resulting in the diamond shape.

It also demonstrates how to select `HexTile`s from the `HexGrid` by `Hex` position. At which point, the retrieving
script can perform any necessary actions.

## Scene

The `ParallelogrammaticGrid` GameObject has two script components.

1. The `HexGrid` script holds a Collection of HexTiles, and calls the `Generate` method on start.
1. The `Single Material Diamond Shaped Grid Generator` defines the `Generate` method. It creates Hex tiles at runtime,
   rather than from a prefab. It defines a `HexType` field which can be used to change the Hex tiles' orientation.

The `Selector` GameObject has a reference to the `ParallelogrammaticGrid`, which it uses to select a single Hex tile,
and change its outline color.

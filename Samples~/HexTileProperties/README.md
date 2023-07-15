# HexTile properties

This sample generates a rectangular grid with `HexTile` properties.

## Features

### Properties

HexTile properties demonstrates one possible way to add a set of properties to each `HexTile`. It uses a 
`ScriptableObject` to avoid duplicating the same property map for each `HexTile`, or put another way, only one instance
of each property map exists for all similar `HexTile`s.

Currently, `HexTiles` each maintain a reference to three property types (`int`, `bool`, `string`). The idea here was to
have some standard way to access properties that may be used for other features within Hex Grid. However, this method is
a little cumbersome when using `ScriptableObject`s and may change in the future.

To use a `ScriptableObject` as the source of `HexTile` properties, this sample creates one `ScriptableObject` for each
flavor of `HexTile`. In this case, they are based on the color of the tile. During `HexGrid` generation, the `Generator`
creates `HexTile`s as normal, but it also adds an `IHexTileProperty` implementation for `int` and `bool` properties. It
then sets the underlying `Dictionary` references to those from the `ScriptableObjects`. When the property map is
accessed through the `HexTile` script, it loads the `IHexTileProperty` from the GameObject and saves a reference to it.

### Rectangular grid generation

HexTile properties also demonstrates how to generate a rectangular `HexGrid`.

Rectangular grids use offset coordinates to determine hex placement because, as seen in the
[Randomly selecting hexes sample](https://github.com/MichaelJBradley/hex-grid/tree/master/Samples~/RandomlySelectingHexes),
simply iterating over the axial coordinates generates a parallelogram.

The corners of the grid are determined by putting the origin as close to the middle as possible. That is, each corner is
about half the length and width away from the origin.

Once the corners are determined, the generator can iterate from min to max along the first axis. Where pointy tops use
top for min and bottom for max, and flat use left for min and right for max. For each tile, determine the offset by
taking the current position and dividing by two. In the sample, odd rows are offset.

Then, it's as simple as iterating over the other axis (top to bottom for flat tops and left to right for pointy).
Detemermine `Hex` positions by setting `Q` and `R` to `x` and `y`, respectively, for pointy tops, or vice versa for flat.

## Scene

The `Hexagonal Grid` GameObject has two script components

1. The `HexGrid` script holds a Collection of `HexTile`s, and calls the `Generate` method on start.
1. The `Rectangular Grid Generator` script defines the `Generate` method. The `Generate` method generates
   `HexTiles` at runtime using properties from the `Tile` list.

The `Tile` list holds references to `ScriptableObjects` that define each `HexTile`s properties. In this scene `HexTile`s
have the following properties, in addition to referencing the color defined by the `ScriptableObjects`.

1. Movement cost (`int`)
1. Impassible (`bool`)

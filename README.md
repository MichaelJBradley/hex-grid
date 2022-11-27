# Hex Grid

A Unity package designed to implement a hex grid for use as a map or anything else!

## Overview

Hex Grid supports the open ended generation of hex grids by handling the hex tile GameObject creation. It is meant to be
as flexible as possible, so users must write the logic to generate a hex grid. Hex Grid, handles the underlying hex tile GameObject creation.

## Installation instructions

Hex Grid adheres to Unity's package standards, so it can be installed using Unity's package manager.

This package does not exist in a registry. The two options are either
* [Install from GitHub](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
* Download the latest release and [install from tarball](https://docs.unity3d.com/Manual/upm-ui-tarball.html)

## Requirements

Hex Grid has been tested with Unity 2021.3.2f1, but may be compatible with other releases.

## Workflows

### Generating a grid

To generate a grid, create an empty GameObject.

![new hex grid object](https://github.com/MichaelJBradley/hex-grid-docs/blob/main/images/readme/workflow/new-hex-grid-gameobject.png?raw=true)

Add a `HexGrid` script component from the HexGrid package.

![add hex grid script](https://github.com/MichaelJBradley/hex-grid-docs/blob/main/images/readme/workflow/add-hex-grid-script.png?raw=true)

The `HexGrid` maintains references to `HexTile`s, and is responsible generating the grid from a grid generator. By
default, `HexGrid` generates on Start.

Create a new C# script for the grid generator, that implements both `MonoBehavior` and `IGridGenerator`.

The grid generator holds any references to grid settings (size, orientation) and `HexTile` components (materials, prefabs, etc). For example grid generators, see: [Samples](#samples)

The finished GameObject would look similar to this. In this case the grid generator has a material and two `Hex`
fields for generating the grid.

![completed hex grid](https://github.com/MichaelJBradley/hex-grid-docs/blob/main/images/readme/workflow/completed-hex-grid-gameobject.png?raw=true)

Play the scene, and the `HexGrid` generates grid at runtime.

## Reference

[API reference](https://michaeljbradley.github.io/hex-grid-docs/index.html)

## Samples

Hex Grid includes samples to demonstrate most, if not all available features. Each sample includes a README with more
information.

| Sample | Feature demonstrated |
| - | - |
| [Pistons on hexes](https://github.com/MichaelJBradley/hex-grid/tree/master/Samples%7E/PistonsOnHexes) | - Hexagonal grid generation<br> - Generating grids from prefabs |
| [Randomly selecting hexes](https://github.com/MichaelJBradley/hex-grid/tree/master/Samples%7E/RandomlySelectingHexes) | - Generating `HexTile`s at runtime<br> - Selecting `HexTile`s by hex position |

## Credits

A great deal of this code was lovingly ~~stolen~~ adapted from Red Blob Games'
[Implementation of Hex Grids](https://www.redblobgames.com/grids/hexagons/implementation.html).
# Hex Grid

A Unity package designed to implement a hex grid for use as a map or anything else!

## Overview

Hex Grid supports the open ended generation of hex grids by handling the hex tile GameObject creation. It is meant to be
as flexible as possible, so users must write the logic to generate a hex grid. Hex Grid, however, handles the underlying
hex tile GameObject creation.

## Installation instructions

Hex Grid conforms to Unity's package standards, so it can be installed using Unity's package manager.

This package does not exist in a registry. The two options are either
* [Install from GitHub](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
* Download the latest release and [install from tarball](https://docs.unity3d.com/Manual/upm-ui-tarball.html)

## Requirements

Hex Grid has been tested with Unity 2021.3.2f1, but may be compatible with other releases.

## Workflows

### Generating a grid

**TODO: ADD IMAGES FROM GIST**

To generate a grid, create an empty GameObject and attach a `Hex Grid` script component.

The `Hex Grid` maintains references to `Hex Tiles`, and is responsible generating the grid from a grid generator. By
default, `Hex Grid` generates on Start.

Create a new C# script for the grid generator, that implements both `MonoBehavior` and `IGridGenerator`.

The grid generator holds any references to grid settings (size, orientation) and `Hex Tile` components (materials, prefabs, etc). For example grid generators, see: [Samples](#samples)

Play the scene, and the `Hex Grid` generates grid at runtime.

## Reference

[API reference](https://michaeljbradley.github.io/hex-grid-docs/index.html)

## Samples

Hex Grid includes samples to demonstrate most, if not all available features. Each sample includes a README with more
information.

| Sample | Feature demonstrated |
| - | - |
| [Pistons on hexes](https://github.com/MichaelJBradley/hex-grid/tree/master/Samples%7E/PistonsOnHexes) | - Hexagonal grid generation<br> - Generating grids from prefabs |
| [Randomly selecting hexes](https://github.com/MichaelJBradley/hex-grid/tree/master/Samples%7E/RandomlySelectingHexes) | - Generating `Hex Tiles` at runtime<br> - Selecting `Hex Tiles` by hex position |

## Credits

A great deal of this code was lovingly ~~stolen~~ adapted from Red Blob Games'
[Implementation of Hex Grids](https://www.redblobgames.com/grids/hexagons/implementation.html).
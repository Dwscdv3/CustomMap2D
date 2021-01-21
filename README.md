# CustomMap2D

<small>(That name is a meme.)</small>

CustomMap2D is an improvised ill-designed project used to generate Minecraft map art as their final `.dat` NBT files.

The current version is designed for 1.16. For 1.12+ compatibility, check the v1.0 release.

## Color Tables Overview

| Version | Type      | Colors |
|---------|:---------:|-------:|
| 1.16    | Full      |    232 |
| 1.16    | Staircase |    174 |
| 1.16    | Flat      |     58 |
| 1.12    | Full      |    204 |
| 1.12    | Staircase |    153 |
| 1.12    | Flat      |     51 |

* Full: every colors possible (include colors that exist but not used in game)
* Staircase: every colors used in game (exclude 4th shade of every colorset)
* Flat: only 2nd shade of every colorset

> Staircase and Flat are the name of two techniques to create map art by arranging blocks (without external tools).

## How to Use

Note:

* This method needs write access to the file system, this essentially means the game server must be managed by you if you play multiplayer.

### Preprocessing the Images

Requirements for an input image:

1. Its width and height must be integer multiples of 128.
2. Its color palette must match the program's hard-coded palette. (There are ACT files for Adobe Photoshop in the `extra/` directory, or you can look into the code to make one for another image processing software.)

Steps for Adobe Photoshop:

1. Download the needed Adobe Photoshop color table in the `extra/` directory.
2. Open images with Adobe Photoshop.
3. Crop and resize the images to match requirement #1.
4. File > Export > Save for Web (Legacy) (Alt+Shift+Ctrl+S).
5. Select PNG-8 as the format, select "Load color table…" from the drop-down menu of the section "Color Table", load with the color table you just downloaded.
6. Save.

### Generating Map Data

To generate for all `.bmp`, `.gif` and `.png` files under the current working directory, simply execute it without arguments:

```
CM2D
```

To generate for specific files:

```
CM2D file1 file2 file3 ...
```

File name must be a negative integer number, e.g. `-1000.png`. This number will be the actual map ID used in game.

Input image with width or height greater than 128px will be splitted into many files, with auto-decreasing IDs:

> map_-1000.dat  
> map_-1001.dat  
> map_-1002.dat  
> ...

Put these `.dat` files into `.minecraft/saves/<WORLD_NAME>/data/` for local worlds, or `<MC_SERVER_PATH>/world/data/` for multiplayer servers.

### In-game

There's no need to restart the world or server to load these new `.dat` files. However, a restart is needed to replace existing files.

You can get map items with the command `/give @p minecraft:filled_map{map:<ID_number>}`.


## Still Confused?

Most technical details can be found at [the wiki](https://minecraft.gamepedia.com/Map_item_format).

For any further questions, you can probably read the code if you know C#, it's even shorter than this README (besides embedded data such as the color table).

## Pending Features

<small>(They are unlikely to be added unless someone requested them.)</small>

* Custom color palette (currently v1.16 palette is hard-coded)
* Input image resizing and dithering

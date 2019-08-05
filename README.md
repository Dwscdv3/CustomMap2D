# CustomMap2D

<small>(Apparently that name is a meme.)</small>

CustomMap2D is an improvised ill-designed project used to generate Minecraft map
art as their final `.dat` NBT files.

Currently, only properly converted or created images are supported. That is,
images with exactly 128 x 128 size, and only contain version 1.12 colors
mentioned on [this page](https://minecraft.gamepedia.com/Map_item_format).

## How to Use

<small>(Assuming you have imtermediate command-line knowledge since here is
GitHub.)</small>

Warning: This method requires direct modifying of world directory. since we are
just creating some new files inside it, your save is generally safe. However,
back-up your save first if you can't figure out what you are doing!

And because of that, this method can't be used on servers you don't own,
obviously.

### Preprocessing the Images

Preprocess the input images first. To make this step easier, I made the needed
Adobe Photoshop color table in the `extra/` directory. Press Ctrl+Alt+Shift+S,
select PNG-8 as the format, select "Load color table¡­" from the drop down menu
of section "Color Table", then open that color table file you just downloaded.
(Excuse for the inaccurate of menu literals if there is any, I'm not using the
English version Photoshop.)

And don't forget to crop the image to 128 x 128.

> If you aren't a Photoshop user¡­  
> Read the link above to make a color table for your favorite image processing
> software.

> For the reason I've decided to not bundle these steps in the program? Because
> those professional softwares generally have better algorithms than many image
> processing libraries (and my shitty code), you can even fine-tune the
> parameters to find a best setting for a specific image. (some algorithm just
> performs worse with certain images.)

### Generating Map Data

To generate for all `.bmp`, `.gif` and `.png` files under the current working
directory, simply execute it without arguments:
```
/path/to/CM2D
```

To generate for specific files:
```
/path/to/CM2D file1 file2 file3 ...
```
Wildcards has not been supported yet.

Output files will be under the current working directory named
`map_<original_file_name_without_extension>.dat`, but you need to naming them
`map_<32-bit_signed_integer>.dat` (such as `map_-1.dat`) to use them in game.
Negative numbers are recommended, since they never conflict with normal maps.

Put these `.dat` files inside `<world>/data/`.

### In-game

There's no need to restart the world or server to load these new `.dat` files.
Just enter the command `/give @p minecraft:filled_map{map:<the_ID>}` (such as
`/give @p minecraft:filled_map{map:-1}`). And whoops, that's the "map".

## Still Confused?

Most technical details can be found at the link above.

For any further questions, you can probably read the code if you know C#, it's
not even as long as this README (Besides embedded data such as the color table).

## Pending Features

<small>(They are unlikely to be added unless someone requested them.)</small>

* Subset of the color palette
* Input image resizing and dithering
* Split an image into multiple maps

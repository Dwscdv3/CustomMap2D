# CustomMap2D

<small>(That name is a meme.)</small>

CustomMap2D is an improvised ill-designed project used to generate Minecraft map art as their final `.dat` NBT files.

Currently, only properly converted or created images are supported. That is, images with exactly 128 x 128 size, and only contain version 1.12 colors mentioned on [this page](https://minecraft.gamepedia.com/Map_item_format).

## How to Use

<small>(Assuming you have intermediate level command-line knowledge since here is GitHub.)</small>

Warning: This method requires direct modifying of world directory. Your save will be generally safe, however, back-up your save first if you can't figure out what you are doing!

And because of that, you can't use this method on servers you don't own, obviously.

Angle brackets and its content are placeholders.

### Preprocessing the Images

Reduce the input images' color to a limited set first. To make this step easier, you can download the needed Adobe Photoshop color table in the `extra/` directory. Press Ctrl+Alt+Shift+S, select PNG-8 as the format, select "Load color table…" from the drop-down menu of the section "Color Table", then open that color table you just downloaded. (Excuse for the inaccurate of menu literals if there is any, I'm not using the English version Photoshop.)

And don't forget to crop the image to 128 x 128.

> If you aren't a Photoshop user…  
> Read the link above to make a color table for your favorite image processing software. Feel free to contribute if you made one!

> The reason I've decided to not bundle these steps in the program? Because professional software generally has better algorithms (especially dithering algorithms) than many image processing libraries and my shitty code, and you can even fine-tune the parameters to find out the best setting for a specific image. (Some algorithms just performs worse with certain images.)

### Generating Map Data

To generate for all `.bmp`, `.gif` and `.png` files under the current working directory, simply execute it without arguments:

```
CM2D
```

To generate for specific files:

```
CM2D file1 file2 file3 ...
```

Wildcards haven't been supported yet.

Output files will be under the current working directory named `map_<original_file_name_without_extension>.dat`, but you need to name them `map_<32-bit_signed_integer>.dat` to use them in-game. Negative numbers are recommended since they never conflict with normal maps.

Put these `.dat` files inside `<world>/data/`.

### In-game

There's no need to restart the world or server to load these new `.dat` files. Just enter the command `/give @p minecraft:filled_map{map:<ID_number>}`, that's it. The most exciting thing is you can not only use it as paintings but also a kind of custom texture to enhance your amazing buildings.

## Still Confused?

Most technical details can be found at the link above.

For any further questions, you can probably read the code if you know C#, it's even shorter than this README (besides embedded data such as the color table).

## Pending Features

<small>(They are unlikely to be added unless someone requested them.)</small>

* Subsets of the color palette
* Input image resizing and dithering
* Split an image into multiple maps

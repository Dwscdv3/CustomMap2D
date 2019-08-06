using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using fNbt;
using McMaster.Extensions.CommandLineUtils;

namespace Dwscdv3.CustomMap2D
{
    [Command(AllowArgumentSeparator = true, ThrowOnUnexpectedArgument = false)]
    internal class Program
    {
        private const int SideLength = 128;
        
        public Bitmap Bitmap { get; private set; }

        public Dictionary<Color, byte> ColorTable = new Dictionary<Color, byte>
        {
            { Color.FromArgb(0x59, 0x7D, 0x27),   4 },
            { Color.FromArgb(0x6D, 0x99, 0x30),   5 },
            { Color.FromArgb(0x7F, 0xB2, 0x38),   6 },
            { Color.FromArgb(0x43, 0x5E, 0x1D),   7 },
            { Color.FromArgb(0xAE, 0xA4, 0x73),   8 },
            { Color.FromArgb(0xD5, 0xC9, 0x8C),   9 },
            { Color.FromArgb(0xF7, 0xE9, 0xA3),  10 },
            { Color.FromArgb(0x82, 0x7B, 0x56),  11 },
            { Color.FromArgb(0x8C, 0x8C, 0x8C),  12 },
            { Color.FromArgb(0xAB, 0xAB, 0xAB),  13 },
            { Color.FromArgb(0xC7, 0xC7, 0xC7),  14 },
            { Color.FromArgb(0x69, 0x69, 0x69),  15 },
            { Color.FromArgb(0xB4, 0x00, 0x00),  16 },
            { Color.FromArgb(0xDC, 0x00, 0x00),  17 },
            { Color.FromArgb(0xFF, 0x00, 0x00),  18 },
            { Color.FromArgb(0x87, 0x00, 0x00),  19 },
            { Color.FromArgb(0x70, 0x70, 0xB4),  20 },
            { Color.FromArgb(0x8A, 0x8A, 0xDC),  21 },
            { Color.FromArgb(0xA0, 0xA0, 0xFF),  22 },
            { Color.FromArgb(0x54, 0x54, 0x87),  23 },
            { Color.FromArgb(0x75, 0x75, 0x75),  24 },
            { Color.FromArgb(0x90, 0x90, 0x90),  25 },
            { Color.FromArgb(0xA7, 0xA7, 0xA7),  26 },
            { Color.FromArgb(0x58, 0x58, 0x58),  27 },
            { Color.FromArgb(0x00, 0x57, 0x00),  28 },
            { Color.FromArgb(0x00, 0x6A, 0x00),  29 },
            { Color.FromArgb(0x00, 0x7C, 0x00),  30 },
            { Color.FromArgb(0x00, 0x41, 0x00),  31 },
            { Color.FromArgb(0xB4, 0xB4, 0xB4),  32 },
            { Color.FromArgb(0xDC, 0xDC, 0xDC),  33 },
            { Color.FromArgb(0xFF, 0xFF, 0xFF),  34 },
            { Color.FromArgb(0x87, 0x87, 0x87),  35 },
            { Color.FromArgb(0x73, 0x76, 0x81),  36 },
            { Color.FromArgb(0x8D, 0x90, 0x9E),  37 },
            { Color.FromArgb(0xA4, 0xA8, 0xB8),  38 },
            { Color.FromArgb(0x56, 0x58, 0x61),  39 },
            { Color.FromArgb(0x6A, 0x4C, 0x36),  40 },
            { Color.FromArgb(0x82, 0x5E, 0x42),  41 },
            { Color.FromArgb(0x97, 0x6D, 0x4D),  42 },
            { Color.FromArgb(0x4F, 0x39, 0x28),  43 },
            { Color.FromArgb(0x4F, 0x4F, 0x4F),  44 },
            { Color.FromArgb(0x60, 0x60, 0x60),  45 },
            { Color.FromArgb(0x70, 0x70, 0x70),  46 },
            { Color.FromArgb(0x3B, 0x3B, 0x3B),  47 },
            { Color.FromArgb(0x2D, 0x2D, 0xB4),  48 },
            { Color.FromArgb(0x37, 0x37, 0xDC),  49 },
            { Color.FromArgb(0x40, 0x40, 0xFF),  50 },
            { Color.FromArgb(0x21, 0x21, 0x87),  51 },
            { Color.FromArgb(0x64, 0x54, 0x32),  52 },
            { Color.FromArgb(0x7B, 0x66, 0x3E),  53 },
            { Color.FromArgb(0x8F, 0x77, 0x48),  54 },
            { Color.FromArgb(0x4B, 0x3F, 0x26),  55 },
            { Color.FromArgb(0xB4, 0xB1, 0xAC),  56 },
            { Color.FromArgb(0xDC, 0xD9, 0xD3),  57 },
            { Color.FromArgb(0xFF, 0xFC, 0xF5),  58 },
            { Color.FromArgb(0x87, 0x85, 0x81),  59 },
            { Color.FromArgb(0x98, 0x59, 0x24),  60 },
            { Color.FromArgb(0xBA, 0x6D, 0x2C),  61 },
            { Color.FromArgb(0xD8, 0x7F, 0x33),  62 },
            { Color.FromArgb(0x72, 0x43, 0x1B),  63 },
            { Color.FromArgb(0x7D, 0x35, 0x98),  64 },
            { Color.FromArgb(0x99, 0x41, 0xBA),  65 },
            { Color.FromArgb(0xB2, 0x4C, 0xD8),  66 },
            { Color.FromArgb(0x5E, 0x28, 0x72),  67 },
            { Color.FromArgb(0x48, 0x6C, 0x98),  68 },
            { Color.FromArgb(0x58, 0x84, 0xBA),  69 },
            { Color.FromArgb(0x66, 0x99, 0xD8),  70 },
            { Color.FromArgb(0x36, 0x51, 0x72),  71 },
            { Color.FromArgb(0xA1, 0xA1, 0x24),  72 },
            { Color.FromArgb(0xC5, 0xC5, 0x2C),  73 },
            { Color.FromArgb(0xE5, 0xE5, 0x33),  74 },
            { Color.FromArgb(0x79, 0x79, 0x1B),  75 },
            { Color.FromArgb(0x59, 0x90, 0x11),  76 },
            { Color.FromArgb(0x6D, 0xB0, 0x15),  77 },
            { Color.FromArgb(0x7F, 0xCC, 0x19),  78 },
            { Color.FromArgb(0x43, 0x6C, 0x0D),  79 },
            { Color.FromArgb(0xAA, 0x59, 0x74),  80 },
            { Color.FromArgb(0xD0, 0x6D, 0x8E),  81 },
            { Color.FromArgb(0xF2, 0x7F, 0xA5),  82 },
            { Color.FromArgb(0x80, 0x43, 0x57),  83 },
            { Color.FromArgb(0x35, 0x35, 0x35),  84 },
            { Color.FromArgb(0x41, 0x41, 0x41),  85 },
            { Color.FromArgb(0x4C, 0x4C, 0x4C),  86 },
            { Color.FromArgb(0x28, 0x28, 0x28),  87 },
            { Color.FromArgb(0x6C, 0x6C, 0x6C),  88 },
            { Color.FromArgb(0x84, 0x84, 0x84),  89 },
            { Color.FromArgb(0x99, 0x99, 0x99),  90 },
            { Color.FromArgb(0x51, 0x51, 0x51),  91 },
            { Color.FromArgb(0x35, 0x59, 0x6C),  92 },
            { Color.FromArgb(0x41, 0x6D, 0x84),  93 },
            { Color.FromArgb(0x4C, 0x7F, 0x99),  94 },
            { Color.FromArgb(0x28, 0x43, 0x51),  95 },
            { Color.FromArgb(0x59, 0x2C, 0x7D),  96 },
            { Color.FromArgb(0x6D, 0x36, 0x99),  97 },
            { Color.FromArgb(0x7F, 0x3F, 0xB2),  98 },
            { Color.FromArgb(0x43, 0x21, 0x5E),  99 },
            { Color.FromArgb(0x24, 0x35, 0x7D), 100 },
            { Color.FromArgb(0x2C, 0x41, 0x99), 101 },
            { Color.FromArgb(0x33, 0x4C, 0xB2), 102 },
            { Color.FromArgb(0x1B, 0x28, 0x5E), 103 },
            { Color.FromArgb(0x48, 0x35, 0x24), 104 },
            { Color.FromArgb(0x58, 0x41, 0x2C), 105 },
            { Color.FromArgb(0x66, 0x4C, 0x33), 106 },
            { Color.FromArgb(0x36, 0x28, 0x1B), 107 },
            { Color.FromArgb(0x48, 0x59, 0x24), 108 },
            { Color.FromArgb(0x58, 0x6D, 0x2C), 109 },
            { Color.FromArgb(0x66, 0x7F, 0x33), 110 },
            { Color.FromArgb(0x36, 0x43, 0x1B), 111 },
            { Color.FromArgb(0x6C, 0x24, 0x24), 112 },
            { Color.FromArgb(0x84, 0x2C, 0x2C), 113 },
            { Color.FromArgb(0x99, 0x33, 0x33), 114 },
            { Color.FromArgb(0x51, 0x1B, 0x1B), 115 },
            { Color.FromArgb(0x11, 0x11, 0x11), 116 },
            { Color.FromArgb(0x15, 0x15, 0x15), 117 },
            { Color.FromArgb(0x19, 0x19, 0x19), 118 },
            { Color.FromArgb(0x0D, 0x0D, 0x0D), 119 },
            { Color.FromArgb(0xB0, 0xA8, 0x36), 120 },
            { Color.FromArgb(0xD7, 0xCD, 0x42), 121 },
            { Color.FromArgb(0xFA, 0xEE, 0x4D), 122 },
            { Color.FromArgb(0x84, 0x7E, 0x28), 123 },
            { Color.FromArgb(0x40, 0x9A, 0x96), 124 },
            { Color.FromArgb(0x4F, 0xBC, 0xB7), 125 },
            { Color.FromArgb(0x5C, 0xDB, 0xD5), 126 },
            { Color.FromArgb(0x30, 0x73, 0x70), 127 },
            { Color.FromArgb(0x34, 0x5A, 0xB4), 128 },
            { Color.FromArgb(0x3F, 0x6E, 0xDC), 129 },
            { Color.FromArgb(0x4A, 0x80, 0xFF), 130 },
            { Color.FromArgb(0x27, 0x43, 0x87), 131 },
            { Color.FromArgb(0x00, 0x99, 0x28), 132 },
            { Color.FromArgb(0x00, 0xBB, 0x32), 133 },
            { Color.FromArgb(0x00, 0xD9, 0x3A), 134 },
            { Color.FromArgb(0x00, 0x72, 0x1E), 135 },
            { Color.FromArgb(0x5B, 0x3C, 0x22), 136 },
            { Color.FromArgb(0x6F, 0x4A, 0x2A), 137 },
            { Color.FromArgb(0x81, 0x56, 0x31), 138 },
            { Color.FromArgb(0x44, 0x2D, 0x19), 139 },
            { Color.FromArgb(0x4F, 0x01, 0x00), 140 },
            { Color.FromArgb(0x60, 0x01, 0x00), 141 },
            { Color.FromArgb(0x70, 0x02, 0x00), 142 },
            { Color.FromArgb(0x3B, 0x01, 0x00), 143 },
            { Color.FromArgb(0x93, 0x7C, 0x71), 144 },
            { Color.FromArgb(0xB4, 0x98, 0x8A), 145 },
            { Color.FromArgb(0xD1, 0xB1, 0xA1), 146 },
            { Color.FromArgb(0x6E, 0x5D, 0x55), 147 },
            { Color.FromArgb(0x70, 0x39, 0x19), 148 },
            { Color.FromArgb(0x89, 0x46, 0x1F), 149 },
            { Color.FromArgb(0x9F, 0x52, 0x24), 150 },
            { Color.FromArgb(0x54, 0x2B, 0x13), 151 },
            { Color.FromArgb(0x69, 0x3D, 0x4C), 152 },
            { Color.FromArgb(0x80, 0x4B, 0x5D), 153 },
            { Color.FromArgb(0x95, 0x57, 0x6C), 154 },
            { Color.FromArgb(0x4E, 0x2E, 0x39), 155 },
            { Color.FromArgb(0x4F, 0x4C, 0x61), 156 },
            { Color.FromArgb(0x60, 0x5D, 0x77), 157 },
            { Color.FromArgb(0x70, 0x6C, 0x8A), 158 },
            { Color.FromArgb(0x3B, 0x39, 0x49), 159 },
            { Color.FromArgb(0x83, 0x5D, 0x19), 160 },
            { Color.FromArgb(0xA0, 0x72, 0x1F), 161 },
            { Color.FromArgb(0xBA, 0x85, 0x24), 162 },
            { Color.FromArgb(0x62, 0x46, 0x13), 163 },
            { Color.FromArgb(0x48, 0x52, 0x25), 164 },
            { Color.FromArgb(0x58, 0x64, 0x2D), 165 },
            { Color.FromArgb(0x67, 0x75, 0x35), 166 },
            { Color.FromArgb(0x36, 0x3D, 0x1C), 167 },
            { Color.FromArgb(0x70, 0x36, 0x37), 168 },
            { Color.FromArgb(0x8A, 0x42, 0x43), 169 },
            { Color.FromArgb(0xA0, 0x4D, 0x4E), 170 },
            { Color.FromArgb(0x54, 0x28, 0x29), 171 },
            { Color.FromArgb(0x28, 0x1C, 0x18), 172 },
            { Color.FromArgb(0x31, 0x23, 0x1E), 173 },
            { Color.FromArgb(0x39, 0x29, 0x23), 174 },
            { Color.FromArgb(0x1E, 0x15, 0x12), 175 },
            { Color.FromArgb(0x5F, 0x4B, 0x45), 176 },
            { Color.FromArgb(0x74, 0x5C, 0x54), 177 },
            { Color.FromArgb(0x87, 0x6B, 0x62), 178 },
            { Color.FromArgb(0x47, 0x38, 0x33), 179 },
            { Color.FromArgb(0x3D, 0x40, 0x40), 180 },
            { Color.FromArgb(0x4B, 0x4F, 0x4F), 181 },
            { Color.FromArgb(0x57, 0x5C, 0x5C), 182 },
            { Color.FromArgb(0x2E, 0x30, 0x30), 183 },
            { Color.FromArgb(0x56, 0x33, 0x3E), 184 },
            { Color.FromArgb(0x69, 0x3E, 0x4B), 185 },
            { Color.FromArgb(0x7A, 0x49, 0x58), 186 },
            { Color.FromArgb(0x40, 0x26, 0x2E), 187 },
            { Color.FromArgb(0x35, 0x2B, 0x40), 188 },
            { Color.FromArgb(0x41, 0x35, 0x4F), 189 },
            { Color.FromArgb(0x4C, 0x3E, 0x5C), 190 },
            { Color.FromArgb(0x28, 0x20, 0x30), 191 },
            { Color.FromArgb(0x35, 0x23, 0x18), 192 },
            { Color.FromArgb(0x41, 0x2B, 0x1E), 193 },
            { Color.FromArgb(0x4C, 0x32, 0x23), 194 },
            { Color.FromArgb(0x28, 0x1A, 0x12), 195 },
            { Color.FromArgb(0x35, 0x39, 0x1D), 196 },
            { Color.FromArgb(0x41, 0x46, 0x24), 197 },
            { Color.FromArgb(0x4C, 0x52, 0x2A), 198 },
            { Color.FromArgb(0x28, 0x2B, 0x16), 199 },
            { Color.FromArgb(0x64, 0x2A, 0x20), 200 },
            { Color.FromArgb(0x7A, 0x33, 0x27), 201 },
            { Color.FromArgb(0x8E, 0x3C, 0x2E), 202 },
            { Color.FromArgb(0x4B, 0x1F, 0x18), 203 },
            { Color.FromArgb(0x1A, 0x0F, 0x0B), 204 },
            { Color.FromArgb(0x1F, 0x12, 0x0D), 205 },
            { Color.FromArgb(0x25, 0x16, 0x10), 206 },
            { Color.FromArgb(0x13, 0x0B, 0x08), 207 },
        };

        private static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute(CommandLineApplication app)
        {
            var imagePaths = app.RemainingArguments.Count > 0
                ? app.RemainingArguments
                : Enumerable.Empty<string>()
                  .Concat(Directory.EnumerateFiles(".", "*.bmp"))
                  .Concat(Directory.EnumerateFiles(".", "*.gif"))
                  .Concat(Directory.EnumerateFiles(".", "*.png"));
            
            foreach (var imagePath in imagePaths)
            {
                Bitmap = new Bitmap(imagePath);
                if (Bitmap.Width != SideLength || Bitmap.Height != SideLength)
                {
                    Console.WriteLine("Width and height should be 128 pixels long.");
                    return;
                }

                var colors = new byte[SideLength * SideLength];

                for (var i = 0; i < SideLength * SideLength; i += 1)
                {
                    colors[i] = ColorTable[Bitmap.GetPixel(i % SideLength, i / SideLength)];
                }

                var nbtPath = $"map_{Path.GetFileNameWithoutExtension(imagePath)}.dat";
                new NbtFile(new NbtCompound("root")
                {
                    new NbtCompound("data")
                    {
                        new NbtByte("scale", 0),
                        new NbtByte("dimemsion", 0),
                        new NbtByte("trackingPosition", 0),
                        new NbtByte("unlimitedTracking", 0),
                        new NbtByte("locked", 1),
                        new NbtInt("xCenter", 0),
                        new NbtInt("zCenter", 0),
                        new NbtList("banners", NbtTagType.Compound),
                        new NbtList("frames", NbtTagType.Compound),
                        new NbtByteArray("colors", colors),
                    },
                    new NbtInt("DataVersion", 1343),
                }).SaveToFile(nbtPath, NbtCompression.GZip);

                Console.WriteLine(Path.GetFullPath(nbtPath));
            }
        }
    }
}

// Copyright Carl Reinke
//
// This file is part of a library that is licensed under the terms of the GNU
// Lesser General Public License Version 3 as published by the Free Software
// Foundation.
//
// This license does not grant rights under trademark law for use of any trade
// names, trademarks, or service marks.

namespace Tetractic.Formats.Gif;

/// <summary>
/// A 24-bit RGB color.
/// </summary>
public readonly struct GifColor
{
    /// <summary>
    /// Initializes a new <see cref="GifColor"/> instance.
    /// </summary>
    /// <param name="r">The red component.</param>
    /// <param name="g">The green component.</param>
    /// <param name="b">The blue component.</param>
    public GifColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    /// Gets the red component.
    /// </summary>
    public byte R { get; }

    /// <summary>
    /// Gets the green component.
    /// </summary>
    public byte G { get; }

    /// <summary>
    /// Gets the blue component.
    /// </summary>
    public byte B { get; }
}

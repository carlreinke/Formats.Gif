// Copyright Carl Reinke
//
// This file is part of a library that is licensed under the terms of the GNU
// Lesser General Public License Version 3 as published by the Free Software
// Foundation.
//
// This license does not grant rights under trademark law for use of any trade
// names, trademarks, or service marks.

using System;

namespace Tetractic.Formats.Gif;

/// <summary>
/// An Image Descriptor, which describes an image to be displayed.
/// </summary>
public readonly struct GifImageDescriptor
{
#pragma warning disable format
    private const byte _hasLocalColorTableFlag  = 0b1_0_0_00_000;
    private const byte _interlacedFlag          = 0b0_1_0_00_000;
    private const byte _sortedFlag              = 0b0_0_1_00_000;
    private const byte _reserved                = 0b0_0_0_11_000;
    private const byte _localColorTableSizeMask = 0b0_0_0_00_111;
#pragma warning restore format

    internal byte PackedFields { get; init; }

    /// <summary>
    /// Gets or sets the number of pixels from the left edge of the logical screen to the left edge
    /// of the image.
    /// </summary>
    public ushort Left { get; init; }

    /// <summary>
    /// Gets or sets the number of pixels from the top edge of the logical screen to the top edge of
    /// the image.
    /// </summary>
    public ushort Top { get; init; }

    /// <summary>
    /// Gets or sets the width of the image in pixels.
    /// </summary>
    public ushort Width { get; init; }

    /// <summary>
    /// Gets or sets the height of the image in pixels.
    /// </summary>
    public ushort Height { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether there is a local color table.
    /// </summary>
    public bool HasLocalColorTable
    {
        readonly get => (PackedFields & _hasLocalColorTableFlag) != 0;
        init
        {
            const byte flag = _hasLocalColorTableFlag;
            PackedFields = (byte)((PackedFields & ~flag) | (value ? flag : 0));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the image data is interlaced.
    /// </summary>
    public bool Interlaced
    {
        readonly get => (PackedFields & _interlacedFlag) != 0;
        init
        {
            const byte flag = _interlacedFlag;
            PackedFields = (byte)((PackedFields & ~flag) | (value ? flag : 0));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the local color table is sorted in order of
    /// decreasing importance.
    /// </summary>
    /// <remarks>
    /// Requires <see cref="GifVersion.Version89a"/>.
    /// </remarks>
    public bool Sorted
    {
        readonly get => (PackedFields & _sortedFlag) != 0;
        init
        {
            const byte flag = _sortedFlag;
            PackedFields = (byte)((PackedFields & ~flag) | (value ? flag : 0));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating the size of the local color table.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The number of colors in the table is <c>2^(x+1)</c>.
    /// </para>
    /// <para>
    /// If <see cref="HasLocalColorTable"/> is <see langword="false"/> then the value should be
    /// zero.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException" accessor="set">The property is being set, and
    ///     <paramref name="value"/> is greater than 7.</exception>
    public byte LocalColorTableSize
    {
        readonly get => (byte)(PackedFields & _localColorTableSizeMask);
        init
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 7);

            const byte mask = _localColorTableSizeMask;
            PackedFields = (byte)((PackedFields & ~mask) | value);
        }
    }

    internal readonly bool Reserved => (PackedFields & _reserved) != 0;
}

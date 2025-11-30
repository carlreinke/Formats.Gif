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
/// A Logical Screen Descriptor, which describes the area within which the images are rendered.
/// </summary>
public readonly struct GifLogicalScreenDescriptor
{
#pragma warning disable format
    private const byte _hasGlobalColorTableFlag  = 0b1_000_0_000;
    private const byte _colorResolutionMask      = 0b0_111_0_000;
    private const byte _sortedFlag               = 0b0_000_1_000;
    private const byte _globalColorTableSizeMask = 0b0_000_0_111;
#pragma warning restore format

    internal byte PackedFields { get; init; }

    /// <summary>
    /// Gets or sets the width of the logical screen in pixels.
    /// </summary>
    public ushort Width { get; init; }

    /// <summary>
    /// Gets or sets the height of the logical screen in pixels.
    /// </summary>
    public ushort Height { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether there is a global color table.
    /// </summary>
    public bool HasGlobalColorTable
    {
        readonly get => (PackedFields & _hasGlobalColorTableFlag) != 0;
        init
        {
            const byte flag = _hasGlobalColorTableFlag;
            PackedFields = (byte)((PackedFields & ~flag) | (value ? flag : 0));
        }
    }

    /// <summary>
    /// Gets or sets the color resolution of the original image.
    /// </summary>
    /// <remarks>
    /// The value is the number of bits per primary color minus 1.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException" accessor="set">The property is being set, and
    ///     <paramref name="value"/> is greater than 7.</exception>
    public byte ColorResolution
    {
        readonly get => (byte)((PackedFields & _colorResolutionMask) >> 4);
        init
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 7);

            const byte mask = _colorResolutionMask;
            PackedFields = (byte)((PackedFields & ~mask) | (value << 4));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the global color table is sorted in order of
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
    /// Gets or sets a value indicating the size of the global color table.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The number of colors in the table is <c>2^(x+1)</c>.
    /// </para>
    /// <para>
    /// If <see cref="HasGlobalColorTable"/> is <see langword="false"/> then the value should be set
    /// to indicate the number of colors of the logical screen.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException" accessor="set">The property is being set, and
    ///     <paramref name="value"/> is greater than 7.</exception>
    public byte GlobalColorTableSize
    {
        readonly get => (byte)(PackedFields & _globalColorTableSizeMask);
        init
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 7);

            const byte mask = _globalColorTableSizeMask;
            PackedFields = (byte)((PackedFields & ~mask) | value);
        }
    }

    /// <summary>
    /// Gets or sets the index of the background color in the global color table.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <see cref="HasGlobalColorTable"/> is <see langword="false"/> then the value should be
    /// zero.
    /// </para>
    /// <para>
    /// Decoders typically ignore this value and make the background transparent.
    /// </para>
    /// </remarks>
    public byte BackgroundColorIndex { get; init; }

    /// <summary>
    /// Gets or sets a value indicating the pixel aspect ratio.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Requires <see cref="GifVersion.Version89a"/>.
    /// </para>
    /// <para>
    /// If the value is zero then the pixel aspect ratio is unspecified.
    /// </para>
    /// </remarks>
    public byte PixelAspectRatio { get; init; }
}

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
/// A Netscape 2.0 Application Extension.
/// </summary>
public static class NetscapeApplicationExtension
{
    /// <summary>
    /// Gets the application identifier.
    /// </summary>
    public static ReadOnlySpan<byte> ApplicationIdentifier => "NETSCAPE"u8;

    /// <summary>
    /// Gets the application authentication code.
    /// </summary>
    public static ReadOnlySpan<byte> ApplicationAuthenticationCode => "2.0"u8;

    /// <summary>
    /// A sub-block of the extension.
    /// </summary>
    public abstract class Subblock
    {
        private protected Subblock()
        {
        }
    }

    /// <summary>
    /// A sub-block that describes looping behavior.
    /// </summary>
    public sealed class LoopingSubblock : Subblock
    {
        internal const byte SubblockId = 1;

        /// <summary>
        /// Gets or sets the number of times that the following sequence of images is displayed.  If
        /// the value is zero then the sequence is displayed indefinitely.
        /// </summary>
        public ushort LoopCount { get; init; }
    }

    /// <summary>
    /// A sub-block that describes buffering behavior.
    /// </summary>
    public sealed class BufferingSubblock : Subblock
    {
        internal const byte SubblockId = 2;

        /// <summary>
        /// Gets or sets the amount of image data to be buffered before starting to display the
        /// sequence of images.
        /// </summary>
        public uint BufferLength { get; init; }
    }
}

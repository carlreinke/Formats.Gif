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
/// Represents the operation to perform after a graphic is done being displayed.
/// </summary>
/// <seealso href="http://web.archive.org/web/20240121171058/http://www.imagemagick.org/Usage/anim_basics/"/>
public enum GifDisposalMethod : byte
{
    /// <summary>
    /// Unspecified.
    /// </summary>
    Unspecified = 0,

    /// <summary>
    /// The graphic area should not be modified.
    /// </summary>
    DoNotDispose = 1,

    /// <summary>
    /// The graphic area should be replaced by the background color.
    /// </summary>
    /// <remarks>
    /// Decoders typically ignore the background color and make the graphic area transparent.
    /// </remarks>
    RestoreToBackground = 2,

    /// <summary>
    /// The graphic area should be replaced by the previous contents.
    /// </summary>
    RestoreToPrevious = 3,
}

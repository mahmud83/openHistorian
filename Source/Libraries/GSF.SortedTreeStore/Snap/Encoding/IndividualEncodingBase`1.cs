﻿//******************************************************************************************************
//  IndividualEncodingBase`1.cs - Gbtc
//
//  Copyright © 2014, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  02/21/2014 - Steven E. Chisholm
//       Generated original version of source code. 
//     
//******************************************************************************************************

using GSF.IO;
using GSF.IO.Unmanaged;

namespace GSF.Snap.Encoding
{
    /// <summary>
    /// Base Class that allows compressing of a single value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IndividualEncodingBase<T>
    {
        /// <summary>
        /// Gets if the stream supports a symbol that 
        /// represents that the end of the stream has been encountered.
        /// </summary>
        /// <remarks>
        /// An example of a symbol would be the byte code 0xFF.
        /// In this case, if the first byte of the
        /// word is 0xFF, the encoding has specifically
        /// designated this as the end of the stream. Therefore, calls to
        /// Decompress will result in an end of stream exception.
        /// 
        /// Failing to reserve a code as the end of stream will mean that
        /// streaming points will include its own symbol to represent the end of the
        /// stream, taking 1 extra byte per point encoded.
        /// </remarks>
        public abstract bool ContainsEndOfStreamSymbol { get; }

        /// <summary>
        /// The byte code to use as the end of stream symbol.
        /// May throw NotSupportedException if <see cref="ContainsEndOfStreamSymbol"/> is false.
        /// </summary>
        public abstract byte EndOfStreamSymbol { get; }

        /// <summary>
        /// Gets if the previous value will need to be presented to the encoding algorithms to
        /// property encode the next sample. Returning false will cause nulls to be passed
        /// in a parameters to the encoding.
        /// </summary>
        public abstract bool UsesPreviousValue { get; }

        /// <summary>
        /// Gets the maximum amount of space that is required for the compression algorithm. This
        /// prevents lower levels from having overflows on the underlying streams. It is critical
        /// that this value be correct. Error on the side of too large of a value as a value
        /// too small will corrupt data and be next to impossible to track down the point of corruption
        /// </summary>
        public abstract int MaxCompressionSize { get; }

        /// <summary>
        /// Encodes <see cref="value"/> to the provided <see cref="stream"/>.
        /// </summary>
        /// <param name="stream">where to write the data</param>
        /// <param name="prevValue">the previous value if required by <see cref="UsesPreviousValue"/>. Otherwise null.</param>
        /// <param name="value">the value to encode</param>
        /// <returns>the number of bytes necessary to encode this key/value.</returns>
        public abstract void Encode(BinaryStreamBase stream, T prevValue, T value);

        /// <summary>
        /// Decodes <see cref="value"/> from the provided <see cref="stream"/>.
        /// </summary>
        /// <param name="stream">where to read the data</param>
        /// <param name="prevValue">the previous value if required by <see cref="UsesPreviousValue"/>. Otherwise null.</param>
        /// <param name="value">the place to store the decoded value</param>
        /// <param name="isEndOfStream">outputs true if the end of the stream symbol is detected. Not all encoding methods have an end of stream symbol and therefore will always return false.</param>
        /// <returns>the number of bytes necessary to decode the next key/value.</returns>
        public abstract void Decode(BinaryStreamBase stream, T prevValue, T value, out bool isEndOfStream);

        /// <summary>
        /// Encodes <see cref="value"/> to the provided <see cref="stream"/>.
        /// </summary>
        /// <param name="stream">where to write the data</param>
        /// <param name="prevValue">the previous value if required by <see cref="UsesPreviousValue"/>. Otherwise null.</param>
        /// <param name="value">the value to encode</param>
        /// <returns>the number of bytes necessary to encode this key/value.</returns>
        public unsafe virtual int Encode(byte* stream, T prevValue, T value)
        {
            var bs = new BinaryStreamPointerWrapper(stream, MaxCompressionSize);
            Encode(bs, prevValue, value);
            return (int)bs.Position;
        }

        /// <summary>
        /// Decodes <see cref="value"/> from the provided <see cref="stream"/>.
        /// </summary>
        /// <param name="stream">where to read the data</param>
        /// <param name="prevValue">the previous value if required by <see cref="UsesPreviousValue"/>. Otherwise null.</param>
        /// <param name="value">the place to store the decoded value</param>
        /// <param name="isEndOfStream">outputs true if the end of the stream symbol is detected. Not all encoding methods have an end of stream symbol and therefore will always return false.</param>
        /// <returns>the number of bytes necessary to decode the next key/value.</returns>
        public unsafe virtual int Decode(byte* stream, T prevValue, T value, out bool isEndOfStream)
        {
            var bs = new BinaryStreamPointerWrapper(stream, MaxCompressionSize);
            Decode(bs, prevValue, value, out isEndOfStream);
            return (int)bs.Position;
        }

        /// <summary>
        /// Clones this encoding method.
        /// </summary>
        /// <returns>A clone</returns>
        public abstract IndividualEncodingBase<T> Clone();

    }
}

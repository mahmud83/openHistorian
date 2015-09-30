﻿//******************************************************************************************************
//  BlockType.cs - Gbtc
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
//  1/4/2012 - Steven E. Chisholm
//       Generated original version of source code. 
//       
//
//******************************************************************************************************


namespace GSF.IO.FileStructure
{
    /// <summary>
    /// Each block of bytes in a file is one of these types.
    /// </summary>
    internal enum BlockType : byte
    {
        /// <summary>
        /// The first few pages of a file system
        /// </summary>
        FileAllocationTable = 1,
        /// <summary>
        /// Contains the actual data.
        /// </summary>
        DataBlock = 2,
        /// <summary>
        /// A metadata block. Contains a set of pointer blocks that point to <see cref="IndexIndirect2"/> blocks.
        /// </summary>
        IndexIndirect1 = 3,
        /// <summary>
        /// A metadata block. Contains a set of pointer blocks that point to <see cref="IndexIndirect3"/> blocks.
        /// </summary>
        IndexIndirect2 = 4,
        /// <summary>
        /// A metadata block. Contains a set of pointer blocks that point to <see cref="IndexIndirect4"/> blocks.
        /// </summary>
        IndexIndirect3 = 5,
        /// <summary>
        /// A metadata block. Contains a set of pointer blocks that point to the actual data.
        /// </summary>
        IndexIndirect4 = 6
    }
}
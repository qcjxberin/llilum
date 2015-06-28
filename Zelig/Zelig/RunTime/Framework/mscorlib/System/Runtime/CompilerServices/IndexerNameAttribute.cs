// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==
namespace System.Runtime.CompilerServices
{
    using System;

    [Serializable]
    [AttributeUsage( AttributeTargets.Property, Inherited = true )]
    public sealed class IndexerNameAttribute : Attribute
    {
        public IndexerNameAttribute( String indexerName )
        {
        }
    }
}

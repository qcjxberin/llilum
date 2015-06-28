// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
namespace System
{
    using System;

    // The base class for all event classes.
    [Serializable]
    public class EventArgs
    {
        public static readonly EventArgs Empty = new EventArgs();

        public EventArgs()
        {
        }
    }
}

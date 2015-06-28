// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==
/*============================================================
**
** Interface:  IDictionary
**
**
** Purpose: Base interface for all dictionaries.
**
**
===========================================================*/
namespace System.Collections
{
    using System;

    // An IDictionary is a possibly unordered set of key-value pairs.
    // Keys can be any non-null object.  Values can be any object.
    // You can look up a value in an IDictionary via the default indexed
    // property, Items.
    public interface IDictionary : ICollection
    {
        // Interfaces are not serializable
        // The Item property provides methods to read and edit entries
        // in the Dictionary.
        Object this[Object key]
        {
            get;
            set;
        }

        // Returns a collections of the keys in this dictionary.
        ICollection Keys
        {
            get;
        }

        // Returns a collections of the values in this dictionary.
        ICollection Values
        {
            get;
        }

        // Returns whether this dictionary contains a particular key.
        //
        bool Contains( Object key );

        // Adds a key-value pair to the dictionary.
        //
        void Add( Object key, Object value );

        // Removes all pairs from the dictionary.
        void Clear();

        bool IsReadOnly
        {
            get;
        }

        bool IsFixedSize
        {
            get;
        }

        // Returns an IDictionaryEnumerator for this dictionary.
        new IDictionaryEnumerator GetEnumerator();

        // Removes a particular key from the dictionary.
        //
        void Remove( Object key );
    }
}

// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==
/*============================================================
**
** Interface:  IList
**
**
** Purpose: Base interface for all generic lists.
**
**
===========================================================*/
namespace System.Collections.Generic {

    using System;
    using System.Runtime.CompilerServices;

    // An IList is an ordered collection of objects.  The exact ordering
    // is up to the implementation of the list, ranging from a sorted
    // order to insertion order.

    // Note that T[] : IList<T>, and we want to ensure that if you use
    // IList<YourValueType>, we ensure a YourValueType[] can be used
    // without jitting.  Hence the TypeDependencyAttribute on SZArrayHelper.
    // This is a special hack internally though - see VM\compile.cpp.
    // The same attribute is on IEnumerable<T> and ICollection<T>.
    [Microsoft.Zelig.Internals.WellKnownType( "System_Collections_Generic_IList_of_T" )]
////[TypeDependencyAttribute("System.SZArrayHelper")]
    public interface IList<T> : ICollection<T>
    {
        // The Item property provides methods to read and edit entries in the List.
        T this[int index] {
            get;
            set;
        }

        // Returns the index of a particular item, if it is in the list.
        // Returns -1 if the item isn't in the list.
        int IndexOf(T item);

        // Inserts value into the list at position index.
        // index must be non-negative and less than or equal to the
        // number of elements in the list.  If index equals the number
        // of items in the list, then value is appended to the end.
        void Insert(int index, T item);

        // Removes the item at position index.
        void RemoveAt(int index);
    }
}

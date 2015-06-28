// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==
/*============================================================
**
** Class:  DebuggerAttributes
**
**
** Purpose: Attributes for debugger
**
**
===========================================================*/


namespace System.Diagnostics
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable]
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false )]
    public sealed class DebuggerStepThroughAttribute : Attribute
    {
        public DebuggerStepThroughAttribute()
        {
        }
    }

    [Serializable]
    [AttributeUsage( AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false )]
    public sealed class DebuggerStepperBoundaryAttribute : Attribute
    {
        public DebuggerStepperBoundaryAttribute()
        {
        }
    }

    [Serializable]
    [AttributeUsage( AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor, Inherited = false )]
    public sealed class DebuggerHiddenAttribute : Attribute
    {
        public DebuggerHiddenAttribute()
        {
        }
    }

    [Serializable]
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Struct, Inherited = false )]
    public sealed class DebuggerNonUserCodeAttribute : Attribute
    {
        public DebuggerNonUserCodeAttribute()
        {
        }
    }

    // Attribute class used by the compiler to mark modules.
    // If present, then debugging information for everything in the
    // assembly was generated by the compiler, and will be preserved
    // by the Runtime so that the debugger can provide full functionality
    // in the case of JIT attach. If not present, then the compiler may
    // or may not have included debugging information, and the Runtime
    // won't preserve the debugging info, which will make debugging after
    // a JIT attach difficult.
    [AttributeUsage( AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false )]
    public sealed class DebuggableAttribute : Attribute
    {
        [Flags]
        public enum DebuggingModes
        {
            None                            = 0x000,
            Default                         = 0x001,
            IgnoreSymbolStoreSequencePoints = 0x002,
            EnableEditAndContinue           = 0x004,
            DisableOptimizations            = 0x100,
        }

        private DebuggingModes m_debuggingModes;

        public DebuggableAttribute( bool isJITTrackingEnabled, bool isJITOptimizerDisabled )
        {
            m_debuggingModes = 0;

            if(isJITTrackingEnabled)
            {
                m_debuggingModes |= DebuggingModes.Default;
            }

            if(isJITOptimizerDisabled)
            {
                m_debuggingModes |= DebuggingModes.DisableOptimizations;
            }
        }

        public DebuggableAttribute( DebuggingModes modes )
        {
            m_debuggingModes = modes;
        }

        public bool IsJITTrackingEnabled
        {
            get
            {
                return ((m_debuggingModes & DebuggingModes.Default) != 0);
            }
        }

        public bool IsJITOptimizerDisabled
        {
            get
            {
                return ((m_debuggingModes & DebuggingModes.DisableOptimizations) != 0);
            }
        }

        public DebuggingModes DebuggingFlags
        {
            get
            {
                return m_debuggingModes;
            }
        }
    }

    //  DebuggerBrowsableState states are defined as follows:
    //      Never       never show this element
    //      Expanded    expansion of the class is done, so that all visible internal members are shown
    //      Collapsed   expansion of the class is not performed. Internal visible members are hidden
    //      RootHidden  The target element itself should not be shown, but should instead be
    //                  automatically expanded to have its members displayed.
    //  Default value is collapsed

    //  Please also change the code which validates DebuggerBrowsableState variable (in this file)
    //  if you change this enum.
    public enum DebuggerBrowsableState
    {
        Never      = 0,
        //Expanded is not supported in this release
        //Expanded = 1,
        Collapsed  = 2,
        RootHidden = 3,
    }


    // the one currently supported with the csee.dat
    // (mcee.dat, autoexp.dat) file.
    [AttributeUsage( AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false )]
    public sealed class DebuggerBrowsableAttribute : Attribute
    {
        private DebuggerBrowsableState m_state;

        public DebuggerBrowsableAttribute( DebuggerBrowsableState state )
        {
            if(state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentOutOfRangeException( "state" );
#else
                throw new ArgumentOutOfRangeException();
#endif
            }

            m_state = state;
        }

        public DebuggerBrowsableState State
        {
            get
            {
                return m_state;
            }
        }
    }


    // DebuggerTypeProxyAttribute
    [AttributeUsage( AttributeTargets.Struct | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true )]
    public sealed class DebuggerTypeProxyAttribute : Attribute
    {
        private string m_typeName;
        private string m_targetName;
        private Type   m_target;
    
        public DebuggerTypeProxyAttribute( Type type )
        {
            if(type == null)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentNullException( "type" );
#else
                throw new ArgumentNullException();
#endif
            }
    
            m_typeName = type.AssemblyQualifiedName;
        }
    
        public DebuggerTypeProxyAttribute( string typeName )
        {
            m_typeName = typeName;
        }
    
        public string ProxyTypeName
        {
            get
            {
                return m_typeName;
            }
        }
    
        public Type Target
        {
            set
            {
                if(value == null)
                {
#if EXCEPTION_STRINGS
                    throw new ArgumentNullException( "value" );
#else
                    throw new ArgumentNullException();
#endif
                }
    
                m_targetName = value.AssemblyQualifiedName;
                m_target     = value;
            }
    
            get
            {
                return m_target;
            }
        }
    
        public string TargetTypeName
        {
            get
            {
                return m_targetName;
            }
    
            set
            {
                m_targetName = value;
            }
        }
    }

    // This attribute is used to control what is displayed for the given class or field
    // in the data windows in the debugger.  The single argument to this attribute is
    // the string that will be displayed in the value column for instances of the type.
    // This string can include text between { and } which can be either a field,
    // property or method (as will be documented in mscorlib).  In the C# case,
    // a general expression will be allowed which only has implicit access to the this pointer
    // for the current instance of the target type. The expression will be limited,
    // however: there is no access to aliases, locals, or pointers.
    // In addition, attributes on properties referenced in the expression are not processed.
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Assembly, AllowMultiple = true )]
    public sealed class DebuggerDisplayAttribute : Attribute
    {
        private string m_name;
        private string m_value;
        private string m_type;
        private string m_targetName;
        private Type   m_target;
    
        public DebuggerDisplayAttribute( string value )
        {
            if(value == null)
            {
                m_value = "";
            }
            else
            {
                m_value = value;
            }
    
            m_name = "";
            m_type = "";
        }
    
        public string Value
        {
            get
            {
                return m_value;
            }
        }
    
        public string Name
        {
            get
            {
                return m_name;
            }
    
            set
            {
                m_name = value;
            }
        }
    
        public string Type
        {
            get
            {
                return m_type;
            }
    
            set
            {
                m_type = value;
            }
        }
    
        public Type Target
        {
            set
            {
                if(value == null)
                {
#if EXCEPTION_STRINGS
                    throw new ArgumentNullException( "value" );
#else
                    throw new ArgumentNullException();
#endif
                }
    
                m_targetName = value.AssemblyQualifiedName;
                m_target     = value;
            }
    
            get
            {
                return m_target;
            }
        }
    
        public string TargetTypeName
        {
            get
            {
                return m_targetName;
            }
    
            set
            {
                m_targetName = value;
            }
        }
    }


    /// <summary>
    /// Signifies that the attributed type has a visualizer which is pointed
    /// to by the parameter type name strings.
    /// </summary>
    [AttributeUsage( AttributeTargets.Struct | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true )]
    public sealed class DebuggerVisualizerAttribute : Attribute
    {
        private string m_visualizerObjectSourceName;
        private string m_visualizerName;
        private string m_description;
        private string m_targetName;
        private Type   m_target;
    
        public DebuggerVisualizerAttribute( string visualizerTypeName )
        {
            m_visualizerName = visualizerTypeName;
        }
    
        public DebuggerVisualizerAttribute( string visualizerTypeName, string visualizerObjectSourceTypeName )
        {
            m_visualizerName             = visualizerTypeName;
            m_visualizerObjectSourceName = visualizerObjectSourceTypeName;
        }
    
        public DebuggerVisualizerAttribute( string visualizerTypeName, Type visualizerObjectSource )
        {
            if(visualizerObjectSource == null)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentNullException( "visualizerObjectSource" );
#else
                throw new ArgumentNullException();
#endif
            }
    
            m_visualizerName             = visualizerTypeName;
            m_visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
        }
    
        public DebuggerVisualizerAttribute( Type visualizer )
        {
            if(visualizer == null)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentNullException( "visualizer" );
#else
                throw new ArgumentNullException();
#endif
            }
    
            m_visualizerName = visualizer.AssemblyQualifiedName;
        }
    
        public DebuggerVisualizerAttribute( Type visualizer, Type visualizerObjectSource )
        {
            if(visualizer == null)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentNullException( "visualizer" );
#else
                throw new ArgumentNullException();
#endif
            }
    
            if(visualizerObjectSource == null)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentNullException( "visualizerObjectSource" );
#else
                throw new ArgumentNullException();
#endif
            }
    
            m_visualizerName             = visualizer            .AssemblyQualifiedName;
            m_visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
        }
    
        public DebuggerVisualizerAttribute( Type visualizer, string visualizerObjectSourceTypeName )
        {
            if(visualizer == null)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentNullException( "visualizer" );
#else
                throw new ArgumentNullException();
#endif
            }
    
            m_visualizerName             = visualizer.AssemblyQualifiedName;
            m_visualizerObjectSourceName = visualizerObjectSourceTypeName;
        }
    
        public string VisualizerObjectSourceTypeName
        {
            get
            {
                return m_visualizerObjectSourceName;
            }
        }
    
        public string VisualizerTypeName
        {
            get
            {
                return m_visualizerName;
            }
        }
    
        public string Description
        {
            get
            {
                return m_description;
            }
    
            set
            {
                m_description = value;
            }
        }
    
        public Type Target
        {
            set
            {
                if(value == null)
                {
#if EXCEPTION_STRINGS
                    throw new ArgumentNullException( "value" );
#else
                    throw new ArgumentNullException();
#endif
                }
    
                m_targetName = value.AssemblyQualifiedName;
                m_target     = value;
            }
    
            get
            {
                return m_target;
            }
        }
    
        public string TargetTypeName
        {
            set
            {
                m_targetName = value;
            }
    
            get
            {
                return m_targetName;
            }
        }
    }
}



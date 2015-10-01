//
// Copyright (c) Microsoft Corporation.    All rights reserved.
//

namespace Microsoft.Zelig.Configuration.Environment
{
    using System;
    using System.Collections.Generic;


    public abstract class LoaderCompilationSetupCategory : CompilationSetupCategory
    {
        [LinkToConfigurationOption("ExcludeDebuggerHooks")]
        public bool ExcludeDebuggerHooks = true;
    }
}

//
// Copyright (c) Microsoft Corporation.    All rights reserved.
//

namespace Microsoft.Zelig.Runtime
{
    using System;
    using System.Runtime.CompilerServices;

    using TS = Microsoft.Zelig.Runtime.TypeSystem;


    [ImplicitInstance]
    [ForceDevirtualization]
    public abstract class Processor
    {
        [ForceDevirtualization]
        public abstract class Context
        {
            //
            // State
            //

            public bool InPrologue;
            public bool InEpilogue;

            //
            // Helper Methods
            //

            public abstract void Populate();

            public abstract void Populate( Context context );

            public abstract void PopulateFromDelegate( Delegate dlg   ,
                                                       uint[]   stack );

            public abstract void SetupForExceptionHandling( uint mode );

            public abstract bool Unwind();

            public abstract void SwitchTo();

            public abstract UIntPtr GetRegisterByIndex( uint idx );

            public abstract void SetRegisterByIndex( uint    idx   ,
                                                     UIntPtr value );

            //
            // Access Methods
            //

            public abstract UIntPtr StackPointer
            {
                get;
                set;
            }

            public abstract UIntPtr ProgramCounter
            {
                get;
                set;
            }

            public abstract uint ScratchedIntegerRegisters
            {
                get;
            }
        }

        //
        // Helper Methods
        //

        public abstract void InitializeProcessor();

        public abstract Context AllocateProcessorContext();

        //--//

        public abstract bool AreInterruptsDisabled( );

        public abstract bool AreAllInterruptsDisabled( );

        //--//
        
        public virtual bool AreFaultsDisabled( )
        {
            return true;
        }

        //--//

        public abstract UIntPtr GetCacheableAddress( UIntPtr ptr );

        public abstract UIntPtr GetUncacheableAddress( UIntPtr ptr );

        public abstract void FlushCacheLine( UIntPtr target );
        
        //--//

        public abstract void Breakpoint();

        //--//

        [NoInline]
        [MemoryUsage(MemoryUsage.Bootstrap)]
        public static int Delay( int count )
        {
            //
            // BUGBUG: This should be calibrated based on the processor and they should be generated by the compiler!
            //
            const int fixedOverhead = 16;
            const int perRoundCost  = 6;

            count -= fixedOverhead;

            while(count > 0)
            {
                count -= perRoundCost;
            }

            return count;
        }

        [Inline]
        public static void Delay( uint timeNanosec    ,
                                  uint clockFrequency )
        {
            Delay( (int)(timeNanosec / 1E9 * clockFrequency) );
        }

        [Inline]
        public static void DelayMicroseconds( uint timeMicrosec )
        {
            Delay( (int)(timeMicrosec / 1E6 * Configuration.CoreClockFrequency) );
        }

        //
        // Access Methods
        //

        public static extern Processor Instance
        {
            [SingletonFactory(ReadOnly=true)]
            [MethodImpl( MethodImplOptions.InternalCall )]
            get;
        }
    }
}

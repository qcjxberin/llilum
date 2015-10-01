//
// Copyright (c) Microsoft Corporation.    All rights reserved.
//

namespace Microsoft.Zelig.Runtime
{
    using System;
    using System.Runtime.CompilerServices;

    using TS = Microsoft.Zelig.Runtime.TypeSystem;


    public static class BugCheck
    {
        public enum StopCode
        {
            InterruptsNotDisabled   ,
            InterruptsNotEnabled    ,

            UnwindFailure           ,

            KernelNodeStillLinked   ,
            NoCurrentThread         ,
            ExpectingReadyThread    ,

            NoFreeSyncBlock         ,
            SyncBlockCorruption     ,

            NegativeIndex           ,
            IncorrectArgument       ,

            NoMemory                ,
            NoMarkStack             ,
            NotAMemoryReference     ,
            HeapCorruptionDetected  ,
            
            IllegalMode             ,
            IllegalConfiguration    ,
            FailedBootstrap         ,
            IllegalSchedule         ,
            CtxSwtchOntoSelf        ,
            CtxSwtchWrongContext    ,
            CtxSwtchFailed          ,
            StackCorruptionDetected ,
            InvalidSupervisorCall   ,
            InvalidOperation        ,
            Impossible              ,
        }

        //
        // Helper Methods
        //

        [Inline]
        public static void Assert( bool     condition ,
                                   StopCode code      )
        {
            if(!condition)
            {
                Raise( code );
            }
        }

        [NoReturn]
        [NoInline]
        [TS.WellKnownMethod( "BugCheck_Raise" )]
        public static void Raise( StopCode code )
        {
            Device.Instance.ProcessBugCheck( code );
        }

        public static void Log(string format)
        {
            Device.Instance.ProcessLog(format);
        }

        public static void Log(string format, int p1)
        {
            Device.Instance.ProcessLog(format, p1);
        }

        public static void Log(string format, int p1, int p2)
        {
            Device.Instance.ProcessLog(format, p1, p2);
        }

        public static void Log(string format, int p1, int p2, int p3)
        {
            Device.Instance.ProcessLog(format, p1, p2, p3);
        }

        public static void Log(string format, int p1, int p2, int p3, int p4)
        {
            Device.Instance.ProcessLog(format, p1, p2, p3, p4);
        }

        public static void Log(string format, int p1, int p2, int p3, int p4, int p5)
        {
            Device.Instance.ProcessLog(format, p1, p2, p3, p4, p5);
        }

        //--//

        public static void AssertInterruptsOff()
        {
            Assert( Processor.Instance.AreInterruptsDisabled() == true, BugCheck.StopCode.InterruptsNotDisabled );
        }

        public static void AssertInterruptsOn()
        {
            Assert( Processor.Instance.AreInterruptsDisabled() == false, BugCheck.StopCode.InterruptsNotEnabled );
        }

        //--//

        static readonly System.Text.StringBuilder s_sb = new System.Text.StringBuilder();

        [NoInline]
        [TS.WellKnownMethod( "BugCheck_WriteLine" )]
        public static void WriteLine( string text )
        {
            s_sb.Append( text );
            s_sb.AppendLine();
        }

        public static void WriteLineFormat(        string   fmt   ,
                                            params object[] parms )
        {
            WriteLine( string.Format( fmt, parms ) );
        }
    }
}

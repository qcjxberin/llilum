// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==
// EncoderReplacementFallback.cs
//
namespace System.Text
{
    using System;

    [Serializable]
    public sealed class EncoderReplacementFallback : EncoderFallback
    {
        // Our variables
        private String strDefault;

        // Construction.  Default replacement fallback uses no best fit and ? replacement string
        public EncoderReplacementFallback() : this( "?" )
        {
        }

        public EncoderReplacementFallback( String replacement )
        {
            // Must not be null
            if(replacement == null)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentNullException( "replacement" );
#else
                throw new ArgumentNullException();
#endif
            }

            // Make sure it doesn't have bad surrogate pairs
            bool bFoundHigh = false;
            for(int i = 0; i < replacement.Length; i++)
            {
                // Found a surrogate?
                if(Char.IsSurrogate( replacement, i ))
                {
                    // High or Low?
                    if(Char.IsHighSurrogate( replacement, i ))
                    {
                        // if already had a high one, stop
                        if(bFoundHigh)
                        {
                            break;  // break & throw at the bFoundHIgh below
                        }

                        bFoundHigh = true;
                    }
                    else
                    {
                        // Low, did we have a high?
                        if(!bFoundHigh)
                        {
                            // Didn't have one, make if fail when we stop
                            bFoundHigh = true;
                            break;
                        }

                        // Clear flag
                        bFoundHigh = false;
                    }
                }
                // If last was high we're in trouble (not surrogate so not low surrogate, so break)
                else if(bFoundHigh)
                {
                    break;
                }
            }

            if(bFoundHigh)
            {
#if EXCEPTION_STRINGS
                throw new ArgumentException( Environment.GetResourceString( "Argument_InvalidCharSequenceNoIndex", "replacement" ) );
#else
                throw new ArgumentException();
#endif
            }

            strDefault = replacement;
        }

        public String DefaultString
        {
            get
            {
                return strDefault;
            }
        }

        public override EncoderFallbackBuffer CreateFallbackBuffer()
        {
            return new EncoderReplacementFallbackBuffer( this );
        }

        // Maximum number of characters that this instance of this fallback could return
        public override int MaxCharCount
        {
            get
            {
                return strDefault.Length;
            }
        }

        public override bool Equals( Object value )
        {
            EncoderReplacementFallback that = value as EncoderReplacementFallback;
            if(that != null)
            {
                return (this.strDefault == that.strDefault);
            }
            return (false);
        }

        public override int GetHashCode()
        {
            return strDefault.GetHashCode();
        }
    }



    public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
    {
        // Store our default string
        private String strDefault;
        int fallbackCount = -1;
        int fallbackIndex = -1;

        // Construction
        public EncoderReplacementFallbackBuffer( EncoderReplacementFallback fallback )
        {
            // 2X in case we're a surrogate pair
            this.strDefault = fallback.DefaultString + fallback.DefaultString;
        }

        // Fallback Methods
        public override bool Fallback( char charUnknown, int index )
        {
            // If we had a buffer already we're being recursive, throw, it's probably at the suspect
            // character in our array.
            if(fallbackCount >= 1)
            {
                // If we're recursive we may still have something in our buffer that makes this a surrogate
                if(char.IsHighSurrogate( charUnknown ) && fallbackCount >= 0 &&
                    char.IsLowSurrogate( strDefault[fallbackIndex + 1] ))
                {
                    ThrowLastCharRecursive( Char.ConvertToUtf32( charUnknown, strDefault[fallbackIndex + 1] ) );
                }

                // Nope, just one character
                ThrowLastCharRecursive( unchecked( (int)charUnknown ) );
            }

            // Go ahead and get our fallback
            // Divide by 2 because we aren't a surrogate pair
            fallbackCount = strDefault.Length / 2;
            fallbackIndex = -1;

            return fallbackCount != 0;
        }

        public override bool Fallback( char charUnknownHigh, char charUnknownLow, int index )
        {
            // Double check input surrogate pair
            if(!Char.IsHighSurrogate( charUnknownHigh ))
            {
#if EXCEPTION_STRINGS
                throw new ArgumentOutOfRangeException( "charUnknownHigh", Environment.GetResourceString( "ArgumentOutOfRange_Range", 0xD800, 0xDBFF ) );
#else
                throw new ArgumentOutOfRangeException();
#endif
            }

            if(!Char.IsLowSurrogate( charUnknownLow ))
            {
#if EXCEPTION_STRINGS
                throw new ArgumentOutOfRangeException( "CharUnknownLow", Environment.GetResourceString( "ArgumentOutOfRange_Range", 0xDC00, 0xDFFF ) );
#else
                throw new ArgumentOutOfRangeException();
#endif
            }

            // If we had a buffer already we're being recursive, throw, it's probably at the suspect
            // character in our array.
            if(fallbackCount >= 1)
            {
                ThrowLastCharRecursive( Char.ConvertToUtf32( charUnknownHigh, charUnknownLow ) );
            }

            // Go ahead and get our fallback
            fallbackCount = strDefault.Length;
            fallbackIndex = -1;

            return fallbackCount != 0;
        }

        public override char GetNextChar()
        {
            // We want it to get < 0 because == 0 means that the current/last character is a fallback
            // and we need to detect recursion.  We could have a flag but we already have this counter.
            fallbackCount--;
            fallbackIndex++;

            // Do we have anything left? 0 is now last fallback char, negative is nothing left
            if(fallbackCount < 0)
            {
                return (char)0;
            }

            // Need to get it out of the buffer.
            BCLDebug.Assert( fallbackIndex < strDefault.Length && fallbackIndex >= 0, "Index exceeds buffer range" );

            return strDefault[fallbackIndex];
        }

        public override bool MovePrevious()
        {
            // Back up one, only if we just processed the last character (or earlier)
            if(fallbackCount >= -1 && fallbackIndex >= 0)
            {
                fallbackIndex--;
                fallbackCount++;
                return true;
            }

            // Return false 'cause we couldn't do it.
            return false;
        }

        // How many characters left to output?
        public override int Remaining
        {
            get
            {
                // Our count is 0 for 1 character left.
                return (fallbackCount < 0) ? 0 : fallbackCount;
            }
        }

        // Clear the buffer
        public override unsafe void Reset()
        {
            fallbackCount = -1;
            fallbackIndex = 0;
            charStart = null;
            bFallingBack = false;
        }
    }
}


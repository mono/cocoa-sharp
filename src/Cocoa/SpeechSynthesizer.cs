//
//  
//  CocoaSharpSpeakLine
//  SpeechSynthesizer.cs
//
//  Created by Todd Schavey on 2/23/07.
//  Copyright 2007 __MyCompanyName__. All rights reserved.
//
//

using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa
{
    
	public class SpeechSynthesizer : Window 
    {
        public class SpeechSynthesizerListener : Cocoa.Object
        {
            private SpeechSynthesizer mSynth = null;
            
            public SpeechSynthesizerListener ( SpeechSynthesizer aSynth ) 
                : base ()    
            {
                mSynth = aSynth;
            }
            
            public SpeechSynthesizerListener (IntPtr native_object) 
            : base (native_object)
            {
            }
            
            [Export( "speechSynthesizer:didFinishSpeaking:" )]
            public void SpeechSynthesizerFinishedSpeaking( SpeechSynthesizer aSender, bool aFinishedSpeaking )
            {
                mSynth.FireEvent_DidFinishSpeaking( aFinishedSpeaking );
            }
            
            [Export( "speechSynthesizer:willSpeakWord:ofString" )]
                public void SpeechSynthesizerWillSpeakWordOfString( SpeechSynthesizer aSender, Range aCharacterRange, Cocoa.String aString )
            {
                    mSynth.FireEvent_WillSpeak( aCharacterRange, aString );
            }
            
            [Export( "speechSynthesizer:willSpeakPhoneme:" )]
                public void SpeechSynthesizerWillSpeakPhoneme( SpeechSynthesizer aSender, short aPhonemeOpCode )
            {
                    mSynth.FireEvent_WillSpeakPhoneme( aPhonemeOpCode );
            }
        
        }
        
        #region Delegates
        
        public delegate void DidFinishSpeakingEventHandler( SpeechSynthesizer aSender, bool aFinishedSpeaking );
        public delegate void WillSpeakEventHandler( SpeechSynthesizer aSender, Range aCharacterRange, string aWord );
        public delegate void WillSpeakPhonemeEventHandler( SpeechSynthesizer aSender, short aPhonemeOpCode );
        
        #endregion
        
        #region Events
        
        public event DidFinishSpeakingEventHandler  DidFinishSpeaking;
        public event WillSpeakEventHandler          WillSpeak;
        public event WillSpeakPhonemeEventHandler   WillSpeakPhoneme;
        
        #endregion
        
        #region Attributes
        
        private static string ObjectiveCName = "NSSpeechSynthesizer";
        
        protected SpeechSynthesizerListener mSpeechSynthesizerListener;
        
        #endregion
        
        #region Constructors
        
        public SpeechSynthesizer () 
            : base () 
        {
                mSpeechSynthesizerListener = new SpeechSynthesizerListener( this );
        }
        
        public SpeechSynthesizer (IntPtr native_object) 
            : base (native_object)
        {
                NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithVoice:", typeof (IntPtr), typeof (IntPtr), null );
        }
        
        #endregion
        
        protected void SubscribeToCocoaDelegate( Cocoa.Object aTarget )
        {
            ObjCMessaging.objc_msgSend (NativeObject, "setDelegate:", typeof (void), typeof (System.IntPtr), aTarget.NativeObject );
        }
        
        public bool EnableDelegate
        {
            set
            {
                if( value )
                {
                    SubscribeToCocoaDelegate( mSpeechSynthesizerListener );
                }
                else
                {
                    SubscribeToCocoaDelegate( null );
                }
            }
        }
        
        public bool StartSpeaking( string text )
        {
            return (bool) ObjCMessaging.objc_msgSend (NativeObject, "startSpeakingString:", typeof (bool), typeof (System.IntPtr), new Cocoa.String (text).NativeObject );
        }
        
        public void StopSpeaking()
        {
            ObjCMessaging.objc_msgSend (NativeObject, "stopSpeaking", typeof (void) );
        }
        

        
#region Cocoa Framework Event Handers
        
        protected void FireEvent_DidFinishSpeaking( bool aDidFinishSpeaking )
        {
            if( DidFinishSpeaking != null )
            {
                DidFinishSpeaking( this, aDidFinishSpeaking );
            }
        }
        
        protected void FireEvent_WillSpeak( Range aCharacterRange, Cocoa.String aString )
        {
            if( WillSpeak != null )
            {
                WillSpeak( this, aCharacterRange, aString.ToString() );
            }
        }
        
        protected void FireEvent_WillSpeakPhoneme( short aPhonemeOpCode )
        {
            if( WillSpeakPhoneme != null )
            {
                WillSpeakPhoneme( this, aPhonemeOpCode );
            }
        }
        
#endregion
        
        
    }
    
    
}


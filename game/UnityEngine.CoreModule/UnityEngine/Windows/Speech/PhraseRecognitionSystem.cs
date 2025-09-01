using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200028A RID: 650
	public static class PhraseRecognitionSystem
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001C2F RID: 7215
		public static extern bool isSupported { [NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")] [ThreadSafe] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001C30 RID: 7216
		public static extern SpeechSystemStatus Status { [NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001C31 RID: 7217
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Restart();

		// Token: 0x06001C32 RID: 7218
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Shutdown();

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001C33 RID: 7219 RVA: 0x0002D414 File Offset: 0x0002B614
		// (remove) Token: 0x06001C34 RID: 7220 RVA: 0x0002D448 File Offset: 0x0002B648
		public static event PhraseRecognitionSystem.ErrorDelegate OnError
		{
			[CompilerGenerated]
			add
			{
				PhraseRecognitionSystem.ErrorDelegate errorDelegate = PhraseRecognitionSystem.OnError;
				PhraseRecognitionSystem.ErrorDelegate errorDelegate2;
				do
				{
					errorDelegate2 = errorDelegate;
					PhraseRecognitionSystem.ErrorDelegate value2 = (PhraseRecognitionSystem.ErrorDelegate)Delegate.Combine(errorDelegate2, value);
					errorDelegate = Interlocked.CompareExchange<PhraseRecognitionSystem.ErrorDelegate>(ref PhraseRecognitionSystem.OnError, value2, errorDelegate2);
				}
				while (errorDelegate != errorDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				PhraseRecognitionSystem.ErrorDelegate errorDelegate = PhraseRecognitionSystem.OnError;
				PhraseRecognitionSystem.ErrorDelegate errorDelegate2;
				do
				{
					errorDelegate2 = errorDelegate;
					PhraseRecognitionSystem.ErrorDelegate value2 = (PhraseRecognitionSystem.ErrorDelegate)Delegate.Remove(errorDelegate2, value);
					errorDelegate = Interlocked.CompareExchange<PhraseRecognitionSystem.ErrorDelegate>(ref PhraseRecognitionSystem.OnError, value2, errorDelegate2);
				}
				while (errorDelegate != errorDelegate2);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06001C35 RID: 7221 RVA: 0x0002D47C File Offset: 0x0002B67C
		// (remove) Token: 0x06001C36 RID: 7222 RVA: 0x0002D4B0 File Offset: 0x0002B6B0
		public static event PhraseRecognitionSystem.StatusDelegate OnStatusChanged
		{
			[CompilerGenerated]
			add
			{
				PhraseRecognitionSystem.StatusDelegate statusDelegate = PhraseRecognitionSystem.OnStatusChanged;
				PhraseRecognitionSystem.StatusDelegate statusDelegate2;
				do
				{
					statusDelegate2 = statusDelegate;
					PhraseRecognitionSystem.StatusDelegate value2 = (PhraseRecognitionSystem.StatusDelegate)Delegate.Combine(statusDelegate2, value);
					statusDelegate = Interlocked.CompareExchange<PhraseRecognitionSystem.StatusDelegate>(ref PhraseRecognitionSystem.OnStatusChanged, value2, statusDelegate2);
				}
				while (statusDelegate != statusDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				PhraseRecognitionSystem.StatusDelegate statusDelegate = PhraseRecognitionSystem.OnStatusChanged;
				PhraseRecognitionSystem.StatusDelegate statusDelegate2;
				do
				{
					statusDelegate2 = statusDelegate;
					PhraseRecognitionSystem.StatusDelegate value2 = (PhraseRecognitionSystem.StatusDelegate)Delegate.Remove(statusDelegate2, value);
					statusDelegate = Interlocked.CompareExchange<PhraseRecognitionSystem.StatusDelegate>(ref PhraseRecognitionSystem.OnStatusChanged, value2, statusDelegate2);
				}
				while (statusDelegate != statusDelegate2);
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0002D4E4 File Offset: 0x0002B6E4
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeErrorEvent(SpeechError errorCode)
		{
			PhraseRecognitionSystem.ErrorDelegate onError = PhraseRecognitionSystem.OnError;
			bool flag = onError != null;
			if (flag)
			{
				onError(errorCode);
			}
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0002D508 File Offset: 0x0002B708
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeStatusChangedEvent(SpeechSystemStatus status)
		{
			PhraseRecognitionSystem.StatusDelegate onStatusChanged = PhraseRecognitionSystem.OnStatusChanged;
			bool flag = onStatusChanged != null;
			if (flag)
			{
				onStatusChanged(status);
			}
		}

		// Token: 0x04000929 RID: 2345
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static PhraseRecognitionSystem.ErrorDelegate OnError;

		// Token: 0x0400092A RID: 2346
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static PhraseRecognitionSystem.StatusDelegate OnStatusChanged;

		// Token: 0x0200028B RID: 651
		// (Invoke) Token: 0x06001C3A RID: 7226
		public delegate void ErrorDelegate(SpeechError errorCode);

		// Token: 0x0200028C RID: 652
		// (Invoke) Token: 0x06001C3E RID: 7230
		public delegate void StatusDelegate(SpeechSystemStatus status);
	}
}

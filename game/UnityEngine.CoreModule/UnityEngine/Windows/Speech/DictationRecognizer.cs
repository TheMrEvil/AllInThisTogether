using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200028F RID: 655
	public sealed class DictationRecognizer : IDisposable
	{
		// Token: 0x06001C56 RID: 7254
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(object self, ConfidenceLevel minimumConfidence, DictationTopicConstraint topicConstraint);

		// Token: 0x06001C57 RID: 7255
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Start(IntPtr self);

		// Token: 0x06001C58 RID: 7256
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop(IntPtr self);

		// Token: 0x06001C59 RID: 7257
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr self);

		// Token: 0x06001C5A RID: 7258
		[ThreadSafe]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyThreaded(IntPtr self);

		// Token: 0x06001C5B RID: 7259
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpeechSystemStatus GetStatus(IntPtr self);

		// Token: 0x06001C5C RID: 7260
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAutoSilenceTimeoutSeconds(IntPtr self);

		// Token: 0x06001C5D RID: 7261
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAutoSilenceTimeoutSeconds(IntPtr self, float value);

		// Token: 0x06001C5E RID: 7262
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetInitialSilenceTimeoutSeconds(IntPtr self);

		// Token: 0x06001C5F RID: 7263
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInitialSilenceTimeoutSeconds(IntPtr self, float value);

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001C60 RID: 7264 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		// (remove) Token: 0x06001C61 RID: 7265 RVA: 0x0002D80C File Offset: 0x0002BA0C
		public event DictationRecognizer.DictationHypothesisDelegate DictationHypothesis
		{
			[CompilerGenerated]
			add
			{
				DictationRecognizer.DictationHypothesisDelegate dictationHypothesisDelegate = this.DictationHypothesis;
				DictationRecognizer.DictationHypothesisDelegate dictationHypothesisDelegate2;
				do
				{
					dictationHypothesisDelegate2 = dictationHypothesisDelegate;
					DictationRecognizer.DictationHypothesisDelegate value2 = (DictationRecognizer.DictationHypothesisDelegate)Delegate.Combine(dictationHypothesisDelegate2, value);
					dictationHypothesisDelegate = Interlocked.CompareExchange<DictationRecognizer.DictationHypothesisDelegate>(ref this.DictationHypothesis, value2, dictationHypothesisDelegate2);
				}
				while (dictationHypothesisDelegate != dictationHypothesisDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				DictationRecognizer.DictationHypothesisDelegate dictationHypothesisDelegate = this.DictationHypothesis;
				DictationRecognizer.DictationHypothesisDelegate dictationHypothesisDelegate2;
				do
				{
					dictationHypothesisDelegate2 = dictationHypothesisDelegate;
					DictationRecognizer.DictationHypothesisDelegate value2 = (DictationRecognizer.DictationHypothesisDelegate)Delegate.Remove(dictationHypothesisDelegate2, value);
					dictationHypothesisDelegate = Interlocked.CompareExchange<DictationRecognizer.DictationHypothesisDelegate>(ref this.DictationHypothesis, value2, dictationHypothesisDelegate2);
				}
				while (dictationHypothesisDelegate != dictationHypothesisDelegate2);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001C62 RID: 7266 RVA: 0x0002D844 File Offset: 0x0002BA44
		// (remove) Token: 0x06001C63 RID: 7267 RVA: 0x0002D87C File Offset: 0x0002BA7C
		public event DictationRecognizer.DictationResultDelegate DictationResult
		{
			[CompilerGenerated]
			add
			{
				DictationRecognizer.DictationResultDelegate dictationResultDelegate = this.DictationResult;
				DictationRecognizer.DictationResultDelegate dictationResultDelegate2;
				do
				{
					dictationResultDelegate2 = dictationResultDelegate;
					DictationRecognizer.DictationResultDelegate value2 = (DictationRecognizer.DictationResultDelegate)Delegate.Combine(dictationResultDelegate2, value);
					dictationResultDelegate = Interlocked.CompareExchange<DictationRecognizer.DictationResultDelegate>(ref this.DictationResult, value2, dictationResultDelegate2);
				}
				while (dictationResultDelegate != dictationResultDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				DictationRecognizer.DictationResultDelegate dictationResultDelegate = this.DictationResult;
				DictationRecognizer.DictationResultDelegate dictationResultDelegate2;
				do
				{
					dictationResultDelegate2 = dictationResultDelegate;
					DictationRecognizer.DictationResultDelegate value2 = (DictationRecognizer.DictationResultDelegate)Delegate.Remove(dictationResultDelegate2, value);
					dictationResultDelegate = Interlocked.CompareExchange<DictationRecognizer.DictationResultDelegate>(ref this.DictationResult, value2, dictationResultDelegate2);
				}
				while (dictationResultDelegate != dictationResultDelegate2);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001C64 RID: 7268 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		// (remove) Token: 0x06001C65 RID: 7269 RVA: 0x0002D8EC File Offset: 0x0002BAEC
		public event DictationRecognizer.DictationCompletedDelegate DictationComplete
		{
			[CompilerGenerated]
			add
			{
				DictationRecognizer.DictationCompletedDelegate dictationCompletedDelegate = this.DictationComplete;
				DictationRecognizer.DictationCompletedDelegate dictationCompletedDelegate2;
				do
				{
					dictationCompletedDelegate2 = dictationCompletedDelegate;
					DictationRecognizer.DictationCompletedDelegate value2 = (DictationRecognizer.DictationCompletedDelegate)Delegate.Combine(dictationCompletedDelegate2, value);
					dictationCompletedDelegate = Interlocked.CompareExchange<DictationRecognizer.DictationCompletedDelegate>(ref this.DictationComplete, value2, dictationCompletedDelegate2);
				}
				while (dictationCompletedDelegate != dictationCompletedDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				DictationRecognizer.DictationCompletedDelegate dictationCompletedDelegate = this.DictationComplete;
				DictationRecognizer.DictationCompletedDelegate dictationCompletedDelegate2;
				do
				{
					dictationCompletedDelegate2 = dictationCompletedDelegate;
					DictationRecognizer.DictationCompletedDelegate value2 = (DictationRecognizer.DictationCompletedDelegate)Delegate.Remove(dictationCompletedDelegate2, value);
					dictationCompletedDelegate = Interlocked.CompareExchange<DictationRecognizer.DictationCompletedDelegate>(ref this.DictationComplete, value2, dictationCompletedDelegate2);
				}
				while (dictationCompletedDelegate != dictationCompletedDelegate2);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001C66 RID: 7270 RVA: 0x0002D924 File Offset: 0x0002BB24
		// (remove) Token: 0x06001C67 RID: 7271 RVA: 0x0002D95C File Offset: 0x0002BB5C
		public event DictationRecognizer.DictationErrorHandler DictationError
		{
			[CompilerGenerated]
			add
			{
				DictationRecognizer.DictationErrorHandler dictationErrorHandler = this.DictationError;
				DictationRecognizer.DictationErrorHandler dictationErrorHandler2;
				do
				{
					dictationErrorHandler2 = dictationErrorHandler;
					DictationRecognizer.DictationErrorHandler value2 = (DictationRecognizer.DictationErrorHandler)Delegate.Combine(dictationErrorHandler2, value);
					dictationErrorHandler = Interlocked.CompareExchange<DictationRecognizer.DictationErrorHandler>(ref this.DictationError, value2, dictationErrorHandler2);
				}
				while (dictationErrorHandler != dictationErrorHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DictationRecognizer.DictationErrorHandler dictationErrorHandler = this.DictationError;
				DictationRecognizer.DictationErrorHandler dictationErrorHandler2;
				do
				{
					dictationErrorHandler2 = dictationErrorHandler;
					DictationRecognizer.DictationErrorHandler value2 = (DictationRecognizer.DictationErrorHandler)Delegate.Remove(dictationErrorHandler2, value);
					dictationErrorHandler = Interlocked.CompareExchange<DictationRecognizer.DictationErrorHandler>(ref this.DictationError, value2, dictationErrorHandler2);
				}
				while (dictationErrorHandler != dictationErrorHandler2);
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0002D994 File Offset: 0x0002BB94
		public SpeechSystemStatus Status
		{
			get
			{
				return (this.m_Recognizer != IntPtr.Zero) ? DictationRecognizer.GetStatus(this.m_Recognizer) : SpeechSystemStatus.Stopped;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0002D9C8 File Offset: 0x0002BBC8
		// (set) Token: 0x06001C6A RID: 7274 RVA: 0x0002DA04 File Offset: 0x0002BC04
		public float AutoSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float result;
				if (flag)
				{
					result = 0f;
				}
				else
				{
					result = DictationRecognizer.GetAutoSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return result;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetAutoSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0002DA38 File Offset: 0x0002BC38
		// (set) Token: 0x06001C6C RID: 7276 RVA: 0x0002DA74 File Offset: 0x0002BC74
		public float InitialSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float result;
				if (flag)
				{
					result = 0f;
				}
				else
				{
					result = DictationRecognizer.GetInitialSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return result;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetInitialSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0002DAA5 File Offset: 0x0002BCA5
		public DictationRecognizer() : this(ConfidenceLevel.Medium, DictationTopicConstraint.Dictation)
		{
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0002DAB1 File Offset: 0x0002BCB1
		public DictationRecognizer(ConfidenceLevel confidenceLevel) : this(confidenceLevel, DictationTopicConstraint.Dictation)
		{
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0002DABD File Offset: 0x0002BCBD
		public DictationRecognizer(DictationTopicConstraint topic) : this(ConfidenceLevel.Medium, topic)
		{
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0002DAC9 File Offset: 0x0002BCC9
		public DictationRecognizer(ConfidenceLevel minimumConfidence, DictationTopicConstraint topic)
		{
			this.m_Recognizer = DictationRecognizer.Create(this, minimumConfidence, topic);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0002DAE4 File Offset: 0x0002BCE4
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					DictationRecognizer.DestroyThreaded(this.m_Recognizer);
					this.m_Recognizer = IntPtr.Zero;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0002DB44 File Offset: 0x0002BD44
		public void Start()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Start(this.m_Recognizer);
			}
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0002DB74 File Offset: 0x0002BD74
		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Stop(this.m_Recognizer);
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0002DBA4 File Offset: 0x0002BDA4
		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				DictationRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0002DBE8 File Offset: 0x0002BDE8
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeHypothesisGeneratedEvent(string keyword)
		{
			DictationRecognizer.DictationHypothesisDelegate dictationHypothesis = this.DictationHypothesis;
			bool flag = dictationHypothesis != null;
			if (flag)
			{
				dictationHypothesis(keyword);
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0002DC10 File Offset: 0x0002BE10
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeResultGeneratedEvent(string keyword, ConfidenceLevel minimumConfidence)
		{
			DictationRecognizer.DictationResultDelegate dictationResult = this.DictationResult;
			bool flag = dictationResult != null;
			if (flag)
			{
				dictationResult(keyword, minimumConfidence);
			}
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0002DC38 File Offset: 0x0002BE38
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeCompletedEvent(DictationCompletionCause cause)
		{
			DictationRecognizer.DictationCompletedDelegate dictationComplete = this.DictationComplete;
			bool flag = dictationComplete != null;
			if (flag)
			{
				dictationComplete(cause);
			}
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0002DC60 File Offset: 0x0002BE60
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeErrorEvent(string error, int hresult)
		{
			DictationRecognizer.DictationErrorHandler dictationError = this.DictationError;
			bool flag = dictationError != null;
			if (flag)
			{
				dictationError(error, hresult);
			}
		}

		// Token: 0x0400092D RID: 2349
		private IntPtr m_Recognizer;

		// Token: 0x0400092E RID: 2350
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DictationRecognizer.DictationHypothesisDelegate DictationHypothesis;

		// Token: 0x0400092F RID: 2351
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DictationRecognizer.DictationResultDelegate DictationResult;

		// Token: 0x04000930 RID: 2352
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DictationRecognizer.DictationCompletedDelegate DictationComplete;

		// Token: 0x04000931 RID: 2353
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DictationRecognizer.DictationErrorHandler DictationError;

		// Token: 0x02000290 RID: 656
		// (Invoke) Token: 0x06001C7A RID: 7290
		public delegate void DictationHypothesisDelegate(string text);

		// Token: 0x02000291 RID: 657
		// (Invoke) Token: 0x06001C7E RID: 7294
		public delegate void DictationResultDelegate(string text, ConfidenceLevel confidence);

		// Token: 0x02000292 RID: 658
		// (Invoke) Token: 0x06001C82 RID: 7298
		public delegate void DictationCompletedDelegate(DictationCompletionCause cause);

		// Token: 0x02000293 RID: 659
		// (Invoke) Token: 0x06001C86 RID: 7302
		public delegate void DictationErrorHandler(string error, int hresult);
	}
}

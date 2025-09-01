using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200028D RID: 653
	public abstract class PhraseRecognizer : IDisposable
	{
		// Token: 0x06001C41 RID: 7233
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected static extern IntPtr CreateFromKeywords(object self, [Unmarshalled] string[] keywords, ConfidenceLevel minimumConfidence);

		// Token: 0x06001C42 RID: 7234
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected static extern IntPtr CreateFromGrammarFile(object self, string grammarFilePath, ConfidenceLevel minimumConfidence);

		// Token: 0x06001C43 RID: 7235
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Start_Internal(IntPtr recognizer);

		// Token: 0x06001C44 RID: 7236
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Internal(IntPtr recognizer);

		// Token: 0x06001C45 RID: 7237
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRunning_Internal(IntPtr recognizer);

		// Token: 0x06001C46 RID: 7238
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr recognizer);

		// Token: 0x06001C47 RID: 7239
		[ThreadSafe]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyThreaded(IntPtr recognizer);

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001C48 RID: 7240 RVA: 0x0002D52C File Offset: 0x0002B72C
		// (remove) Token: 0x06001C49 RID: 7241 RVA: 0x0002D564 File Offset: 0x0002B764
		public event PhraseRecognizer.PhraseRecognizedDelegate OnPhraseRecognized
		{
			[CompilerGenerated]
			add
			{
				PhraseRecognizer.PhraseRecognizedDelegate phraseRecognizedDelegate = this.OnPhraseRecognized;
				PhraseRecognizer.PhraseRecognizedDelegate phraseRecognizedDelegate2;
				do
				{
					phraseRecognizedDelegate2 = phraseRecognizedDelegate;
					PhraseRecognizer.PhraseRecognizedDelegate value2 = (PhraseRecognizer.PhraseRecognizedDelegate)Delegate.Combine(phraseRecognizedDelegate2, value);
					phraseRecognizedDelegate = Interlocked.CompareExchange<PhraseRecognizer.PhraseRecognizedDelegate>(ref this.OnPhraseRecognized, value2, phraseRecognizedDelegate2);
				}
				while (phraseRecognizedDelegate != phraseRecognizedDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				PhraseRecognizer.PhraseRecognizedDelegate phraseRecognizedDelegate = this.OnPhraseRecognized;
				PhraseRecognizer.PhraseRecognizedDelegate phraseRecognizedDelegate2;
				do
				{
					phraseRecognizedDelegate2 = phraseRecognizedDelegate;
					PhraseRecognizer.PhraseRecognizedDelegate value2 = (PhraseRecognizer.PhraseRecognizedDelegate)Delegate.Remove(phraseRecognizedDelegate2, value);
					phraseRecognizedDelegate = Interlocked.CompareExchange<PhraseRecognizer.PhraseRecognizedDelegate>(ref this.OnPhraseRecognized, value2, phraseRecognizedDelegate2);
				}
				while (phraseRecognizedDelegate != phraseRecognizedDelegate2);
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00008CBB File Offset: 0x00006EBB
		internal PhraseRecognizer()
		{
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0002D59C File Offset: 0x0002B79C
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					PhraseRecognizer.DestroyThreaded(this.m_Recognizer);
					this.m_Recognizer = IntPtr.Zero;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0002D5FC File Offset: 0x0002B7FC
		public void Start()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				PhraseRecognizer.Start_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0002D62C File Offset: 0x0002B82C
		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				PhraseRecognizer.Stop_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0002D65C File Offset: 0x0002B85C
		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				PhraseRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0002D6A0 File Offset: 0x0002B8A0
		public bool IsRunning
		{
			get
			{
				return this.m_Recognizer != IntPtr.Zero && PhraseRecognizer.IsRunning_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
		[RequiredByNativeCode]
		private void InvokePhraseRecognizedEvent(string text, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, long phraseStartFileTime, long phraseDurationTicks)
		{
			PhraseRecognizer.PhraseRecognizedDelegate onPhraseRecognized = this.OnPhraseRecognized;
			bool flag = onPhraseRecognized != null;
			if (flag)
			{
				onPhraseRecognized(new PhraseRecognizedEventArgs(text, confidence, semanticMeanings, DateTime.FromFileTime(phraseStartFileTime), TimeSpan.FromTicks(phraseDurationTicks)));
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0002D710 File Offset: 0x0002B910
		[RequiredByNativeCode]
		private unsafe static SemanticMeaning[] MarshalSemanticMeaning(IntPtr keys, IntPtr values, IntPtr valueSizes, int valueCount)
		{
			SemanticMeaning[] array = new SemanticMeaning[valueCount];
			int num = 0;
			for (int i = 0; i < valueCount; i++)
			{
				uint num2 = *(uint*)((byte*)((void*)valueSizes) + (IntPtr)i * 4);
				SemanticMeaning semanticMeaning = new SemanticMeaning
				{
					key = new string(*(IntPtr*)((byte*)((void*)keys) + (IntPtr)i * (IntPtr)sizeof(char*))),
					values = new string[num2]
				};
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					semanticMeaning.values[num3] = new string(*(IntPtr*)((byte*)((void*)values) + (IntPtr)(num + num3) * (IntPtr)sizeof(char*)));
					num3++;
				}
				array[i] = semanticMeaning;
				num += (int)num2;
			}
			return array;
		}

		// Token: 0x0400092B RID: 2347
		protected IntPtr m_Recognizer;

		// Token: 0x0400092C RID: 2348
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PhraseRecognizer.PhraseRecognizedDelegate OnPhraseRecognized;

		// Token: 0x0200028E RID: 654
		// (Invoke) Token: 0x06001C53 RID: 7251
		public delegate void PhraseRecognizedDelegate(PhraseRecognizedEventArgs args);
	}
}

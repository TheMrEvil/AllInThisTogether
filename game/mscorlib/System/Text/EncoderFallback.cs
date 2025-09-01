using System;
using System.Threading;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an input character that cannot be converted to an encoded output byte sequence.</summary>
	// Token: 0x020003A1 RID: 929
	[Serializable]
	public abstract class EncoderFallback
	{
		/// <summary>Gets an object that outputs a substitute string in place of an input character that cannot be encoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.EncoderFallback" /> class. The default value is a <see cref="T:System.Text.EncoderReplacementFallback" /> object that replaces unknown input characters with the QUESTION MARK character ("?", U+003F).</returns>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000871AF File Offset: 0x000853AF
		public static EncoderFallback ReplacementFallback
		{
			get
			{
				if (EncoderFallback.s_replacementFallback == null)
				{
					Interlocked.CompareExchange<EncoderFallback>(ref EncoderFallback.s_replacementFallback, new EncoderReplacementFallback(), null);
				}
				return EncoderFallback.s_replacementFallback;
			}
		}

		/// <summary>Gets an object that throws an exception when an input character cannot be encoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.EncoderFallback" /> class. The default value is a <see cref="T:System.Text.EncoderExceptionFallback" /> object.</returns>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x000871CE File Offset: 0x000853CE
		public static EncoderFallback ExceptionFallback
		{
			get
			{
				if (EncoderFallback.s_exceptionFallback == null)
				{
					Interlocked.CompareExchange<EncoderFallback>(ref EncoderFallback.s_exceptionFallback, new EncoderExceptionFallback(), null);
				}
				return EncoderFallback.s_exceptionFallback;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.EncoderFallbackBuffer" /> class.</summary>
		/// <returns>An object that provides a fallback buffer for an encoder.</returns>
		// Token: 0x0600261C RID: 9756
		public abstract EncoderFallbackBuffer CreateFallbackBuffer();

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.EncoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.EncoderFallback" /> object can return.</returns>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600261D RID: 9757
		public abstract int MaxCharCount { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallback" /> class.</summary>
		// Token: 0x0600261E RID: 9758 RVA: 0x0000259F File Offset: 0x0000079F
		protected EncoderFallback()
		{
		}

		// Token: 0x04001DB1 RID: 7601
		private static EncoderFallback s_replacementFallback;

		// Token: 0x04001DB2 RID: 7602
		private static EncoderFallback s_exceptionFallback;
	}
}

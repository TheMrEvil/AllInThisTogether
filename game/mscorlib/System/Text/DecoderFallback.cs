using System;
using System.Threading;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an output character.</summary>
	// Token: 0x02000396 RID: 918
	[Serializable]
	public abstract class DecoderFallback
	{
		/// <summary>Gets an object that outputs a substitute string in place of an input byte sequence that cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderReplacementFallback" /> object that emits the QUESTION MARK character ("?", U+003F) in place of unknown byte sequences.</returns>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x00086013 File Offset: 0x00084213
		public static DecoderFallback ReplacementFallback
		{
			get
			{
				DecoderFallback result;
				if ((result = DecoderFallback.s_replacementFallback) == null)
				{
					result = (Interlocked.CompareExchange<DecoderFallback>(ref DecoderFallback.s_replacementFallback, new DecoderReplacementFallback(), null) ?? DecoderFallback.s_replacementFallback);
				}
				return result;
			}
		}

		/// <summary>Gets an object that throws an exception when an input byte sequence cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderExceptionFallback" /> object.</returns>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x00086037 File Offset: 0x00084237
		public static DecoderFallback ExceptionFallback
		{
			get
			{
				DecoderFallback result;
				if ((result = DecoderFallback.s_exceptionFallback) == null)
				{
					result = (Interlocked.CompareExchange<DecoderFallback>(ref DecoderFallback.s_exceptionFallback, new DecoderExceptionFallback(), null) ?? DecoderFallback.s_exceptionFallback);
				}
				return result;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.DecoderFallbackBuffer" /> class.</summary>
		/// <returns>An object that provides a fallback buffer for a decoder.</returns>
		// Token: 0x060025BB RID: 9659
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060025BC RID: 9660
		public abstract int MaxCharCount { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallback" /> class.</summary>
		// Token: 0x060025BD RID: 9661 RVA: 0x0000259F File Offset: 0x0000079F
		protected DecoderFallback()
		{
		}

		// Token: 0x04001D98 RID: 7576
		private static DecoderFallback s_replacementFallback;

		// Token: 0x04001D99 RID: 7577
		private static DecoderFallback s_exceptionFallback;
	}
}

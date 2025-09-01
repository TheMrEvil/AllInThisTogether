using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents an exception handler in a byte array of IL to be passed to a method such as <see cref="M:System.Reflection.Emit.MethodBuilder.SetMethodBody(System.Byte[],System.Int32,System.Byte[],System.Collections.Generic.IEnumerable{System.Reflection.Emit.ExceptionHandler},System.Collections.Generic.IEnumerable{System.Int32})" />.</summary>
	// Token: 0x0200090B RID: 2315
	[ComVisible(false)]
	public readonly struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		/// <summary>Gets the token of the exception type handled by this handler.</summary>
		/// <returns>The token of the exception type handled by this handler, or 0 if none exists.</returns>
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004E4F RID: 20047 RVA: 0x000F6147 File Offset: 0x000F4347
		public int ExceptionTypeToken
		{
			get
			{
				return this.m_exceptionClass;
			}
		}

		/// <summary>Gets the byte offset at which the code that is protected by this exception handler begins.</summary>
		/// <returns>The byte offset at which the code that is protected by this exception handler begins.</returns>
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004E50 RID: 20048 RVA: 0x000F614F File Offset: 0x000F434F
		public int TryOffset
		{
			get
			{
				return this.m_tryStartOffset;
			}
		}

		/// <summary>Gets the length, in bytes, of the code protected by this exception handler.</summary>
		/// <returns>The length, in bytes, of the code protected by this exception handler.</returns>
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004E51 RID: 20049 RVA: 0x000F6157 File Offset: 0x000F4357
		public int TryLength
		{
			get
			{
				return this.m_tryEndOffset - this.m_tryStartOffset;
			}
		}

		/// <summary>Gets the byte offset at which the filter code for the exception handler begins.</summary>
		/// <returns>The byte offset at which the filter code begins, or 0 if no filter  is present.</returns>
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x000F6166 File Offset: 0x000F4366
		public int FilterOffset
		{
			get
			{
				return this.m_filterOffset;
			}
		}

		/// <summary>Gets the byte offset of the first instruction of the exception handler.</summary>
		/// <returns>The byte offset of the first instruction of the exception handler.</returns>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x000F616E File Offset: 0x000F436E
		public int HandlerOffset
		{
			get
			{
				return this.m_handlerStartOffset;
			}
		}

		/// <summary>Gets the length, in bytes, of the exception handler.</summary>
		/// <returns>The length, in bytes, of the exception handler.</returns>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x000F6176 File Offset: 0x000F4376
		public int HandlerLength
		{
			get
			{
				return this.m_handlerEndOffset - this.m_handlerStartOffset;
			}
		}

		/// <summary>Gets a value that represents the kind of exception handler this object represents.</summary>
		/// <returns>One of the enumeration values that specifies the kind of exception handler.</returns>
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004E55 RID: 20053 RVA: 0x000F6185 File Offset: 0x000F4385
		public ExceptionHandlingClauseOptions Kind
		{
			get
			{
				return this.m_kind;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Emit.ExceptionHandler" /> class with the specified parameters.</summary>
		/// <param name="tryOffset">The byte offset of the first instruction protected by this exception handler.</param>
		/// <param name="tryLength">The number of bytes protected by this exception handler.</param>
		/// <param name="filterOffset">The byte offset of the beginning of the filter code. The filter code ends at the first instruction of the handler block. For non-filter exception handlers, specify 0 (zero) for this parameter.</param>
		/// <param name="handlerOffset">The byte offset of the first instruction of this exception handler.</param>
		/// <param name="handlerLength">The number of bytes in this exception handler.</param>
		/// <param name="kind">One of the enumeration values that specifies the kind of exception handler.</param>
		/// <param name="exceptionTypeToken">The token of the exception type handled by this exception handler. If not applicable, specify 0 (zero).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tryOffset" />, <paramref name="filterOffset" />, <paramref name="handlerOffset" />, <paramref name="tryLength" />, or <paramref name="handlerLength" /> are negative.</exception>
		// Token: 0x06004E56 RID: 20054 RVA: 0x000F6190 File Offset: 0x000F4390
		public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
		{
			if (tryOffset < 0)
			{
				throw new ArgumentOutOfRangeException("tryOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (tryLength < 0)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("Non-negative number required."));
			}
			if (filterOffset < 0)
			{
				throw new ArgumentOutOfRangeException("filterOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (handlerOffset < 0)
			{
				throw new ArgumentOutOfRangeException("handlerOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (handlerLength < 0)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("Non-negative number required."));
			}
			if ((long)tryOffset + (long)tryLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					0,
					int.MaxValue - tryOffset
				}));
			}
			if ((long)handlerOffset + (long)handlerLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					0,
					int.MaxValue - handlerOffset
				}));
			}
			if (kind == ExceptionHandlingClauseOptions.Clause && (exceptionTypeToken & 16777215) == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Token {0:x} is not a valid Type token.", new object[]
				{
					exceptionTypeToken
				}), "exceptionTypeToken");
			}
			if (!ExceptionHandler.IsValidKind(kind))
			{
				throw new ArgumentOutOfRangeException("kind", Environment.GetResourceString("Enum value was out of legal range."));
			}
			this.m_tryStartOffset = tryOffset;
			this.m_tryEndOffset = tryOffset + tryLength;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerOffset;
			this.m_handlerEndOffset = handlerOffset + handlerLength;
			this.m_kind = kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x000F632A File Offset: 0x000F452A
		internal ExceptionHandler(int tryStartOffset, int tryEndOffset, int filterOffset, int handlerStartOffset, int handlerEndOffset, int kind, int exceptionTypeToken)
		{
			this.m_tryStartOffset = tryStartOffset;
			this.m_tryEndOffset = tryEndOffset;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerStartOffset;
			this.m_handlerEndOffset = handlerEndOffset;
			this.m_kind = (ExceptionHandlingClauseOptions)kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x000F6361 File Offset: 0x000F4561
		private static bool IsValidKind(ExceptionHandlingClauseOptions kind)
		{
			return kind <= ExceptionHandlingClauseOptions.Finally || kind == ExceptionHandlingClauseOptions.Fault;
		}

		/// <summary>Serves as the default hash function.</summary>
		/// <returns>The hash code for the current object.</returns>
		// Token: 0x06004E59 RID: 20057 RVA: 0x000F636E File Offset: 0x000F456E
		public override int GetHashCode()
		{
			return this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset ^ (int)this.m_kind;
		}

		/// <summary>Indicates whether this instance of the <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object is equal to a specified object.</summary>
		/// <param name="obj">The object to compare this instance to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this instance are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E5A RID: 20058 RVA: 0x000F63A0 File Offset: 0x000F45A0
		public override bool Equals(object obj)
		{
			return obj is ExceptionHandler && this.Equals((ExceptionHandler)obj);
		}

		/// <summary>Indicates whether this instance of the <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object is equal to another <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object.</summary>
		/// <param name="other">The exception handler object to compare this instance to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> and this instance are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E5B RID: 20059 RVA: 0x000F63B8 File Offset: 0x000F45B8
		public bool Equals(ExceptionHandler other)
		{
			return other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset && other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset && other.m_kind == this.m_kind;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.Reflection.Emit.ExceptionHandler" /> are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E5C RID: 20060 RVA: 0x000F6429 File Offset: 0x000F4629
		public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.Reflection.Emit.ExceptionHandler" /> are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E5D RID: 20061 RVA: 0x000F6433 File Offset: 0x000F4633
		public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040030C5 RID: 12485
		internal readonly int m_exceptionClass;

		// Token: 0x040030C6 RID: 12486
		internal readonly int m_tryStartOffset;

		// Token: 0x040030C7 RID: 12487
		internal readonly int m_tryEndOffset;

		// Token: 0x040030C8 RID: 12488
		internal readonly int m_filterOffset;

		// Token: 0x040030C9 RID: 12489
		internal readonly int m_handlerStartOffset;

		// Token: 0x040030CA RID: 12490
		internal readonly int m_handlerEndOffset;

		// Token: 0x040030CB RID: 12491
		internal readonly ExceptionHandlingClauseOptions m_kind;
	}
}

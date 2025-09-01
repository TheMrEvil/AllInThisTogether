using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Unity;

namespace System.Globalization
{
	/// <summary>Enumerates the text elements of a string.</summary>
	// Token: 0x02000998 RID: 2456
	[ComVisible(true)]
	[Serializable]
	public class TextElementEnumerator : IEnumerator
	{
		// Token: 0x06005817 RID: 22551 RVA: 0x00128F55 File Offset: 0x00127155
		internal TextElementEnumerator(string str, int startIndex, int strLen)
		{
			this.str = str;
			this.startIndex = startIndex;
			this.strLen = strLen;
			this.Reset();
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x00128F78 File Offset: 0x00127178
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.charLen = -1;
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x00128F84 File Offset: 0x00127184
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.strLen = this.endIndex + 1;
			this.currTextElementLen = this.nextTextElementLen;
			if (this.charLen == -1)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x00128FD1 File Offset: 0x001271D1
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.endIndex = this.strLen - 1;
			this.nextTextElementLen = this.currTextElementLen;
		}

		/// <summary>Advances the enumerator to the next text element of the string.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next text element; <see langword="false" /> if the enumerator has passed the end of the string.</returns>
		// Token: 0x0600581B RID: 22555 RVA: 0x00128FF0 File Offset: 0x001271F0
		public bool MoveNext()
		{
			if (this.index >= this.strLen)
			{
				this.index = this.strLen + 1;
				return false;
			}
			this.currTextElementLen = StringInfo.GetCurrentTextElementLen(this.str, this.index, this.strLen, ref this.uc, ref this.charLen);
			this.index += this.currTextElementLen;
			return true;
		}

		/// <summary>Gets the current text element in the string.</summary>
		/// <returns>An object containing the current text element in the string.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element.</exception>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x0600581C RID: 22556 RVA: 0x00129058 File Offset: 0x00127258
		public object Current
		{
			get
			{
				return this.GetTextElement();
			}
		}

		/// <summary>Gets the current text element in the string.</summary>
		/// <returns>A new string containing the current text element in the string being read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element.</exception>
		// Token: 0x0600581D RID: 22557 RVA: 0x00129060 File Offset: 0x00127260
		public string GetTextElement()
		{
			if (this.index == this.startIndex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
			}
			if (this.index > this.strLen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
			}
			return this.str.Substring(this.index - this.currTextElementLen, this.currTextElementLen);
		}

		/// <summary>Gets the index of the text element that the enumerator is currently positioned over.</summary>
		/// <returns>The index of the text element that the enumerator is currently positioned over.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element.</exception>
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x0600581E RID: 22558 RVA: 0x001290C7 File Offset: 0x001272C7
		public int ElementIndex
		{
			get
			{
				if (this.index == this.startIndex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
				}
				return this.index - this.currTextElementLen;
			}
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first text element in the string.</summary>
		// Token: 0x0600581F RID: 22559 RVA: 0x001290F4 File Offset: 0x001272F4
		public void Reset()
		{
			this.index = this.startIndex;
			if (this.index < this.strLen)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x000173AD File Offset: 0x000155AD
		internal TextElementEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040036A0 RID: 13984
		private string str;

		// Token: 0x040036A1 RID: 13985
		private int index;

		// Token: 0x040036A2 RID: 13986
		private int startIndex;

		// Token: 0x040036A3 RID: 13987
		[NonSerialized]
		private int strLen;

		// Token: 0x040036A4 RID: 13988
		[NonSerialized]
		private int currTextElementLen;

		// Token: 0x040036A5 RID: 13989
		[OptionalField(VersionAdded = 2)]
		private UnicodeCategory uc;

		// Token: 0x040036A6 RID: 13990
		[OptionalField(VersionAdded = 2)]
		private int charLen;

		// Token: 0x040036A7 RID: 13991
		private int endIndex;

		// Token: 0x040036A8 RID: 13992
		private int nextTextElementLen;
	}
}

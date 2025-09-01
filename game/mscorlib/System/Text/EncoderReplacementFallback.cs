using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>Provides a failure handling mechanism, called a fallback, for an input character that cannot be converted to an output byte sequence. The fallback uses a user-specified replacement string instead of the original input character. This class cannot be inherited.</summary>
	// Token: 0x020003A4 RID: 932
	[Serializable]
	public sealed class EncoderReplacementFallback : EncoderFallback, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallback" /> class.</summary>
		// Token: 0x06002638 RID: 9784 RVA: 0x000877C3 File Offset: 0x000859C3
		public EncoderReplacementFallback() : this("?")
		{
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000877D0 File Offset: 0x000859D0
		internal EncoderReplacementFallback(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this._strDefault = info.GetString("strDefault");
			}
			catch
			{
				this._strDefault = info.GetString("_strDefault");
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x0008781C File Offset: 0x00085A1C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("strDefault", this._strDefault);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallback" /> class using a specified replacement string.</summary>
		/// <param name="replacement">A string that is converted in an encoding operation in place of an input character that cannot be encoded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacement" /> contains an invalid surrogate pair. In other words, the surrogate does not consist of one high surrogate component followed by one low surrogate component.</exception>
		// Token: 0x0600263B RID: 9787 RVA: 0x00087830 File Offset: 0x00085A30
		public EncoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			bool flag = false;
			for (int i = 0; i < replacement.Length; i++)
			{
				if (char.IsSurrogate(replacement, i))
				{
					if (char.IsHighSurrogate(replacement, i))
					{
						if (flag)
						{
							break;
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							break;
						}
						flag = false;
					}
				}
				else if (flag)
				{
					break;
				}
			}
			if (flag)
			{
				throw new ArgumentException(SR.Format("String contains invalid Unicode code points.", "replacement"));
			}
			this._strDefault = replacement;
		}

		/// <summary>Gets the replacement string that is the value of the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>A substitute string that is used in place of an input character that cannot be encoded.</returns>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x000878AA File Offset: 0x00085AAA
		public string DefaultString
		{
			get
			{
				return this._strDefault;
			}
		}

		/// <summary>Creates a <see cref="T:System.Text.EncoderFallbackBuffer" /> object that is initialized with the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.EncoderFallbackBuffer" /> object equal to this <see cref="T:System.Text.EncoderReplacementFallback" /> object.</returns>
		// Token: 0x0600263D RID: 9789 RVA: 0x000878B2 File Offset: 0x00085AB2
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		/// <summary>Gets the number of characters in the replacement string for the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>The number of characters in the string used in place of an input character that cannot be encoded.</returns>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000878BA File Offset: 0x00085ABA
		public override int MaxCharCount
		{
			get
			{
				return this._strDefault.Length;
			}
		}

		/// <summary>Indicates whether the value of a specified object is equal to the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <param name="value">A <see cref="T:System.Text.EncoderReplacementFallback" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter specifies an <see cref="T:System.Text.EncoderReplacementFallback" /> object and the replacement string of that object is equal to the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600263F RID: 9791 RVA: 0x000878C8 File Offset: 0x00085AC8
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this._strDefault == encoderReplacementFallback._strDefault;
		}

		/// <summary>Retrieves the hash code for the value of the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>The hash code of the value of the object.</returns>
		// Token: 0x06002640 RID: 9792 RVA: 0x000878F2 File Offset: 0x00085AF2
		public override int GetHashCode()
		{
			return this._strDefault.GetHashCode();
		}

		// Token: 0x04001DC0 RID: 7616
		private string _strDefault;
	}
}

using System;

namespace System.Text
{
	/// <summary>Provides the base class for an encoding provider, which supplies encodings that are unavailable on a particular platform.</summary>
	// Token: 0x020003A8 RID: 936
	public abstract class EncodingProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncodingProvider" /> class.</summary>
		// Token: 0x0600265E RID: 9822 RVA: 0x0000259F File Offset: 0x0000079F
		public EncodingProvider()
		{
		}

		/// <summary>Returns the encoding with the specified name.</summary>
		/// <param name="name">The name of the requested encoding.</param>
		/// <returns>The encoding that is associated with the specified name, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="name" />.</returns>
		// Token: 0x0600265F RID: 9823
		public abstract Encoding GetEncoding(string name);

		/// <summary>Returns the encoding associated with the specified code page identifier.</summary>
		/// <param name="codepage">The code page identifier of the requested encoding.</param>
		/// <returns>The encoding that is associated with the specified code page, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="codepage" />.</returns>
		// Token: 0x06002660 RID: 9824
		public abstract Encoding GetEncoding(int codepage);

		/// <summary>Returns the encoding associated with the specified name. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <param name="name">The name of the preferred encoding.</param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with this encoding.</param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding.</param>
		/// <returns>The encoding that is associated with the specified name, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="name" />.</returns>
		// Token: 0x06002661 RID: 9825 RVA: 0x0008809C File Offset: 0x0008629C
		public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(name);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(name).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		/// <summary>Returns the encoding associated with the specified code page identifier. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <param name="codepage">The code page identifier of the requested encoding.</param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with this encoding.</param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with this encoding.</param>
		/// <returns>The encoding that is associated with the specified code page, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="codepage" />.</returns>
		// Token: 0x06002662 RID: 9826 RVA: 0x000880D8 File Offset: 0x000862D8
		public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(codepage);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(codepage).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00088114 File Offset: 0x00086314
		internal static void AddProvider(EncodingProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			object obj = EncodingProvider.s_InternalSyncObject;
			lock (obj)
			{
				if (EncodingProvider.s_providers == null)
				{
					EncodingProvider.s_providers = new EncodingProvider[]
					{
						provider
					};
				}
				else if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) < 0)
				{
					EncodingProvider[] array = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
					Array.Copy(EncodingProvider.s_providers, array, EncodingProvider.s_providers.Length);
					array[array.Length - 1] = provider;
					EncodingProvider.s_providers = array;
				}
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000881C0 File Offset: 0x000863C0
		internal static Encoding GetEncodingFromProvider(int codepage)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(codepage);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x00088200 File Offset: 0x00086400
		internal static Encoding GetEncodingFromProvider(string encodingName)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(encodingName);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x00088240 File Offset: 0x00086440
		internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(codepage, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x00088280 File Offset: 0x00086480
		internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(encodingName, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000882C0 File Offset: 0x000864C0
		// Note: this type is marked as 'beforefieldinit'.
		static EncodingProvider()
		{
		}

		// Token: 0x04001DC7 RID: 7623
		private static object s_InternalSyncObject = new object();

		// Token: 0x04001DC8 RID: 7624
		private static volatile EncodingProvider[] s_providers;
	}
}

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Represents the public key information (called a blob) for a strong name. This class cannot be inherited.</summary>
	// Token: 0x0200045F RID: 1119
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNamePublicKeyBlob
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> class with raw bytes of the public key blob.</summary>
		/// <param name="publicKey">The array of bytes representing the raw public key data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="publicKey" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002D7B RID: 11643 RVA: 0x000A3060 File Offset: 0x000A1260
		public StrongNamePublicKeyBlob(byte[] publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			this.pubkey = publicKey;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000A3080 File Offset: 0x000A1280
		internal static StrongNamePublicKeyBlob FromString(string s)
		{
			if (s == null || s.Length == 0)
			{
				return null;
			}
			byte[] array = new byte[s.Length / 2];
			int i = 0;
			int num = 0;
			while (i < s.Length)
			{
				byte b = StrongNamePublicKeyBlob.CharToByte(s[i]);
				byte b2 = StrongNamePublicKeyBlob.CharToByte(s[i + 1]);
				array[num] = Convert.ToByte((int)(b * 16 + b2));
				i += 2;
				num++;
			}
			return new StrongNamePublicKeyBlob(array);
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000A30F4 File Offset: 0x000A12F4
		private static byte CharToByte(char c)
		{
			char c2 = char.ToLowerInvariant(c);
			if (char.IsDigit(c2))
			{
				return (byte)(c2 - '0');
			}
			return (byte)(c2 - 'a' + '\n');
		}

		/// <summary>Gets or sets a value indicating whether the current public key blob is equal to the specified public key blob.</summary>
		/// <param name="obj">An object containing a public key blob.</param>
		/// <returns>
		///   <see langword="true" /> if the public key blob of the current object is equal to the public key blob of the <paramref name="o" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D7E RID: 11646 RVA: 0x000A3120 File Offset: 0x000A1320
		public override bool Equals(object obj)
		{
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = obj as StrongNamePublicKeyBlob;
			if (strongNamePublicKeyBlob == null)
			{
				return false;
			}
			bool flag = this.pubkey.Length == strongNamePublicKeyBlob.pubkey.Length;
			if (flag)
			{
				for (int i = 0; i < this.pubkey.Length; i++)
				{
					if (this.pubkey[i] != strongNamePublicKeyBlob.pubkey[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		/// <summary>Returns a hash code based on the public key.</summary>
		/// <returns>The hash code based on the public key.</returns>
		// Token: 0x06002D7F RID: 11647 RVA: 0x000A3178 File Offset: 0x000A1378
		public override int GetHashCode()
		{
			int num = 0;
			int i = 0;
			int num2 = Math.Min(this.pubkey.Length, 4);
			while (i < num2)
			{
				num = (num << 8) + (int)this.pubkey[i++];
			}
			return num;
		}

		/// <summary>Creates and returns a string representation of the public key blob.</summary>
		/// <returns>A hexadecimal version of the public key blob.</returns>
		// Token: 0x06002D80 RID: 11648 RVA: 0x000A31B0 File Offset: 0x000A13B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.pubkey.Length; i++)
			{
				stringBuilder.Append(this.pubkey[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040020B7 RID: 8375
		internal byte[] pubkey;
	}
}

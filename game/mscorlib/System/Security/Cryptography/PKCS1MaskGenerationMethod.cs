using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Computes masks according to PKCS #1 for use by key exchange algorithms.</summary>
	// Token: 0x0200049F RID: 1183
	[ComVisible(true)]
	public class PKCS1MaskGenerationMethod : MaskGenerationMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PKCS1MaskGenerationMethod" /> class.</summary>
		// Token: 0x06002F5C RID: 12124 RVA: 0x000A8C33 File Offset: 0x000A6E33
		public PKCS1MaskGenerationMethod()
		{
			this.HashNameValue = "SHA1";
		}

		/// <summary>Gets or sets the name of the hash algorithm type to use for generating the mask.</summary>
		/// <returns>The name of the type that implements the hash algorithm to use for computing the mask.</returns>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x000A8C46 File Offset: 0x000A6E46
		// (set) Token: 0x06002F5E RID: 12126 RVA: 0x000A8C4E File Offset: 0x000A6E4E
		public string HashName
		{
			get
			{
				return this.HashNameValue;
			}
			set
			{
				this.HashNameValue = value;
				if (this.HashNameValue == null)
				{
					this.HashNameValue = "SHA1";
				}
			}
		}

		/// <summary>Generates and returns a mask from the specified random seed of the specified length.</summary>
		/// <param name="rgbSeed">The random seed to use for computing the mask.</param>
		/// <param name="cbReturn">The length of the generated mask in bytes.</param>
		/// <returns>A randomly generated mask whose length is equal to the <paramref name="cbReturn" /> parameter.</returns>
		// Token: 0x06002F5F RID: 12127 RVA: 0x000A8C6A File Offset: 0x000A6E6A
		public override byte[] GenerateMask(byte[] rgbSeed, int cbReturn)
		{
			return PKCS1.MGF1(HashAlgorithm.Create(this.HashNameValue), rgbSeed, cbReturn);
		}

		// Token: 0x04002191 RID: 8593
		private string HashNameValue;
	}
}

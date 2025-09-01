using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo" /> class represents information associated with a public key.</summary>
	// Token: 0x0200007F RID: 127
	public sealed class PublicKeyInfo
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00012CC2 File Offset: 0x00010EC2
		internal PublicKeyInfo(AlgorithmIdentifier algorithm, byte[] keyValue)
		{
			this.Algorithm = algorithm;
			this.KeyValue = keyValue;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.Algorithm" /> property retrieves the algorithm identifier associated with the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that represents the algorithm.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00012CD8 File Offset: 0x00010ED8
		public AlgorithmIdentifier Algorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<Algorithm>k__BackingField;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.KeyValue" /> property retrieves the value of the encoded public component of the public key pair.</summary>
		/// <returns>An array of byte values  that represents the encoded public component of the public key pair.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00012CE0 File Offset: 0x00010EE0
		public byte[] KeyValue
		{
			[CompilerGenerated]
			get
			{
				return this.<KeyValue>k__BackingField;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal PublicKeyInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000297 RID: 663
		[CompilerGenerated]
		private readonly AlgorithmIdentifier <Algorithm>k__BackingField;

		// Token: 0x04000298 RID: 664
		[CompilerGenerated]
		private readonly byte[] <KeyValue>k__BackingField;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class defines the algorithm used for a cryptographic operation.</summary>
	// Token: 0x02000068 RID: 104
	public sealed class AlgorithmIdentifier
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class by using a set of default parameters.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000375 RID: 885 RVA: 0x00010DE6 File Offset: 0x0000EFE6
		public AlgorithmIdentifier() : this(Oid.FromOidValue("1.2.840.113549.3.7", OidGroup.EncryptionAlgorithm), 0)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class with the specified algorithm identifier.</summary>
		/// <param name="oid">An object identifier for the algorithm.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000376 RID: 886 RVA: 0x00010DFA File Offset: 0x0000EFFA
		public AlgorithmIdentifier(Oid oid) : this(oid, 0)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid,System.Int32)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class with the specified algorithm identifier and key length.</summary>
		/// <param name="oid">An object identifier for the algorithm.</param>
		/// <param name="keyLength">The length, in bits, of the key.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000377 RID: 887 RVA: 0x00010E04 File Offset: 0x0000F004
		public AlgorithmIdentifier(Oid oid, int keyLength)
		{
			this.Oid = oid;
			this.KeyLength = keyLength;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Oid" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid" /> object that specifies the object identifier for the algorithm.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the algorithm.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00010E25 File Offset: 0x0000F025
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00010E2D File Offset: 0x0000F02D
		public Oid Oid
		{
			[CompilerGenerated]
			get
			{
				return this.<Oid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Oid>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.KeyLength" /> property sets or retrieves the key length, in bits. This property is not used for algorithms that use a fixed key length.</summary>
		/// <returns>An int value that represents the key length, in bits.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00010E36 File Offset: 0x0000F036
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00010E3E File Offset: 0x0000F03E
		public int KeyLength
		{
			[CompilerGenerated]
			get
			{
				return this.<KeyLength>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<KeyLength>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Parameters" /> property sets or retrieves any parameters required by the algorithm.</summary>
		/// <returns>An array of byte values that specifies any parameters required by the algorithm.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00010E47 File Offset: 0x0000F047
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00010E4F File Offset: 0x0000F04F
		public byte[] Parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<Parameters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Parameters>k__BackingField = value;
			}
		} = Array.Empty<byte>();

		// Token: 0x04000261 RID: 609
		[CompilerGenerated]
		private Oid <Oid>k__BackingField;

		// Token: 0x04000262 RID: 610
		[CompilerGenerated]
		private int <KeyLength>k__BackingField;

		// Token: 0x04000263 RID: 611
		[CompilerGenerated]
		private byte[] <Parameters>k__BackingField;
	}
}

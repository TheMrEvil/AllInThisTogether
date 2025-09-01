using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Specifies an Elliptic Curve Diffie-Hellman (ECDH) public key for use with the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class.</summary>
	// Token: 0x0200036F RID: 879
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class ECDiffieHellmanCngPublicKey : ECDiffieHellmanPublicKey
	{
		// Token: 0x06001AC9 RID: 6857 RVA: 0x0000235B File Offset: 0x0000055B
		internal ECDiffieHellmanCngPublicKey()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the key BLOB format for a <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object.</summary>
		/// <returns>The format that the key BLOB is expressed in.</returns>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0005A05A File Offset: 0x0005825A
		public CngKeyBlobFormat BlobFormat
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Converts a byte array that contains a public key to a <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object according to the specified format.</summary>
		/// <param name="publicKeyBlob">A byte array that contains an Elliptic Curve Diffie-Hellman (ECDH) public key.</param>
		/// <param name="format">An object that specifies the format of the key BLOB.</param>
		/// <returns>An object that contains the ECDH public key that is serialized in the byte array.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="publicKeyBlob" /> or <paramref name="format" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="publicKeyBlob" /> parameter does not contain an <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> key. </exception>
		// Token: 0x06001ACB RID: 6859 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecuritySafeCritical]
		public static ECDiffieHellmanPublicKey FromByteArray(byte[] publicKeyBlob, CngKeyBlobFormat format)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Converts an XML string to an <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object.</summary>
		/// <param name="xml">An XML string that contains an Elliptic Curve Diffie-Hellman (ECDH) key.</param>
		/// <returns>An object that contains the ECDH public key that is specified by the given XML.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xml" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="xml" /> parameter does not specify an <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> key.</exception>
		// Token: 0x06001ACC RID: 6860 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecuritySafeCritical]
		public static ECDiffieHellmanCngPublicKey FromXmlString(string xml)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Converts the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object to a <see cref="T:System.Security.Cryptography.CngKey" /> object.</summary>
		/// <returns>An object that contains the key represented by the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object.</returns>
		// Token: 0x06001ACD RID: 6861 RVA: 0x0005A05A File Offset: 0x0005825A
		public CngKey Import()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}

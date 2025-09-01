using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Provides an abstract base class from which all <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> implementations must inherit.</summary>
	// Token: 0x02000049 RID: 73
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public abstract class ECDiffieHellmanPublicKey : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> class.</summary>
		// Token: 0x06000175 RID: 373 RVA: 0x00003A15 File Offset: 0x00001C15
		protected ECDiffieHellmanPublicKey()
		{
			this.m_keyBlob = new byte[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> class.</summary>
		/// <param name="keyBlob">A byte array that represents an <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="keyBlob" /> is <see langword="null" />.</exception>
		// Token: 0x06000176 RID: 374 RVA: 0x00003A29 File Offset: 0x00001C29
		protected ECDiffieHellmanPublicKey(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			this.m_keyBlob = (keyBlob.Clone() as byte[]);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> class.</summary>
		// Token: 0x06000177 RID: 375 RVA: 0x00003A50 File Offset: 0x00001C50
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000178 RID: 376 RVA: 0x00003A59 File Offset: 0x00001C59
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Serializes the <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> key BLOB to a byte array.</summary>
		/// <returns>A byte array that contains the serialized Elliptic Curve Diffie-Hellman (ECDH) public key.</returns>
		// Token: 0x06000179 RID: 377 RVA: 0x00003A5B File Offset: 0x00001C5B
		public virtual byte[] ToByteArray()
		{
			return this.m_keyBlob.Clone() as byte[];
		}

		/// <summary>Serializes the <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> public key to an XML string.</summary>
		/// <returns>An XML string that contains the serialized Elliptic Curve Diffie-Hellman (ECDH) public key.</returns>
		// Token: 0x0600017A RID: 378 RVA: 0x00003A6D File Offset: 0x00001C6D
		public virtual string ToXmlString()
		{
			throw new NotImplementedException(SR.GetString("Method not supported. Derived class must override."));
		}

		/// <summary>When overridden in a derived class, exports the named or explicit <see cref="T:System.Security.Cryptography.ECParameters" /> for an <see cref="T:System.Security.Cryptography.ECCurve" /> object.  </summary>
		/// <returns>An object that represents the point on the curve for this key.</returns>
		/// <exception cref="T:System.NotSupportedException">A derived class must override this method.</exception>
		// Token: 0x0600017B RID: 379 RVA: 0x000039F5 File Offset: 0x00001BF5
		public virtual ECParameters ExportParameters()
		{
			throw new NotSupportedException(SR.GetString("Method not supported. Derived class must override."));
		}

		/// <summary>When overridden in a derived class, exports the explicit <see cref="T:System.Security.Cryptography.ECParameters" /> for an <see cref="T:System.Security.Cryptography.ECCurve" /> object.  </summary>
		/// <returns>An object that represents the point on the curve for this key, using the explicit curve format. </returns>
		/// <exception cref="T:System.NotSupportedException">A derived class must override this method.</exception>
		// Token: 0x0600017C RID: 380 RVA: 0x000039F5 File Offset: 0x00001BF5
		public virtual ECParameters ExportExplicitParameters()
		{
			throw new NotSupportedException(SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x04000329 RID: 809
		private byte[] m_keyBlob;
	}
}

using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Specifies a key BLOB format for use with Microsoft Cryptography Next Generation (CNG) objects. </summary>
	// Token: 0x02000042 RID: 66
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngKeyBlobFormat : IEquatable<CngKeyBlobFormat>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> class by using the specified format.</summary>
		/// <param name="format">The key BLOB format to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="format" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="format" /> parameter length is 0 (zero).</exception>
		// Token: 0x06000121 RID: 289 RVA: 0x00003318 File Offset: 0x00001518
		public CngKeyBlobFormat(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The key blob format '{0}' is invalid.", new object[]
				{
					format
				}), "format");
			}
			this.m_format = format;
		}

		/// <summary>Gets the name of the key BLOB format that the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object specifies.</summary>
		/// <returns>The embedded key BLOB format name.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003367 File Offset: 0x00001567
		public string Format
		{
			get
			{
				return this.m_format;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> objects specify the same key BLOB format.</summary>
		/// <param name="left">An object that specifies a key BLOB format.</param>
		/// <param name="right">A second object, to be compared to the object identified by the <paramref name="left" /> parameter.</param>
		/// <returns>
		///     <see langword="true" /> if the two objects specify the same key BLOB format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000123 RID: 291 RVA: 0x0000336F File Offset: 0x0000156F
		public static bool operator ==(CngKeyBlobFormat left, CngKeyBlobFormat right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> objects do not specify the same key BLOB format.</summary>
		/// <param name="left">An object that specifies a key BLOB format.</param>
		/// <param name="right">A second object, to be compared to the object identified by the <paramref name="left" /> parameter.</param>
		/// <returns>
		///     <see langword="true" /> if the two objects do not specify the same key BLOB format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000124 RID: 292 RVA: 0x00003380 File Offset: 0x00001580
		public static bool operator !=(CngKeyBlobFormat left, CngKeyBlobFormat right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</summary>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies the same key BLOB format as the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000125 RID: 293 RVA: 0x00003394 File Offset: 0x00001594
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngKeyBlobFormat);
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object to the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</summary>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <paramref name="other" /> parameter specifies the same key BLOB format as the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000126 RID: 294 RVA: 0x000033A2 File Offset: 0x000015A2
		public bool Equals(CngKeyBlobFormat other)
		{
			return other != null && this.m_format.Equals(other.Format);
		}

		/// <summary>Generates a hash value for the embedded key BLOB format in the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</summary>
		/// <returns>The hash value of the embedded key BLOB format. </returns>
		// Token: 0x06000127 RID: 295 RVA: 0x000033BA File Offset: 0x000015BA
		public override int GetHashCode()
		{
			return this.m_format.GetHashCode();
		}

		/// <summary>Gets the name of the key BLOB format that the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object specifies.</summary>
		/// <returns>The embedded key BLOB format name.</returns>
		// Token: 0x06000128 RID: 296 RVA: 0x00003367 File Offset: 0x00001567
		public override string ToString()
		{
			return this.m_format;
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a private key BLOB for an elliptic curve cryptography (ECC) key.</summary>
		/// <returns>An object that specifies an ECC private key BLOB.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000033C7 File Offset: 0x000015C7
		public static CngKeyBlobFormat EccPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccPrivate == null)
				{
					CngKeyBlobFormat.s_eccPrivate = new CngKeyBlobFormat("ECCPRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_eccPrivate;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a public key BLOB for an elliptic curve cryptography (ECC) key.</summary>
		/// <returns>An object that specifies an ECC public key BLOB.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000033F0 File Offset: 0x000015F0
		public static CngKeyBlobFormat EccPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccPublic == null)
				{
					CngKeyBlobFormat.s_eccPublic = new CngKeyBlobFormat("ECCPUBLICBLOB");
				}
				return CngKeyBlobFormat.s_eccPublic;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a private key BLOB for an elliptic curve cryptography (ECC) key which contains explicit curve parameters.</summary>
		/// <returns>An object describing a private key BLOB.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00003419 File Offset: 0x00001619
		public static CngKeyBlobFormat EccFullPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccFullPrivate == null)
				{
					CngKeyBlobFormat.s_eccFullPrivate = new CngKeyBlobFormat("ECCFULLPRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_eccFullPrivate;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a public key BLOB for an elliptic curve cryptography (ECC) key which contains explicit curve parameters.</summary>
		/// <returns>An object describing a public key BLOB.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003442 File Offset: 0x00001642
		public static CngKeyBlobFormat EccFullPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccFullPublic == null)
				{
					CngKeyBlobFormat.s_eccFullPublic = new CngKeyBlobFormat("ECCFULLPUBLICBLOB");
				}
				return CngKeyBlobFormat.s_eccFullPublic;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a generic private key BLOB.</summary>
		/// <returns>An object that specifies a generic private key BLOB.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000346B File Offset: 0x0000166B
		public static CngKeyBlobFormat GenericPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_genericPrivate == null)
				{
					CngKeyBlobFormat.s_genericPrivate = new CngKeyBlobFormat("PRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_genericPrivate;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a generic public key BLOB.</summary>
		/// <returns>An object that specifies a generic public key BLOB.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003494 File Offset: 0x00001694
		public static CngKeyBlobFormat GenericPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_genericPublic == null)
				{
					CngKeyBlobFormat.s_genericPublic = new CngKeyBlobFormat("PUBLICBLOB");
				}
				return CngKeyBlobFormat.s_genericPublic;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies an opaque transport key BLOB.</summary>
		/// <returns>An object that specifies an opaque transport key BLOB.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000034BD File Offset: 0x000016BD
		public static CngKeyBlobFormat OpaqueTransportBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_opaqueTransport == null)
				{
					CngKeyBlobFormat.s_opaqueTransport = new CngKeyBlobFormat("OpaqueTransport");
				}
				return CngKeyBlobFormat.s_opaqueTransport;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a Private Key Information Syntax Standard (PKCS #8) key BLOB.</summary>
		/// <returns>An object that specifies a PKCS #8 private key BLOB.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000034E6 File Offset: 0x000016E6
		public static CngKeyBlobFormat Pkcs8PrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_pkcs8Private == null)
				{
					CngKeyBlobFormat.s_pkcs8Private = new CngKeyBlobFormat("PKCS8_PRIVATEKEY");
				}
				return CngKeyBlobFormat.s_pkcs8Private;
			}
		}

		// Token: 0x0400030D RID: 781
		private static volatile CngKeyBlobFormat s_eccPrivate;

		// Token: 0x0400030E RID: 782
		private static volatile CngKeyBlobFormat s_eccPublic;

		// Token: 0x0400030F RID: 783
		private static volatile CngKeyBlobFormat s_eccFullPrivate;

		// Token: 0x04000310 RID: 784
		private static volatile CngKeyBlobFormat s_eccFullPublic;

		// Token: 0x04000311 RID: 785
		private static volatile CngKeyBlobFormat s_genericPrivate;

		// Token: 0x04000312 RID: 786
		private static volatile CngKeyBlobFormat s_genericPublic;

		// Token: 0x04000313 RID: 787
		private static volatile CngKeyBlobFormat s_opaqueTransport;

		// Token: 0x04000314 RID: 788
		private static volatile CngKeyBlobFormat s_pkcs8Private;

		// Token: 0x04000315 RID: 789
		private string m_format;
	}
}

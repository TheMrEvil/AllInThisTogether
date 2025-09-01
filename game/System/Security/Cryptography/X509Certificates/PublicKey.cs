using System;
using Mono.Security;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a certificate's public key information. This class cannot be inherited.</summary>
	// Token: 0x020002CD RID: 717
	public sealed class PublicKey
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" /> class using an object identifier (OID) object of the public key, an ASN.1-encoded representation of the public key parameters, and an ASN.1-encoded representation of the public key value.</summary>
		/// <param name="oid">An object identifier (OID) object that represents the public key.</param>
		/// <param name="parameters">An ASN.1-encoded representation of the public key parameters.</param>
		/// <param name="keyValue">An ASN.1-encoded representation of the public key value.</param>
		// Token: 0x06001610 RID: 5648 RVA: 0x0005841C File Offset: 0x0005661C
		public PublicKey(Oid oid, AsnEncodedData parameters, AsnEncodedData keyValue)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			this._oid = new Oid(oid);
			this._params = new AsnEncodedData(parameters);
			this._keyValue = new AsnEncodedData(keyValue);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00058480 File Offset: 0x00056680
		internal PublicKey(X509Certificate certificate)
		{
			bool flag = true;
			if (certificate.KeyAlgorithm == "1.2.840.113549.1.1.1")
			{
				RSACryptoServiceProvider rsacryptoServiceProvider = certificate.RSA as RSACryptoServiceProvider;
				if (rsacryptoServiceProvider != null && rsacryptoServiceProvider.PublicOnly)
				{
					this._key = certificate.RSA;
					flag = false;
				}
				else
				{
					RSAManaged rsamanaged = certificate.RSA as RSAManaged;
					if (rsamanaged != null && rsamanaged.PublicOnly)
					{
						this._key = certificate.RSA;
						flag = false;
					}
				}
				if (flag)
				{
					RSAParameters parameters = certificate.RSA.ExportParameters(false);
					this._key = RSA.Create();
					(this._key as RSA).ImportParameters(parameters);
				}
			}
			else
			{
				DSACryptoServiceProvider dsacryptoServiceProvider = certificate.DSA as DSACryptoServiceProvider;
				if (dsacryptoServiceProvider != null && dsacryptoServiceProvider.PublicOnly)
				{
					this._key = certificate.DSA;
					flag = false;
				}
				if (flag)
				{
					DSAParameters parameters2 = certificate.DSA.ExportParameters(false);
					this._key = DSA.Create();
					(this._key as DSA).ImportParameters(parameters2);
				}
			}
			this._oid = new Oid(certificate.KeyAlgorithm);
			this._keyValue = new AsnEncodedData(this._oid, certificate.PublicKey);
			this._params = new AsnEncodedData(this._oid, certificate.KeyAlgorithmParameters ?? PublicKey.Empty);
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key value.</summary>
		/// <returns>The ASN.1-encoded representation of the public key value.</returns>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x000585C2 File Offset: 0x000567C2
		public AsnEncodedData EncodedKeyValue
		{
			get
			{
				return this._keyValue;
			}
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key parameters.</summary>
		/// <returns>The ASN.1-encoded representation of the public key parameters.</returns>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x000585CA File Offset: 0x000567CA
		public AsnEncodedData EncodedParameters
		{
			get
			{
				return this._params;
			}
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.RSA" /> derived object or a <see cref="T:System.Security.Cryptography.DSA" /> derived object representing the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object representing the public key.</returns>
		/// <exception cref="T:System.NotSupportedException">The key algorithm is not supported.</exception>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x000585D4 File Offset: 0x000567D4
		public AsymmetricAlgorithm Key
		{
			get
			{
				string value = this._oid.Value;
				if (value == "1.2.840.113549.1.1.1")
				{
					return PublicKey.DecodeRSA(this._keyValue.RawData);
				}
				if (!(value == "1.2.840.10040.4.1"))
				{
					throw new NotSupportedException(Locale.GetText("Cannot decode public key from unknown OID '{0}'.", new object[]
					{
						this._oid.Value
					}));
				}
				return PublicKey.DecodeDSA(this._keyValue.RawData, this._params.RawData);
			}
		}

		/// <summary>Gets an object identifier (OID) object of the public key.</summary>
		/// <returns>An object identifier (OID) object of the public key.</returns>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00058659 File Offset: 0x00056859
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00058664 File Offset: 0x00056864
		private static byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] != 0)
			{
				return integer;
			}
			int num = integer.Length - 1;
			byte[] array = new byte[num];
			Buffer.BlockCopy(integer, 1, array, 0, num);
			return array;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00058690 File Offset: 0x00056890
		internal static DSA DecodeDSA(byte[] rawPublicKey, byte[] rawParameters)
		{
			DSAParameters dsaparameters = default(DSAParameters);
			try
			{
				ASN1 asn = new ASN1(rawPublicKey);
				if (asn.Tag != 2)
				{
					throw new CryptographicException(Locale.GetText("Missing DSA Y integer."));
				}
				dsaparameters.Y = PublicKey.GetUnsignedBigInteger(asn.Value);
				ASN1 asn2 = new ASN1(rawParameters);
				if (asn2 == null || asn2.Tag != 48 || asn2.Count < 3)
				{
					throw new CryptographicException(Locale.GetText("Missing DSA parameters."));
				}
				if (asn2[0].Tag != 2 || asn2[1].Tag != 2 || asn2[2].Tag != 2)
				{
					throw new CryptographicException(Locale.GetText("Invalid DSA parameters."));
				}
				dsaparameters.P = PublicKey.GetUnsignedBigInteger(asn2[0].Value);
				dsaparameters.Q = PublicKey.GetUnsignedBigInteger(asn2[1].Value);
				dsaparameters.G = PublicKey.GetUnsignedBigInteger(asn2[2].Value);
			}
			catch (Exception inner)
			{
				throw new CryptographicException(Locale.GetText("Error decoding the ASN.1 structure."), inner);
			}
			DSACryptoServiceProvider dsacryptoServiceProvider = new DSACryptoServiceProvider(dsaparameters.Y.Length << 3);
			dsacryptoServiceProvider.ImportParameters(dsaparameters);
			return dsacryptoServiceProvider;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000587C4 File Offset: 0x000569C4
		internal static RSA DecodeRSA(byte[] rawPublicKey)
		{
			RSAParameters rsaparameters = default(RSAParameters);
			try
			{
				ASN1 asn = new ASN1(rawPublicKey);
				if (asn.Count == 0)
				{
					throw new CryptographicException(Locale.GetText("Missing RSA modulus and exponent."));
				}
				ASN1 asn2 = asn[0];
				if (asn2 == null || asn2.Tag != 2)
				{
					throw new CryptographicException(Locale.GetText("Missing RSA modulus."));
				}
				ASN1 asn3 = asn[1];
				if (asn3.Tag != 2)
				{
					throw new CryptographicException(Locale.GetText("Missing RSA public exponent."));
				}
				rsaparameters.Modulus = PublicKey.GetUnsignedBigInteger(asn2.Value);
				rsaparameters.Exponent = asn3.Value;
			}
			catch (Exception inner)
			{
				throw new CryptographicException(Locale.GetText("Error decoding the ASN.1 structure."), inner);
			}
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(rsaparameters.Modulus.Length << 3);
			rsacryptoServiceProvider.ImportParameters(rsaparameters);
			return rsacryptoServiceProvider;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00058894 File Offset: 0x00056A94
		// Note: this type is marked as 'beforefieldinit'.
		static PublicKey()
		{
		}

		// Token: 0x04000CDF RID: 3295
		private const string rsaOid = "1.2.840.113549.1.1.1";

		// Token: 0x04000CE0 RID: 3296
		private const string dsaOid = "1.2.840.10040.4.1";

		// Token: 0x04000CE1 RID: 3297
		private AsymmetricAlgorithm _key;

		// Token: 0x04000CE2 RID: 3298
		private AsnEncodedData _keyValue;

		// Token: 0x04000CE3 RID: 3299
		private AsnEncodedData _params;

		// Token: 0x04000CE4 RID: 3300
		private Oid _oid;

		// Token: 0x04000CE5 RID: 3301
		private static byte[] Empty = new byte[0];
	}
}

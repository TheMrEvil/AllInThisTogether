using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.Asn1;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.X509Certificates;
using Internal.Cryptography;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200006C RID: 108
	internal abstract class CmsSignature
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00011128 File Offset: 0x0000F328
		static CmsSignature()
		{
			CmsSignature.PrepareRegistrationRsa(CmsSignature.s_lookup);
			CmsSignature.PrepareRegistrationDsa(CmsSignature.s_lookup);
			CmsSignature.PrepareRegistrationECDsa(CmsSignature.s_lookup);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011154 File Offset: 0x0000F354
		private static void PrepareRegistrationRsa(Dictionary<string, CmsSignature> lookup)
		{
			lookup.Add("1.2.840.113549.1.1.1", new CmsSignature.RSAPkcs1CmsSignature(null, null));
			lookup.Add("1.2.840.113549.1.1.5", new CmsSignature.RSAPkcs1CmsSignature("1.2.840.113549.1.1.5", new HashAlgorithmName?(HashAlgorithmName.SHA1)));
			lookup.Add("1.2.840.113549.1.1.11", new CmsSignature.RSAPkcs1CmsSignature("1.2.840.113549.1.1.11", new HashAlgorithmName?(HashAlgorithmName.SHA256)));
			lookup.Add("1.2.840.113549.1.1.12", new CmsSignature.RSAPkcs1CmsSignature("1.2.840.113549.1.1.12", new HashAlgorithmName?(HashAlgorithmName.SHA384)));
			lookup.Add("1.2.840.113549.1.1.13", new CmsSignature.RSAPkcs1CmsSignature("1.2.840.113549.1.1.13", new HashAlgorithmName?(HashAlgorithmName.SHA512)));
			lookup.Add("1.2.840.113549.1.1.10", new CmsSignature.RSAPssCmsSignature());
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011208 File Offset: 0x0000F408
		private static void PrepareRegistrationDsa(Dictionary<string, CmsSignature> lookup)
		{
			lookup.Add("1.2.840.10040.4.3", new CmsSignature.DSACmsSignature("1.2.840.10040.4.3", HashAlgorithmName.SHA1));
			lookup.Add("2.16.840.1.101.3.4.3.2", new CmsSignature.DSACmsSignature("2.16.840.1.101.3.4.3.2", HashAlgorithmName.SHA256));
			lookup.Add("2.16.840.1.101.3.4.3.3", new CmsSignature.DSACmsSignature("2.16.840.1.101.3.4.3.3", HashAlgorithmName.SHA384));
			lookup.Add("2.16.840.1.101.3.4.3.4", new CmsSignature.DSACmsSignature("2.16.840.1.101.3.4.3.4", HashAlgorithmName.SHA512));
			lookup.Add("1.2.840.10040.4.1", new CmsSignature.DSACmsSignature(null, default(HashAlgorithmName)));
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011298 File Offset: 0x0000F498
		private static void PrepareRegistrationECDsa(Dictionary<string, CmsSignature> lookup)
		{
			lookup.Add("1.2.840.10045.4.1", new CmsSignature.ECDsaCmsSignature("1.2.840.10045.4.1", HashAlgorithmName.SHA1));
			lookup.Add("1.2.840.10045.4.3.2", new CmsSignature.ECDsaCmsSignature("1.2.840.10045.4.3.2", HashAlgorithmName.SHA256));
			lookup.Add("1.2.840.10045.4.3.3", new CmsSignature.ECDsaCmsSignature("1.2.840.10045.4.3.3", HashAlgorithmName.SHA384));
			lookup.Add("1.2.840.10045.4.3.4", new CmsSignature.ECDsaCmsSignature("1.2.840.10045.4.3.4", HashAlgorithmName.SHA512));
			lookup.Add("1.2.840.10045.2.1", new CmsSignature.ECDsaCmsSignature(null, default(HashAlgorithmName)));
		}

		// Token: 0x06000399 RID: 921
		internal abstract bool VerifySignature(byte[] valueHash, byte[] signature, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, ReadOnlyMemory<byte>? signatureParameters, X509Certificate2 certificate);

		// Token: 0x0600039A RID: 922
		protected abstract bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue);

		// Token: 0x0600039B RID: 923 RVA: 0x00011328 File Offset: 0x0000F528
		internal static CmsSignature Resolve(string signatureAlgorithmOid)
		{
			CmsSignature result;
			if (CmsSignature.s_lookup.TryGetValue(signatureAlgorithmOid, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011348 File Offset: 0x0000F548
		internal static bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid oid, out ReadOnlyMemory<byte> signatureValue)
		{
			CmsSignature cmsSignature = CmsSignature.Resolve(certificate.GetKeyAlgorithm());
			if (cmsSignature == null)
			{
				oid = null;
				signatureValue = default(ReadOnlyMemory<byte>);
				return false;
			}
			byte[] array;
			bool result = cmsSignature.Sign(dataHash, hashAlgorithmName, certificate, silent, out oid, out array);
			signatureValue = array;
			return result;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00011390 File Offset: 0x0000F590
		private unsafe static bool DsaDerToIeee(ReadOnlyMemory<byte> derSignature, Span<byte> ieeeSignature)
		{
			int num = ieeeSignature.Length / 2;
			bool result;
			try
			{
				AsnReader asnReader = new AsnReader(derSignature, AsnEncodingRules.DER);
				AsnReader asnReader2 = asnReader.ReadSequence();
				if (asnReader.HasData)
				{
					result = false;
				}
				else
				{
					ieeeSignature.Clear();
					ReadOnlySpan<byte> readOnlySpan = asnReader2.GetIntegerBytes().Span;
					if (readOnlySpan.Length > num && *readOnlySpan[0] == 0)
					{
						readOnlySpan = readOnlySpan.Slice(1);
					}
					if (readOnlySpan.Length <= num)
					{
						readOnlySpan.CopyTo(ieeeSignature.Slice(num - readOnlySpan.Length, readOnlySpan.Length));
					}
					readOnlySpan = asnReader2.GetIntegerBytes().Span;
					if (readOnlySpan.Length > num && *readOnlySpan[0] == 0)
					{
						readOnlySpan = readOnlySpan.Slice(1);
					}
					if (readOnlySpan.Length <= num)
					{
						readOnlySpan.CopyTo(ieeeSignature.Slice(num + num - readOnlySpan.Length, readOnlySpan.Length));
					}
					result = !asnReader2.HasData;
				}
			}
			catch (CryptographicException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001149C File Offset: 0x0000F69C
		private static byte[] DsaIeeeToDer(ReadOnlySpan<byte> ieeeSignature)
		{
			int num = ieeeSignature.Length / 2;
			byte[] result;
			using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
			{
				asnWriter.PushSequence();
				byte[] array = new byte[num + 1];
				Span<byte> destination = new Span<byte>(array, 1, num);
				ieeeSignature.Slice(0, num).CopyTo(destination);
				Array.Reverse<byte>(array);
				BigInteger value = new BigInteger(array);
				asnWriter.WriteInteger(value);
				array[0] = 0;
				ieeeSignature.Slice(num, num).CopyTo(destination);
				Array.Reverse<byte>(array);
				value = new BigInteger(array);
				asnWriter.WriteInteger(value);
				asnWriter.PopSequence();
				result = asnWriter.Encode();
			}
			return result;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00002145 File Offset: 0x00000345
		protected CmsSignature()
		{
		}

		// Token: 0x04000269 RID: 617
		private static readonly Dictionary<string, CmsSignature> s_lookup = new Dictionary<string, CmsSignature>();

		// Token: 0x0200006D RID: 109
		private class DSACmsSignature : CmsSignature
		{
			// Token: 0x060003A0 RID: 928 RVA: 0x00011558 File Offset: 0x0000F758
			internal DSACmsSignature(string signatureAlgorithm, HashAlgorithmName expectedDigest)
			{
				this._signatureAlgorithm = signatureAlgorithm;
				this._expectedDigest = expectedDigest;
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x00011570 File Offset: 0x0000F770
			internal override bool VerifySignature(byte[] valueHash, byte[] signature, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, ReadOnlyMemory<byte>? signatureParameters, X509Certificate2 certificate)
			{
				if (this._expectedDigest != digestAlgorithmName)
				{
					throw new CryptographicException(SR.Format("SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.", digestAlgorithmOid, this._signatureAlgorithm));
				}
				DSA dsapublicKey = certificate.GetDSAPublicKey();
				if (dsapublicKey == null)
				{
					return false;
				}
				DSAParameters dsaparameters = dsapublicKey.ExportParameters(false);
				byte[] array = new byte[2 * dsaparameters.Q.Length];
				return CmsSignature.DsaDerToIeee(signature, array) && dsapublicKey.VerifySignature(valueHash, array);
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x000115E8 File Offset: 0x0000F7E8
			protected override bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue)
			{
				DSA dsa = PkcsPal.Instance.GetPrivateKeyForSigning<DSA>(certificate, silent) ?? certificate.GetDSAPublicKey();
				if (dsa == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				string text = (hashAlgorithmName == HashAlgorithmName.SHA1) ? "1.2.840.10040.4.3" : ((hashAlgorithmName == HashAlgorithmName.SHA256) ? "2.16.840.1.101.3.4.3.2" : ((hashAlgorithmName == HashAlgorithmName.SHA384) ? "2.16.840.1.101.3.4.3.3" : ((hashAlgorithmName == HashAlgorithmName.SHA512) ? "2.16.840.1.101.3.4.3.4" : null)));
				if (text == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				signatureAlgorithm = new Oid(text, text);
				byte[] array = dsa.CreateSignature(dataHash);
				signatureValue = CmsSignature.DsaIeeeToDer(new ReadOnlySpan<byte>(array));
				return true;
			}

			// Token: 0x0400026A RID: 618
			private readonly HashAlgorithmName _expectedDigest;

			// Token: 0x0400026B RID: 619
			private readonly string _signatureAlgorithm;
		}

		// Token: 0x0200006E RID: 110
		private class ECDsaCmsSignature : CmsSignature
		{
			// Token: 0x060003A3 RID: 931 RVA: 0x0001169A File Offset: 0x0000F89A
			internal ECDsaCmsSignature(string signatureAlgorithm, HashAlgorithmName expectedDigest)
			{
				this._signatureAlgorithm = signatureAlgorithm;
				this._expectedDigest = expectedDigest;
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x000116B0 File Offset: 0x0000F8B0
			internal override bool VerifySignature(byte[] valueHash, byte[] signature, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, ReadOnlyMemory<byte>? signatureParameters, X509Certificate2 certificate)
			{
				if (this._expectedDigest != digestAlgorithmName)
				{
					throw new CryptographicException(SR.Format("SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.", digestAlgorithmOid, this._signatureAlgorithm));
				}
				ECDsa ecdsaPublicKey = certificate.GetECDsaPublicKey();
				if (ecdsaPublicKey == null)
				{
					return false;
				}
				byte[] array = new byte[ecdsaPublicKey.KeySize / 4];
				return CmsSignature.DsaDerToIeee(signature, array) && ecdsaPublicKey.VerifyHash(valueHash, array);
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x0001171C File Offset: 0x0000F91C
			protected override bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue)
			{
				ECDsa ecdsa = PkcsPal.Instance.GetPrivateKeyForSigning<ECDsa>(certificate, silent) ?? certificate.GetECDsaPublicKey();
				if (ecdsa == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				string text = (hashAlgorithmName == HashAlgorithmName.SHA1) ? "1.2.840.10045.4.1" : ((hashAlgorithmName == HashAlgorithmName.SHA256) ? "1.2.840.10045.4.3.2" : ((hashAlgorithmName == HashAlgorithmName.SHA384) ? "1.2.840.10045.4.3.3" : ((hashAlgorithmName == HashAlgorithmName.SHA512) ? "1.2.840.10045.4.3.4" : null)));
				if (text == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				signatureAlgorithm = new Oid(text, text);
				signatureValue = CmsSignature.DsaIeeeToDer(ecdsa.SignHash(dataHash));
				return true;
			}

			// Token: 0x0400026C RID: 620
			private readonly HashAlgorithmName _expectedDigest;

			// Token: 0x0400026D RID: 621
			private readonly string _signatureAlgorithm;
		}

		// Token: 0x0200006F RID: 111
		private abstract class RSACmsSignature : CmsSignature
		{
			// Token: 0x060003A6 RID: 934 RVA: 0x000117CC File Offset: 0x0000F9CC
			protected RSACmsSignature(string signatureAlgorithm, HashAlgorithmName? expectedDigest)
			{
				this._signatureAlgorithm = signatureAlgorithm;
				this._expectedDigest = expectedDigest;
			}

			// Token: 0x060003A7 RID: 935 RVA: 0x000117E4 File Offset: 0x0000F9E4
			internal override bool VerifySignature(byte[] valueHash, byte[] signature, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, ReadOnlyMemory<byte>? signatureParameters, X509Certificate2 certificate)
			{
				if (this._expectedDigest != null && this._expectedDigest.Value != digestAlgorithmName)
				{
					throw new CryptographicException(SR.Format("SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.", digestAlgorithmOid, this._signatureAlgorithm));
				}
				RSASignaturePadding signaturePadding = this.GetSignaturePadding(signatureParameters, digestAlgorithmOid, digestAlgorithmName, valueHash.Length);
				RSA rsapublicKey = certificate.GetRSAPublicKey();
				return rsapublicKey != null && rsapublicKey.VerifyHash(valueHash, signature, digestAlgorithmName, signaturePadding);
			}

			// Token: 0x060003A8 RID: 936
			protected abstract RSASignaturePadding GetSignaturePadding(ReadOnlyMemory<byte>? signatureParameters, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, int digestValueLength);

			// Token: 0x0400026E RID: 622
			private readonly string _signatureAlgorithm;

			// Token: 0x0400026F RID: 623
			private readonly HashAlgorithmName? _expectedDigest;
		}

		// Token: 0x02000070 RID: 112
		private sealed class RSAPkcs1CmsSignature : CmsSignature.RSACmsSignature
		{
			// Token: 0x060003A9 RID: 937 RVA: 0x00011850 File Offset: 0x0000FA50
			public RSAPkcs1CmsSignature(string signatureAlgorithm, HashAlgorithmName? expectedDigest) : base(signatureAlgorithm, expectedDigest)
			{
			}

			// Token: 0x060003AA RID: 938 RVA: 0x0001185C File Offset: 0x0000FA5C
			protected unsafe override RSASignaturePadding GetSignaturePadding(ReadOnlyMemory<byte>? signatureParameters, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, int digestValueLength)
			{
				if (signatureParameters == null)
				{
					return RSASignaturePadding.Pkcs1;
				}
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)2], 2);
				*span[0] = 5;
				*span[1] = 0;
				if (span.SequenceEqual(signatureParameters.Value.Span))
				{
					return RSASignaturePadding.Pkcs1;
				}
				throw new CryptographicException("Invalid signature paramters.");
			}

			// Token: 0x060003AB RID: 939 RVA: 0x000118C0 File Offset: 0x0000FAC0
			protected override bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue)
			{
				RSA rsa = PkcsPal.Instance.GetPrivateKeyForSigning<RSA>(certificate, silent) ?? certificate.GetRSAPublicKey();
				if (rsa == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				signatureAlgorithm = new Oid("1.2.840.113549.1.1.1", "1.2.840.113549.1.1.1");
				signatureValue = rsa.SignHash(dataHash, hashAlgorithmName, RSASignaturePadding.Pkcs1);
				return true;
			}
		}

		// Token: 0x02000071 RID: 113
		private class RSAPssCmsSignature : CmsSignature.RSACmsSignature
		{
			// Token: 0x060003AC RID: 940 RVA: 0x00011918 File Offset: 0x0000FB18
			public RSAPssCmsSignature() : base(null, null)
			{
			}

			// Token: 0x060003AD RID: 941 RVA: 0x00011938 File Offset: 0x0000FB38
			protected override RSASignaturePadding GetSignaturePadding(ReadOnlyMemory<byte>? signatureParameters, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, int digestValueLength)
			{
				if (signatureParameters == null)
				{
					throw new CryptographicException("PSS parameters were not present.");
				}
				PssParamsAsn pssParamsAsn = AsnSerializer.Deserialize<PssParamsAsn>(signatureParameters.Value, AsnEncodingRules.DER);
				if (pssParamsAsn.HashAlgorithm.Algorithm.Value != digestAlgorithmOid)
				{
					throw new CryptographicException(SR.Format("This platform requires that the PSS hash algorithm ({0}) match the data digest algorithm ({1}).", pssParamsAsn.HashAlgorithm.Algorithm.Value, digestAlgorithmOid));
				}
				if (pssParamsAsn.TrailerField != 1)
				{
					throw new CryptographicException("Invalid signature paramters.");
				}
				if (pssParamsAsn.SaltLength != digestValueLength)
				{
					throw new CryptographicException(SR.Format("PSS salt size {0} is not supported by this platform with hash algorithm {1}.", pssParamsAsn.SaltLength, digestAlgorithmName.Name));
				}
				if (pssParamsAsn.MaskGenAlgorithm.Algorithm.Value != "1.2.840.113549.1.1.8")
				{
					throw new CryptographicException("Mask generation function '{0}' is not supported by this platform.", pssParamsAsn.MaskGenAlgorithm.Algorithm.Value);
				}
				if (pssParamsAsn.MaskGenAlgorithm.Parameters == null)
				{
					throw new CryptographicException("Invalid signature paramters.");
				}
				AlgorithmIdentifierAsn algorithmIdentifierAsn = AsnSerializer.Deserialize<AlgorithmIdentifierAsn>(pssParamsAsn.MaskGenAlgorithm.Parameters.Value, AsnEncodingRules.DER);
				if (algorithmIdentifierAsn.Algorithm.Value != digestAlgorithmOid)
				{
					throw new CryptographicException(SR.Format("This platform does not support the MGF hash algorithm ({0}) being different from the signature hash algorithm ({1}).", algorithmIdentifierAsn.Algorithm.Value, digestAlgorithmOid));
				}
				return RSASignaturePadding.Pss;
			}

			// Token: 0x060003AE RID: 942 RVA: 0x00011A81 File Offset: 0x0000FC81
			protected override bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue)
			{
				throw new CryptographicException();
			}
		}
	}
}

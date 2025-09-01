using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Asn1;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Internal.Cryptography
{
	// Token: 0x02000109 RID: 265
	internal static class Helpers
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x0001C21E File Offset: 0x0001A41E
		internal static void AppendData(this IncrementalHash hasher, ReadOnlySpan<byte> data)
		{
			hasher.AppendData(data.ToArray());
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001C22D File Offset: 0x0001A42D
		internal static HashAlgorithmName GetDigestAlgorithm(Oid oid)
		{
			return Helpers.GetDigestAlgorithm(oid.Value);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001C23C File Offset: 0x0001A43C
		internal static HashAlgorithmName GetDigestAlgorithm(string oidValue)
		{
			if (oidValue == "1.2.840.113549.2.5")
			{
				return HashAlgorithmName.MD5;
			}
			if (oidValue == "1.3.14.3.2.26")
			{
				return HashAlgorithmName.SHA1;
			}
			if (oidValue == "2.16.840.1.101.3.4.2.1")
			{
				return HashAlgorithmName.SHA256;
			}
			if (oidValue == "2.16.840.1.101.3.4.2.2")
			{
				return HashAlgorithmName.SHA384;
			}
			if (!(oidValue == "2.16.840.1.101.3.4.2.3"))
			{
				throw new CryptographicException("'{0}' is not a known hash algorithm.", oidValue);
			}
			return HashAlgorithmName.SHA512;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		internal static string GetOidFromHashAlgorithm(HashAlgorithmName algName)
		{
			if (algName == HashAlgorithmName.MD5)
			{
				return "1.2.840.113549.2.5";
			}
			if (algName == HashAlgorithmName.SHA1)
			{
				return "1.3.14.3.2.26";
			}
			if (algName == HashAlgorithmName.SHA256)
			{
				return "2.16.840.1.101.3.4.2.1";
			}
			if (algName == HashAlgorithmName.SHA384)
			{
				return "2.16.840.1.101.3.4.2.2";
			}
			if (algName == HashAlgorithmName.SHA512)
			{
				return "2.16.840.1.101.3.4.2.3";
			}
			throw new CryptographicException("Unknown algorithm '{0}'.", algName.Name);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001C335 File Offset: 0x0001A535
		public static byte[] Resize(this byte[] a, int size)
		{
			Array.Resize<byte>(ref a, size);
			return a;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001C340 File Offset: 0x0001A540
		public static void RemoveAt<T>(ref T[] arr, int idx)
		{
			if (arr.Length == 1)
			{
				arr = Array.Empty<T>();
				return;
			}
			T[] array = new T[arr.Length - 1];
			if (idx != 0)
			{
				Array.Copy(arr, 0, array, 0, idx);
			}
			if (idx < array.Length)
			{
				Array.Copy(arr, idx + 1, array, idx, array.Length - idx);
			}
			arr = array;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001C390 File Offset: 0x0001A590
		public static T[] NormalizeSet<T>(T[] setItems, Action<byte[]> encodedValueProcessor = null)
		{
			byte[] array = AsnSerializer.Serialize<Helpers.AsnSet<T>>(new Helpers.AsnSet<T>
			{
				SetData = setItems
			}, AsnEncodingRules.DER).Encode();
			ref Helpers.AsnSet<T> ptr = AsnSerializer.Deserialize<Helpers.AsnSet<T>>(array, AsnEncodingRules.DER);
			if (encodedValueProcessor != null)
			{
				encodedValueProcessor(array);
			}
			return ptr.SetData;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		internal static byte[] EncodeContentInfo<T>(T value, string contentType, AsnEncodingRules ruleSet = AsnEncodingRules.DER)
		{
			byte[] result;
			using (AsnWriter asnWriter = AsnSerializer.Serialize<T>(value, ruleSet))
			{
				using (AsnWriter asnWriter2 = AsnSerializer.Serialize<ContentInfoAsn>(new ContentInfoAsn
				{
					ContentType = contentType,
					Content = asnWriter.Encode()
				}, ruleSet))
				{
					result = asnWriter2.Encode();
				}
			}
			return result;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001C454 File Offset: 0x0001A654
		public static CmsRecipientCollection DeepCopy(this CmsRecipientCollection recipients)
		{
			CmsRecipientCollection cmsRecipientCollection = new CmsRecipientCollection();
			foreach (CmsRecipient cmsRecipient in recipients)
			{
				X509Certificate2 certificate = cmsRecipient.Certificate;
				X509Certificate2 certificate2 = new X509Certificate2(certificate.Handle);
				CmsRecipient recipient = new CmsRecipient(cmsRecipient.RecipientIdentifierType, certificate2);
				cmsRecipientCollection.Add(recipient);
				GC.KeepAlive(certificate);
			}
			return cmsRecipientCollection;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		public static byte[] UnicodeToOctetString(this string s)
		{
			byte[] array = new byte[2 * (s.Length + 1)];
			Encoding.Unicode.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001C4E3 File Offset: 0x0001A6E3
		public static string OctetStringToUnicode(this byte[] octets)
		{
			if (octets.Length < 2)
			{
				return string.Empty;
			}
			return Encoding.Unicode.GetString(octets, 0, octets.Length - 2);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001C504 File Offset: 0x0001A704
		public static X509Certificate2Collection GetStoreCertificates(StoreName storeName, StoreLocation storeLocation, bool openExistingOnly)
		{
			X509Certificate2Collection certificates;
			using (X509Store x509Store = new X509Store(storeName, storeLocation))
			{
				OpenFlags openFlags = OpenFlags.IncludeArchived;
				if (openExistingOnly)
				{
					openFlags |= OpenFlags.OpenExistingOnly;
				}
				x509Store.Open(openFlags);
				certificates = x509Store.Certificates;
			}
			return certificates;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001C550 File Offset: 0x0001A750
		public static X509Certificate2 TryFindMatchingCertificate(this X509Certificate2Collection certs, SubjectIdentifier recipientIdentifier)
		{
			SubjectIdentifierType type = recipientIdentifier.Type;
			if (type != SubjectIdentifierType.IssuerAndSerialNumber)
			{
				if (type != SubjectIdentifierType.SubjectKeyIdentifier)
				{
					throw new CryptographicException();
				}
				byte[] ba = ((string)recipientIdentifier.Value).ToSkiBytes();
				foreach (X509Certificate2 x509Certificate in certs)
				{
					byte[] subjectKeyIdentifier = PkcsPal.Instance.GetSubjectKeyIdentifier(x509Certificate);
					if (Helpers.AreByteArraysEqual(ba, subjectKeyIdentifier))
					{
						return x509Certificate;
					}
				}
			}
			else
			{
				X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)recipientIdentifier.Value;
				byte[] ba2 = x509IssuerSerial.SerialNumber.ToSerialBytes();
				string issuerName = x509IssuerSerial.IssuerName;
				foreach (X509Certificate2 x509Certificate2 in certs)
				{
					if (Helpers.AreByteArraysEqual(x509Certificate2.GetSerialNumber(), ba2) && x509Certificate2.Issuer == issuerName)
					{
						return x509Certificate2;
					}
				}
			}
			return null;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001C624 File Offset: 0x0001A824
		private static bool AreByteArraysEqual(byte[] ba1, byte[] ba2)
		{
			if (ba1.Length != ba2.Length)
			{
				return false;
			}
			for (int i = 0; i < ba1.Length; i++)
			{
				if (ba1[i] != ba2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001C654 File Offset: 0x0001A854
		private static byte[] ToSkiBytes(this string skiString)
		{
			return skiString.UpperHexStringToByteArray();
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001C65C File Offset: 0x0001A85C
		public static string ToSkiString(this byte[] skiBytes)
		{
			return Helpers.ToUpperHexString(skiBytes);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001C669 File Offset: 0x0001A869
		public static string ToBigEndianHex(this ReadOnlySpan<byte> bytes)
		{
			return Helpers.ToUpperHexString(bytes);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001C671 File Offset: 0x0001A871
		private static byte[] ToSerialBytes(this string serialString)
		{
			byte[] array = serialString.UpperHexStringToByteArray();
			Array.Reverse<byte>(array);
			return array;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001C67F File Offset: 0x0001A87F
		public static string ToSerialString(this byte[] serialBytes)
		{
			serialBytes = serialBytes.CloneByteArray();
			Array.Reverse<byte>(serialBytes);
			return Helpers.ToUpperHexString(serialBytes);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001C69C File Offset: 0x0001A89C
		private static string ToUpperHexString(ReadOnlySpan<byte> ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			for (int i = 0; i < ba.Length; i++)
			{
				stringBuilder.Append(ba[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001C6EC File Offset: 0x0001A8EC
		private static byte[] UpperHexStringToByteArray(this string normalizedString)
		{
			byte[] array = new byte[normalizedString.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				char c = normalizedString[i * 2];
				byte b = (byte)(Helpers.UpperHexCharToNybble(c) << 4);
				c = normalizedString[i * 2 + 1];
				b |= Helpers.UpperHexCharToNybble(c);
				array[i] = b;
			}
			return array;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001C744 File Offset: 0x0001A944
		private static byte UpperHexCharToNybble(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (byte)(c - '0');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (byte)(c - 'A' + '\n');
			}
			throw new CryptographicException();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001C770 File Offset: 0x0001A970
		public static Pkcs9AttributeObject CreateBestPkcs9AttributeObjectAvailable(Oid oid, byte[] encodedAttribute)
		{
			Pkcs9AttributeObject pkcs9AttributeObject = new Pkcs9AttributeObject(oid, encodedAttribute);
			string value = oid.Value;
			if (!(value == "1.3.6.1.4.1.311.88.2.1"))
			{
				if (!(value == "1.3.6.1.4.1.311.88.2.2"))
				{
					if (!(value == "1.2.840.113549.1.9.5"))
					{
						if (!(value == "1.2.840.113549.1.9.3"))
						{
							if (value == "1.2.840.113549.1.9.4")
							{
								pkcs9AttributeObject = Helpers.Upgrade<Pkcs9MessageDigest>(pkcs9AttributeObject);
							}
						}
						else
						{
							pkcs9AttributeObject = Helpers.Upgrade<Pkcs9ContentType>(pkcs9AttributeObject);
						}
					}
					else
					{
						pkcs9AttributeObject = Helpers.Upgrade<Pkcs9SigningTime>(pkcs9AttributeObject);
					}
				}
				else
				{
					pkcs9AttributeObject = Helpers.Upgrade<Pkcs9DocumentDescription>(pkcs9AttributeObject);
				}
			}
			else
			{
				pkcs9AttributeObject = Helpers.Upgrade<Pkcs9DocumentName>(pkcs9AttributeObject);
			}
			return pkcs9AttributeObject;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001C7FB File Offset: 0x0001A9FB
		private static T Upgrade<T>(Pkcs9AttributeObject basicAttribute) where T : Pkcs9AttributeObject, new()
		{
			T t = Activator.CreateInstance<T>();
			t.CopyFrom(basicAttribute);
			return t;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001C810 File Offset: 0x0001AA10
		public static byte[] GetSubjectKeyIdentifier(this X509Certificate2 certificate)
		{
			X509Extension x509Extension = certificate.Extensions["2.5.29.14"];
			if (x509Extension == null)
			{
				byte[] result;
				using (HashAlgorithm hashAlgorithm = SHA1.Create())
				{
					result = hashAlgorithm.ComputeHash(Helpers.GetSubjectPublicKeyInfo(certificate).ToArray());
				}
				return result;
			}
			ReadOnlyMemory<byte> readOnlyMemory;
			if (new AsnReader(x509Extension.RawData, AsnEncodingRules.DER).TryGetPrimitiveOctetStringBytes(out readOnlyMemory))
			{
				return readOnlyMemory.ToArray();
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001C898 File Offset: 0x0001AA98
		internal static byte[] OneShot(this ICryptoTransform transform, byte[] data)
		{
			return transform.OneShot(data, 0, data.Length);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		internal static byte[] OneShot(this ICryptoTransform transform, byte[] data, int offset, int length)
		{
			if (transform.CanTransformMultipleBlocks)
			{
				return transform.TransformFinalBlock(data, offset, length);
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
				{
					cryptoStream.Write(data, offset, length);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001C91C File Offset: 0x0001AB1C
		private static ReadOnlyMemory<byte> GetSubjectPublicKeyInfo(X509Certificate2 certificate)
		{
			return AsnSerializer.Deserialize<Helpers.Certificate>(certificate.RawData, AsnEncodingRules.DER).TbsCertificate.SubjectPublicKeyInfo;
		}

		// Token: 0x0200010A RID: 266
		private struct Certificate
		{
			// Token: 0x0400041D RID: 1053
			internal Helpers.TbsCertificateLite TbsCertificate;

			// Token: 0x0400041E RID: 1054
			internal AlgorithmIdentifierAsn AlgorithmIdentifier;

			// Token: 0x0400041F RID: 1055
			[BitString]
			internal ReadOnlyMemory<byte> SignatureValue;
		}

		// Token: 0x0200010B RID: 267
		private struct TbsCertificateLite
		{
			// Token: 0x04000420 RID: 1056
			[ExpectedTag(0, ExplicitTag = true)]
			[DefaultValue(new byte[]
			{
				160,
				3,
				2,
				1,
				0
			})]
			internal int Version;

			// Token: 0x04000421 RID: 1057
			[Integer]
			internal ReadOnlyMemory<byte> SerialNumber;

			// Token: 0x04000422 RID: 1058
			internal AlgorithmIdentifierAsn AlgorithmIdentifier;

			// Token: 0x04000423 RID: 1059
			[AnyValue]
			[ExpectedTag(TagClass.Universal, 16)]
			internal ReadOnlyMemory<byte> Issuer;

			// Token: 0x04000424 RID: 1060
			[AnyValue]
			[ExpectedTag(TagClass.Universal, 16)]
			internal ReadOnlyMemory<byte> Validity;

			// Token: 0x04000425 RID: 1061
			[AnyValue]
			[ExpectedTag(TagClass.Universal, 16)]
			internal ReadOnlyMemory<byte> Subject;

			// Token: 0x04000426 RID: 1062
			[AnyValue]
			[ExpectedTag(TagClass.Universal, 16)]
			internal ReadOnlyMemory<byte> SubjectPublicKeyInfo;

			// Token: 0x04000427 RID: 1063
			[BitString]
			[ExpectedTag(1)]
			[OptionalValue]
			internal ReadOnlyMemory<byte>? IssuerUniqueId;

			// Token: 0x04000428 RID: 1064
			[OptionalValue]
			[BitString]
			[ExpectedTag(2)]
			internal ReadOnlyMemory<byte>? SubjectUniqueId;

			// Token: 0x04000429 RID: 1065
			[ExpectedTag(3)]
			[AnyValue]
			[OptionalValue]
			internal ReadOnlyMemory<byte>? Extensions;
		}

		// Token: 0x0200010C RID: 268
		internal struct AsnSet<T>
		{
			// Token: 0x0400042A RID: 1066
			[SetOf]
			public T[] SetData;
		}
	}
}

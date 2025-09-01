using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Asn1;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace Internal.Cryptography.Pal.AnyOS
{
	// Token: 0x02000113 RID: 275
	internal static class AsnHelpers
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0001C954 File Offset: 0x0001AB54
		internal static SubjectIdentifierOrKey ToSubjectIdentifierOrKey(this OriginatorIdentifierOrKeyAsn originator)
		{
			if (originator.IssuerAndSerialNumber != null)
			{
				IssuerAndSerialNumberAsn value = originator.IssuerAndSerialNumber.Value;
				X500DistinguishedName x500DistinguishedName = new X500DistinguishedName(value.Issuer.ToArray());
				SubjectIdentifierOrKeyType type = SubjectIdentifierOrKeyType.IssuerAndSerialNumber;
				string name = x500DistinguishedName.Name;
				value = originator.IssuerAndSerialNumber.Value;
				return new SubjectIdentifierOrKey(type, new X509IssuerSerial(name, value.SerialNumber.Span.ToBigEndianHex()));
			}
			if (originator.SubjectKeyIdentifier != null)
			{
				return new SubjectIdentifierOrKey(SubjectIdentifierOrKeyType.SubjectKeyIdentifier, originator.SubjectKeyIdentifier.Value.Span.ToBigEndianHex());
			}
			if (originator.OriginatorKey != null)
			{
				OriginatorPublicKeyAsn originatorKey = originator.OriginatorKey;
				return new SubjectIdentifierOrKey(SubjectIdentifierOrKeyType.PublicKeyInfo, new PublicKeyInfo(originatorKey.Algorithm.ToPresentationObject(), originatorKey.PublicKey.ToArray()));
			}
			return new SubjectIdentifierOrKey(SubjectIdentifierOrKeyType.Unknown, string.Empty);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001CA30 File Offset: 0x0001AC30
		internal unsafe static AlgorithmIdentifier ToPresentationObject(this AlgorithmIdentifierAsn asn)
		{
			string value = asn.Algorithm.Value;
			int keyLength;
			if (!(value == "1.2.840.113549.3.2"))
			{
				if (!(value == "1.2.840.113549.3.4"))
				{
					if (!(value == "1.3.14.3.2.7"))
					{
						if (!(value == "1.2.840.113549.3.7"))
						{
							keyLength = 0;
						}
						else
						{
							keyLength = 192;
						}
					}
					else
					{
						keyLength = 64;
					}
				}
				else if (asn.Parameters == null)
				{
					keyLength = 0;
				}
				else
				{
					int num = 0;
					AsnReader asnReader = new AsnReader(asn.Parameters.Value, AsnEncodingRules.BER);
					if (asnReader.PeekTag() != Asn1Tag.Null)
					{
						ReadOnlyMemory<byte> readOnlyMemory;
						if (asnReader.TryGetPrimitiveOctetStringBytes(out readOnlyMemory))
						{
							num = readOnlyMemory.Length;
						}
						else
						{
							Span<byte> destination = new Span<byte>(stackalloc byte[(UIntPtr)16], 16);
							if (!asnReader.TryCopyOctetStringBytes(destination, out num))
							{
								throw new CryptographicException();
							}
						}
					}
					keyLength = 128 - 8 * num;
				}
			}
			else if (asn.Parameters == null)
			{
				keyLength = 0;
			}
			else
			{
				int effectiveKeyBits = AsnSerializer.Deserialize<Rc2CbcParameters>(asn.Parameters.Value, AsnEncodingRules.BER).GetEffectiveKeyBits();
				if (effectiveKeyBits <= 56)
				{
					if (effectiveKeyBits != 40 && effectiveKeyBits != 56)
					{
						goto IL_A3;
					}
				}
				else if (effectiveKeyBits != 64 && effectiveKeyBits != 128)
				{
					goto IL_A3;
				}
				keyLength = effectiveKeyBits;
				goto IL_139;
				IL_A3:
				keyLength = 0;
			}
			IL_139:
			return new AlgorithmIdentifier(new Oid(asn.Algorithm), keyLength);
		}
	}
}

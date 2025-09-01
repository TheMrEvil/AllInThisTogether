using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using Internal.Cryptography;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" />.  The subject can be identified by the certificate issuer and serial number or the subject key.</summary>
	// Token: 0x02000089 RID: 137
	public sealed class SubjectIdentifier
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00014843 File Offset: 0x00012A43
		internal SubjectIdentifier(SubjectIdentifierType type, object value)
		{
			this.Type = type;
			this.Value = value;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00014859 File Offset: 0x00012A59
		internal SubjectIdentifier(SignerIdentifierAsn signerIdentifierAsn) : this(signerIdentifierAsn.IssuerAndSerialNumber, signerIdentifierAsn.SubjectKeyIdentifier)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00014870 File Offset: 0x00012A70
		internal unsafe SubjectIdentifier(IssuerAndSerialNumberAsn? issuerAndSerialNumber, ReadOnlyMemory<byte>? subjectKeyIdentifier)
		{
			if (issuerAndSerialNumber != null)
			{
				IssuerAndSerialNumberAsn value = issuerAndSerialNumber.Value;
				ReadOnlySpan<byte> span = value.Issuer.Span;
				value = issuerAndSerialNumber.Value;
				ReadOnlySpan<byte> span2 = value.SerialNumber.Span;
				bool flag = false;
				for (int i = 0; i < span2.Length; i++)
				{
					if (*span2[i] != 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag && SubjectIdentifier.DummySignerEncodedValue.AsSpan<byte>().SequenceEqual(span))
				{
					this.Type = 3;
					this.Value = null;
					return;
				}
				this.Type = 1;
				X500DistinguishedName x500DistinguishedName = new X500DistinguishedName(span.ToArray());
				this.Value = new X509IssuerSerial(x500DistinguishedName.Name, span2.ToBigEndianHex());
				return;
			}
			else
			{
				if (subjectKeyIdentifier != null)
				{
					this.Type = 2;
					this.Value = subjectKeyIdentifier.Value.Span.ToBigEndianHex();
					return;
				}
				throw new CryptographicException();
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property retrieves the type of subject identifier. The subject can be identified by the certificate issuer and serial number or the subject key.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that identifies the type of subject.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00014969 File Offset: 0x00012B69
		public SubjectIdentifierType Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value" /> property retrieves the value of the subject identifier. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property to determine the type of subject identifier, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value" /> property to retrieve the corresponding value.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that represents the value of the subject identifier. This <see cref="T:System.Object" /> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property.  
		///  <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property  
		///
		///   Object  
		///
		///   IssuerAndSerialNumber  
		///
		///  <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> SubjectKeyIdentifier  
		///
		///  <see cref="T:System.String" /></returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00014971 File Offset: 0x00012B71
		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00014979 File Offset: 0x00012B79
		// Note: this type is marked as 'beforefieldinit'.
		static SubjectIdentifier()
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal SubjectIdentifier()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040002BF RID: 703
		private const string DummySignerSubjectName = "CN=Dummy Signer";

		// Token: 0x040002C0 RID: 704
		internal static readonly byte[] DummySignerEncodedValue = new X500DistinguishedName("CN=Dummy Signer").RawData;

		// Token: 0x040002C1 RID: 705
		[CompilerGenerated]
		private readonly SubjectIdentifierType <Type>k__BackingField;

		// Token: 0x040002C2 RID: 706
		[CompilerGenerated]
		private readonly object <Value>k__BackingField;
	}
}

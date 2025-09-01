using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>Represents an attribute used for CMS/PKCS #7 and PKCS #9 operations.</summary>
	// Token: 0x02000079 RID: 121
	public class Pkcs9AttributeObject : AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class.</summary>
		// Token: 0x060003FA RID: 1018 RVA: 0x000128BC File Offset: 0x00010ABC
		public Pkcs9AttributeObject()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified string representation of an object identifier (OID) as the attribute type and a specified ASN.1 encoded data as the attribute value.</summary>
		/// <param name="oid">The string representation of an OID that represents the PKCS #9 attribute type.</param>
		/// <param name="encodedData">An array of byte values that contains the PKCS #9 attribute value.</param>
		// Token: 0x060003FB RID: 1019 RVA: 0x000128C4 File Offset: 0x00010AC4
		public Pkcs9AttributeObject(string oid, byte[] encodedData) : this(new AsnEncodedData(oid, encodedData))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified <see cref="T:System.Security.Cryptography.Oid" /> object as the attribute type and a specified ASN.1 encoded data as the attribute value.</summary>
		/// <param name="oid">An object that represents the PKCS #9 attribute type.</param>
		/// <param name="encodedData">An array of byte values that represents the PKCS #9 attribute value.</param>
		// Token: 0x060003FC RID: 1020 RVA: 0x000128D3 File Offset: 0x00010AD3
		public Pkcs9AttributeObject(Oid oid, byte[] encodedData) : this(new AsnEncodedData(oid, encodedData))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object as its attribute type and value.</summary>
		/// <param name="asnEncodedData">An object that contains the PKCS #9 attribute type and value to use.</param>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="Value" /> member of the <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="Value" /> member of the <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		// Token: 0x060003FD RID: 1021 RVA: 0x000128E4 File Offset: 0x00010AE4
		public Pkcs9AttributeObject(AsnEncodedData asnEncodedData) : base(asnEncodedData)
		{
			if (asnEncodedData.Oid == null)
			{
				throw new ArgumentNullException("Oid");
			}
			string value = base.Oid.Value;
			if (value == null)
			{
				throw new ArgumentNullException("oid.Value");
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("String cannot be empty or null.", "oid.Value");
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001293B File Offset: 0x00010B3B
		internal Pkcs9AttributeObject(Oid oid)
		{
			base.Oid = oid;
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.Oid" /> object that represents the type of attribute associated with this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> object.</summary>
		/// <returns>An object that represents the type of attribute associated with this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> object.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001294A File Offset: 0x00010B4A
		public new Oid Oid
		{
			get
			{
				return base.Oid;
			}
		}

		/// <summary>Copies a PKCS #9 attribute type and value for this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> from the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">An object that contains the PKCS #9 attribute type and value to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asnEncodeData" /> does not represent a compatible attribute type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		// Token: 0x06000400 RID: 1024 RVA: 0x00012952 File Offset: 0x00010B52
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (!(asnEncodedData is Pkcs9AttributeObject))
			{
				throw new ArgumentException("The parameter should be a PKCS 9 attribute.");
			}
			base.CopyFrom(asnEncodedData);
		}
	}
}

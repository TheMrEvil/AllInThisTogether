using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains a type and a collection of values associated with that type.</summary>
	// Token: 0x0200000D RID: 13
	public sealed class CryptographicAttributeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> class using an attribute represented by the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">The attribute to store in this <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object.</param>
		// Token: 0x06000029 RID: 41 RVA: 0x00002628 File Offset: 0x00000828
		public CryptographicAttributeObject(Oid oid) : this(oid, new AsnEncodedDataCollection())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> class using an attribute represented by the specified <see cref="T:System.Security.Cryptography.Oid" /> object and the set of values associated with that attribute represented by the specified <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection.</summary>
		/// <param name="oid">The attribute to store in this <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object.</param>
		/// <param name="values">The set of values associated with the attribute represented by the <paramref name="oid" /> parameter.</param>
		/// <exception cref="T:System.InvalidOperationException">The collection contains duplicate items.</exception>
		// Token: 0x0600002A RID: 42 RVA: 0x00002638 File Offset: 0x00000838
		public CryptographicAttributeObject(Oid oid, AsnEncodedDataCollection values)
		{
			this._oid = new Oid(oid);
			if (values == null)
			{
				this.Values = new AsnEncodedDataCollection();
				return;
			}
			foreach (AsnEncodedData asnEncodedData in values)
			{
				if (!string.Equals(asnEncodedData.Oid.Value, oid.Value, StringComparison.Ordinal))
				{
					throw new InvalidOperationException(SR.Format("AsnEncodedData element in the collection has wrong Oid value: expected = '{0}', actual = '{1}'.", oid.Value, asnEncodedData.Oid.Value));
				}
			}
			this.Values = values;
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Oid" /> object that specifies the object identifier for the attribute.</summary>
		/// <returns>The object identifier for the attribute.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026BF File Offset: 0x000008BF
		public Oid Oid
		{
			get
			{
				return new Oid(this._oid);
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection that contains the set of values that are associated with the attribute.</summary>
		/// <returns>The set of values that is associated with the attribute.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000026CC File Offset: 0x000008CC
		public AsnEncodedDataCollection Values
		{
			[CompilerGenerated]
			get
			{
				return this.<Values>k__BackingField;
			}
		}

		// Token: 0x04000080 RID: 128
		[CompilerGenerated]
		private readonly AsnEncodedDataCollection <Values>k__BackingField;

		// Token: 0x04000081 RID: 129
		private readonly Oid _oid;
	}
}

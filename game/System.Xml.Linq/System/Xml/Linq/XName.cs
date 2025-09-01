using System;
using System.Runtime.Serialization;
using Unity;

namespace System.Xml.Linq
{
	/// <summary>Represents a name of an XML element or attribute.</summary>
	// Token: 0x0200004D RID: 77
	[Serializable]
	public sealed class XName : IEquatable<XName>, ISerializable
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000CC00 File Offset: 0x0000AE00
		internal XName(XNamespace ns, string localName)
		{
			this._ns = ns;
			this._localName = XmlConvert.VerifyNCName(localName);
			this._hashCode = (ns.GetHashCode() ^ localName.GetHashCode());
		}

		/// <summary>Gets the local (unqualified) part of the name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local (unqualified) part of the name.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000CC2E File Offset: 0x0000AE2E
		public string LocalName
		{
			get
			{
				return this._localName;
			}
		}

		/// <summary>Gets the namespace part of the fully qualified name.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> that contains the namespace part of the name.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000CC36 File Offset: 0x0000AE36
		public XNamespace Namespace
		{
			get
			{
				return this._ns;
			}
		}

		/// <summary>Returns the URI of the <see cref="T:System.Xml.Linq.XNamespace" /> for this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>The URI of the <see cref="T:System.Xml.Linq.XNamespace" /> for this <see cref="T:System.Xml.Linq.XName" />.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000CC3E File Offset: 0x0000AE3E
		public string NamespaceName
		{
			get
			{
				return this._ns.NamespaceName;
			}
		}

		/// <summary>Returns the expanded XML name in the format {namespace}localname.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the expanded XML name in the format {namespace}localname.</returns>
		// Token: 0x0600027F RID: 639 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		public override string ToString()
		{
			if (this._ns.NamespaceName.Length == 0)
			{
				return this._localName;
			}
			return "{" + this._ns.NamespaceName + "}" + this._localName;
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XName" /> object from an expanded name.</summary>
		/// <param name="expandedName">A <see cref="T:System.String" /> that contains an expanded XML name in the format {namespace}localname.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object constructed from the expanded name.</returns>
		// Token: 0x06000280 RID: 640 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public static XName Get(string expandedName)
		{
			if (expandedName == null)
			{
				throw new ArgumentNullException("expandedName");
			}
			if (expandedName.Length == 0)
			{
				throw new ArgumentException(SR.Format("'{0}' is an invalid expanded name.", expandedName));
			}
			if (expandedName[0] != '{')
			{
				return XNamespace.None.GetName(expandedName);
			}
			int num = expandedName.LastIndexOf('}');
			if (num <= 1 || num == expandedName.Length - 1)
			{
				throw new ArgumentException(SR.Format("'{0}' is an invalid expanded name.", expandedName));
			}
			return XNamespace.Get(expandedName, 1, num - 1).GetName(expandedName, num + 1, expandedName.Length - num - 1);
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XName" /> object from a local name and a namespace.</summary>
		/// <param name="localName">A local (unqualified) name.</param>
		/// <param name="namespaceName">An XML namespace.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object created from the specified local name and namespace.</returns>
		// Token: 0x06000281 RID: 641 RVA: 0x0000CD19 File Offset: 0x0000AF19
		public static XName Get(string localName, string namespaceName)
		{
			return XNamespace.Get(namespaceName).GetName(localName);
		}

		/// <summary>Converts a string formatted as an expanded XML name (that is,{namespace}localname) to an <see cref="T:System.Xml.Linq.XName" /> object.</summary>
		/// <param name="expandedName">A string that contains an expanded XML name in the format {namespace}localname.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object constructed from the expanded name.</returns>
		// Token: 0x06000282 RID: 642 RVA: 0x0000CD27 File Offset: 0x0000AF27
		[CLSCompliant(false)]
		public static implicit operator XName(string expandedName)
		{
			if (expandedName == null)
			{
				return null;
			}
			return XName.Get(expandedName);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.Linq.XName" /> is equal to this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XName" /> to compare to the current <see cref="T:System.Xml.Linq.XName" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Xml.Linq.XName" /> is equal to the current <see cref="T:System.Xml.Linq.XName" />; otherwise <see langword="false" />.</returns>
		// Token: 0x06000283 RID: 643 RVA: 0x0000CD34 File Offset: 0x0000AF34
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the hash code for the <see cref="T:System.Xml.Linq.XName" />.</returns>
		// Token: 0x06000284 RID: 644 RVA: 0x0000CD3A File Offset: 0x0000AF3A
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XName" /> are equal.</summary>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x06000285 RID: 645 RVA: 0x0000CD34 File Offset: 0x0000AF34
		public static bool operator ==(XName left, XName right)
		{
			return left == right;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XName" /> are not equal.</summary>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise <see langword="false" />.</returns>
		// Token: 0x06000286 RID: 646 RVA: 0x0000CD42 File Offset: 0x0000AF42
		public static bool operator !=(XName left, XName right)
		{
			return left != right;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000CD34 File Offset: 0x0000AF34
		bool IEquatable<XName>.Equals(XName other)
		{
			return this == other;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06000288 RID: 648 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000CD52 File Offset: 0x0000AF52
		internal XName()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000182 RID: 386
		private XNamespace _ns;

		// Token: 0x04000183 RID: 387
		private string _localName;

		// Token: 0x04000184 RID: 388
		private int _hashCode;
	}
}

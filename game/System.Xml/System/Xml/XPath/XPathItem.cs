using System;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	/// <summary>Represents an item in the XQuery 1.0 and XPath 2.0 Data Model.</summary>
	// Token: 0x02000257 RID: 599
	public abstract class XPathItem
	{
		/// <summary>When overridden in a derived class, gets a value indicating whether the item represents an XPath node or an atomic value.</summary>
		/// <returns>
		///     <see langword="true" /> if the item represents an XPath node; <see langword="false" /> if the item represents an atomic value.</returns>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060015F8 RID: 5624
		public abstract bool IsNode { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the item.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the item.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060015F9 RID: 5625
		public abstract XmlSchemaType XmlType { get; }

		/// <summary>When overridden in a derived class, gets the <see langword="string" /> value of the item.</summary>
		/// <returns>The <see langword="string" /> value of the item.</returns>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060015FA RID: 5626
		public abstract string Value { get; }

		/// <summary>When overridden in a derived class, gets the current item as a boxed object of the most appropriate .NET Framework 2.0 type according to its schema type.</summary>
		/// <returns>The current item as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060015FB RID: 5627
		public abstract object TypedValue { get; }

		/// <summary>When overridden in a derived class, gets the .NET Framework 2.0 type of the item.</summary>
		/// <returns>The .NET Framework type of the item. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060015FC RID: 5628
		public abstract Type ValueType { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Boolean" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060015FD RID: 5629
		public abstract bool ValueAsBoolean { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.DateTime" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060015FE RID: 5630
		public abstract DateTime ValueAsDateTime { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060015FF RID: 5631
		public abstract double ValueAsDouble { get; }

		/// <summary>When overridden in a derived class, gets the item's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The item's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Int32" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001600 RID: 5632
		public abstract int ValueAsInt { get; }

		/// <summary>When overridden in a derived class, gets the item's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The item's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Int64" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001601 RID: 5633
		public abstract long ValueAsLong { get; }

		/// <summary>Returns the item's value as the specified type.</summary>
		/// <param name="returnType">The type to return the item value as.</param>
		/// <returns>The value of the item as the type requested.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x06001602 RID: 5634 RVA: 0x0008561C File Offset: 0x0008381C
		public virtual object ValueAs(Type returnType)
		{
			return this.ValueAs(returnType, null);
		}

		/// <summary>When overridden in a derived class, returns the item's value as the type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <param name="returnType">The type to return the item's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <returns>The value of the item as the type requested.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x06001603 RID: 5635
		public abstract object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver);

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathItem" /> class.</summary>
		// Token: 0x06001604 RID: 5636 RVA: 0x0000216B File Offset: 0x0000036B
		protected XPathItem()
		{
		}
	}
}

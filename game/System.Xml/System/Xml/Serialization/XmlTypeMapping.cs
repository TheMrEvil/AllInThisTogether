using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Contains a mapping of one type to another.</summary>
	// Token: 0x0200030A RID: 778
	public class XmlTypeMapping : XmlMapping
	{
		// Token: 0x0600205D RID: 8285 RVA: 0x000D0C35 File Offset: 0x000CEE35
		internal XmlTypeMapping(TypeScope scope, ElementAccessor accessor) : base(scope, accessor)
		{
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x000D0C3F File Offset: 0x000CEE3F
		internal TypeMapping Mapping
		{
			get
			{
				return base.Accessor.Mapping;
			}
		}

		/// <summary>Gets the type name of the mapped object.</summary>
		/// <returns>The type name of the mapped object.</returns>
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x000D0C4C File Offset: 0x000CEE4C
		public string TypeName
		{
			get
			{
				return this.Mapping.TypeDesc.Name;
			}
		}

		/// <summary>The fully qualified type name that includes the namespace (or namespaces) and type.</summary>
		/// <returns>The fully qualified type name.</returns>
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x000D0C5E File Offset: 0x000CEE5E
		public string TypeFullName
		{
			get
			{
				return this.Mapping.TypeDesc.FullName;
			}
		}

		/// <summary>Gets the XML element name of the mapped object.</summary>
		/// <returns>The XML element name of the mapped object. The default is the class name of the object.</returns>
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x000D0C70 File Offset: 0x000CEE70
		public string XsdTypeName
		{
			get
			{
				return this.Mapping.TypeName;
			}
		}

		/// <summary>Gets the XML namespace of the mapped object.</summary>
		/// <returns>The XML namespace of the mapped object. The default is an empty string ("").</returns>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x000D0C7D File Offset: 0x000CEE7D
		public string XsdTypeNamespace
		{
			get
			{
				return this.Mapping.Namespace;
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlTypeMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}

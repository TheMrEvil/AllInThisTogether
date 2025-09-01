using System;
using System.Text;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides mappings between .NET Framework Web service methods and Web Services Description Language (WSDL) messages that are defined for SOAP Web services. </summary>
	// Token: 0x020002D8 RID: 728
	public class XmlMembersMapping : XmlMapping
	{
		// Token: 0x06001C23 RID: 7203 RVA: 0x0009E9A8 File Offset: 0x0009CBA8
		internal XmlMembersMapping(TypeScope scope, ElementAccessor accessor, XmlMappingAccess access) : base(scope, accessor, access)
		{
			MembersMapping membersMapping = (MembersMapping)accessor.Mapping;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(":");
			this.mappings = new XmlMemberMapping[membersMapping.Members.Length];
			for (int i = 0; i < this.mappings.Length; i++)
			{
				if (membersMapping.Members[i].TypeDesc.Type != null)
				{
					stringBuilder.Append(XmlMapping.GenerateKey(membersMapping.Members[i].TypeDesc.Type, null, null));
					stringBuilder.Append(":");
				}
				this.mappings[i] = new XmlMemberMapping(membersMapping.Members[i]);
			}
			base.SetKeyInternal(stringBuilder.ToString());
		}

		/// <summary>Gets the name of the .NET Framework type being mapped to the data type of an XML Schema element that represents a SOAP message.</summary>
		/// <returns>The name of the .NET Framework type.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0009EA69 File Offset: 0x0009CC69
		public string TypeName
		{
			get
			{
				return base.Accessor.Mapping.TypeName;
			}
		}

		/// <summary>Gets the namespace of the .NET Framework type being mapped to the data type of an XML Schema element that represents a SOAP message.</summary>
		/// <returns>The .NET Framework namespace of the mapping.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0009EA7B File Offset: 0x0009CC7B
		public string TypeNamespace
		{
			get
			{
				return base.Accessor.Mapping.Namespace;
			}
		}

		/// <summary>Gets an item that contains internal type mapping information for a .NET Framework code entity that belongs to a Web service method being mapped to a SOAP message.</summary>
		/// <param name="index">The index of the mapping to return.</param>
		/// <returns>The requested <see cref="T:System.Xml.Serialization.XmlMemberMapping" />.</returns>
		// Token: 0x170005A5 RID: 1445
		public XmlMemberMapping this[int index]
		{
			get
			{
				return this.mappings[index];
			}
		}

		/// <summary>Gets the number of .NET Framework code entities that belong to a Web service method to which a SOAP message is being mapped. </summary>
		/// <returns>The number of mappings in the collection.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0009EA97 File Offset: 0x0009CC97
		public int Count
		{
			get
			{
				return this.mappings.Length;
			}
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlMembersMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A01 RID: 6657
		private XmlMemberMapping[] mappings;
	}
}

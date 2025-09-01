using System;

namespace System.Xml.Serialization
{
	/// <summary>When applied to a type, stores the name of a static method of the type that returns an XML schema and a <see cref="T:System.Xml.XmlQualifiedName" /> (or <see cref="T:System.Xml.Schema.XmlSchemaType" /> for anonymous types) that controls the serialization of the type.</summary>
	// Token: 0x020002E5 RID: 741
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class XmlSchemaProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> class, taking the name of the static method that supplies the type's XML schema.</summary>
		/// <param name="methodName">The name of the static method that must be implemented.</param>
		// Token: 0x06001D0F RID: 7439 RVA: 0x000A9DA9 File Offset: 0x000A7FA9
		public XmlSchemaProviderAttribute(string methodName)
		{
			this.methodName = methodName;
		}

		/// <summary>Gets the name of the static method that supplies the type's XML schema and the name of its XML Schema data type.</summary>
		/// <returns>The name of the method that is invoked by the XML infrastructure to return an XML schema.</returns>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x000A9DB8 File Offset: 0x000A7FB8
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		/// <summary>Gets or sets a value that determines whether the target class is a wildcard, or that the schema for the class has contains only an <see langword="xs:any" /> element.</summary>
		/// <returns>
		///     <see langword="true" />, if the class is a wildcard, or if the schema contains only the <see langword="xs:any" /> element; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x000A9DC0 File Offset: 0x000A7FC0
		// (set) Token: 0x06001D12 RID: 7442 RVA: 0x000A9DC8 File Offset: 0x000A7FC8
		public bool IsAny
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x04001A36 RID: 6710
		private string methodName;

		// Token: 0x04001A37 RID: 6711
		private bool any;
	}
}

using System;

namespace System.Xml.Serialization
{
	/// <summary>Applied to a Web service client proxy, enables you to specify an assembly that contains custom-made serializers. </summary>
	// Token: 0x02000304 RID: 772
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class XmlSerializerAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class. </summary>
		// Token: 0x06002025 RID: 8229 RVA: 0x000D06E0 File Offset: 0x000CE8E0
		public XmlSerializerAssemblyAttribute() : this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class with the specified assembly name.</summary>
		/// <param name="assemblyName">The simple, unencrypted name of the assembly. </param>
		// Token: 0x06002026 RID: 8230 RVA: 0x000D06EA File Offset: 0x000CE8EA
		public XmlSerializerAssemblyAttribute(string assemblyName) : this(assemblyName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class with the specified assembly name and location of the assembly.</summary>
		/// <param name="assemblyName">The simple, unencrypted name of the assembly. </param>
		/// <param name="codeBase">A string that is the URL location of the assembly.</param>
		// Token: 0x06002027 RID: 8231 RVA: 0x000D06F4 File Offset: 0x000CE8F4
		public XmlSerializerAssemblyAttribute(string assemblyName, string codeBase)
		{
			this.assemblyName = assemblyName;
			this.codeBase = codeBase;
		}

		/// <summary>Gets or sets the location of the assembly that contains the serializers.</summary>
		/// <returns>A location, such as a path or URI, that points to the assembly.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x000D070A File Offset: 0x000CE90A
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x000D0712 File Offset: 0x000CE912
		public string CodeBase
		{
			get
			{
				return this.codeBase;
			}
			set
			{
				this.codeBase = value;
			}
		}

		/// <summary>Gets or sets the name of the assembly that contains serializers for a specific set of types.</summary>
		/// <returns>The simple, unencrypted name of the assembly. </returns>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x000D071B File Offset: 0x000CE91B
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x000D0723 File Offset: 0x000CE923
		public string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
			set
			{
				this.assemblyName = value;
			}
		}

		// Token: 0x04001B1D RID: 6941
		private string assemblyName;

		// Token: 0x04001B1E RID: 6942
		private string codeBase;
	}
}

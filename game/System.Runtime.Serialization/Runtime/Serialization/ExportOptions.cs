using System;
using System.Collections.ObjectModel;

namespace System.Runtime.Serialization
{
	/// <summary>Represents the options that can be set for an <see cref="T:System.Runtime.Serialization.XsdDataContractExporter" />.</summary>
	// Token: 0x020000D3 RID: 211
	public class ExportOptions
	{
		/// <summary>Gets or sets a serialization surrogate.</summary>
		/// <returns>An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> interface that can be used to customize how an XML schema representation is exported for a specific type.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00032BA2 File Offset: 0x00030DA2
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x00032BAA File Offset: 0x00030DAA
		public IDataContractSurrogate DataContractSurrogate
		{
			get
			{
				return this.dataContractSurrogate;
			}
			set
			{
				this.dataContractSurrogate = value;
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00032BA2 File Offset: 0x00030DA2
		internal IDataContractSurrogate GetSurrogate()
		{
			return this.dataContractSurrogate;
		}

		/// <summary>Gets the collection of types that may be encountered during serialization or deserialization.</summary>
		/// <returns>A <see langword="KnownTypes" /> collection that contains types that may be encountered during serialization or deserialization. XML schema representations are exported for all the types specified in this collection by the <see cref="T:System.Runtime.Serialization.XsdDataContractExporter" />.</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00032BB3 File Offset: 0x00030DB3
		public Collection<Type> KnownTypes
		{
			get
			{
				if (this.knownTypes == null)
				{
					this.knownTypes = new Collection<Type>();
				}
				return this.knownTypes;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.ExportOptions" /> class.</summary>
		// Token: 0x06000C35 RID: 3125 RVA: 0x0000222F File Offset: 0x0000042F
		public ExportOptions()
		{
		}

		// Token: 0x04000517 RID: 1303
		private Collection<Type> knownTypes;

		// Token: 0x04000518 RID: 1304
		private IDataContractSurrogate dataContractSurrogate;
	}
}

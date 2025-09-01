using System;

namespace System.Runtime.Serialization
{
	/// <summary>Provides a data structure to store extra data encountered by the <see cref="T:System.Runtime.Serialization.XmlObjectSerializer" /> during deserialization of a type marked with the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute.</summary>
	// Token: 0x020000E7 RID: 231
	public interface IExtensibleDataObject
	{
		/// <summary>Gets or sets the structure that contains extra data.</summary>
		/// <returns>An <see cref="T:System.Runtime.Serialization.ExtensionDataObject" /> that contains data that is not recognized as belonging to the data contract.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000D3F RID: 3391
		// (set) Token: 0x06000D40 RID: 3392
		ExtensionDataObject ExtensionData { get; set; }
	}
}

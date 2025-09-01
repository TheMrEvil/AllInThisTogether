using System;

namespace System.Data
{
	/// <summary>Determines the serialization format for a <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x020000C9 RID: 201
	public enum SerializationFormat
	{
		/// <summary>Serialize as XML content. The default.</summary>
		// Token: 0x040007F9 RID: 2041
		Xml,
		/// <summary>Serialize as binary content. Available in ADO.NET 2.0 only.</summary>
		// Token: 0x040007FA RID: 2042
		Binary
	}
}

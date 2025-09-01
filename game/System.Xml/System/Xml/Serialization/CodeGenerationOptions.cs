using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies various options to use when generating .NET Framework types for use with an XML Web Service.</summary>
	// Token: 0x02000267 RID: 615
	[Flags]
	public enum CodeGenerationOptions
	{
		/// <summary>Represents primitive types by fields and primitive types by <see cref="N:System" /> namespace types.</summary>
		// Token: 0x04001843 RID: 6211
		[XmlIgnore]
		None = 0,
		/// <summary>Represents primitive types by properties.</summary>
		// Token: 0x04001844 RID: 6212
		[XmlEnum("properties")]
		GenerateProperties = 1,
		/// <summary>Creates events for the asynchronous invocation of Web methods.</summary>
		// Token: 0x04001845 RID: 6213
		[XmlEnum("newAsync")]
		GenerateNewAsync = 2,
		/// <summary>Creates Begin and End methods for the asynchronous invocation of Web methods.</summary>
		// Token: 0x04001846 RID: 6214
		[XmlEnum("oldAsync")]
		GenerateOldAsync = 4,
		/// <summary>Generates explicitly ordered serialization code as specified through the <see langword="Order" /> property of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />, <see cref="T:System.Xml.Serialization.XmlArrayAttribute" />, and <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> attributes. </summary>
		// Token: 0x04001847 RID: 6215
		[XmlEnum("order")]
		GenerateOrder = 8,
		/// <summary>Enables data binding.</summary>
		// Token: 0x04001848 RID: 6216
		[XmlEnum("enableDataBinding")]
		EnableDataBinding = 16
	}
}

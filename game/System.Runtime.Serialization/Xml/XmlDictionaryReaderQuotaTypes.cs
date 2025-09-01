using System;

namespace System.Xml
{
	/// <summary>Enumerates the configurable quota values for XmlDictionaryReaders.</summary>
	// Token: 0x02000062 RID: 98
	[Flags]
	public enum XmlDictionaryReaderQuotaTypes
	{
		/// <summary>Specifies the maximum nested node depth.</summary>
		// Token: 0x0400027E RID: 638
		MaxDepth = 1,
		/// <summary>Specifies the maximum string length returned by the reader.</summary>
		// Token: 0x0400027F RID: 639
		MaxStringContentLength = 2,
		/// <summary>Specifies the maximum allowed array length.</summary>
		// Token: 0x04000280 RID: 640
		MaxArrayLength = 4,
		/// <summary>Specifies the maximum allowed bytes returned for each read.</summary>
		// Token: 0x04000281 RID: 641
		MaxBytesPerRead = 8,
		/// <summary>Specifies the maximum characters allowed in a table name.</summary>
		// Token: 0x04000282 RID: 642
		MaxNameTableCharCount = 16
	}
}

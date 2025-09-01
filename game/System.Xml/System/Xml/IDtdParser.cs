using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200003B RID: 59
	internal interface IDtdParser
	{
		// Token: 0x060001DF RID: 479
		IDtdInfo ParseInternalDtd(IDtdParserAdapter adapter, bool saveInternalSubset);

		// Token: 0x060001E0 RID: 480
		IDtdInfo ParseFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter);

		// Token: 0x060001E1 RID: 481
		Task<IDtdInfo> ParseInternalDtdAsync(IDtdParserAdapter adapter, bool saveInternalSubset);

		// Token: 0x060001E2 RID: 482
		Task<IDtdInfo> ParseFreeFloatingDtdAsync(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter);
	}
}

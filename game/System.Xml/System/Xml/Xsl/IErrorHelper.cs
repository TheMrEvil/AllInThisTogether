using System;

namespace System.Xml.Xsl
{
	// Token: 0x02000341 RID: 833
	internal interface IErrorHelper
	{
		// Token: 0x06002264 RID: 8804
		void ReportError(string res, params string[] args);

		// Token: 0x06002265 RID: 8805
		void ReportWarning(string res, params string[] args);
	}
}

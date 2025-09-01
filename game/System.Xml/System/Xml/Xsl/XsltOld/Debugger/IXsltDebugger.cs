using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld.Debugger
{
	// Token: 0x020003D2 RID: 978
	internal interface IXsltDebugger
	{
		// Token: 0x06002740 RID: 10048
		string GetBuiltInTemplatesUri();

		// Token: 0x06002741 RID: 10049
		void OnInstructionCompile(XPathNavigator styleSheetNavigator);

		// Token: 0x06002742 RID: 10050
		void OnInstructionExecute(IXsltProcessor xsltProcessor);
	}
}

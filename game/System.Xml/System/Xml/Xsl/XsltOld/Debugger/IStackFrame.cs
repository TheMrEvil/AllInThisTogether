using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld.Debugger
{
	// Token: 0x020003D0 RID: 976
	internal interface IStackFrame
	{
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002739 RID: 10041
		XPathNavigator Instruction { get; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600273A RID: 10042
		XPathNodeIterator NodeSet { get; }

		// Token: 0x0600273B RID: 10043
		int GetVariablesCount();

		// Token: 0x0600273C RID: 10044
		XPathNavigator GetVariable(int varIndex);

		// Token: 0x0600273D RID: 10045
		object GetVariableValue(int varIndex);
	}
}

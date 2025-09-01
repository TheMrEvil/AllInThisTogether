using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003A5 RID: 933
	internal interface RecordOutput
	{
		// Token: 0x06002628 RID: 9768
		Processor.OutputResult RecordDone(RecordBuilder record);

		// Token: 0x06002629 RID: 9769
		void TheEnd();
	}
}

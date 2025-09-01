using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000041 RID: 65
	internal interface IValidationEventHandling
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600020E RID: 526
		object EventHandler { get; }

		// Token: 0x0600020F RID: 527
		void SendEvent(Exception exception, XmlSeverityType severity);
	}
}

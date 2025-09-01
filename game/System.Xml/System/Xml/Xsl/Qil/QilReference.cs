using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004CB RID: 1227
	internal class QilReference : QilNode
	{
		// Token: 0x06003205 RID: 12805 RVA: 0x001227C1 File Offset: 0x001209C1
		public QilReference(QilNodeType nodeType) : base(nodeType)
		{
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x001227CA File Offset: 0x001209CA
		// (set) Token: 0x06003207 RID: 12807 RVA: 0x001227D2 File Offset: 0x001209D2
		public string DebugName
		{
			get
			{
				return this._debugName;
			}
			set
			{
				if (value.Length > 1000)
				{
					value = value.Substring(0, 1000);
				}
				this._debugName = value;
			}
		}

		// Token: 0x04002649 RID: 9801
		private const int MaxDebugNameLength = 1000;

		// Token: 0x0400264A RID: 9802
		private string _debugName;
	}
}

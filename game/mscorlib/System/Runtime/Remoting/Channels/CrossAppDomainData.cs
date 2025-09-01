using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005AC RID: 1452
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x06003850 RID: 14416 RVA: 0x000CA29A File Offset: 0x000C849A
		internal CrossAppDomainData(int domainId)
		{
			this._ContextID = 0;
			this._DomainID = domainId;
			this._processGuid = RemotingConfiguration.ProcessId;
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000CA2C0 File Offset: 0x000C84C0
		internal int DomainID
		{
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000CA2C8 File Offset: 0x000C84C8
		internal string ProcessID
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x040025DD RID: 9693
		private object _ContextID;

		// Token: 0x040025DE RID: 9694
		private int _DomainID;

		// Token: 0x040025DF RID: 9695
		private string _processGuid;
	}
}

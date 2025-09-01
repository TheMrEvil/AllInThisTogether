using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000607 RID: 1543
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000CCEA7 File Offset: 0x000CB0A7
		// (set) Token: 0x06003A74 RID: 14964 RVA: 0x000CCEAF File Offset: 0x000CB0AF
		internal string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000CCEB8 File Offset: 0x000CB0B8
		internal bool HasInfo
		{
			get
			{
				return this._logicalCallID != null;
			}
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000CCEC3 File Offset: 0x000CB0C3
		public object Clone()
		{
			return new CallContextRemotingData
			{
				LogicalCallID = this.LogicalCallID
			};
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x0000259F File Offset: 0x0000079F
		public CallContextRemotingData()
		{
		}

		// Token: 0x04002658 RID: 9816
		private string _logicalCallID;
	}
}

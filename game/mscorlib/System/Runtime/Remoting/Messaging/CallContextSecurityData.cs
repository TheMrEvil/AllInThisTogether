using System;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000606 RID: 1542
	[Serializable]
	internal class CallContextSecurityData : ICloneable
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000CCE78 File Offset: 0x000CB078
		// (set) Token: 0x06003A6F RID: 14959 RVA: 0x000CCE80 File Offset: 0x000CB080
		internal IPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x000CCE89 File Offset: 0x000CB089
		internal bool HasInfo
		{
			get
			{
				return this._principal != null;
			}
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000CCE94 File Offset: 0x000CB094
		public object Clone()
		{
			return new CallContextSecurityData
			{
				_principal = this._principal
			};
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x0000259F File Offset: 0x0000079F
		public CallContextSecurityData()
		{
		}

		// Token: 0x04002657 RID: 9815
		private IPrincipal _principal;
	}
}

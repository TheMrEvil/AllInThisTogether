using System;

namespace Mono
{
	// Token: 0x02000049 RID: 73
	internal struct RuntimeRemoteClassHandle
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00004767 File Offset: 0x00002967
		internal unsafe RuntimeRemoteClassHandle(RuntimeStructs.RemoteClass* value)
		{
			this.value = value;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004770 File Offset: 0x00002970
		internal unsafe RuntimeClassHandle ProxyClass
		{
			get
			{
				return new RuntimeClassHandle(this.value->proxy_class);
			}
		}

		// Token: 0x04000DE0 RID: 3552
		private unsafe RuntimeStructs.RemoteClass* value;
	}
}

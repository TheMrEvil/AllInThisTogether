using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E8 RID: 488
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ResourceRequest : AsyncOperation
	{
		// Token: 0x0600161C RID: 5660 RVA: 0x00023790 File Offset: 0x00021990
		protected virtual Object GetResult()
		{
			return Resources.Load(this.m_Path, this.m_Type);
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x000237B4 File Offset: 0x000219B4
		public Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000237CC File Offset: 0x000219CC
		public ResourceRequest()
		{
		}

		// Token: 0x040007C9 RID: 1993
		internal string m_Path;

		// Token: 0x040007CA RID: 1994
		internal Type m_Type;
	}
}

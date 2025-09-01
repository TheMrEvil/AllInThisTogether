using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x020002C5 RID: 709
	internal sealed class SystemThreading_ThreadLocalDebugView<T>
	{
		// Token: 0x06001ECC RID: 7884 RVA: 0x000727B4 File Offset: 0x000709B4
		public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
		{
			this.m_tlocal = tlocal;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x000727C3 File Offset: 0x000709C3
		public bool IsValueCreated
		{
			get
			{
				return this.m_tlocal.IsValueCreated;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000727D0 File Offset: 0x000709D0
		public T Value
		{
			get
			{
				return this.m_tlocal.ValueForDebugDisplay;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000727DD File Offset: 0x000709DD
		public List<T> Values
		{
			get
			{
				return this.m_tlocal.ValuesForDebugDisplay;
			}
		}

		// Token: 0x04001AED RID: 6893
		private readonly ThreadLocal<T> m_tlocal;
	}
}

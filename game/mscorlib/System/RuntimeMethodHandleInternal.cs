using System;

namespace System
{
	// Token: 0x02000220 RID: 544
	internal struct RuntimeMethodHandleInternal
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0005D80C File Offset: 0x0005BA0C
		internal static RuntimeMethodHandleInternal EmptyHandle
		{
			get
			{
				return default(RuntimeMethodHandleInternal);
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0005D822 File Offset: 0x0005BA22
		internal bool IsNullHandle()
		{
			return this.m_handle.IsNull();
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0005D82F File Offset: 0x0005BA2F
		internal IntPtr Value
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0005D837 File Offset: 0x0005BA37
		internal RuntimeMethodHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x040016B0 RID: 5808
		internal IntPtr m_handle;
	}
}

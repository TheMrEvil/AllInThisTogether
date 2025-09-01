using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200004A RID: 74
	[DebuggerDisplay("Value = {Value}; Count = {Count}")]
	[UsedByNativeCode]
	public struct ProfilerRecorderSample
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002A40 File Offset: 0x00000C40
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002A48 File Offset: 0x00000C48
		public long Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0400012B RID: 299
		private long value;

		// Token: 0x0400012C RID: 300
		private long count;

		// Token: 0x0400012D RID: 301
		private long refValue;
	}
}

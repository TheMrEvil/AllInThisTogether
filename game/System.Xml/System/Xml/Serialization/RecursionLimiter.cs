using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002DE RID: 734
	internal class RecursionLimiter
	{
		// Token: 0x06001C7D RID: 7293 RVA: 0x000A3A19 File Offset: 0x000A1C19
		internal RecursionLimiter()
		{
			this.depth = 0;
			this.maxDepth = (DiagnosticsSwitches.NonRecursiveTypeLoading.Enabled ? 1 : int.MaxValue);
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x000A3A42 File Offset: 0x000A1C42
		internal bool IsExceededLimit
		{
			get
			{
				return this.depth > this.maxDepth;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x000A3A52 File Offset: 0x000A1C52
		// (set) Token: 0x06001C80 RID: 7296 RVA: 0x000A3A5A File Offset: 0x000A1C5A
		internal int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x000A3A63 File Offset: 0x000A1C63
		internal WorkItems DeferredWorkItems
		{
			get
			{
				if (this.deferredWorkItems == null)
				{
					this.deferredWorkItems = new WorkItems();
				}
				return this.deferredWorkItems;
			}
		}

		// Token: 0x04001A1A RID: 6682
		private int maxDepth;

		// Token: 0x04001A1B RID: 6683
		private int depth;

		// Token: 0x04001A1C RID: 6684
		private WorkItems deferredWorkItems;
	}
}

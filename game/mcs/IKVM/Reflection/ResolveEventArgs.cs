using System;

namespace IKVM.Reflection
{
	// Token: 0x0200006B RID: 107
	public sealed class ResolveEventArgs : EventArgs
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00011FA6 File Offset: 0x000101A6
		public ResolveEventArgs(string name) : this(name, null)
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00011FB0 File Offset: 0x000101B0
		public ResolveEventArgs(string name, Assembly requestingAssembly)
		{
			this.name = name;
			this.requestingAssembly = requestingAssembly;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00011FC6 File Offset: 0x000101C6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00011FCE File Offset: 0x000101CE
		public Assembly RequestingAssembly
		{
			get
			{
				return this.requestingAssembly;
			}
		}

		// Token: 0x0400021C RID: 540
		private readonly string name;

		// Token: 0x0400021D RID: 541
		private readonly Assembly requestingAssembly;
	}
}

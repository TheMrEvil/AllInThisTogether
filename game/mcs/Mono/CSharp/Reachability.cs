using System;

namespace Mono.CSharp
{
	// Token: 0x02000218 RID: 536
	public struct Reachability
	{
		// Token: 0x06001B4D RID: 6989 RVA: 0x00084A02 File Offset: 0x00082C02
		private Reachability(bool unreachable)
		{
			this.unreachable = unreachable;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00084A0B File Offset: 0x00082C0B
		public bool IsUnreachable
		{
			get
			{
				return this.unreachable;
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00084A13 File Offset: 0x00082C13
		public static Reachability CreateUnreachable()
		{
			return new Reachability(true);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00084A1B File Offset: 0x00082C1B
		public static Reachability operator &(Reachability a, Reachability b)
		{
			return new Reachability(a.unreachable && b.unreachable);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00084A33 File Offset: 0x00082C33
		public static Reachability operator |(Reachability a, Reachability b)
		{
			return new Reachability(a.unreachable | b.unreachable);
		}

		// Token: 0x04000A2D RID: 2605
		private readonly bool unreachable;
	}
}

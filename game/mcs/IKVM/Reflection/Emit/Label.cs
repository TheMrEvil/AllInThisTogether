using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E5 RID: 229
	public struct Label
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x00024082 File Offset: 0x00022282
		internal Label(int index)
		{
			this.index1 = index + 1;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002408D File Offset: 0x0002228D
		internal int Index
		{
			get
			{
				return this.index1 - 1;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00024097 File Offset: 0x00022297
		public bool Equals(Label other)
		{
			return other.index1 == this.index1;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000240A8 File Offset: 0x000222A8
		public override bool Equals(object obj)
		{
			return this == obj as Label?;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000240E0 File Offset: 0x000222E0
		public override int GetHashCode()
		{
			return this.index1;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000240E8 File Offset: 0x000222E8
		public static bool operator ==(Label arg1, Label arg2)
		{
			return arg1.index1 == arg2.index1;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000240F8 File Offset: 0x000222F8
		public static bool operator !=(Label arg1, Label arg2)
		{
			return !(arg1 == arg2);
		}

		// Token: 0x0400049A RID: 1178
		private readonly int index1;
	}
}

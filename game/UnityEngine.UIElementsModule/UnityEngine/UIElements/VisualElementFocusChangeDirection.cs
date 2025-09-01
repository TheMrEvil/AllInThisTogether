using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E9 RID: 233
	public class VisualElementFocusChangeDirection : FocusChangeDirection
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001AE3B File Offset: 0x0001903B
		public static FocusChangeDirection left
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Left;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001AE42 File Offset: 0x00019042
		public static FocusChangeDirection right
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001AE4C File Offset: 0x0001904C
		protected new static VisualElementFocusChangeDirection lastValue
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001AE63 File Offset: 0x00019063
		protected VisualElementFocusChangeDirection(int value) : base(value)
		{
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001AE6E File Offset: 0x0001906E
		// Note: this type is marked as 'beforefieldinit'.
		static VisualElementFocusChangeDirection()
		{
		}

		// Token: 0x040002F8 RID: 760
		private static readonly VisualElementFocusChangeDirection s_Left = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 1);

		// Token: 0x040002F9 RID: 761
		private static readonly VisualElementFocusChangeDirection s_Right = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 2);
	}
}

using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x02000381 RID: 897
	internal struct ExpressionMultiplier
	{
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x000880EC File Offset: 0x000862EC
		// (set) Token: 0x06001CB0 RID: 7344 RVA: 0x00088104 File Offset: 0x00086304
		public ExpressionMultiplierType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.SetType(value);
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00088110 File Offset: 0x00086310
		public ExpressionMultiplier(ExpressionMultiplierType type = ExpressionMultiplierType.None)
		{
			this.m_Type = type;
			this.min = (this.max = 1);
			this.SetType(type);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00088140 File Offset: 0x00086340
		private void SetType(ExpressionMultiplierType value)
		{
			this.m_Type = value;
			switch (value)
			{
			case ExpressionMultiplierType.ZeroOrMore:
				this.min = 0;
				this.max = 100;
				return;
			case ExpressionMultiplierType.OneOrMore:
			case ExpressionMultiplierType.OneOrMoreComma:
			case ExpressionMultiplierType.GroupAtLeastOne:
				this.min = 1;
				this.max = 100;
				return;
			case ExpressionMultiplierType.ZeroOrOne:
				this.min = 0;
				this.max = 1;
				return;
			}
			this.min = (this.max = 1);
		}

		// Token: 0x04000E7D RID: 3709
		public const int Infinity = 100;

		// Token: 0x04000E7E RID: 3710
		private ExpressionMultiplierType m_Type;

		// Token: 0x04000E7F RID: 3711
		public int min;

		// Token: 0x04000E80 RID: 3712
		public int max;
	}
}

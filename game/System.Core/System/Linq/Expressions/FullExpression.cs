using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000272 RID: 626
	internal sealed class FullExpression<TDelegate> : ExpressionN<TDelegate>
	{
		// Token: 0x0600124B RID: 4683 RVA: 0x0003B41C File Offset: 0x0003961C
		public FullExpression(Expression body, string name, bool tailCall, IReadOnlyList<ParameterExpression> parameters) : base(body, parameters)
		{
			this.NameCore = name;
			this.TailCallCore = tailCall;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0003B435 File Offset: 0x00039635
		internal override string NameCore
		{
			[CompilerGenerated]
			get
			{
				return this.<NameCore>k__BackingField;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0003B43D File Offset: 0x0003963D
		internal override bool TailCallCore
		{
			[CompilerGenerated]
			get
			{
				return this.<TailCallCore>k__BackingField;
			}
		}

		// Token: 0x04000A15 RID: 2581
		[CompilerGenerated]
		private readonly string <NameCore>k__BackingField;

		// Token: 0x04000A16 RID: 2582
		[CompilerGenerated]
		private readonly bool <TailCallCore>k__BackingField;
	}
}

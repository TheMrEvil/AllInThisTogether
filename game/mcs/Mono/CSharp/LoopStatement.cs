using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200029F RID: 671
	public abstract class LoopStatement : Statement
	{
		// Token: 0x06002072 RID: 8306 RVA: 0x000A02DE File Offset: 0x0009E4DE
		protected LoopStatement(Statement statement)
		{
			this.Statement = statement;
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x000A02ED File Offset: 0x0009E4ED
		// (set) Token: 0x06002074 RID: 8308 RVA: 0x000A02F5 File Offset: 0x0009E4F5
		public Statement Statement
		{
			[CompilerGenerated]
			get
			{
				return this.<Statement>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Statement>k__BackingField = value;
			}
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000A0300 File Offset: 0x0009E500
		public override bool Resolve(BlockContext bc)
		{
			LoopStatement enclosingLoop = bc.EnclosingLoop;
			LoopStatement enclosingLoopOrSwitch = bc.EnclosingLoopOrSwitch;
			bc.EnclosingLoop = this;
			bc.EnclosingLoopOrSwitch = this;
			bool result = this.Statement.Resolve(bc);
			bc.EnclosingLoopOrSwitch = enclosingLoopOrSwitch;
			bc.EnclosingLoop = enclosingLoop;
			return result;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void AddEndDefiniteAssignment(FlowAnalysisContext fc)
		{
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void SetEndReachable()
		{
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void SetIteratorReachable()
		{
		}

		// Token: 0x04000C26 RID: 3110
		[CompilerGenerated]
		private Statement <Statement>k__BackingField;
	}
}

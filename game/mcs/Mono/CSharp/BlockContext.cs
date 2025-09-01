using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000158 RID: 344
	public class BlockContext : ResolveContext
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x000474A4 File Offset: 0x000456A4
		public BlockContext(IMemberContext mc, ExplicitBlock block, TypeSpec returnType) : base(mc)
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			this.return_type = returnType;
			this.CurrentBlock = block;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000474CC File Offset: 0x000456CC
		public BlockContext(ResolveContext rc, ExplicitBlock block, TypeSpec returnType) : this(rc.MemberContext, block, returnType)
		{
			if (rc.IsUnsafe)
			{
				this.flags |= ResolveContext.Options.UnsafeScope;
			}
			if (rc.HasSet(ResolveContext.Options.CheckedScope))
			{
				this.flags |= ResolveContext.Options.CheckedScope;
			}
			if (!rc.ConstantCheckState)
			{
				this.flags &= ~ResolveContext.Options.ConstantCheckState;
			}
			if (rc.IsInProbingMode)
			{
				this.flags |= ResolveContext.Options.ProbingMode;
			}
			if (rc.HasSet(ResolveContext.Options.FieldInitializerScope))
			{
				this.flags |= ResolveContext.Options.FieldInitializerScope;
			}
			if (rc.HasSet(ResolveContext.Options.ExpressionTreeConversion))
			{
				this.flags |= ResolveContext.Options.ExpressionTreeConversion;
			}
			if (rc.HasSet(ResolveContext.Options.BaseInitializer))
			{
				this.flags |= ResolveContext.Options.BaseInitializer;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0004759C File Offset: 0x0004579C
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x000475A4 File Offset: 0x000457A4
		public ExceptionStatement CurrentTryBlock
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentTryBlock>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentTryBlock>k__BackingField = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x000475AD File Offset: 0x000457AD
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x000475B5 File Offset: 0x000457B5
		public LoopStatement EnclosingLoop
		{
			[CompilerGenerated]
			get
			{
				return this.<EnclosingLoop>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnclosingLoop>k__BackingField = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x000475BE File Offset: 0x000457BE
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x000475C6 File Offset: 0x000457C6
		public LoopStatement EnclosingLoopOrSwitch
		{
			[CompilerGenerated]
			get
			{
				return this.<EnclosingLoopOrSwitch>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnclosingLoopOrSwitch>k__BackingField = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000475CF File Offset: 0x000457CF
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x000475D7 File Offset: 0x000457D7
		public Switch Switch
		{
			[CompilerGenerated]
			get
			{
				return this.<Switch>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Switch>k__BackingField = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x000475E0 File Offset: 0x000457E0
		public TypeSpec ReturnType
		{
			get
			{
				return this.return_type;
			}
		}

		// Token: 0x0400074B RID: 1867
		private readonly TypeSpec return_type;

		// Token: 0x0400074C RID: 1868
		public int AssignmentInfoOffset;

		// Token: 0x0400074D RID: 1869
		[CompilerGenerated]
		private ExceptionStatement <CurrentTryBlock>k__BackingField;

		// Token: 0x0400074E RID: 1870
		[CompilerGenerated]
		private LoopStatement <EnclosingLoop>k__BackingField;

		// Token: 0x0400074F RID: 1871
		[CompilerGenerated]
		private LoopStatement <EnclosingLoopOrSwitch>k__BackingField;

		// Token: 0x04000750 RID: 1872
		[CompilerGenerated]
		private Switch <Switch>k__BackingField;
	}
}

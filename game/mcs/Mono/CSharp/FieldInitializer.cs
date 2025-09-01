using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000116 RID: 278
	public class FieldInitializer : Assign
	{
		// Token: 0x06000DB2 RID: 3506 RVA: 0x00032924 File Offset: 0x00030B24
		public FieldInitializer(FieldBase mc, Expression expression, Location loc) : base(new FieldExpr(mc.Spec, expression.Location), expression, loc)
		{
			this.mc = mc;
			if (!mc.IsStatic)
			{
				((FieldExpr)this.target).InstanceExpression = new CompilerGeneratedThis(mc.CurrentType, expression.Location);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0003297A File Offset: 0x00030B7A
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00032982 File Offset: 0x00030B82
		public int AssignmentOffset
		{
			[CompilerGenerated]
			get
			{
				return this.<AssignmentOffset>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AssignmentOffset>k__BackingField = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0003298B File Offset: 0x00030B8B
		public FieldBase Field
		{
			get
			{
				return this.mc;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00032993 File Offset: 0x00030B93
		public override Location StartLocation
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003299C File Offset: 0x00030B9C
		protected override Expression DoResolve(ResolveContext rc)
		{
			if (this.source == null)
			{
				return null;
			}
			if (this.resolved == null)
			{
				BlockContext blockContext = (BlockContext)rc;
				FieldInitializer.FieldInitializerContext fieldInitializerContext = new FieldInitializer.FieldInitializerContext(this.mc, blockContext);
				this.resolved = (base.DoResolve(fieldInitializerContext) as ExpressionStatement);
				this.AssignmentOffset = fieldInitializerContext.AssignmentInfoOffset - blockContext.AssignmentInfoOffset;
			}
			return this.resolved;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000329FC File Offset: 0x00030BFC
		public override void EmitStatement(EmitContext ec)
		{
			if (this.resolved == null)
			{
				return;
			}
			if (ec.HasSet(BuilderContext.Options.OmitDebugInfo) && ec.HasMethodSymbolBuilder)
			{
				using (ec.With(BuilderContext.Options.OmitDebugInfo, false))
				{
					ec.Mark(this.loc);
				}
			}
			if (this.resolved != this)
			{
				this.resolved.EmitStatement(ec);
				return;
			}
			base.EmitStatement(ec);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00032A78 File Offset: 0x00030C78
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.source.FlowAnalysis(fc);
			((FieldExpr)this.target).SetFieldAssigned(fc);
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00032A98 File Offset: 0x00030C98
		public bool IsDefaultInitializer
		{
			get
			{
				Constant constant = this.source as Constant;
				if (constant == null)
				{
					return false;
				}
				FieldExpr fieldExpr = (FieldExpr)this.target;
				return constant.IsDefaultInitializer(fieldExpr.Type);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00032ACE File Offset: 0x00030CCE
		public override bool IsSideEffectFree
		{
			get
			{
				return this.source.IsSideEffectFree;
			}
		}

		// Token: 0x04000664 RID: 1636
		private ExpressionStatement resolved;

		// Token: 0x04000665 RID: 1637
		private FieldBase mc;

		// Token: 0x04000666 RID: 1638
		[CompilerGenerated]
		private int <AssignmentOffset>k__BackingField;

		// Token: 0x02000380 RID: 896
		private sealed class FieldInitializerContext : BlockContext
		{
			// Token: 0x06002698 RID: 9880 RVA: 0x000B6BEC File Offset: 0x000B4DEC
			public FieldInitializerContext(IMemberContext mc, BlockContext constructorContext) : base(mc, null, constructorContext.ReturnType)
			{
				this.flags |= (ResolveContext.Options.FieldInitializerScope | ResolveContext.Options.ConstructorScope);
				this.ctor_block = constructorContext.CurrentBlock.Explicit;
				if (this.ctor_block.IsCompilerGenerated)
				{
					this.CurrentBlock = this.ctor_block;
				}
			}

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x06002699 RID: 9881 RVA: 0x000B6C43 File Offset: 0x000B4E43
			public override ExplicitBlock ConstructorBlock
			{
				get
				{
					return this.ctor_block;
				}
			}

			// Token: 0x04000F4B RID: 3915
			private readonly ExplicitBlock ctor_block;
		}
	}
}

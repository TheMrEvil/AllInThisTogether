using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200029E RID: 670
	public class For : LoopStatement
	{
		// Token: 0x06002062 RID: 8290 RVA: 0x0009FF3B File Offset: 0x0009E13B
		public For(Location l) : base(null)
		{
			this.loc = l;
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x0009FF4B File Offset: 0x0009E14B
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x0009FF53 File Offset: 0x0009E153
		public Statement Initializer
		{
			[CompilerGenerated]
			get
			{
				return this.<Initializer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Initializer>k__BackingField = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x0009FF5C File Offset: 0x0009E15C
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x0009FF64 File Offset: 0x0009E164
		public Expression Condition
		{
			[CompilerGenerated]
			get
			{
				return this.<Condition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Condition>k__BackingField = value;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0009FF6D File Offset: 0x0009E16D
		// (set) Token: 0x06002068 RID: 8296 RVA: 0x0009FF75 File Offset: 0x0009E175
		public Statement Iterator
		{
			[CompilerGenerated]
			get
			{
				return this.<Iterator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Iterator>k__BackingField = value;
			}
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0009FF80 File Offset: 0x0009E180
		public override bool Resolve(BlockContext bc)
		{
			this.Initializer.Resolve(bc);
			if (this.Condition != null)
			{
				this.Condition = this.Condition.Resolve(bc);
				Constant constant = this.Condition as Constant;
				if (constant != null)
				{
					if (constant.IsDefaultValue)
					{
						this.empty = true;
					}
					else
					{
						this.infinite = true;
					}
				}
			}
			else
			{
				this.infinite = true;
			}
			return base.Resolve(bc) && this.Iterator.Resolve(bc);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0009FFFC File Offset: 0x0009E1FC
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.Initializer.FlowAnalysis(fc);
			DefiniteAssignmentBitSet definiteAssignment;
			if (this.Condition != null)
			{
				this.Condition.FlowAnalysisConditional(fc);
				fc.DefiniteAssignment = fc.DefiniteAssignmentOnTrue;
				definiteAssignment = new DefiniteAssignmentBitSet(fc.DefiniteAssignmentOnFalse);
			}
			else
			{
				definiteAssignment = fc.BranchDefiniteAssignment();
			}
			base.Statement.FlowAnalysis(fc);
			this.Iterator.FlowAnalysis(fc);
			if (this.end_reachable_das != null)
			{
				definiteAssignment = DefiniteAssignmentBitSet.And(this.end_reachable_das);
				this.end_reachable_das = null;
			}
			fc.DefiniteAssignment = definiteAssignment;
			return this.infinite && !this.end_reachable;
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000A009C File Offset: 0x0009E29C
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			this.Initializer.MarkReachable(rc);
			if (!base.Statement.MarkReachable(rc).IsUnreachable || this.iterator_reachable)
			{
				this.Iterator.MarkReachable(rc);
			}
			if (this.infinite && !this.end_reachable)
			{
				return Reachability.CreateUnreachable();
			}
			return rc;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000A0100 File Offset: 0x0009E300
		protected override void DoEmit(EmitContext ec)
		{
			if (this.Initializer != null)
			{
				this.Initializer.Emit(ec);
			}
			if (this.empty)
			{
				this.Condition.EmitSideEffect(ec);
				return;
			}
			Label loopBegin = ec.LoopBegin;
			Label loopEnd = ec.LoopEnd;
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			ec.LoopBegin = ec.DefineLabel();
			ec.LoopEnd = ec.DefineLabel();
			ec.Emit(OpCodes.Br, label2);
			ec.MarkLabel(label);
			base.Statement.Emit(ec);
			ec.MarkLabel(ec.LoopBegin);
			this.Iterator.Emit(ec);
			ec.MarkLabel(label2);
			if (this.Condition != null)
			{
				ec.Mark(this.Condition.Location);
				if (this.Condition is Constant)
				{
					this.Condition.EmitSideEffect(ec);
					ec.Emit(OpCodes.Br, label);
				}
				else
				{
					this.Condition.EmitBranchable(ec, label, true);
				}
			}
			else
			{
				ec.Emit(OpCodes.Br, label);
			}
			ec.MarkLabel(ec.LoopEnd);
			ec.LoopBegin = loopBegin;
			ec.LoopEnd = loopEnd;
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000A0220 File Offset: 0x0009E420
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			For @for = (For)t;
			if (this.Initializer != null)
			{
				@for.Initializer = this.Initializer.Clone(clonectx);
			}
			if (this.Condition != null)
			{
				@for.Condition = this.Condition.Clone(clonectx);
			}
			if (this.Iterator != null)
			{
				@for.Iterator = this.Iterator.Clone(clonectx);
			}
			@for.Statement = base.Statement.Clone(clonectx);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000A0294 File Offset: 0x0009E494
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000A029D File Offset: 0x0009E49D
		public override void AddEndDefiniteAssignment(FlowAnalysisContext fc)
		{
			if (!this.infinite)
			{
				return;
			}
			if (this.end_reachable_das == null)
			{
				this.end_reachable_das = new List<DefiniteAssignmentBitSet>();
			}
			this.end_reachable_das.Add(fc.DefiniteAssignment);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000A02CC File Offset: 0x0009E4CC
		public override void SetEndReachable()
		{
			this.end_reachable = true;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000A02D5 File Offset: 0x0009E4D5
		public override void SetIteratorReachable()
		{
			this.iterator_reachable = true;
		}

		// Token: 0x04000C1E RID: 3102
		private bool infinite;

		// Token: 0x04000C1F RID: 3103
		private bool empty;

		// Token: 0x04000C20 RID: 3104
		private bool iterator_reachable;

		// Token: 0x04000C21 RID: 3105
		private bool end_reachable;

		// Token: 0x04000C22 RID: 3106
		private List<DefiniteAssignmentBitSet> end_reachable_das;

		// Token: 0x04000C23 RID: 3107
		[CompilerGenerated]
		private Statement <Initializer>k__BackingField;

		// Token: 0x04000C24 RID: 3108
		[CompilerGenerated]
		private Expression <Condition>k__BackingField;

		// Token: 0x04000C25 RID: 3109
		[CompilerGenerated]
		private Statement <Iterator>k__BackingField;
	}
}

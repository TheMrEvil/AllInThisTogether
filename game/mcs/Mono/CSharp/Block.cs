using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x020002B4 RID: 692
	public class Block : Statement
	{
		// Token: 0x06002129 RID: 8489 RVA: 0x000A213A File Offset: 0x000A033A
		public Block(Block parent, Location start, Location end) : this(parent, (Block.Flags)0, start, end)
		{
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000A2148 File Offset: 0x000A0348
		public Block(Block parent, Block.Flags flags, Location start, Location end)
		{
			if (parent != null)
			{
				this.ParametersBlock = parent.ParametersBlock;
				this.Explicit = parent.Explicit;
			}
			this.Parent = parent;
			this.flags = flags;
			this.StartLocation = start;
			this.EndLocation = end;
			this.loc = start;
			this.statements = new List<Statement>(4);
			this.original = this;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x000A21AD File Offset: 0x000A03AD
		// (set) Token: 0x0600212C RID: 8492 RVA: 0x000A21B5 File Offset: 0x000A03B5
		public Block Original
		{
			get
			{
				return this.original;
			}
			protected set
			{
				this.original = value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x000A21BE File Offset: 0x000A03BE
		// (set) Token: 0x0600212E RID: 8494 RVA: 0x000A21CF File Offset: 0x000A03CF
		public bool IsCompilerGenerated
		{
			get
			{
				return (this.flags & Block.Flags.CompilerGenerated) > (Block.Flags)0;
			}
			set
			{
				this.flags = (value ? (this.flags | Block.Flags.CompilerGenerated) : (this.flags & ~Block.Flags.CompilerGenerated));
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x000A21F4 File Offset: 0x000A03F4
		public bool IsCatchBlock
		{
			get
			{
				return (this.flags & Block.Flags.CatchBlock) > (Block.Flags)0;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x000A2205 File Offset: 0x000A0405
		public bool IsFinallyBlock
		{
			get
			{
				return (this.flags & Block.Flags.FinallyBlock) > (Block.Flags)0;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x000A2216 File Offset: 0x000A0416
		// (set) Token: 0x06002132 RID: 8498 RVA: 0x000A2223 File Offset: 0x000A0423
		public bool Unchecked
		{
			get
			{
				return (this.flags & Block.Flags.Unchecked) > (Block.Flags)0;
			}
			set
			{
				this.flags = (value ? (this.flags | Block.Flags.Unchecked) : (this.flags & ~Block.Flags.Unchecked));
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x000A2241 File Offset: 0x000A0441
		// (set) Token: 0x06002134 RID: 8500 RVA: 0x000A224F File Offset: 0x000A044F
		public bool Unsafe
		{
			get
			{
				return (this.flags & Block.Flags.Unsafe) > (Block.Flags)0;
			}
			set
			{
				this.flags |= Block.Flags.Unsafe;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x000A2260 File Offset: 0x000A0460
		public List<Statement> Statements
		{
			get
			{
				return this.statements;
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000A2268 File Offset: 0x000A0468
		public void SetEndLocation(Location loc)
		{
			this.EndLocation = loc;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000A2271 File Offset: 0x000A0471
		public void AddLabel(LabeledStatement target)
		{
			this.ParametersBlock.TopBlock.AddLabel(target.Name, target);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000A228A File Offset: 0x000A048A
		public void AddLocalName(LocalVariable li)
		{
			this.AddLocalName(li.Name, li);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000A2299 File Offset: 0x000A0499
		public void AddLocalName(string name, INamedBlockVariable li)
		{
			this.ParametersBlock.TopBlock.AddLocalName(name, li, false);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000A22AE File Offset: 0x000A04AE
		public virtual void Error_AlreadyDeclared(string name, INamedBlockVariable variable, string reason)
		{
			if (reason == null)
			{
				this.Error_AlreadyDeclared(name, variable);
				return;
			}
			this.ParametersBlock.TopBlock.Report.Error(136, variable.Location, "A local variable named `{0}' cannot be declared in this scope because it would give a different meaning to `{0}', which is already used in a `{1}' scope to denote something else", name, reason);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000A22E4 File Offset: 0x000A04E4
		public virtual void Error_AlreadyDeclared(string name, INamedBlockVariable variable)
		{
			ParametersBlock.ParameterInfo parameterInfo = variable as ParametersBlock.ParameterInfo;
			if (parameterInfo != null)
			{
				parameterInfo.Parameter.Error_DuplicateName(this.ParametersBlock.TopBlock.Report);
				return;
			}
			this.ParametersBlock.TopBlock.Report.Error(128, variable.Location, "A local variable named `{0}' is already defined in this scope", name);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000A233D File Offset: 0x000A053D
		public virtual void Error_AlreadyDeclaredTypeParameter(string name, Location loc)
		{
			this.ParametersBlock.TopBlock.Report.Error(412, loc, "The type parameter name `{0}' is the same as local variable or parameter name", name);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000A2360 File Offset: 0x000A0560
		public void AddScopeStatement(Statement s)
		{
			if (this.scope_initializers == null)
			{
				this.scope_initializers = new List<Statement>();
			}
			if (this.resolving_init_idx != null)
			{
				this.scope_initializers.Insert(this.resolving_init_idx.Value, s);
				this.resolving_init_idx++;
				return;
			}
			this.scope_initializers.Add(s);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000A23E2 File Offset: 0x000A05E2
		public void InsertStatement(int index, Statement s)
		{
			this.statements.Insert(index, s);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000A23F1 File Offset: 0x000A05F1
		public void AddStatement(Statement s)
		{
			this.statements.Add(s);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000A23FF File Offset: 0x000A05FF
		public LabeledStatement LookupLabel(string name)
		{
			return this.ParametersBlock.GetLabel(name, this);
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000A2410 File Offset: 0x000A0610
		public override Reachability MarkReachable(Reachability rc)
		{
			if (rc.IsUnreachable)
			{
				return rc;
			}
			this.MarkReachableScope(rc);
			foreach (Statement statement in this.statements)
			{
				rc = statement.MarkReachable(rc);
				if (rc.IsUnreachable)
				{
					if ((this.flags & Block.Flags.ReachableEnd) != (Block.Flags)0)
					{
						return default(Reachability);
					}
					return rc;
				}
			}
			this.flags |= Block.Flags.ReachableEnd;
			return rc;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000A24AC File Offset: 0x000A06AC
		public void MarkReachableScope(Reachability rc)
		{
			base.MarkReachable(rc);
			if (this.scope_initializers != null)
			{
				foreach (Statement statement in this.scope_initializers)
				{
					statement.MarkReachable(rc);
				}
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000A2510 File Offset: 0x000A0710
		public override bool Resolve(BlockContext bc)
		{
			if ((this.flags & Block.Flags.Resolved) != (Block.Flags)0)
			{
				return true;
			}
			Block currentBlock = bc.CurrentBlock;
			bc.CurrentBlock = this;
			if (this.scope_initializers != null)
			{
				this.resolving_init_idx = new int?(0);
				while (this.resolving_init_idx < this.scope_initializers.Count)
				{
					this.scope_initializers[this.resolving_init_idx.Value].Resolve(bc);
					this.resolving_init_idx++;
				}
				this.resolving_init_idx = null;
			}
			bool result = true;
			int count = this.statements.Count;
			for (int i = 0; i < count; i++)
			{
				Statement statement = this.statements[i];
				if (!statement.Resolve(bc))
				{
					result = false;
					this.statements[i] = new EmptyStatement(statement.loc);
				}
			}
			bc.CurrentBlock = currentBlock;
			this.flags |= Block.Flags.Resolved;
			return result;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000A2648 File Offset: 0x000A0848
		protected override void DoEmit(EmitContext ec)
		{
			for (int i = 0; i < this.statements.Count; i++)
			{
				this.statements[i].Emit(ec);
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000A267D File Offset: 0x000A087D
		public override void Emit(EmitContext ec)
		{
			if (this.scope_initializers != null)
			{
				this.EmitScopeInitializers(ec);
			}
			this.DoEmit(ec);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000A2698 File Offset: 0x000A0898
		protected void EmitScopeInitializers(EmitContext ec)
		{
			foreach (Statement statement in this.scope_initializers)
			{
				statement.Emit(ec);
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000A26EC File Offset: 0x000A08EC
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.scope_initializers != null)
			{
				foreach (Statement statement in this.scope_initializers)
				{
					statement.FlowAnalysis(fc);
				}
			}
			return this.DoFlowAnalysis(fc, 0);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000A2750 File Offset: 0x000A0950
		private bool DoFlowAnalysis(FlowAnalysisContext fc, int startIndex)
		{
			bool flag = !this.reachable;
			bool flag2 = startIndex != 0;
			while (startIndex < this.statements.Count)
			{
				Statement statement = this.statements[startIndex];
				flag = statement.FlowAnalysis(fc);
				if (statement.IsUnreachable)
				{
					this.statements[startIndex] = Block.RewriteUnreachableStatement(statement);
				}
				else if (flag)
				{
					bool flag3 = flag2 && statement is GotoCase;
					startIndex++;
					while (startIndex < this.statements.Count)
					{
						statement = this.statements[startIndex];
						if (statement is SwitchLabel)
						{
							if (!flag3)
							{
								statement.FlowAnalysis(fc);
								break;
							}
							break;
						}
						else
						{
							if (statement.IsUnreachable)
							{
								statement.FlowAnalysis(fc);
								this.statements[startIndex] = Block.RewriteUnreachableStatement(statement);
							}
							startIndex++;
						}
					}
					if (flag3)
					{
						break;
					}
				}
				else
				{
					LabeledStatement labeledStatement = statement as LabeledStatement;
					if (labeledStatement != null && fc.AddReachedLabel(labeledStatement))
					{
						break;
					}
				}
				startIndex++;
			}
			return !this.Explicit.HasReachableClosingBrace;
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000A2855 File Offset: 0x000A0A55
		private static Statement RewriteUnreachableStatement(Statement s)
		{
			if (s is BlockVariable || s is EmptyStatement)
			{
				return s;
			}
			return new EmptyStatement(s.loc);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000A2874 File Offset: 0x000A0A74
		public void ScanGotoJump(Statement label)
		{
			int i = 0;
			while (i < this.statements.Count && this.statements[i] != label)
			{
				i++;
			}
			Reachability rc = default(Reachability);
			for (i++; i < this.statements.Count; i++)
			{
				rc = this.statements[i].MarkReachable(rc);
				if (rc.IsUnreachable)
				{
					return;
				}
			}
			this.flags |= Block.Flags.ReachableEnd;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000A28F4 File Offset: 0x000A0AF4
		public void ScanGotoJump(Statement label, FlowAnalysisContext fc)
		{
			int num = 0;
			while (num < this.statements.Count && this.statements[num] != label)
			{
				num++;
			}
			this.DoFlowAnalysis(fc, num + 1);
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000A2934 File Offset: 0x000A0B34
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Block block = (Block)t;
			clonectx.AddBlockMap(this, block);
			if (this.original != this)
			{
				clonectx.AddBlockMap(this.original, block);
			}
			block.ParametersBlock = (ParametersBlock)((this.ParametersBlock == this) ? block : clonectx.RemapBlockCopy(this.ParametersBlock));
			block.Explicit = (ExplicitBlock)((this.Explicit == this) ? block : clonectx.LookupBlock(this.Explicit));
			if (this.Parent != null)
			{
				block.Parent = clonectx.RemapBlockCopy(this.Parent);
			}
			block.statements = new List<Statement>(this.statements.Count);
			foreach (Statement statement in this.statements)
			{
				block.statements.Add(statement.Clone(clonectx));
			}
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000A2A30 File Offset: 0x000A0C30
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C4B RID: 3147
		public Block Parent;

		// Token: 0x04000C4C RID: 3148
		public Location StartLocation;

		// Token: 0x04000C4D RID: 3149
		public Location EndLocation;

		// Token: 0x04000C4E RID: 3150
		public ExplicitBlock Explicit;

		// Token: 0x04000C4F RID: 3151
		public ParametersBlock ParametersBlock;

		// Token: 0x04000C50 RID: 3152
		protected Block.Flags flags;

		// Token: 0x04000C51 RID: 3153
		protected List<Statement> statements;

		// Token: 0x04000C52 RID: 3154
		protected List<Statement> scope_initializers;

		// Token: 0x04000C53 RID: 3155
		private int? resolving_init_idx;

		// Token: 0x04000C54 RID: 3156
		private Block original;

		// Token: 0x020003F1 RID: 1009
		[Flags]
		public enum Flags
		{
			// Token: 0x04001138 RID: 4408
			Unchecked = 1,
			// Token: 0x04001139 RID: 4409
			ReachableEnd = 8,
			// Token: 0x0400113A RID: 4410
			Unsafe = 16,
			// Token: 0x0400113B RID: 4411
			HasCapturedVariable = 64,
			// Token: 0x0400113C RID: 4412
			HasCapturedThis = 128,
			// Token: 0x0400113D RID: 4413
			IsExpressionTree = 256,
			// Token: 0x0400113E RID: 4414
			CompilerGenerated = 512,
			// Token: 0x0400113F RID: 4415
			HasAsyncModifier = 1024,
			// Token: 0x04001140 RID: 4416
			Resolved = 2048,
			// Token: 0x04001141 RID: 4417
			YieldBlock = 4096,
			// Token: 0x04001142 RID: 4418
			AwaitBlock = 8192,
			// Token: 0x04001143 RID: 4419
			FinallyBlock = 16384,
			// Token: 0x04001144 RID: 4420
			CatchBlock = 32768,
			// Token: 0x04001145 RID: 4421
			Iterator = 1048576,
			// Token: 0x04001146 RID: 4422
			NoFlowAnalysis = 2097152,
			// Token: 0x04001147 RID: 4423
			InitializationEmitted = 4194304
		}
	}
}

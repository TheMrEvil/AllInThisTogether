using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002B6 RID: 694
	public class ParametersBlock : ExplicitBlock
	{
		// Token: 0x06002162 RID: 8546 RVA: 0x000A3200 File Offset: 0x000A1400
		public ParametersBlock(Block parent, ParametersCompiled parameters, Location start, Block.Flags flags = (Block.Flags)0) : base(parent, (Block.Flags)0, start, start)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.parameters = parameters;
			this.ParametersBlock = this;
			this.flags |= (flags | (parent.ParametersBlock.flags & (Block.Flags.YieldBlock | Block.Flags.AwaitBlock)));
			this.top_block = parent.ParametersBlock.top_block;
			this.ProcessParameters();
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000A326B File Offset: 0x000A146B
		protected ParametersBlock(ParametersCompiled parameters, Location start) : base(null, (Block.Flags)0, start, start)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.parameters = parameters;
			this.ParametersBlock = this;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000A3294 File Offset: 0x000A1494
		protected ParametersBlock(ParametersBlock source, ParametersCompiled parameters) : base(null, (Block.Flags)0, source.StartLocation, source.EndLocation)
		{
			this.parameters = parameters;
			this.statements = source.statements;
			this.scope_initializers = source.scope_initializers;
			this.resolved = true;
			this.reachable = source.reachable;
			this.am_storey = source.am_storey;
			this.state_machine = source.state_machine;
			this.flags = (source.flags & Block.Flags.ReachableEnd);
			this.ParametersBlock = this;
			base.Original = source.Original;
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x000A3320 File Offset: 0x000A1520
		// (set) Token: 0x06002166 RID: 8550 RVA: 0x000A3331 File Offset: 0x000A1531
		public bool IsAsync
		{
			get
			{
				return (this.flags & Block.Flags.HasAsyncModifier) > (Block.Flags)0;
			}
			set
			{
				this.flags = (value ? (this.flags | Block.Flags.HasAsyncModifier) : (this.flags & ~Block.Flags.HasAsyncModifier));
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x000A3356 File Offset: 0x000A1556
		public bool IsExpressionTree
		{
			get
			{
				return (this.flags & Block.Flags.IsExpressionTree) > (Block.Flags)0;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x000A3367 File Offset: 0x000A1567
		public ParametersCompiled Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x000A336F File Offset: 0x000A156F
		public StateMachine StateMachine
		{
			get
			{
				return this.state_machine;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000A3377 File Offset: 0x000A1577
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x000A337F File Offset: 0x000A157F
		public ToplevelBlock TopBlock
		{
			get
			{
				return this.top_block;
			}
			set
			{
				this.top_block = value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x000A3388 File Offset: 0x000A1588
		public bool Resolved
		{
			get
			{
				return (this.flags & Block.Flags.Resolved) > (Block.Flags)0;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x000A3399 File Offset: 0x000A1599
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x000A33A1 File Offset: 0x000A15A1
		public int TemporaryLocalsCount
		{
			[CompilerGenerated]
			get
			{
				return this.<TemporaryLocalsCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TemporaryLocalsCount>k__BackingField = value;
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000A33AA File Offset: 0x000A15AA
		public void CheckControlExit(FlowAnalysisContext fc)
		{
			this.CheckControlExit(fc, fc.DefiniteAssignment);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000A33BC File Offset: 0x000A15BC
		public virtual void CheckControlExit(FlowAnalysisContext fc, DefiniteAssignmentBitSet dat)
		{
			if (this.parameter_info == null)
			{
				return;
			}
			foreach (ParametersBlock.ParameterInfo parameterInfo in this.parameter_info)
			{
				if (parameterInfo.VariableInfo != null && !parameterInfo.VariableInfo.IsAssigned(dat))
				{
					fc.Report.Error(177, parameterInfo.Location, "The out parameter `{0}' must be assigned to before control leaves the current method", parameterInfo.Parameter.Name);
				}
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000A3428 File Offset: 0x000A1628
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			base.CloneTo(clonectx, t);
			ParametersBlock parametersBlock = (ParametersBlock)t;
			ParametersBlock parametersBlock2 = this;
			for (;;)
			{
				if (parametersBlock2.labels != null)
				{
					parametersBlock.labels = new Dictionary<string, object>();
					using (Dictionary<string, object>.Enumerator enumerator = parametersBlock2.labels.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair = enumerator.Current;
							List<LabeledStatement> list = keyValuePair.Value as List<LabeledStatement>;
							if (list != null)
							{
								List<LabeledStatement> list2 = new List<LabeledStatement>();
								foreach (LabeledStatement labeledStatement in list)
								{
									list2.Add(this.RemapLabeledStatement(labeledStatement, clonectx.RemapBlockCopy(labeledStatement.Block)));
								}
								parametersBlock.labels.Add(keyValuePair.Key, list2);
							}
							else
							{
								LabeledStatement labeledStatement2 = (LabeledStatement)keyValuePair.Value;
								parametersBlock.labels.Add(keyValuePair.Key, this.RemapLabeledStatement(labeledStatement2, clonectx.RemapBlockCopy(labeledStatement2.Block)));
							}
						}
						break;
					}
				}
				if (parametersBlock2.Parent == null)
				{
					break;
				}
				parametersBlock2 = parametersBlock2.Parent.ParametersBlock;
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000A3574 File Offset: 0x000A1774
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.statements.Count == 1)
			{
				Expression expression = this.statements[0].CreateExpressionTree(ec);
				if (this.scope_initializers != null)
				{
					expression = new ParametersBlock.BlockScopeExpression(expression, this);
				}
				return expression;
			}
			return base.CreateExpressionTree(ec);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000A35BB File Offset: 0x000A17BB
		public override void Emit(EmitContext ec)
		{
			if (this.state_machine != null && this.state_machine.OriginalSourceBlock != this)
			{
				base.DefineStoreyContainer(ec, this.state_machine);
				this.state_machine.EmitStoreyInstantiation(ec, this);
			}
			base.Emit(ec);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000A35BB File Offset: 0x000A17BB
		public void EmitEmbedded(EmitContext ec)
		{
			if (this.state_machine != null && this.state_machine.OriginalSourceBlock != this)
			{
				base.DefineStoreyContainer(ec, this.state_machine);
				this.state_machine.EmitStoreyInstantiation(ec, this);
			}
			base.Emit(ec);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000A35F4 File Offset: 0x000A17F4
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			bool result = base.DoFlowAnalysis(fc);
			if (base.HasReachableClosingBrace)
			{
				this.CheckControlExit(fc);
			}
			return result;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000A360C File Offset: 0x000A180C
		public LabeledStatement GetLabel(string name, Block block)
		{
			if (this.labels == null)
			{
				if (this.Parent != null)
				{
					return this.Parent.ParametersBlock.GetLabel(name, block);
				}
				return null;
			}
			else
			{
				object obj;
				if (!this.labels.TryGetValue(name, out obj))
				{
					return null;
				}
				LabeledStatement labeledStatement = obj as LabeledStatement;
				if (labeledStatement != null)
				{
					if (ParametersBlock.IsLabelVisible(labeledStatement, block))
					{
						return labeledStatement;
					}
				}
				else
				{
					List<LabeledStatement> list = (List<LabeledStatement>)obj;
					for (int i = 0; i < list.Count; i++)
					{
						labeledStatement = list[i];
						if (ParametersBlock.IsLabelVisible(labeledStatement, block))
						{
							return labeledStatement;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000A3698 File Offset: 0x000A1898
		private static bool IsLabelVisible(LabeledStatement label, Block b)
		{
			while (label.Block != b)
			{
				b = b.Parent;
				if (b == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000A36B4 File Offset: 0x000A18B4
		public ParametersBlock.ParameterInfo GetParameterInfo(Parameter p)
		{
			for (int i = 0; i < this.parameters.Count; i++)
			{
				if (this.parameters[i] == p)
				{
					return this.parameter_info[i];
				}
			}
			throw new ArgumentException("Invalid parameter");
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000A36F9 File Offset: 0x000A18F9
		public ParameterReference GetParameterReference(int index, Location loc)
		{
			return new ParameterReference(this.parameter_info[index], loc);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000A370C File Offset: 0x000A190C
		public Statement PerformClone()
		{
			CloneContext clonectx = new CloneContext();
			return base.Clone(clonectx);
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000A3728 File Offset: 0x000A1928
		protected void ProcessParameters()
		{
			if (this.parameters.Count == 0)
			{
				return;
			}
			this.parameter_info = new ParametersBlock.ParameterInfo[this.parameters.Count];
			for (int i = 0; i < this.parameter_info.Length; i++)
			{
				IParameterData parameterData = this.parameters.FixedParameters[i];
				if (parameterData != null)
				{
					this.parameter_info[i] = new ParametersBlock.ParameterInfo(this, i);
					if (parameterData.Name != null)
					{
						base.AddLocalName(parameterData.Name, this.parameter_info[i]);
					}
				}
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000A37A8 File Offset: 0x000A19A8
		private LabeledStatement RemapLabeledStatement(LabeledStatement stmt, Block dst)
		{
			Block block = stmt.Block;
			if (block.ParametersBlock != this)
			{
				return stmt;
			}
			List<Statement> statements = block.Statements;
			for (int i = 0; i < statements.Count; i++)
			{
				if (statements[i] == stmt)
				{
					return (LabeledStatement)dst.Statements[i];
				}
			}
			throw new InternalErrorException("Should never be reached");
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000A3808 File Offset: 0x000A1A08
		public override bool Resolve(BlockContext bc)
		{
			if (this.resolved)
			{
				return true;
			}
			this.resolved = true;
			if (bc.HasSet(ResolveContext.Options.ExpressionTreeConversion))
			{
				this.flags |= Block.Flags.IsExpressionTree;
			}
			try
			{
				this.PrepareAssignmentAnalysis(bc);
				if (!base.Resolve(bc))
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				if (ex is CompletionResult || bc.Report.IsDisabled || ex is FatalException || bc.Report.Printer is NullReportPrinter || bc.Module.Compiler.Settings.BreakOnInternalError)
				{
					throw;
				}
				if (bc.CurrentBlock != null)
				{
					bc.Report.Error(584, bc.CurrentBlock.StartLocation, "Internal compiler error: {0}", ex.Message);
				}
				else
				{
					bc.Report.Error(587, "Internal compiler error: {0}", ex.Message);
				}
			}
			if (this.IsAsync)
			{
				AnonymousMethodBody anonymousMethodBody = bc.CurrentAnonymousMethod as AnonymousMethodBody;
				if (anonymousMethodBody != null && anonymousMethodBody.ReturnTypeInference != null && !anonymousMethodBody.ReturnTypeInference.HasBounds(0))
				{
					anonymousMethodBody.ReturnTypeInference = null;
					anonymousMethodBody.ReturnType = bc.Module.PredefinedTypes.Task.TypeSpec;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000A395C File Offset: 0x000A1B5C
		private void PrepareAssignmentAnalysis(BlockContext bc)
		{
			for (int i = 0; i < this.parameters.Count; i++)
			{
				IParameterData parameterData = this.parameters.FixedParameters[i];
				if ((parameterData.ModFlags & Parameter.Modifier.OUT) != Parameter.Modifier.NONE)
				{
					this.parameter_info[i].VariableInfo = VariableInfo.Create(bc, (Parameter)parameterData);
				}
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000A39B0 File Offset: 0x000A1BB0
		public ToplevelBlock ConvertToIterator(IMethodData method, TypeDefinition host, TypeSpec iterator_type, bool is_enumerable)
		{
			Iterator iterator = new Iterator(this, method, host, iterator_type, is_enumerable);
			IteratorStorey stateMachine = new IteratorStorey(iterator);
			this.state_machine = stateMachine;
			iterator.SetStateMachine(stateMachine);
			ToplevelBlock toplevelBlock = new ToplevelBlock(host.Compiler, this.Parameters, Location.Null, Block.Flags.CompilerGenerated);
			toplevelBlock.Original = this;
			toplevelBlock.state_machine = stateMachine;
			toplevelBlock.AddStatement(new Return(iterator, iterator.Location));
			return toplevelBlock;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000A3A1C File Offset: 0x000A1C1C
		public ParametersBlock ConvertToAsyncTask(IMemberContext context, TypeDefinition host, ParametersCompiled parameters, TypeSpec returnType, TypeSpec delegateType, Location loc)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				Parameter parameter = parameters[i];
				if ((parameter.ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
				{
					host.Compiler.Report.Error(1988, parameter.Location, "Async methods cannot have ref or out parameters");
					return this;
				}
				if (parameter is ArglistParameter)
				{
					host.Compiler.Report.Error(4006, parameter.Location, "__arglist is not allowed in parameter list of async methods");
					return this;
				}
				if (parameters.Types[i].IsPointer)
				{
					host.Compiler.Report.Error(4005, parameter.Location, "Async methods cannot have unsafe parameters");
					return this;
				}
			}
			if (!base.HasAwait)
			{
				host.Compiler.Report.Warning(1998, 1, loc, "Async block lacks `await' operator and will run synchronously");
			}
			BuiltinTypeSpec @void = host.Module.Compiler.BuiltinTypes.Void;
			AsyncInitializer asyncInitializer = new AsyncInitializer(this, host, @void);
			asyncInitializer.Type = @void;
			asyncInitializer.DelegateType = delegateType;
			AsyncTaskStorey stateMachine = new AsyncTaskStorey(this, context, asyncInitializer, returnType);
			this.state_machine = stateMachine;
			asyncInitializer.SetStateMachine(stateMachine);
			ToplevelBlock toplevelBlock = (this is ToplevelBlock) ? new ToplevelBlock(host.Compiler, this.Parameters, Location.Null, Block.Flags.CompilerGenerated) : new ParametersBlock(this.Parent, parameters, Location.Null, Block.Flags.CompilerGenerated | Block.Flags.HasAsyncModifier);
			toplevelBlock.Original = this;
			toplevelBlock.state_machine = stateMachine;
			toplevelBlock.AddStatement(new AsyncInitializerStatement(asyncInitializer));
			return toplevelBlock;
		}

		// Token: 0x04000C56 RID: 3158
		protected ParametersCompiled parameters;

		// Token: 0x04000C57 RID: 3159
		protected ParametersBlock.ParameterInfo[] parameter_info;

		// Token: 0x04000C58 RID: 3160
		protected bool resolved;

		// Token: 0x04000C59 RID: 3161
		protected ToplevelBlock top_block;

		// Token: 0x04000C5A RID: 3162
		protected StateMachine state_machine;

		// Token: 0x04000C5B RID: 3163
		protected Dictionary<string, object> labels;

		// Token: 0x04000C5C RID: 3164
		[CompilerGenerated]
		private int <TemporaryLocalsCount>k__BackingField;

		// Token: 0x020003F2 RID: 1010
		public class ParameterInfo : INamedBlockVariable
		{
			// Token: 0x060027DC RID: 10204 RVA: 0x000BD51A File Offset: 0x000BB71A
			public ParameterInfo(ParametersBlock block, int index)
			{
				this.block = block;
				this.index = index;
			}

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x060027DD RID: 10205 RVA: 0x000BD530 File Offset: 0x000BB730
			public ParametersBlock Block
			{
				get
				{
					return this.block;
				}
			}

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x060027DE RID: 10206 RVA: 0x000BD530 File Offset: 0x000BB730
			Block INamedBlockVariable.Block
			{
				get
				{
					return this.block;
				}
			}

			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x060027DF RID: 10207 RVA: 0x0000212D File Offset: 0x0000032D
			public bool IsDeclared
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x060027E0 RID: 10208 RVA: 0x0000212D File Offset: 0x0000032D
			public bool IsParameter
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000905 RID: 2309
			// (get) Token: 0x060027E1 RID: 10209 RVA: 0x000BD538 File Offset: 0x000BB738
			// (set) Token: 0x060027E2 RID: 10210 RVA: 0x000BD540 File Offset: 0x000BB740
			public bool IsLocked
			{
				get
				{
					return this.is_locked;
				}
				set
				{
					this.is_locked = value;
				}
			}

			// Token: 0x17000906 RID: 2310
			// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000BD549 File Offset: 0x000BB749
			public Location Location
			{
				get
				{
					return this.Parameter.Location;
				}
			}

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x060027E4 RID: 10212 RVA: 0x000BD556 File Offset: 0x000BB756
			public Parameter Parameter
			{
				get
				{
					return this.block.Parameters[this.index];
				}
			}

			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000BD56E File Offset: 0x000BB76E
			public TypeSpec ParameterType
			{
				get
				{
					return this.Parameter.Type;
				}
			}

			// Token: 0x060027E6 RID: 10214 RVA: 0x000BD57B File Offset: 0x000BB77B
			public Expression CreateReferenceExpression(ResolveContext rc, Location loc)
			{
				return new ParameterReference(this, loc);
			}

			// Token: 0x04001148 RID: 4424
			private readonly ParametersBlock block;

			// Token: 0x04001149 RID: 4425
			private readonly int index;

			// Token: 0x0400114A RID: 4426
			public VariableInfo VariableInfo;

			// Token: 0x0400114B RID: 4427
			private bool is_locked;
		}

		// Token: 0x020003F3 RID: 1011
		private sealed class BlockScopeExpression : Expression
		{
			// Token: 0x060027E7 RID: 10215 RVA: 0x000BD584 File Offset: 0x000BB784
			public BlockScopeExpression(Expression child, ParametersBlock block)
			{
				this.child = child;
				this.block = block;
			}

			// Token: 0x060027E8 RID: 10216 RVA: 0x000BD59A File Offset: 0x000BB79A
			public override bool ContainsEmitWithAwait()
			{
				return this.child.ContainsEmitWithAwait();
			}

			// Token: 0x060027E9 RID: 10217 RVA: 0x0000225C File Offset: 0x0000045C
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060027EA RID: 10218 RVA: 0x000BD5A8 File Offset: 0x000BB7A8
			protected override Expression DoResolve(ResolveContext ec)
			{
				if (this.child == null)
				{
					return null;
				}
				this.child = this.child.Resolve(ec);
				if (this.child == null)
				{
					return null;
				}
				this.eclass = this.child.eclass;
				this.type = this.child.Type;
				return this;
			}

			// Token: 0x060027EB RID: 10219 RVA: 0x000BD5FE File Offset: 0x000BB7FE
			public override void Emit(EmitContext ec)
			{
				this.block.EmitScopeInitializers(ec);
				this.child.Emit(ec);
			}

			// Token: 0x0400114C RID: 4428
			private Expression child;

			// Token: 0x0400114D RID: 4429
			private readonly ParametersBlock block;
		}
	}
}

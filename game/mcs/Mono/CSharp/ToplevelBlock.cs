using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002B7 RID: 695
	public class ToplevelBlock : ParametersBlock
	{
		// Token: 0x06002181 RID: 8577 RVA: 0x000A3B96 File Offset: 0x000A1D96
		public ToplevelBlock(CompilerContext ctx, Location loc) : this(ctx, ParametersCompiled.EmptyReadOnlyParameters, loc, (Block.Flags)0)
		{
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000A3BA6 File Offset: 0x000A1DA6
		public ToplevelBlock(CompilerContext ctx, ParametersCompiled parameters, Location start, Block.Flags flags = (Block.Flags)0) : base(parameters, start)
		{
			this.compiler = ctx;
			this.flags = flags;
			this.top_block = this;
			base.ProcessParameters();
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000A3BCC File Offset: 0x000A1DCC
		public ToplevelBlock(ParametersBlock source, ParametersCompiled parameters) : base(source, parameters)
		{
			this.compiler = source.TopBlock.compiler;
			this.top_block = this;
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x000A3BEE File Offset: 0x000A1DEE
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x000A3BFF File Offset: 0x000A1DFF
		public bool IsIterator
		{
			get
			{
				return (this.flags & Block.Flags.Iterator) > (Block.Flags)0;
			}
			set
			{
				this.flags = (value ? (this.flags | Block.Flags.Iterator) : (this.flags & ~Block.Flags.Iterator));
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000A3C24 File Offset: 0x000A1E24
		public Report Report
		{
			get
			{
				return this.compiler.Report;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x000A3C31 File Offset: 0x000A1E31
		public List<ExplicitBlock> ThisReferencesFromChildrenBlock
		{
			get
			{
				return this.this_references;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000A3C39 File Offset: 0x000A1E39
		public LocalVariable ThisVariable
		{
			get
			{
				return this.this_variable;
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000A3C44 File Offset: 0x000A1E44
		public void AddLocalName(string name, INamedBlockVariable li, bool ignoreChildrenBlocks)
		{
			if (this.names == null)
			{
				this.names = new Dictionary<string, object>();
			}
			object obj;
			if (!this.names.TryGetValue(name, out obj))
			{
				this.names.Add(name, li);
				return;
			}
			INamedBlockVariable namedBlockVariable = obj as INamedBlockVariable;
			List<INamedBlockVariable> list;
			if (namedBlockVariable != null)
			{
				list = new List<INamedBlockVariable>();
				list.Add(namedBlockVariable);
				this.names[name] = list;
			}
			else
			{
				list = (List<INamedBlockVariable>)obj;
			}
			ExplicitBlock @explicit = li.Block.Explicit;
			for (int i = 0; i < list.Count; i++)
			{
				namedBlockVariable = list[i];
				Block block = namedBlockVariable.Block.Explicit;
				if (@explicit == block)
				{
					li.Block.Error_AlreadyDeclared(name, li);
					break;
				}
				Block block2 = @explicit;
				while ((block2 = block2.Parent) != null)
				{
					if (block2 == block)
					{
						li.Block.Error_AlreadyDeclared(name, li, "parent or current");
						i = list.Count;
						break;
					}
				}
				if (!ignoreChildrenBlocks && @explicit.Parent != block.Parent)
				{
					while ((block = block.Parent) != null)
					{
						if (@explicit == block)
						{
							li.Block.Error_AlreadyDeclared(name, li, "child");
							i = list.Count;
							break;
						}
					}
				}
			}
			list.Add(li);
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000A3D80 File Offset: 0x000A1F80
		public void AddLabel(string name, LabeledStatement label)
		{
			if (this.labels == null)
			{
				this.labels = new Dictionary<string, object>();
			}
			object obj;
			if (!this.labels.TryGetValue(name, out obj))
			{
				this.labels.Add(name, label);
				return;
			}
			LabeledStatement labeledStatement = obj as LabeledStatement;
			List<LabeledStatement> list;
			if (labeledStatement != null)
			{
				list = new List<LabeledStatement>();
				list.Add(labeledStatement);
				this.labels[name] = list;
			}
			else
			{
				list = (List<LabeledStatement>)obj;
			}
			for (int i = 0; i < list.Count; i++)
			{
				labeledStatement = list[i];
				Block block = labeledStatement.Block;
				if (label.Block == block)
				{
					this.Report.SymbolRelatedToPreviousError(labeledStatement.loc, name);
					this.Report.Error(140, label.loc, "The label `{0}' is a duplicate", name);
					break;
				}
				block = label.Block;
				while ((block = block.Parent) != null)
				{
					if (labeledStatement.Block == block)
					{
						this.Report.Error(158, label.loc, "The label `{0}' shadows another label by the same name in a contained scope", name);
						i = list.Count;
						break;
					}
				}
				block = labeledStatement.Block;
				while ((block = block.Parent) != null)
				{
					if (label.Block == block)
					{
						this.Report.Error(158, label.loc, "The label `{0}' shadows another label by the same name in a contained scope", name);
						i = list.Count;
						break;
					}
				}
			}
			list.Add(label);
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000A3EE0 File Offset: 0x000A20E0
		public void AddThisReferenceFromChildrenBlock(ExplicitBlock block)
		{
			if (this.this_references == null)
			{
				this.this_references = new List<ExplicitBlock>();
			}
			if (!this.this_references.Contains(block))
			{
				this.this_references.Add(block);
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000A3F0F File Offset: 0x000A210F
		public void RemoveThisReferenceFromChildrenBlock(ExplicitBlock block)
		{
			this.this_references.Remove(block);
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000A3F20 File Offset: 0x000A2120
		public Arguments GetAllParametersArguments()
		{
			int count = this.parameters.Count;
			Arguments arguments = new Arguments(count);
			for (int i = 0; i < count; i++)
			{
				ParametersBlock.ParameterInfo parameterInfo = this.parameter_info[i];
				ParameterReference parameterReference = base.GetParameterReference(i, parameterInfo.Location);
				Parameter.Modifier modifier = parameterInfo.Parameter.ParameterModifier & Parameter.Modifier.RefOutMask;
				Argument.AType type;
				if (modifier != Parameter.Modifier.REF)
				{
					if (modifier != Parameter.Modifier.OUT)
					{
						type = Argument.AType.None;
					}
					else
					{
						type = Argument.AType.Out;
					}
				}
				else
				{
					type = Argument.AType.Ref;
				}
				arguments.Add(new Argument(parameterReference, type));
			}
			return arguments;
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000A3F9C File Offset: 0x000A219C
		public bool GetLocalName(string name, Block block, ref INamedBlockVariable variable)
		{
			if (this.names == null)
			{
				return false;
			}
			object obj;
			if (!this.names.TryGetValue(name, out obj))
			{
				return false;
			}
			variable = (obj as INamedBlockVariable);
			Block block2 = block;
			if (variable != null)
			{
				while (variable.Block != block2.Original)
				{
					block2 = block2.Parent;
					if (block2 == null)
					{
						block2 = variable.Block;
						while (block != block2)
						{
							block2 = block2.Parent;
							if (block2 == null)
							{
								goto IL_B5;
							}
						}
						return false;
					}
				}
				return true;
			}
			List<INamedBlockVariable> list = (List<INamedBlockVariable>)obj;
			int i = 0;
			IL_AC:
			while (i < list.Count)
			{
				variable = list[i];
				while (variable.Block != block2.Original)
				{
					block2 = block2.Parent;
					if (block2 == null)
					{
						block2 = variable.Block;
						while (block != block2)
						{
							block2 = block2.Parent;
							if (block2 == null)
							{
								block2 = block;
								i++;
								goto IL_AC;
							}
						}
						return false;
					}
				}
				return true;
			}
			IL_B5:
			variable = null;
			return false;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000A4064 File Offset: 0x000A2264
		public void IncludeBlock(ParametersBlock pb, ToplevelBlock block)
		{
			if (block.names != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in block.names)
				{
					INamedBlockVariable namedBlockVariable = keyValuePair.Value as INamedBlockVariable;
					if (namedBlockVariable != null)
					{
						if (namedBlockVariable.Block.ParametersBlock == pb)
						{
							this.AddLocalName(keyValuePair.Key, namedBlockVariable, false);
						}
					}
					else
					{
						foreach (INamedBlockVariable namedBlockVariable2 in ((List<INamedBlockVariable>)keyValuePair.Value))
						{
							if (namedBlockVariable2.Block.ParametersBlock == pb)
							{
								this.AddLocalName(keyValuePair.Key, namedBlockVariable2, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000A4154 File Offset: 0x000A2354
		public void AddThisVariable(BlockContext bc)
		{
			if (this.this_variable != null)
			{
				throw new InternalErrorException(this.StartLocation.ToString());
			}
			this.this_variable = new LocalVariable(this, "this", LocalVariable.Flags.Used | LocalVariable.Flags.IsThis, this.StartLocation);
			this.this_variable.Type = bc.CurrentType;
			this.this_variable.PrepareAssignmentAnalysis(bc);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000A41B5 File Offset: 0x000A23B5
		public override void CheckControlExit(FlowAnalysisContext fc, DefiniteAssignmentBitSet dat)
		{
			if (this.this_variable != null)
			{
				this.this_variable.IsThisAssigned(fc, this);
			}
			base.CheckControlExit(fc, dat);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000A41D8 File Offset: 0x000A23D8
		public override void Emit(EmitContext ec)
		{
			if (this.Report.Errors > 0)
			{
				return;
			}
			try
			{
				if (base.IsCompilerGenerated)
				{
					using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
					{
						base.Emit(ec);
						goto IL_3F;
					}
				}
				base.Emit(ec);
				IL_3F:
				if (ec.HasReturnLabel || base.HasReachableClosingBrace)
				{
					if (ec.HasReturnLabel)
					{
						ec.MarkLabel(ec.ReturnLabel);
					}
					if (ec.EmitAccurateDebugInfo && !base.IsCompilerGenerated)
					{
						ec.Mark(this.EndLocation);
					}
					if (ec.ReturnType.Kind != MemberKind.Void)
					{
						ec.Emit(OpCodes.Ldloc, ec.TemporaryReturn());
					}
					ec.Emit(OpCodes.Ret);
				}
			}
			catch (Exception e)
			{
				throw new InternalErrorException(e, this.StartLocation);
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000A42C0 File Offset: 0x000A24C0
		public bool Resolve(BlockContext bc, IMethodData md)
		{
			if (this.resolved)
			{
				return true;
			}
			int errors = bc.Report.Errors;
			base.Resolve(bc);
			if (bc.Report.Errors > errors)
			{
				return false;
			}
			this.MarkReachable(default(Reachability));
			if (base.HasReachableClosingBrace && bc.ReturnType.Kind != MemberKind.Void)
			{
				bc.Report.Error(161, md.Location, "`{0}': not all code paths return a value", md.GetSignatureForError());
			}
			if ((this.flags & Block.Flags.NoFlowAnalysis) != (Block.Flags)0)
			{
				return true;
			}
			FlowAnalysisContext fc = new FlowAnalysisContext(bc.Module.Compiler, this, bc.AssignmentInfoOffset);
			try
			{
				base.FlowAnalysis(fc);
			}
			catch (Exception e)
			{
				throw new InternalErrorException(e, this.StartLocation);
			}
			return true;
		}

		// Token: 0x04000C5D RID: 3165
		private LocalVariable this_variable;

		// Token: 0x04000C5E RID: 3166
		private CompilerContext compiler;

		// Token: 0x04000C5F RID: 3167
		private Dictionary<string, object> names;

		// Token: 0x04000C60 RID: 3168
		private List<ExplicitBlock> this_references;
	}
}

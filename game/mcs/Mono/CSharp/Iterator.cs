using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000234 RID: 564
	public class Iterator : StateMachineInitializer
	{
		// Token: 0x06001C70 RID: 7280 RVA: 0x0008A212 File Offset: 0x00088412
		public Iterator(ParametersBlock block, IMethodData method, TypeDefinition host, TypeSpec iterator_type, bool is_enumerable) : base(block, host, host.Compiler.BuiltinTypes.Bool)
		{
			this.OriginalMethod = method;
			this.OriginalIteratorType = iterator_type;
			this.IsEnumerable = is_enumerable;
			this.type = method.ReturnType;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x0008A24F File Offset: 0x0008844F
		public ToplevelBlock Container
		{
			get
			{
				return this.OriginalMethod.Block;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0008A25C File Offset: 0x0008845C
		public override string ContainerType
		{
			get
			{
				return "iterator";
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsIterator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0008A264 File Offset: 0x00088464
		public Method CreateFinallyHost(TryFinallyBlock block)
		{
			TypeDefinition storey = this.storey;
			FullNamedExpression return_type = new TypeExpression(this.storey.Compiler.BuiltinTypes.Void, this.loc);
			Modifiers mod = Modifiers.COMPILER_GENERATED;
			string host = null;
			string typePrefix = null;
			string name = "Finally";
			int num = this.finally_hosts_counter;
			this.finally_hosts_counter = num + 1;
			Method method = new Method(storey, return_type, mod, new MemberName(CompilerGeneratedContainer.MakeName(host, typePrefix, name, num), this.loc), ParametersCompiled.EmptyReadOnlyParameters, null);
			Method method2 = method;
			method2.Block = new ToplevelBlock(method2.Compiler, method.ParameterInfo, this.loc, Mono.CSharp.Block.Flags.CompilerGenerated | Mono.CSharp.Block.Flags.NoFlowAnalysis);
			method.Block.AddStatement(new Iterator.TryFinallyBlockProxyStatement(this, block));
			return method;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0008A305 File Offset: 0x00088505
		public void EmitYieldBreak(EmitContext ec, bool unwind_protect)
		{
			ec.Emit(unwind_protect ? OpCodes.Leave : OpCodes.Br, this.move_next_error);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0008A322 File Offset: 0x00088522
		public override string GetSignatureForError()
		{
			return this.OriginalMethod.GetSignatureForError();
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0008A330 File Offset: 0x00088530
		public override void Emit(EmitContext ec)
		{
			this.storey.Instance.Emit(ec);
			if (this.IsEnumerable)
			{
				ec.Emit(OpCodes.Dup);
				ec.EmitInt(-2);
				FieldSpec fieldSpec = this.storey.PC.Spec;
				if (this.storey.MemberName.IsGeneric)
				{
					fieldSpec = MemberCache.GetMember<FieldSpec>(this.Storey.Instance.Type, fieldSpec);
				}
				ec.Emit(OpCodes.Stfld, fieldSpec);
			}
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0008A3B0 File Offset: 0x000885B0
		public void EmitDispose(EmitContext ec)
		{
			if (this.resume_points == null)
			{
				return;
			}
			Label label = ec.DefineLabel();
			Label[] array = null;
			for (int i = 0; i < this.resume_points.Count; i++)
			{
				Label label2 = this.resume_points[i].PrepareForDispose(ec, label);
				if (!label2.Equals(label) || array != null)
				{
					if (array == null)
					{
						array = new Label[this.resume_points.Count + 1];
						for (int j = 0; j <= i; j++)
						{
							array[j] = label;
						}
					}
					array[i + 1] = label2;
				}
			}
			if (array != null)
			{
				this.current_pc = ec.GetTemporaryLocal(ec.BuiltinTypes.UInt);
				ec.EmitThis();
				ec.Emit(OpCodes.Ldfld, this.storey.PC.Spec);
				ec.Emit(OpCodes.Stloc, this.current_pc);
			}
			ec.EmitThis();
			ec.EmitInt(1);
			ec.Emit(OpCodes.Stfld, ((IteratorStorey)this.storey).DisposingField.Spec);
			ec.EmitThis();
			ec.EmitInt(-1);
			ec.Emit(OpCodes.Stfld, this.storey.PC.Spec);
			if (array != null)
			{
				ec.Emit(OpCodes.Ldloc, this.current_pc);
				ec.Emit(OpCodes.Switch, array);
				foreach (ResumableStatement resumableStatement in this.resume_points)
				{
					resumableStatement.EmitForDispose(ec, this.current_pc, label, true);
				}
			}
			ec.MarkLabel(label);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override void EmitStatement(EmitContext ec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0008A554 File Offset: 0x00088754
		public override void InjectYield(EmitContext ec, Expression expr, int resume_pc, bool unwind_protect, Label resume_point)
		{
			new FieldExpr(((IteratorStorey)this.storey).CurrentField, this.loc)
			{
				InstanceExpression = new CompilerGeneratedThis(this.storey.CurrentType, this.loc)
			}.EmitAssign(ec, expr, false, false);
			base.InjectYield(ec, expr, resume_pc, unwind_protect, resume_point);
			base.EmitLeave(ec, unwind_protect);
			ec.MarkLabel(resume_point);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0008A5C0 File Offset: 0x000887C0
		public static void CreateIterator(IMethodData method, TypeDefinition parent, Modifiers modifiers)
		{
			TypeSpec returnType = method.ReturnType;
			if (returnType == null)
			{
				return;
			}
			TypeSpec iterator_type;
			bool is_enumerable;
			if (!Iterator.CheckType(returnType, parent, out iterator_type, out is_enumerable))
			{
				parent.Compiler.Report.Error(1624, method.Location, "The body of `{0}' cannot be an iterator block because `{1}' is not an iterator interface type", method.GetSignatureForError(), returnType.GetSignatureForError());
				return;
			}
			ParametersCompiled parameterInfo = method.ParameterInfo;
			for (int i = 0; i < parameterInfo.Count; i++)
			{
				Parameter parameter = parameterInfo[i];
				if ((parameter.ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
				{
					parent.Compiler.Report.Error(1623, parameter.Location, "Iterators cannot have ref or out parameters");
					return;
				}
				if (parameter is ArglistParameter)
				{
					parent.Compiler.Report.Error(1636, method.Location, "__arglist is not allowed in parameter list of iterators");
					return;
				}
				if (parameterInfo.Types[i].IsPointer)
				{
					parent.Compiler.Report.Error(1637, parameter.Location, "Iterators cannot have unsafe parameters or yield types");
					return;
				}
			}
			if ((modifiers & Modifiers.UNSAFE) != (Modifiers)0)
			{
				parent.Compiler.Report.Error(1629, method.Location, "Unsafe code may not appear in iterators");
			}
			method.Block = method.Block.ConvertToIterator(method, parent, iterator_type, is_enumerable);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0008A708 File Offset: 0x00088908
		private static bool CheckType(TypeSpec ret, TypeContainer parent, out TypeSpec original_iterator_type, out bool is_enumerable)
		{
			original_iterator_type = null;
			is_enumerable = false;
			if (ret.BuiltinType == BuiltinTypeSpec.Type.IEnumerable)
			{
				original_iterator_type = parent.Compiler.BuiltinTypes.Object;
				is_enumerable = true;
				return true;
			}
			if (ret.BuiltinType == BuiltinTypeSpec.Type.IEnumerator)
			{
				original_iterator_type = parent.Compiler.BuiltinTypes.Object;
				is_enumerable = false;
				return true;
			}
			InflatedTypeSpec inflatedTypeSpec = ret as InflatedTypeSpec;
			if (inflatedTypeSpec == null)
			{
				return false;
			}
			ITypeDefinition memberDefinition = inflatedTypeSpec.MemberDefinition;
			PredefinedType predefinedType = parent.Module.PredefinedTypes.IEnumerableGeneric;
			if (predefinedType.Define() && predefinedType.TypeSpec.MemberDefinition == memberDefinition)
			{
				original_iterator_type = inflatedTypeSpec.TypeArguments[0];
				is_enumerable = true;
				return true;
			}
			predefinedType = parent.Module.PredefinedTypes.IEnumeratorGeneric;
			if (predefinedType.Define() && predefinedType.TypeSpec.MemberDefinition == memberDefinition)
			{
				original_iterator_type = inflatedTypeSpec.TypeArguments[0];
				is_enumerable = false;
				return true;
			}
			return false;
		}

		// Token: 0x04000A84 RID: 2692
		public readonly IMethodData OriginalMethod;

		// Token: 0x04000A85 RID: 2693
		public readonly bool IsEnumerable;

		// Token: 0x04000A86 RID: 2694
		public readonly TypeSpec OriginalIteratorType;

		// Token: 0x04000A87 RID: 2695
		private int finally_hosts_counter;

		// Token: 0x020003CC RID: 972
		private sealed class TryFinallyBlockProxyStatement : Statement
		{
			// Token: 0x06002763 RID: 10083 RVA: 0x000BC16F File Offset: 0x000BA36F
			public TryFinallyBlockProxyStatement(Iterator iterator, TryFinallyBlock block)
			{
				this.iterator = iterator;
				this.block = block;
			}

			// Token: 0x06002764 RID: 10084 RVA: 0x0000225C File Offset: 0x0000045C
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002765 RID: 10085 RVA: 0x0000225C File Offset: 0x0000045C
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002766 RID: 10086 RVA: 0x000BC188 File Offset: 0x000BA388
			protected override void DoEmit(EmitContext ec)
			{
				ec.CurrentAnonymousMethod = this.iterator;
				using (ec.With(BuilderContext.Options.OmitDebugInfo, !ec.HasMethodSymbolBuilder))
				{
					this.block.EmitFinallyBody(ec);
				}
			}

			// Token: 0x040010C3 RID: 4291
			private TryFinallyBlock block;

			// Token: 0x040010C4 RID: 4292
			private Iterator iterator;
		}
	}
}

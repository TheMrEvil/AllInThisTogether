using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000231 RID: 561
	internal class IteratorStorey : StateMachine
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x00089636 File Offset: 0x00087836
		public IteratorStorey(Iterator iterator) : base(iterator.Container.ParametersBlock, iterator.Host, iterator.OriginalMethod as MemberBase, iterator.OriginalMethod.CurrentTypeParameters, "Iterator", MemberKind.Class)
		{
			this.Iterator = iterator;
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00089676 File Offset: 0x00087876
		public Field CurrentField
		{
			get
			{
				return this.current_field;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0008967E File Offset: 0x0008787E
		public Field DisposingField
		{
			get
			{
				return this.disposing_field;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00089686 File Offset: 0x00087886
		public IList<HoistedParameter> HoistedParameters
		{
			get
			{
				return this.hoisted_params;
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0008968E File Offset: 0x0008788E
		protected override Constructor DefineDefaultConstructor(bool is_static)
		{
			Constructor constructor = base.DefineDefaultConstructor(is_static);
			constructor.ModFlags |= Modifiers.DEBUGGER_HIDDEN;
			return constructor;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000896AC File Offset: 0x000878AC
		protected override TypeSpec[] ResolveBaseTypes(out FullNamedExpression base_class)
		{
			TypeSpec typeSpec = this.Iterator.OriginalIteratorType;
			if (base.Mutator != null)
			{
				typeSpec = base.Mutator.Mutate(typeSpec);
			}
			this.iterator_type_expr = new TypeExpression(typeSpec, base.Location);
			List<TypeSpec> list = new List<TypeSpec>(5);
			if (this.Iterator.IsEnumerable)
			{
				list.Add(this.Compiler.BuiltinTypes.IEnumerable);
				if (this.Module.PredefinedTypes.IEnumerableGeneric.Define())
				{
					this.generic_enumerable_type = this.Module.PredefinedTypes.IEnumerableGeneric.TypeSpec.MakeGenericType(this.Module, new TypeSpec[]
					{
						typeSpec
					});
					list.Add(this.generic_enumerable_type);
				}
			}
			list.Add(this.Compiler.BuiltinTypes.IEnumerator);
			list.Add(this.Compiler.BuiltinTypes.IDisposable);
			PredefinedType ienumeratorGeneric = this.Module.PredefinedTypes.IEnumeratorGeneric;
			if (ienumeratorGeneric.Define())
			{
				this.generic_enumerator_type = ienumeratorGeneric.TypeSpec.MakeGenericType(this.Module, new TypeSpec[]
				{
					typeSpec
				});
				list.Add(this.generic_enumerator_type);
			}
			base_class = null;
			this.base_type = this.Compiler.BuiltinTypes.Object;
			return list.ToArray();
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000897FC File Offset: 0x000879FC
		protected override bool DoDefineMembers()
		{
			this.current_field = base.AddCompilerGeneratedField("$current", this.iterator_type_expr);
			this.disposing_field = base.AddCompilerGeneratedField("$disposing", new TypeExpression(this.Compiler.BuiltinTypes.Bool, base.Location));
			if (this.Iterator.IsEnumerable && this.hoisted_params != null)
			{
				this.hoisted_params_copy = new List<HoistedParameter>(this.hoisted_params.Count);
				foreach (HoistedParameter hoistedParameter in this.hoisted_params)
				{
					HoistedParameter item;
					if (hoistedParameter.IsAssigned)
					{
						item = new HoistedParameter(hoistedParameter, "<$>" + hoistedParameter.Field.Name);
					}
					else
					{
						item = null;
					}
					this.hoisted_params_copy.Add(item);
				}
			}
			if (this.generic_enumerator_type != null)
			{
				this.Define_Current(true);
			}
			this.Define_Current(false);
			new IteratorStorey.DisposeMethod(this);
			this.Define_Reset();
			if (this.Iterator.IsEnumerable)
			{
				FullNamedExpression explicitInterface = new TypeExpression(this.Compiler.BuiltinTypes.IEnumerable, base.Location);
				MemberName name = new MemberName("GetEnumerator", null, explicitInterface, Location.Null);
				if (this.generic_enumerator_type != null)
				{
					explicitInterface = new TypeExpression(this.generic_enumerable_type, base.Location);
					MemberName name2 = new MemberName("GetEnumerator", null, explicitInterface, Location.Null);
					Method method = IteratorStorey.GetEnumeratorMethod.Create(this, new TypeExpression(this.generic_enumerator_type, base.Location), name2);
					Return statement = new Return(new Invocation(new IteratorStorey.DynamicMethodGroupExpr(method, base.Location), null), base.Location);
					Method item2 = IteratorStorey.GetEnumeratorMethod.Create(this, new TypeExpression(this.Compiler.BuiltinTypes.IEnumerator, base.Location), name, statement);
					base.Members.Add(item2);
					base.Members.Add(method);
				}
				else
				{
					base.Members.Add(IteratorStorey.GetEnumeratorMethod.Create(this, new TypeExpression(this.Compiler.BuiltinTypes.IEnumerator, base.Location), name));
				}
			}
			return base.DoDefineMembers();
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00089A34 File Offset: 0x00087C34
		private void Define_Current(bool is_generic)
		{
			FullNamedExpression explicitInterface;
			TypeExpr type;
			if (is_generic)
			{
				explicitInterface = new TypeExpression(this.generic_enumerator_type, base.Location);
				type = this.iterator_type_expr;
			}
			else
			{
				explicitInterface = new TypeExpression(this.Module.Compiler.BuiltinTypes.IEnumerator, base.Location);
				type = new TypeExpression(this.Compiler.BuiltinTypes.Object, base.Location);
			}
			MemberName name = new MemberName("Current", null, explicitInterface, base.Location);
			ToplevelBlock toplevelBlock = new ToplevelBlock(this.Compiler, ParametersCompiled.EmptyReadOnlyParameters, base.Location, Block.Flags.CompilerGenerated | Block.Flags.NoFlowAnalysis);
			toplevelBlock.AddStatement(new Return(new IteratorStorey.DynamicFieldExpr(this.CurrentField, base.Location), base.Location));
			Property property = new Property(this, type, Modifiers.COMPILER_GENERATED | Modifiers.DEBUGGER_HIDDEN, name, null);
			Property property2 = property;
			property2.Get = new PropertyBase.GetMethod(property2, Modifiers.COMPILER_GENERATED, null, base.Location);
			property.Get.Block = toplevelBlock;
			base.Members.Add(property);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00089B30 File Offset: 0x00087D30
		private void Define_Reset()
		{
			Method method = new Method(this, new TypeExpression(this.Compiler.BuiltinTypes.Void, base.Location), Modifiers.PUBLIC | Modifiers.COMPILER_GENERATED | Modifiers.DEBUGGER_HIDDEN, new MemberName("Reset", base.Location), ParametersCompiled.EmptyReadOnlyParameters, null);
			base.Members.Add(method);
			method.Block = new ToplevelBlock(this.Compiler, method.ParameterInfo, base.Location, Block.Flags.CompilerGenerated | Block.Flags.NoFlowAnalysis);
			TypeSpec typeSpec = this.Module.PredefinedTypes.NotSupportedException.Resolve();
			if (typeSpec == null)
			{
				return;
			}
			method.Block.AddStatement(new Throw(new New(new TypeExpression(typeSpec, base.Location), null, base.Location), base.Location));
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00089BF0 File Offset: 0x00087DF0
		protected override void EmitHoistedParameters(EmitContext ec, List<HoistedParameter> hoisted)
		{
			base.EmitHoistedParameters(ec, hoisted);
			if (this.hoisted_params_copy != null)
			{
				base.EmitHoistedParameters(ec, this.hoisted_params_copy);
			}
		}

		// Token: 0x04000A74 RID: 2676
		public readonly Iterator Iterator;

		// Token: 0x04000A75 RID: 2677
		private List<HoistedParameter> hoisted_params_copy;

		// Token: 0x04000A76 RID: 2678
		private TypeExpr iterator_type_expr;

		// Token: 0x04000A77 RID: 2679
		private Field current_field;

		// Token: 0x04000A78 RID: 2680
		private Field disposing_field;

		// Token: 0x04000A79 RID: 2681
		private TypeSpec generic_enumerator_type;

		// Token: 0x04000A7A RID: 2682
		private TypeSpec generic_enumerable_type;

		// Token: 0x020003C7 RID: 967
		private class GetEnumeratorMethod : StateMachineMethod
		{
			// Token: 0x06002754 RID: 10068 RVA: 0x000BBF68 File Offset: 0x000BA168
			private GetEnumeratorMethod(IteratorStorey host, FullNamedExpression returnType, MemberName name) : base(host, null, returnType, Modifiers.DEBUGGER_HIDDEN, name, Mono.CSharp.Block.Flags.CompilerGenerated | Mono.CSharp.Block.Flags.NoFlowAnalysis)
			{
			}

			// Token: 0x06002755 RID: 10069 RVA: 0x000BBF7E File Offset: 0x000BA17E
			public static IteratorStorey.GetEnumeratorMethod Create(IteratorStorey host, FullNamedExpression returnType, MemberName name)
			{
				return IteratorStorey.GetEnumeratorMethod.Create(host, returnType, name, null);
			}

			// Token: 0x06002756 RID: 10070 RVA: 0x000BBF8C File Offset: 0x000BA18C
			public static IteratorStorey.GetEnumeratorMethod Create(IteratorStorey host, FullNamedExpression returnType, MemberName name, Statement statement)
			{
				IteratorStorey.GetEnumeratorMethod getEnumeratorMethod = new IteratorStorey.GetEnumeratorMethod(host, returnType, name);
				Statement s = statement ?? new IteratorStorey.GetEnumeratorMethod.GetEnumeratorStatement(host, getEnumeratorMethod);
				getEnumeratorMethod.block.AddStatement(s);
				return getEnumeratorMethod;
			}

			// Token: 0x02000421 RID: 1057
			private sealed class GetEnumeratorStatement : Statement
			{
				// Token: 0x0600285C RID: 10332 RVA: 0x000BF4E9 File Offset: 0x000BD6E9
				public GetEnumeratorStatement(IteratorStorey host, StateMachineMethod host_method)
				{
					this.host = host;
					this.host_method = host_method;
					this.loc = host_method.Location;
				}

				// Token: 0x0600285D RID: 10333 RVA: 0x0000225C File Offset: 0x0000045C
				protected override void CloneTo(CloneContext clonectx, Statement target)
				{
					throw new NotSupportedException();
				}

				// Token: 0x0600285E RID: 10334 RVA: 0x000BF50C File Offset: 0x000BD70C
				public override bool Resolve(BlockContext ec)
				{
					TypeExpression requested_type = new TypeExpression(this.host.Definition, this.loc);
					List<Expression> list = null;
					if (this.host.hoisted_this != null)
					{
						list = new List<Expression>((this.host.hoisted_params == null) ? 1 : (this.host.HoistedParameters.Count + 1));
						HoistedThis hoisted_this = this.host.hoisted_this;
						FieldExpr fieldExpr = new FieldExpr(hoisted_this.Field, this.loc);
						fieldExpr.InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, this.loc);
						list.Add(new ElementInitializer(hoisted_this.Field.Name, fieldExpr, this.loc));
					}
					if (this.host.hoisted_params != null)
					{
						if (list == null)
						{
							list = new List<Expression>(this.host.HoistedParameters.Count);
						}
						for (int i = 0; i < this.host.hoisted_params.Count; i++)
						{
							HoistedParameter hoistedParameter = this.host.hoisted_params[i];
							FieldExpr fieldExpr2 = new FieldExpr((this.host.hoisted_params_copy[i] ?? hoistedParameter).Field, this.loc);
							fieldExpr2.InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, this.loc);
							list.Add(new ElementInitializer(hoistedParameter.Field.Name, fieldExpr2, this.loc));
						}
					}
					if (list != null)
					{
						this.new_storey = new NewInitialize(requested_type, null, new CollectionOrObjectInitializers(list, this.loc), this.loc);
					}
					else
					{
						this.new_storey = new New(requested_type, null, this.loc);
					}
					this.new_storey = this.new_storey.Resolve(ec);
					if (this.new_storey != null)
					{
						this.new_storey = Convert.ImplicitConversionRequired(ec, this.new_storey, this.host_method.MemberType, this.loc);
					}
					return true;
				}

				// Token: 0x0600285F RID: 10335 RVA: 0x000BF6F0 File Offset: 0x000BD8F0
				protected override void DoEmit(EmitContext ec)
				{
					Label label = ec.DefineLabel();
					ec.EmitThis();
					ec.Emit(OpCodes.Ldflda, this.host.PC.Spec);
					ec.EmitInt(0);
					ec.EmitInt(-2);
					MethodSpec methodSpec = ec.Module.PredefinedMembers.InterlockedCompareExchange.Resolve(this.loc);
					if (methodSpec != null)
					{
						ec.Emit(OpCodes.Call, methodSpec);
					}
					ec.EmitInt(-2);
					ec.Emit(OpCodes.Bne_Un_S, label);
					ec.EmitThis();
					ec.Emit(OpCodes.Ret);
					ec.MarkLabel(label);
					this.new_storey.Emit(ec);
					ec.Emit(OpCodes.Ret);
				}

				// Token: 0x06002860 RID: 10336 RVA: 0x00023DF4 File Offset: 0x00021FF4
				protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
				{
					throw new NotImplementedException();
				}

				// Token: 0x06002861 RID: 10337 RVA: 0x0008953C File Offset: 0x0008773C
				public override Reachability MarkReachable(Reachability rc)
				{
					base.MarkReachable(rc);
					return Reachability.CreateUnreachable();
				}

				// Token: 0x040011A5 RID: 4517
				private readonly IteratorStorey host;

				// Token: 0x040011A6 RID: 4518
				private readonly StateMachineMethod host_method;

				// Token: 0x040011A7 RID: 4519
				private Expression new_storey;
			}
		}

		// Token: 0x020003C8 RID: 968
		private class DisposeMethod : StateMachineMethod
		{
			// Token: 0x06002757 RID: 10071 RVA: 0x000BBFBC File Offset: 0x000BA1BC
			public DisposeMethod(IteratorStorey host) : base(host, null, new TypeExpression(host.Compiler.BuiltinTypes.Void, host.Location), Modifiers.PUBLIC | Modifiers.DEBUGGER_HIDDEN, new MemberName("Dispose", host.Location), Mono.CSharp.Block.Flags.CompilerGenerated | Mono.CSharp.Block.Flags.NoFlowAnalysis)
			{
				host.Members.Add(this);
				base.Block.AddStatement(new IteratorStorey.DisposeMethod.DisposeMethodStatement(host.Iterator));
			}

			// Token: 0x02000422 RID: 1058
			private sealed class DisposeMethodStatement : Statement
			{
				// Token: 0x06002862 RID: 10338 RVA: 0x000BF7A2 File Offset: 0x000BD9A2
				public DisposeMethodStatement(Iterator iterator)
				{
					this.iterator = iterator;
					this.loc = iterator.Location;
				}

				// Token: 0x06002863 RID: 10339 RVA: 0x0000225C File Offset: 0x0000045C
				protected override void CloneTo(CloneContext clonectx, Statement target)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06002864 RID: 10340 RVA: 0x0000212D File Offset: 0x0000032D
				public override bool Resolve(BlockContext ec)
				{
					return true;
				}

				// Token: 0x06002865 RID: 10341 RVA: 0x000BF7BD File Offset: 0x000BD9BD
				protected override void DoEmit(EmitContext ec)
				{
					ec.CurrentAnonymousMethod = this.iterator;
					this.iterator.EmitDispose(ec);
				}

				// Token: 0x06002866 RID: 10342 RVA: 0x00023DF4 File Offset: 0x00021FF4
				protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
				{
					throw new NotImplementedException();
				}

				// Token: 0x040011A8 RID: 4520
				private Iterator iterator;
			}
		}

		// Token: 0x020003C9 RID: 969
		private class DynamicMethodGroupExpr : MethodGroupExpr
		{
			// Token: 0x06002758 RID: 10072 RVA: 0x000BC028 File Offset: 0x000BA228
			public DynamicMethodGroupExpr(Method method, Location loc) : base(null, null, loc)
			{
				this.method = method;
				this.eclass = ExprClass.Unresolved;
			}

			// Token: 0x06002759 RID: 10073 RVA: 0x000BC044 File Offset: 0x000BA244
			protected override Expression DoResolve(ResolveContext ec)
			{
				this.Methods = new List<MemberSpec>(1)
				{
					this.method.Spec
				};
				this.type = this.method.Parent.Definition;
				this.InstanceExpression = new CompilerGeneratedThis(this.type, base.Location);
				return base.DoResolve(ec);
			}

			// Token: 0x040010C0 RID: 4288
			private readonly Method method;
		}

		// Token: 0x020003CA RID: 970
		private class DynamicFieldExpr : FieldExpr
		{
			// Token: 0x0600275A RID: 10074 RVA: 0x000BC0A2 File Offset: 0x000BA2A2
			public DynamicFieldExpr(Field field, Location loc) : base(loc)
			{
				this.field = field;
			}

			// Token: 0x0600275B RID: 10075 RVA: 0x000BC0B4 File Offset: 0x000BA2B4
			protected override Expression DoResolve(ResolveContext ec)
			{
				this.spec = this.field.Spec;
				this.type = this.spec.MemberType;
				this.InstanceExpression = new CompilerGeneratedThis(this.type, base.Location);
				return base.DoResolve(ec);
			}

			// Token: 0x040010C1 RID: 4289
			private readonly Field field;
		}
	}
}

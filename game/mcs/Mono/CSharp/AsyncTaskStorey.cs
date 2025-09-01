using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200011D RID: 285
	public class AsyncTaskStorey : StateMachine
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x000339A4 File Offset: 0x00031BA4
		public AsyncTaskStorey(ParametersBlock block, IMemberContext context, AsyncInitializer initializer, TypeSpec type) : base(block, initializer.Host, context.CurrentMemberDefinition as MemberBase, context.CurrentTypeParameters, "async", MemberKind.Struct)
		{
			this.return_type = type;
			this.awaiter_fields = new Dictionary<TypeSpec, List<Field>>();
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x000339E1 File Offset: 0x00031BE1
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x000339E9 File Offset: 0x00031BE9
		public Expression HoistedReturnValue
		{
			[CompilerGenerated]
			get
			{
				return this.<HoistedReturnValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HoistedReturnValue>k__BackingField = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x000339F2 File Offset: 0x00031BF2
		public TypeSpec ReturnType
		{
			get
			{
				return this.return_type;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x000339FA File Offset: 0x00031BFA
		public PropertySpec Task
		{
			get
			{
				return this.task;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00033A02 File Offset: 0x00031C02
		protected override TypeAttributes TypeAttr
		{
			get
			{
				return base.TypeAttr & ~TypeAttributes.SequentialLayout;
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00033A10 File Offset: 0x00031C10
		public Field AddAwaiter(TypeSpec type)
		{
			if (this.mutator != null)
			{
				type = this.mutator.Mutate(type);
			}
			List<Field> list;
			if (this.awaiter_fields.TryGetValue(type, out list))
			{
				foreach (Field field in list)
				{
					if (field.IsAvailableForReuse)
					{
						field.IsAvailableForReuse = false;
						return field;
					}
				}
			}
			string str = "$awaiter";
			int num = this.awaiters;
			this.awaiters = num + 1;
			Field field2 = base.AddCompilerGeneratedField(str + num.ToString("X"), new TypeExpression(type, base.Location), true);
			field2.Define();
			if (list == null)
			{
				list = new List<Field>();
				this.awaiter_fields.Add(type, list);
			}
			list.Add(field2);
			return field2;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00033AF8 File Offset: 0x00031CF8
		public Field AddCapturedLocalVariable(TypeSpec type, bool requiresUninitialized = false)
		{
			if (this.mutator != null)
			{
				type = this.mutator.Mutate(type);
			}
			List<Field> list = null;
			if (this.stack_fields == null)
			{
				this.stack_fields = new Dictionary<TypeSpec, List<Field>>();
			}
			else if (this.stack_fields.TryGetValue(type, out list) && !requiresUninitialized)
			{
				foreach (Field field in list)
				{
					if (field.IsAvailableForReuse)
					{
						field.IsAvailableForReuse = false;
						return field;
					}
				}
			}
			string str = "$stack";
			int num = this.locals_captured;
			this.locals_captured = num + 1;
			Field field2 = base.AddCompilerGeneratedField(str + num.ToString("X"), new TypeExpression(type, base.Location), true);
			field2.Define();
			if (list == null)
			{
				list = new List<Field>();
				this.stack_fields.Add(type, list);
			}
			list.Add(field2);
			return field2;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00033BF8 File Offset: 0x00031DF8
		protected override bool DoDefineMembers()
		{
			bool flag = false;
			PredefinedMembers predefinedMembers = this.Module.PredefinedMembers;
			PredefinedType predefinedType;
			PredefinedMember<MethodSpec> predefinedMember;
			PredefinedMember<MethodSpec> predefinedMember2;
			PredefinedMember<MethodSpec> predefinedMember3;
			PredefinedMember<MethodSpec> predefinedMember4;
			PredefinedMember<MethodSpec> predefinedMember5;
			if (this.return_type.Kind == MemberKind.Void)
			{
				predefinedType = this.Module.PredefinedTypes.AsyncVoidMethodBuilder;
				predefinedMember = predefinedMembers.AsyncVoidMethodBuilderCreate;
				predefinedMember2 = predefinedMembers.AsyncVoidMethodBuilderStart;
				predefinedMember3 = predefinedMembers.AsyncVoidMethodBuilderSetResult;
				predefinedMember4 = predefinedMembers.AsyncVoidMethodBuilderSetException;
				predefinedMember5 = predefinedMembers.AsyncVoidMethodBuilderSetStateMachine;
			}
			else if (this.return_type == this.Module.PredefinedTypes.Task.TypeSpec)
			{
				predefinedType = this.Module.PredefinedTypes.AsyncTaskMethodBuilder;
				predefinedMember = predefinedMembers.AsyncTaskMethodBuilderCreate;
				predefinedMember2 = predefinedMembers.AsyncTaskMethodBuilderStart;
				predefinedMember3 = predefinedMembers.AsyncTaskMethodBuilderSetResult;
				predefinedMember4 = predefinedMembers.AsyncTaskMethodBuilderSetException;
				predefinedMember5 = predefinedMembers.AsyncTaskMethodBuilderSetStateMachine;
				this.task = predefinedMembers.AsyncTaskMethodBuilderTask.Get();
			}
			else
			{
				predefinedType = this.Module.PredefinedTypes.AsyncTaskMethodBuilderGeneric;
				predefinedMember = predefinedMembers.AsyncTaskMethodBuilderGenericCreate;
				predefinedMember2 = predefinedMembers.AsyncTaskMethodBuilderGenericStart;
				predefinedMember3 = predefinedMembers.AsyncTaskMethodBuilderGenericSetResult;
				predefinedMember4 = predefinedMembers.AsyncTaskMethodBuilderGenericSetException;
				predefinedMember5 = predefinedMembers.AsyncTaskMethodBuilderGenericSetStateMachine;
				this.task = predefinedMembers.AsyncTaskMethodBuilderGenericTask.Get();
				flag = true;
			}
			this.set_result = predefinedMember3.Get();
			this.set_exception = predefinedMember4.Get();
			this.builder_factory = predefinedMember.Get();
			this.builder_start = predefinedMember2.Get();
			PredefinedType iasyncStateMachine = this.Module.PredefinedTypes.IAsyncStateMachine;
			MethodSpec methodSpec = predefinedMember5.Get();
			if (!predefinedType.Define() || !iasyncStateMachine.Define() || this.set_result == null || this.builder_factory == null || this.set_exception == null || methodSpec == null || this.builder_start == null || !this.Module.PredefinedTypes.INotifyCompletion.Define())
			{
				base.Report.Error(1993, base.Location, "Cannot find compiler required types for asynchronous functions support. Are you targeting the wrong framework version?");
				return base.DoDefineMembers();
			}
			TypeSpec typeSpec = predefinedType.TypeSpec;
			if (flag)
			{
				TypeSpec[] targs = this.return_type.TypeArguments;
				if (this.mutator != null)
				{
					targs = this.mutator.Mutate(targs);
				}
				typeSpec = typeSpec.MakeGenericType(this.Module, targs);
				this.set_result = MemberCache.GetMember<MethodSpec>(typeSpec, this.set_result);
				this.set_exception = MemberCache.GetMember<MethodSpec>(typeSpec, this.set_exception);
				methodSpec = MemberCache.GetMember<MethodSpec>(typeSpec, methodSpec);
				if (this.task != null)
				{
					this.task = MemberCache.GetMember<PropertySpec>(typeSpec, this.task);
				}
			}
			this.builder = base.AddCompilerGeneratedField("$builder", new TypeExpression(typeSpec, base.Location));
			Method method = new Method(this, new TypeExpression(this.Compiler.BuiltinTypes.Void, base.Location), Modifiers.PUBLIC | Modifiers.COMPILER_GENERATED | Modifiers.DEBUGGER_HIDDEN, new MemberName("SetStateMachine"), ParametersCompiled.CreateFullyResolved(new Parameter(new TypeExpression(iasyncStateMachine.TypeSpec, base.Location), "stateMachine", Parameter.Modifier.NONE, null, base.Location), iasyncStateMachine.TypeSpec), null);
			ToplevelBlock toplevelBlock = new ToplevelBlock(this.Compiler, method.ParameterInfo, base.Location, (Block.Flags)0);
			toplevelBlock.IsCompilerGenerated = true;
			method.Block = toplevelBlock;
			base.Members.Add(method);
			if (!base.DoDefineMembers())
			{
				return false;
			}
			MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(methodSpec, typeSpec, base.Location);
			methodGroupExpr.InstanceExpression = new FieldExpr(this.builder, base.Location);
			ParameterReference parameterReference = toplevelBlock.GetParameterReference(0, base.Location);
			parameterReference.Type = iasyncStateMachine.TypeSpec;
			parameterReference.eclass = ExprClass.Variable;
			Arguments arguments = new Arguments(1);
			arguments.Add(new Argument(parameterReference));
			method.Block.AddStatement(new StatementExpression(new Invocation(methodGroupExpr, arguments)));
			if (flag)
			{
				this.HoistedReturnValue = TemporaryVariableReference.Create(typeSpec.TypeArguments[0], base.StateMachineMethod.Block, base.Location);
			}
			return true;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00033FDC File Offset: 0x000321DC
		public void EmitAwaitOnCompletedDynamic(EmitContext ec, FieldExpr awaiter)
		{
			PredefinedType icriticalNotifyCompletion = this.Module.PredefinedTypes.ICriticalNotifyCompletion;
			if (!icriticalNotifyCompletion.Define())
			{
				throw new NotImplementedException();
			}
			LocalTemporary localTemporary = new LocalTemporary(icriticalNotifyCompletion.TypeSpec);
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			awaiter.Emit(ec);
			ec.Emit(OpCodes.Isinst, icriticalNotifyCompletion.TypeSpec);
			localTemporary.Store(ec);
			localTemporary.Emit(ec);
			ec.Emit(OpCodes.Brtrue_S, label);
			LocalTemporary localTemporary2 = new LocalTemporary(this.Module.PredefinedTypes.INotifyCompletion.TypeSpec);
			awaiter.Emit(ec);
			ec.Emit(OpCodes.Castclass, localTemporary2.Type);
			localTemporary2.Store(ec);
			this.EmitOnCompleted(ec, localTemporary2, false);
			localTemporary2.Release(ec);
			ec.Emit(OpCodes.Br_S, label2);
			ec.MarkLabel(label);
			this.EmitOnCompleted(ec, localTemporary, true);
			ec.MarkLabel(label2);
			localTemporary.Release(ec);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x000340D0 File Offset: 0x000322D0
		public void EmitAwaitOnCompleted(EmitContext ec, FieldExpr awaiter)
		{
			bool unsafeVersion = false;
			if (this.Module.PredefinedTypes.ICriticalNotifyCompletion.Define())
			{
				unsafeVersion = awaiter.Type.ImplementsInterface(this.Module.PredefinedTypes.ICriticalNotifyCompletion.TypeSpec, false);
			}
			this.EmitOnCompleted(ec, awaiter, unsafeVersion);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00034124 File Offset: 0x00032324
		private void EmitOnCompleted(EmitContext ec, Expression awaiter, bool unsafeVersion)
		{
			PredefinedMembers predefinedMembers = this.Module.PredefinedMembers;
			bool flag = false;
			PredefinedMember<MethodSpec> predefinedMember;
			if (this.return_type.Kind == MemberKind.Void)
			{
				predefinedMember = (unsafeVersion ? predefinedMembers.AsyncVoidMethodBuilderOnCompletedUnsafe : predefinedMembers.AsyncVoidMethodBuilderOnCompleted);
			}
			else if (this.return_type == this.Module.PredefinedTypes.Task.TypeSpec)
			{
				predefinedMember = (unsafeVersion ? predefinedMembers.AsyncTaskMethodBuilderOnCompletedUnsafe : predefinedMembers.AsyncTaskMethodBuilderOnCompleted);
			}
			else
			{
				predefinedMember = (unsafeVersion ? predefinedMembers.AsyncTaskMethodBuilderGenericOnCompletedUnsafe : predefinedMembers.AsyncTaskMethodBuilderGenericOnCompleted);
				flag = true;
			}
			MethodSpec methodSpec = predefinedMember.Resolve(base.Location);
			if (methodSpec == null)
			{
				return;
			}
			if (flag)
			{
				methodSpec = MemberCache.GetMember<MethodSpec>(this.set_result.DeclaringType, methodSpec);
			}
			methodSpec = methodSpec.MakeGenericMethod(this, new TypeSpec[]
			{
				awaiter.Type,
				ec.CurrentType
			});
			MethodSpec methodSpec2 = methodSpec;
			MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(methodSpec2, methodSpec2.DeclaringType, base.Location);
			methodGroupExpr.InstanceExpression = new FieldExpr(this.builder, base.Location)
			{
				InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, base.Location)
			};
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(awaiter, Argument.AType.Ref));
			arguments.Add(new Argument(new CompilerGeneratedThis(this.CurrentType, base.Location), Argument.AType.Ref));
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				methodGroupExpr.EmitCall(ec, arguments, true);
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003429C File Offset: 0x0003249C
		public void EmitInitializer(EmitContext ec)
		{
			if (this.builder == null)
			{
				return;
			}
			TemporaryVariableReference temporaryVariableReference = (TemporaryVariableReference)this.Instance;
			FieldSpec fieldSpec = this.builder.Spec;
			if (base.MemberName.Arity > 0)
			{
				fieldSpec = MemberCache.GetMember<FieldSpec>(temporaryVariableReference.Type, fieldSpec);
			}
			if (this.builder_factory.DeclaringType.IsGeneric)
			{
				TypeSpec[] typeArguments = this.return_type.TypeArguments;
				InflatedTypeSpec container = this.builder_factory.DeclaringType.MakeGenericType(this.Module, typeArguments);
				this.builder_factory = MemberCache.GetMember<MethodSpec>(container, this.builder_factory);
				this.builder_start = MemberCache.GetMember<MethodSpec>(container, this.builder_start);
			}
			temporaryVariableReference.AddressOf(ec, AddressOp.Store);
			ec.Emit(OpCodes.Call, this.builder_factory);
			ec.Emit(OpCodes.Stfld, fieldSpec);
			temporaryVariableReference.AddressOf(ec, AddressOp.Store);
			ec.Emit(OpCodes.Ldflda, fieldSpec);
			if (this.Task != null)
			{
				ec.Emit(OpCodes.Dup);
			}
			temporaryVariableReference.AddressOf(ec, AddressOp.Store);
			ec.Emit(OpCodes.Call, this.builder_start.MakeGenericMethod(this.Module, new TypeSpec[]
			{
				temporaryVariableReference.Type
			}));
			if (this.Task != null)
			{
				MethodSpec methodSpec = this.Task.Get;
				if (base.MemberName.Arity > 0)
				{
					methodSpec = MemberCache.GetMember<MethodSpec>(fieldSpec.MemberType, methodSpec);
				}
				new PropertyExpr(this.Task, base.Location)
				{
					InstanceExpression = EmptyExpression.Null,
					Getter = methodSpec
				}.Emit(ec);
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003441C File Offset: 0x0003261C
		public void EmitSetException(EmitContext ec, LocalVariableReference exceptionVariable)
		{
			MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(this.set_exception, this.set_exception.DeclaringType, base.Location);
			methodGroupExpr.InstanceExpression = new FieldExpr(this.builder, base.Location)
			{
				InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, base.Location)
			};
			Arguments arguments = new Arguments(1);
			arguments.Add(new Argument(exceptionVariable));
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				methodGroupExpr.EmitCall(ec, arguments, true);
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x000344BC File Offset: 0x000326BC
		public void EmitSetResult(EmitContext ec)
		{
			MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(this.set_result, this.set_result.DeclaringType, base.Location);
			methodGroupExpr.InstanceExpression = new FieldExpr(this.builder, base.Location)
			{
				InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, base.Location)
			};
			Arguments arguments;
			if (this.HoistedReturnValue == null)
			{
				arguments = new Arguments(0);
			}
			else
			{
				arguments = new Arguments(1);
				arguments.Add(new Argument(this.HoistedReturnValue));
			}
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				methodGroupExpr.EmitCall(ec, arguments, true);
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00034570 File Offset: 0x00032770
		protected override TypeSpec[] ResolveBaseTypes(out FullNamedExpression base_class)
		{
			this.base_type = this.Compiler.BuiltinTypes.ValueType;
			base_class = null;
			PredefinedType iasyncStateMachine = this.Module.PredefinedTypes.IAsyncStateMachine;
			if (iasyncStateMachine.Define())
			{
				return new TypeSpec[]
				{
					iasyncStateMachine.TypeSpec
				};
			}
			return null;
		}

		// Token: 0x04000675 RID: 1653
		private int awaiters;

		// Token: 0x04000676 RID: 1654
		private Field builder;

		// Token: 0x04000677 RID: 1655
		private readonly TypeSpec return_type;

		// Token: 0x04000678 RID: 1656
		private MethodSpec set_result;

		// Token: 0x04000679 RID: 1657
		private MethodSpec set_exception;

		// Token: 0x0400067A RID: 1658
		private MethodSpec builder_factory;

		// Token: 0x0400067B RID: 1659
		private MethodSpec builder_start;

		// Token: 0x0400067C RID: 1660
		private PropertySpec task;

		// Token: 0x0400067D RID: 1661
		private int locals_captured;

		// Token: 0x0400067E RID: 1662
		private Dictionary<TypeSpec, List<Field>> stack_fields;

		// Token: 0x0400067F RID: 1663
		private Dictionary<TypeSpec, List<Field>> awaiter_fields;

		// Token: 0x04000680 RID: 1664
		[CompilerGenerated]
		private Expression <HoistedReturnValue>k__BackingField;
	}
}

using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000136 RID: 310
	internal struct CallEmitter
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x00040929 File Offset: 0x0003EB29
		public void Emit(EmitContext ec, MethodSpec method, Arguments Arguments, Location loc)
		{
			this.EmitPredefined(ec, method, Arguments, false, new Location?(loc));
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0004093C File Offset: 0x0003EB3C
		public void EmitStatement(EmitContext ec, MethodSpec method, Arguments Arguments, Location loc)
		{
			this.EmitPredefined(ec, method, Arguments, true, new Location?(loc));
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00040950 File Offset: 0x0003EB50
		public void EmitPredefined(EmitContext ec, MethodSpec method, Arguments Arguments, bool statement = false, Location? loc = null)
		{
			Expression expression = null;
			if (!this.HasAwaitArguments && ec.HasSet(BuilderContext.Options.AsyncBody))
			{
				this.HasAwaitArguments = (Arguments != null && Arguments.ContainsEmitWithAwait());
				if (this.HasAwaitArguments && this.InstanceExpressionOnStack)
				{
					throw new NotSupportedException();
				}
			}
			LocalTemporary localTemporary = null;
			OpCode opCode;
			if (method.IsStatic)
			{
				opCode = OpCodes.Call;
			}
			else
			{
				opCode = (CallEmitter.IsVirtualCallRequired(this.InstanceExpression, method) ? OpCodes.Callvirt : OpCodes.Call);
				if (this.HasAwaitArguments)
				{
					expression = this.InstanceExpression.EmitToField(ec);
					InstanceEmitter instanceEmitter = new InstanceEmitter(expression, CallEmitter.IsAddressCall(expression, opCode, method.DeclaringType));
					if (Arguments == null)
					{
						instanceEmitter.EmitLoad(ec, true);
					}
				}
				else if (!this.InstanceExpressionOnStack)
				{
					InstanceEmitter instanceEmitter2 = new InstanceEmitter(this.InstanceExpression, CallEmitter.IsAddressCall(this.InstanceExpression, opCode, method.DeclaringType));
					instanceEmitter2.Emit(ec, this.ConditionalAccess);
					if (this.DuplicateArguments)
					{
						ec.Emit(OpCodes.Dup);
						if (Arguments != null && Arguments.Count != 0)
						{
							localTemporary = new LocalTemporary(instanceEmitter2.GetStackType(ec));
							localTemporary.Store(ec);
							expression = localTemporary;
						}
					}
				}
			}
			if (Arguments != null && !this.InstanceExpressionOnStack)
			{
				this.EmittedArguments = Arguments.Emit(ec, this.DuplicateArguments, this.HasAwaitArguments);
				if (this.EmittedArguments != null)
				{
					if (expression != null)
					{
						InstanceEmitter instanceEmitter3 = new InstanceEmitter(expression, CallEmitter.IsAddressCall(expression, opCode, method.DeclaringType));
						instanceEmitter3.Emit(ec, this.ConditionalAccess);
						if (localTemporary != null)
						{
							localTemporary.Release(ec);
						}
					}
					this.EmittedArguments.Emit(ec);
				}
			}
			if (opCode == OpCodes.Callvirt && (this.InstanceExpression.Type.IsGenericParameter || this.InstanceExpression.Type.IsStructOrEnum))
			{
				ec.Emit(OpCodes.Constrained, this.InstanceExpression.Type);
			}
			if (loc != null)
			{
				ec.MarkCallEntry(loc.Value);
			}
			this.InstanceExpression = expression;
			if (method.Parameters.HasArglist)
			{
				Type[] varargsTypes = CallEmitter.GetVarargsTypes(method, Arguments);
				ec.Emit(opCode, method, varargsTypes);
			}
			else
			{
				ec.Emit(opCode, method);
			}
			if (statement && method.ReturnType.Kind != MemberKind.Void)
			{
				ec.Emit(OpCodes.Pop);
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00040B84 File Offset: 0x0003ED84
		private static Type[] GetVarargsTypes(MethodSpec method, Arguments arguments)
		{
			AParametersCollection parameters = method.Parameters;
			return ((Arglist)arguments[parameters.Count - 1].Expr).ArgumentTypes;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00040BB8 File Offset: 0x0003EDB8
		private static bool IsVirtualCallRequired(Expression instance, MethodSpec method)
		{
			TypeSpec declaringType = method.DeclaringType;
			return !declaringType.IsStruct && !declaringType.IsEnum && !(instance is BaseThis) && (method.IsVirtual || !Expression.IsNeverNull(instance) || instance.Type.IsGenericParameter);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00040C08 File Offset: 0x0003EE08
		private static bool IsAddressCall(Expression instance, OpCode callOpcode, TypeSpec declaringType)
		{
			TypeSpec type = instance.Type;
			return (type.IsStructOrEnum && (callOpcode == OpCodes.Callvirt || (callOpcode == OpCodes.Call && declaringType.IsStruct))) || type.IsGenericParameter || declaringType.IsNullableType;
		}

		// Token: 0x04000723 RID: 1827
		public Expression InstanceExpression;

		// Token: 0x04000724 RID: 1828
		public bool DuplicateArguments;

		// Token: 0x04000725 RID: 1829
		public bool InstanceExpressionOnStack;

		// Token: 0x04000726 RID: 1830
		public bool HasAwaitArguments;

		// Token: 0x04000727 RID: 1831
		public bool ConditionalAccess;

		// Token: 0x04000728 RID: 1832
		public Arguments EmittedArguments;
	}
}

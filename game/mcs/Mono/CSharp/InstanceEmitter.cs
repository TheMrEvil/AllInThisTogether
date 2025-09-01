using System;
using System.Reflection.Emit;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000137 RID: 311
	public struct InstanceEmitter
	{
		// Token: 0x06000FC9 RID: 4041 RVA: 0x00040C56 File Offset: 0x0003EE56
		public InstanceEmitter(Expression instance, bool addressLoad)
		{
			this.instance = instance;
			this.addressRequired = addressLoad;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00040C68 File Offset: 0x0003EE68
		public void Emit(EmitContext ec, bool conditionalAccess)
		{
			if (conditionalAccess && Expression.IsNeverNull(this.instance))
			{
				conditionalAccess = false;
			}
			Label label;
			Unwrap unwrap;
			if (conditionalAccess)
			{
				label = ec.DefineLabel();
				unwrap = (this.instance as Unwrap);
			}
			else
			{
				label = default(Label);
				unwrap = null;
			}
			IMemoryLocation memoryLocation = null;
			bool flag = false;
			if (unwrap != null)
			{
				unwrap.Store(ec);
				unwrap.EmitCheck(ec);
				ec.Emit(OpCodes.Brtrue_S, label);
			}
			else
			{
				if (conditionalAccess && this.addressRequired)
				{
					memoryLocation = (this.instance as VariableReference);
					if (memoryLocation == null)
					{
						memoryLocation = (this.instance as LocalTemporary);
					}
					if (memoryLocation == null)
					{
						this.EmitLoad(ec, false);
						ec.Emit(OpCodes.Dup);
						ec.EmitLoadFromPtr(this.instance.Type);
						flag = true;
					}
					else
					{
						this.instance.Emit(ec);
					}
				}
				else
				{
					this.EmitLoad(ec, !conditionalAccess);
					if (conditionalAccess)
					{
						flag = !this.IsInexpensiveLoad();
						if (flag)
						{
							ec.Emit(OpCodes.Dup);
						}
					}
				}
				if (conditionalAccess)
				{
					if (this.instance.Type.Kind == MemberKind.TypeParameter)
					{
						ec.Emit(OpCodes.Box, this.instance.Type);
					}
					ec.Emit(OpCodes.Brtrue_S, label);
					if (flag)
					{
						ec.Emit(OpCodes.Pop);
					}
				}
			}
			if (conditionalAccess)
			{
				if (!ec.ConditionalAccess.Statement)
				{
					if (ec.ConditionalAccess.Type.IsNullableType)
					{
						LiftedNull.Create(ec.ConditionalAccess.Type, Location.Null).Emit(ec);
					}
					else
					{
						ec.EmitNull();
					}
				}
				ec.Emit(OpCodes.Br, ec.ConditionalAccess.EndLabel);
				ec.MarkLabel(label);
				if (memoryLocation != null)
				{
					memoryLocation.AddressOf(ec, AddressOp.Load);
					return;
				}
				if (unwrap != null)
				{
					unwrap.Emit(ec);
					LocalBuilder temporaryLocal = ec.GetTemporaryLocal(unwrap.Type);
					ec.Emit(OpCodes.Stloc, temporaryLocal);
					ec.Emit(OpCodes.Ldloca, temporaryLocal);
					ec.FreeTemporaryLocal(temporaryLocal, unwrap.Type);
					return;
				}
				if (!flag)
				{
					this.instance.Emit(ec);
				}
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00040E60 File Offset: 0x0003F060
		public void EmitLoad(EmitContext ec, bool boxInstance)
		{
			TypeSpec type = this.instance.Type;
			if (!this.addressRequired)
			{
				this.instance.Emit(ec);
				if (boxInstance && this.RequiresBoxing())
				{
					ec.Emit(OpCodes.Box, type);
				}
				return;
			}
			IMemoryLocation memoryLocation = this.instance as IMemoryLocation;
			if (memoryLocation != null)
			{
				memoryLocation.AddressOf(ec, AddressOp.Load);
				return;
			}
			LocalTemporary localTemporary = new LocalTemporary(type);
			this.instance.Emit(ec);
			localTemporary.Store(ec);
			localTemporary.AddressOf(ec, AddressOp.Load);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00040EE0 File Offset: 0x0003F0E0
		public TypeSpec GetStackType(EmitContext ec)
		{
			TypeSpec type = this.instance.Type;
			if (this.addressRequired)
			{
				return ReferenceContainer.MakeType(ec.Module, type);
			}
			if (type.IsStructOrEnum)
			{
				return ec.Module.Compiler.BuiltinTypes.Object;
			}
			return type;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00040F30 File Offset: 0x0003F130
		private bool RequiresBoxing()
		{
			TypeSpec type = this.instance.Type;
			return (type.IsGenericParameter && !(this.instance is This) && TypeSpec.IsReferenceType(type)) || type.IsStructOrEnum;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00040F74 File Offset: 0x0003F174
		private bool IsInexpensiveLoad()
		{
			if (this.instance is Constant)
			{
				return this.instance.IsSideEffectFree;
			}
			if (this.RequiresBoxing())
			{
				return false;
			}
			VariableReference variableReference = this.instance as VariableReference;
			if (variableReference != null)
			{
				return !variableReference.IsRef && !variableReference.IsHoisted;
			}
			if (this.instance is LocalTemporary)
			{
				return true;
			}
			FieldExpr fieldExpr = this.instance as FieldExpr;
			return fieldExpr != null && (fieldExpr.IsStatic || fieldExpr.InstanceExpression is This);
		}

		// Token: 0x04000729 RID: 1833
		private readonly Expression instance;

		// Token: 0x0400072A RID: 1834
		private readonly bool addressRequired;
	}
}

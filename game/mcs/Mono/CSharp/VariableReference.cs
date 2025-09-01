using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001E4 RID: 484
	public abstract class VariableReference : Expression, IAssignMethod, IMemoryLocation, IVariableReference, IFixedExpression
	{
		// Token: 0x06001930 RID: 6448
		public abstract HoistedVariable GetHoistedVariable(AnonymousExpression ae);

		// Token: 0x06001931 RID: 6449
		public abstract void SetHasAddressTaken();

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001932 RID: 6450
		// (set) Token: 0x06001933 RID: 6451
		public abstract bool IsLockedByStatement { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001934 RID: 6452
		public abstract bool IsFixed { get; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001935 RID: 6453
		public abstract bool IsRef { get; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001936 RID: 6454
		public abstract string Name { get; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001937 RID: 6455
		protected abstract ILocalVariable Variable { get; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001938 RID: 6456
		public abstract VariableInfo VariableInfo { get; }

		// Token: 0x06001939 RID: 6457 RVA: 0x0007CAB4 File Offset: 0x0007ACB4
		public virtual void AddressOf(EmitContext ec, AddressOp mode)
		{
			HoistedVariable hoistedVariable = this.GetHoistedVariable(ec);
			if (hoistedVariable != null)
			{
				hoistedVariable.AddressOf(ec, mode);
				return;
			}
			this.Variable.EmitAddressOf(ec);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0007CAE4 File Offset: 0x0007ACE4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			HoistedVariable hoistedVariable = this.GetHoistedVariable(ec);
			if (hoistedVariable != null)
			{
				return hoistedVariable.CreateExpressionTree();
			}
			Arguments arguments = new Arguments(1);
			arguments.Add(new Argument(this));
			return base.CreateExpressionFactoryCall(ec, "Constant", arguments);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0007CB23 File Offset: 0x0007AD23
		public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			if (this.IsLockedByStatement)
			{
				rc.Report.Warning(728, 2, this.loc, "Possibly incorrect assignment to `{0}' which is the argument to a using or lock statement", this.Name);
			}
			return this;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0007CB50 File Offset: 0x0007AD50
		public override void Emit(EmitContext ec)
		{
			this.Emit(ec, false);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void EmitSideEffect(EmitContext ec)
		{
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0007CB5A File Offset: 0x0007AD5A
		public void EmitLoad(EmitContext ec)
		{
			this.Variable.Emit(ec);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0007CB68 File Offset: 0x0007AD68
		public void Emit(EmitContext ec, bool leave_copy)
		{
			HoistedVariable hoistedVariable = this.GetHoistedVariable(ec);
			if (hoistedVariable != null)
			{
				hoistedVariable.Emit(ec, leave_copy);
				return;
			}
			this.EmitLoad(ec);
			if (this.IsRef)
			{
				ec.EmitLoadFromPtr(this.type);
			}
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				if (this.IsRef)
				{
					this.temp = new LocalTemporary(base.Type);
					this.temp.Store(ec);
				}
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0007CBD8 File Offset: 0x0007ADD8
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool prepare_for_load)
		{
			HoistedVariable hoistedVariable = this.GetHoistedVariable(ec);
			if (hoistedVariable != null)
			{
				hoistedVariable.EmitAssign(ec, source, leave_copy, prepare_for_load);
				return;
			}
			New @new = source as New;
			if (@new != null)
			{
				if (!@new.Emit(ec, this))
				{
					if (leave_copy)
					{
						this.EmitLoad(ec);
						if (this.IsRef)
						{
							ec.EmitLoadFromPtr(this.type);
						}
					}
					return;
				}
			}
			else
			{
				if (this.IsRef)
				{
					this.EmitLoad(ec);
				}
				source.Emit(ec);
			}
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				if (this.IsRef)
				{
					this.temp = new LocalTemporary(base.Type);
					this.temp.Store(ec);
				}
			}
			if (this.IsRef)
			{
				ec.EmitStoreFromPtr(this.type);
			}
			else
			{
				this.Variable.EmitAssign(ec);
			}
			if (this.temp != null)
			{
				this.temp.Emit(ec);
				this.temp.Release(ec);
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0007CCBC File Offset: 0x0007AEBC
		public override Expression EmitToField(EmitContext ec)
		{
			HoistedVariable hoistedVariable = this.GetHoistedVariable(ec);
			if (hoistedVariable != null)
			{
				return hoistedVariable.EmitToField(ec);
			}
			return base.EmitToField(ec);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0007CCE3 File Offset: 0x0007AEE3
		public HoistedVariable GetHoistedVariable(ResolveContext rc)
		{
			return this.GetHoistedVariable(rc.CurrentAnonymousMethod);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0007CCF1 File Offset: 0x0007AEF1
		public HoistedVariable GetHoistedVariable(EmitContext ec)
		{
			return this.GetHoistedVariable(ec.CurrentAnonymousMethod);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0007CCFF File Offset: 0x0007AEFF
		public override string GetSignatureForError()
		{
			return this.Name;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x0007CD07 File Offset: 0x0007AF07
		public bool IsHoisted
		{
			get
			{
				return this.GetHoistedVariable(null) != null;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00068BDB File Offset: 0x00066DDB
		protected VariableReference()
		{
		}

		// Token: 0x040009C6 RID: 2502
		private LocalTemporary temp;
	}
}

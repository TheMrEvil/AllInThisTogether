using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200011C RID: 284
	public class AsyncInitializer : StateMachineInitializer
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00033855 File Offset: 0x00031A55
		public AsyncInitializer(ParametersBlock block, TypeDefinition host, TypeSpec returnType) : base(block, host, returnType)
		{
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00033860 File Offset: 0x00031A60
		public override string ContainerType
		{
			get
			{
				return "async state machine block";
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00033867 File Offset: 0x00031A67
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x0003386F File Offset: 0x00031A6F
		public TypeSpec DelegateType
		{
			[CompilerGenerated]
			get
			{
				return this.<DelegateType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DelegateType>k__BackingField = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00033878 File Offset: 0x00031A78
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x00033880 File Offset: 0x00031A80
		public StackFieldExpr HoistedReturnState
		{
			[CompilerGenerated]
			get
			{
				return this.<HoistedReturnState>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HoistedReturnState>k__BackingField = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsIterator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00033889 File Offset: 0x00031A89
		public TypeInferenceContext ReturnTypeInference
		{
			get
			{
				return this.return_inference;
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00033894 File Offset: 0x00031A94
		protected override BlockContext CreateBlockContext(BlockContext bc)
		{
			BlockContext blockContext = base.CreateBlockContext(bc);
			AnonymousMethodBody anonymousMethodBody = bc.CurrentAnonymousMethod as AnonymousMethodBody;
			if (anonymousMethodBody != null)
			{
				this.return_inference = anonymousMethodBody.ReturnTypeInference;
			}
			blockContext.Set(ResolveContext.Options.TryScope);
			return blockContext;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override void Emit(EmitContext ec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x000338D0 File Offset: 0x00031AD0
		public void EmitCatchBlock(EmitContext ec)
		{
			LocalVariable localVariable = LocalVariable.CreateCompilerGenerated(ec.Module.Compiler.BuiltinTypes.Exception, this.block, base.Location);
			ec.BeginCatchBlock(localVariable.Type);
			localVariable.EmitAssign(ec);
			ec.EmitThis();
			ec.EmitInt(-1);
			ec.Emit(OpCodes.Stfld, this.storey.PC.Spec);
			((AsyncTaskStorey)this.Storey).EmitSetException(ec, new LocalVariableReference(localVariable, base.Location));
			ec.Emit(OpCodes.Leave, this.move_next_ok);
			ec.EndExceptionBlock();
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00033973 File Offset: 0x00031B73
		protected override void EmitMoveNextEpilogue(EmitContext ec)
		{
			((AsyncTaskStorey)this.Storey).EmitSetResult(ec);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00033986 File Offset: 0x00031B86
		public override void EmitStatement(EmitContext ec)
		{
			((AsyncTaskStorey)this.Storey).EmitInitializer(ec);
			ec.Emit(OpCodes.Ret);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void MarkReachable(Reachability rc)
		{
		}

		// Token: 0x04000672 RID: 1650
		private TypeInferenceContext return_inference;

		// Token: 0x04000673 RID: 1651
		[CompilerGenerated]
		private TypeSpec <DelegateType>k__BackingField;

		// Token: 0x04000674 RID: 1652
		[CompilerGenerated]
		private StackFieldExpr <HoistedReturnState>k__BackingField;
	}
}

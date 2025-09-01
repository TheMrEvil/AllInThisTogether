using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000111 RID: 273
	public class LocalTemporary : Expression, IMemoryLocation, IAssignMethod
	{
		// Token: 0x06000D8C RID: 3468 RVA: 0x000323B2 File Offset: 0x000305B2
		public LocalTemporary(TypeSpec t)
		{
			this.type = t;
			this.eclass = ExprClass.Value;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000323C8 File Offset: 0x000305C8
		public LocalTemporary(LocalBuilder b, TypeSpec t) : this(t)
		{
			this.builder = b;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000323D8 File Offset: 0x000305D8
		public void Release(EmitContext ec)
		{
			ec.FreeTemporaryLocal(this.builder, this.type);
			this.builder = null;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000323F4 File Offset: 0x000305F4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(1);
			arguments.Add(new Argument(this));
			return base.CreateExpressionFactoryCall(ec, "Constant", arguments);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00005936 File Offset: 0x00003B36
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			return this;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00032421 File Offset: 0x00030621
		public override void Emit(EmitContext ec)
		{
			if (this.builder == null)
			{
				throw new InternalErrorException("Emit without Store, or after Release");
			}
			ec.Emit(OpCodes.Ldloc, this.builder);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00032447 File Offset: 0x00030647
		public void Emit(EmitContext ec, bool leave_copy)
		{
			this.Emit(ec);
			if (leave_copy)
			{
				this.Emit(ec);
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003245A File Offset: 0x0003065A
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			if (isCompound)
			{
				throw new NotImplementedException();
			}
			source.Emit(ec);
			this.Store(ec);
			if (leave_copy)
			{
				this.Emit(ec);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003247E File Offset: 0x0003067E
		public LocalBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00032486 File Offset: 0x00030686
		public void Store(EmitContext ec)
		{
			if (this.builder == null)
			{
				this.builder = ec.GetTemporaryLocal(this.type);
			}
			ec.Emit(OpCodes.Stloc, this.builder);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000324B4 File Offset: 0x000306B4
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			if (this.builder == null)
			{
				this.builder = ec.GetTemporaryLocal(this.type);
			}
			if (this.builder.LocalType.IsByRef)
			{
				ec.Emit(OpCodes.Ldloc, this.builder);
				return;
			}
			ec.Emit(OpCodes.Ldloca, this.builder);
		}

		// Token: 0x04000661 RID: 1633
		private LocalBuilder builder;
	}
}

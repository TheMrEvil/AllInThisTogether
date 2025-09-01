using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001F5 RID: 501
	internal abstract class TypeOfMember<T> : Expression where T : MemberSpec
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x0008010A File Offset: 0x0007E30A
		protected TypeOfMember(T member, Location loc)
		{
			this.member = member;
			this.loc = loc;
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsSideEffectFree
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00080120 File Offset: 0x0007E320
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this));
			arguments.Add(new Argument(new TypeOf(this.type, this.loc)));
			return base.CreateExpressionFactoryCall(ec, "Constant", arguments);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00080169 File Offset: 0x0007E369
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00080174 File Offset: 0x0007E374
		public override void Emit(EmitContext ec)
		{
			PredefinedMember<MethodSpec> predefinedMember;
			if (this.member.DeclaringType.IsGenericOrParentIsGeneric)
			{
				predefinedMember = this.GetTypeFromHandleGeneric(ec);
				ec.Emit(OpCodes.Ldtoken, this.member.DeclaringType);
			}
			else
			{
				predefinedMember = this.GetTypeFromHandle(ec);
			}
			MethodSpec methodSpec = predefinedMember.Resolve(this.loc);
			if (methodSpec != null)
			{
				ec.Emit(OpCodes.Call, methodSpec);
			}
		}

		// Token: 0x06001A1D RID: 6685
		protected abstract PredefinedMember<MethodSpec> GetTypeFromHandle(EmitContext ec);

		// Token: 0x06001A1E RID: 6686
		protected abstract PredefinedMember<MethodSpec> GetTypeFromHandleGeneric(EmitContext ec);

		// Token: 0x040009E3 RID: 2531
		protected readonly T member;
	}
}

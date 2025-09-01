using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001F4 RID: 500
	internal sealed class TypeOfMethod : TypeOfMember<MethodSpec>
	{
		// Token: 0x06001A12 RID: 6674 RVA: 0x0008004E File Offset: 0x0007E24E
		public TypeOfMethod(MethodSpec method, Location loc) : base(method, loc)
		{
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00080058 File Offset: 0x0007E258
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.member.IsConstructor)
			{
				this.type = ec.Module.PredefinedTypes.ConstructorInfo.Resolve();
			}
			else
			{
				this.type = ec.Module.PredefinedTypes.MethodInfo.Resolve();
			}
			if (this.type == null)
			{
				return null;
			}
			return base.DoResolve(ec);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000800BB File Offset: 0x0007E2BB
		public override void Emit(EmitContext ec)
		{
			ec.Emit(OpCodes.Ldtoken, this.member);
			base.Emit(ec);
			ec.Emit(OpCodes.Castclass, this.type);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000800E6 File Offset: 0x0007E2E6
		protected override PredefinedMember<MethodSpec> GetTypeFromHandle(EmitContext ec)
		{
			return ec.Module.PredefinedMembers.MethodInfoGetMethodFromHandle;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000800F8 File Offset: 0x0007E2F8
		protected override PredefinedMember<MethodSpec> GetTypeFromHandleGeneric(EmitContext ec)
		{
			return ec.Module.PredefinedMembers.MethodInfoGetMethodFromHandle2;
		}
	}
}

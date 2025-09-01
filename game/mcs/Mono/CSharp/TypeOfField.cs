using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001F6 RID: 502
	internal sealed class TypeOfField : TypeOfMember<FieldSpec>
	{
		// Token: 0x06001A1F RID: 6687 RVA: 0x000801E1 File Offset: 0x0007E3E1
		public TypeOfField(FieldSpec field, Location loc) : base(field, loc)
		{
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000801EB File Offset: 0x0007E3EB
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.type = ec.Module.PredefinedTypes.FieldInfo.Resolve();
			if (this.type == null)
			{
				return null;
			}
			return base.DoResolve(ec);
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00080219 File Offset: 0x0007E419
		public override void Emit(EmitContext ec)
		{
			ec.Emit(OpCodes.Ldtoken, this.member);
			base.Emit(ec);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00080233 File Offset: 0x0007E433
		protected override PredefinedMember<MethodSpec> GetTypeFromHandle(EmitContext ec)
		{
			return ec.Module.PredefinedMembers.FieldInfoGetFieldFromHandle;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00080245 File Offset: 0x0007E445
		protected override PredefinedMember<MethodSpec> GetTypeFromHandleGeneric(EmitContext ec)
		{
			return ec.Module.PredefinedMembers.FieldInfoGetFieldFromHandle2;
		}
	}
}

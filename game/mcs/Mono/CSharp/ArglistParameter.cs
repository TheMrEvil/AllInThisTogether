using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200026A RID: 618
	public class ArglistParameter : Parameter
	{
		// Token: 0x06001E57 RID: 7767 RVA: 0x000958D5 File Offset: 0x00093AD5
		public ArglistParameter(Location loc) : base(null, string.Empty, Parameter.Modifier.NONE, null, loc)
		{
			this.parameter_type = InternalType.Arglist;
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void ApplyAttributes(MethodBuilder mb, ConstructorBuilder cb, int index, PredefinedAttributes pa)
		{
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool CheckAccessibility(InterfaceMemberBase member)
		{
			return true;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000958F1 File Offset: 0x00093AF1
		public override TypeSpec Resolve(IMemberContext ec, int index)
		{
			return this.parameter_type;
		}
	}
}

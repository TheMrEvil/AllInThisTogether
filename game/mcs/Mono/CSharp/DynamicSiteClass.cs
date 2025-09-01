using System;

namespace Mono.CSharp
{
	// Token: 0x0200016E RID: 366
	public sealed class DynamicSiteClass : HoistedStoreyClass
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x0004907F File Offset: 0x0004727F
		public DynamicSiteClass(TypeDefinition parent, MemberBase host, TypeParameters tparams) : base(parent, CompilerGeneratedContainer.MakeMemberName(host, "DynamicSite", parent.DynamicSitesCounter, tparams, Location.Null), tparams, Modifiers.STATIC, MemberKind.Class)
		{
			parent.DynamicSitesCounter++;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000490B8 File Offset: 0x000472B8
		public FieldSpec CreateCallSiteField(FullNamedExpression type, Location loc)
		{
			int anonymousMethodsCounter = this.AnonymousMethodsCounter;
			this.AnonymousMethodsCounter = anonymousMethodsCounter + 1;
			int num = anonymousMethodsCounter;
			Field field = new HoistedStoreyClass.HoistedField(this, type, Modifiers.PUBLIC | Modifiers.STATIC, "Site" + num.ToString("X"), null, loc);
			field.Define();
			base.AddField(field);
			return field.Spec;
		}
	}
}

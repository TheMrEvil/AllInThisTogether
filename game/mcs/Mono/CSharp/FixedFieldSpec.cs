using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x020002DB RID: 731
	internal class FixedFieldSpec : FieldSpec
	{
		// Token: 0x060022BD RID: 8893 RVA: 0x000AB2D5 File Offset: 0x000A94D5
		public FixedFieldSpec(ModuleContainer module, TypeSpec declaringType, IMemberDefinition definition, FieldInfo info, FieldSpec element, Modifiers modifiers) : base(declaringType, definition, PointerContainer.MakeType(module, element.MemberType), info, modifiers)
		{
			this.element = element;
			this.state &= ~MemberSpec.StateFlags.CLSCompliant_Undetected;
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x000AB307 File Offset: 0x000A9507
		public FieldSpec Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x000AB30F File Offset: 0x000A950F
		public TypeSpec ElementType
		{
			get
			{
				return this.element.MemberType;
			}
		}

		// Token: 0x04000D63 RID: 3427
		private readonly FieldSpec element;
	}
}

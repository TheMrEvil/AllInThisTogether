using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000131 RID: 305
	public sealed class Interface : TypeDefinition
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x0003E670 File Offset: 0x0003C870
		public Interface(TypeContainer parent, MemberName name, Modifiers mod, Attributes attrs) : base(parent, name, attrs, MemberKind.Interface)
		{
			Modifiers def_access = base.IsTopLevel ? Modifiers.INTERNAL : Modifiers.PRIVATE;
			base.ModFlags = ModifiersExtensions.Check(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.UNSAFE, mod, def_access, name.Location, base.Report);
			this.spec = new TypeSpec(this.Kind, null, this, null, base.ModFlags);
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0003E6D1 File Offset: 0x0003C8D1
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Interface;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		protected override TypeAttributes TypeAttr
		{
			get
			{
				return base.TypeAttr | (TypeAttributes.ClassSemanticsMask | TypeAttributes.Abstract);
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003E6E6 File Offset: 0x0003C8E6
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003E6EF File Offset: 0x0003C8EF
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.ComImport && !this.attributes.Contains(pa.Guid))
			{
				a.Error_MissingGuidAttribute();
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0003E72C File Offset: 0x0003C92C
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			if (this.iface_exprs != null)
			{
				foreach (TypeSpec typeSpec in this.iface_exprs)
				{
					if (!typeSpec.IsCLSCompliant())
					{
						base.Report.SymbolRelatedToPreviousError(typeSpec);
						base.Report.Warning(3027, 1, base.Location, "`{0}' is not CLS-compliant because base interface `{1}' is not CLS-compliant", this.GetSignatureForError(), typeSpec.GetSignatureForError());
					}
				}
			}
			return true;
		}

		// Token: 0x04000700 RID: 1792
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.UNSAFE;
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C2 RID: 706
	public class UxmlRootElementFactory : UxmlFactory<VisualElement, UxmlRootElementTraits>
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00063E81 File Offset: 0x00062081
		public override string uxmlName
		{
			get
			{
				return "UXML";
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00063E88 File Offset: 0x00062088
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00063EA0 File Offset: 0x000620A0
		public override string substituteForTypeName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00063EA0 File Offset: 0x000620A0
		public override string substituteForTypeNamespace
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00063EA0 File Offset: 0x000620A0
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00063EA8 File Offset: 0x000620A8
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00063EBB File Offset: 0x000620BB
		public UxmlRootElementFactory()
		{
		}

		// Token: 0x04000A4C RID: 2636
		internal const string k_ElementName = "UXML";
	}
}

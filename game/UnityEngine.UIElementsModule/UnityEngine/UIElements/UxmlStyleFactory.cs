using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C5 RID: 709
	public class UxmlStyleFactory : UxmlFactory<VisualElement, UxmlStyleTraits>
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00063FE8 File Offset: 0x000621E8
		public override string uxmlName
		{
			get
			{
				return "Style";
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00063FEF File Offset: 0x000621EF
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00064007 File Offset: 0x00062207
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00064018 File Offset: 0x00062218
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00064032 File Offset: 0x00062232
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00064044 File Offset: 0x00062244
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00064057 File Offset: 0x00062257
		public UxmlStyleFactory()
		{
		}

		// Token: 0x04000A53 RID: 2643
		internal const string k_ElementName = "Style";
	}
}

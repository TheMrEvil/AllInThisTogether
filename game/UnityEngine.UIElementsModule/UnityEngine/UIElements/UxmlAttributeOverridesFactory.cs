using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CB RID: 715
	public class UxmlAttributeOverridesFactory : UxmlFactory<VisualElement, UxmlAttributeOverridesTraits>
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x000642DC File Offset: 0x000624DC
		public override string uxmlName
		{
			get
			{
				return "AttributeOverrides";
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x000642E3 File Offset: 0x000624E3
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x00064007 File Offset: 0x00062207
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x00064018 File Offset: 0x00062218
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00064032 File Offset: 0x00062232
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000642FC File Offset: 0x000624FC
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0006430F File Offset: 0x0006250F
		public UxmlAttributeOverridesFactory()
		{
		}

		// Token: 0x04000A63 RID: 2659
		internal const string k_ElementName = "AttributeOverrides";
	}
}

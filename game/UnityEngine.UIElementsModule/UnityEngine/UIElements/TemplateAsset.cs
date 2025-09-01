using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CE RID: 718
	[Serializable]
	internal class TemplateAsset : VisualElementAsset
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00064400 File Offset: 0x00062600
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x00064418 File Offset: 0x00062618
		public string templateAlias
		{
			get
			{
				return this.m_TemplateAlias;
			}
			set
			{
				this.m_TemplateAlias = value;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00064424 File Offset: 0x00062624
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x00064454 File Offset: 0x00062654
		public List<TemplateAsset.AttributeOverride> attributeOverrides
		{
			get
			{
				return (this.m_AttributeOverrides == null) ? (this.m_AttributeOverrides = new List<TemplateAsset.AttributeOverride>()) : this.m_AttributeOverrides;
			}
			set
			{
				this.m_AttributeOverrides = value;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00064460 File Offset: 0x00062660
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x00064478 File Offset: 0x00062678
		internal List<VisualTreeAsset.SlotUsageEntry> slotUsages
		{
			get
			{
				return this.m_SlotUsages;
			}
			set
			{
				this.m_SlotUsages = value;
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00064482 File Offset: 0x00062682
		public TemplateAsset(string templateAlias, string fullTypeName) : base(fullTypeName)
		{
			Assert.IsFalse(string.IsNullOrEmpty(templateAlias), "Template alias must not be null or empty");
			this.m_TemplateAlias = templateAlias;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000644A8 File Offset: 0x000626A8
		public void AddSlotUsage(string slotName, int resId)
		{
			bool flag = this.m_SlotUsages == null;
			if (flag)
			{
				this.m_SlotUsages = new List<VisualTreeAsset.SlotUsageEntry>();
			}
			this.m_SlotUsages.Add(new VisualTreeAsset.SlotUsageEntry(slotName, resId));
		}

		// Token: 0x04000A6A RID: 2666
		[SerializeField]
		private string m_TemplateAlias;

		// Token: 0x04000A6B RID: 2667
		[SerializeField]
		private List<TemplateAsset.AttributeOverride> m_AttributeOverrides;

		// Token: 0x04000A6C RID: 2668
		[SerializeField]
		private List<VisualTreeAsset.SlotUsageEntry> m_SlotUsages;

		// Token: 0x020002CF RID: 719
		[Serializable]
		public struct AttributeOverride
		{
			// Token: 0x04000A6D RID: 2669
			public string m_ElementName;

			// Token: 0x04000A6E RID: 2670
			public string m_AttributeName;

			// Token: 0x04000A6F RID: 2671
			public string m_Value;
		}
	}
}

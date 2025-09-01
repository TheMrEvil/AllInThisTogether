using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector.Internal;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200006A RID: 106
	[Conditional("UNITY_EDITOR")]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public class TabGroupAttribute : PropertyGroupAttribute, ISubGroupProviderAttribute
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00003892 File Offset: 0x00001A92
		public TabGroupAttribute(string tab, bool useFixedHeight = false, float order = 0f) : this("_DefaultTabGroup", tab, useFixedHeight, order)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000038A2 File Offset: 0x00001AA2
		public TabGroupAttribute(string group, string tab, bool useFixedHeight = false, float order = 0f) : base(group, order)
		{
			this.TabId = tab;
			this.UseFixedHeight = useFixedHeight;
			this.Tabs = new List<TabGroupAttribute>
			{
				this
			};
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000038CD File Offset: 0x00001ACD
		public TabGroupAttribute(string group, string tab, SdfIconType icon, bool useFixedHeight = false, float order = 0f) : this(group, tab, useFixedHeight, order)
		{
			this.Icon = icon;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000038E4 File Offset: 0x00001AE4
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			TabGroupAttribute tabGroupAttribute = other as TabGroupAttribute;
			if (tabGroupAttribute.TabId != null)
			{
				if (tabGroupAttribute.TabLayouting != TabLayouting.MultiRow)
				{
					this.TabLayouting = tabGroupAttribute.TabLayouting;
				}
				this.UseFixedHeight = (this.UseFixedHeight || tabGroupAttribute.UseFixedHeight);
				this.Paddingless = (this.Paddingless || tabGroupAttribute.Paddingless);
				this.HideTabGroupIfTabGroupOnlyHasOneTab = (this.HideTabGroupIfTabGroupOnlyHasOneTab || tabGroupAttribute.HideTabGroupIfTabGroupOnlyHasOneTab);
				bool flag = false;
				for (int i = 0; i < this.Tabs.Count; i++)
				{
					TabGroupAttribute tabGroupAttribute2 = this.Tabs[i];
					if (tabGroupAttribute2.TabId == tabGroupAttribute.TabId)
					{
						if (tabGroupAttribute2.TextColor == null)
						{
							tabGroupAttribute2.TextColor = tabGroupAttribute.TextColor;
						}
						if (tabGroupAttribute2.Icon == SdfIconType.None)
						{
							tabGroupAttribute2.Icon = tabGroupAttribute.Icon;
						}
						if (tabGroupAttribute2.TabName == null)
						{
							tabGroupAttribute2.TabName = tabGroupAttribute.TabName;
						}
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.Tabs.Add(tabGroupAttribute);
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000039E4 File Offset: 0x00001BE4
		IList<PropertyGroupAttribute> ISubGroupProviderAttribute.GetSubGroupAttributes()
		{
			int num = 0;
			List<PropertyGroupAttribute> list = new List<PropertyGroupAttribute>(this.Tabs.Count)
			{
				new TabGroupAttribute.TabSubGroupAttribute(this, this.GroupID + "/" + this.TabId, (float)num++)
			};
			foreach (TabGroupAttribute tabGroupAttribute in this.Tabs)
			{
				if (tabGroupAttribute.TabId != this.TabId)
				{
					list.Add(new TabGroupAttribute.TabSubGroupAttribute(tabGroupAttribute, this.GroupID + "/" + tabGroupAttribute.TabId, (float)num++));
				}
			}
			return list;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003AA8 File Offset: 0x00001CA8
		string ISubGroupProviderAttribute.RepathMemberAttribute(PropertyGroupAttribute attr)
		{
			TabGroupAttribute tabGroupAttribute = (TabGroupAttribute)attr;
			return this.GroupID + "/" + tabGroupAttribute.TabId;
		}

		// Token: 0x04000113 RID: 275
		public const string DEFAULT_NAME = "_DefaultTabGroup";

		// Token: 0x04000114 RID: 276
		public string TabName;

		// Token: 0x04000115 RID: 277
		public string TabId;

		// Token: 0x04000116 RID: 278
		public bool UseFixedHeight;

		// Token: 0x04000117 RID: 279
		public bool Paddingless;

		// Token: 0x04000118 RID: 280
		public bool HideTabGroupIfTabGroupOnlyHasOneTab;

		// Token: 0x04000119 RID: 281
		public string TextColor;

		// Token: 0x0400011A RID: 282
		public SdfIconType Icon;

		// Token: 0x0400011B RID: 283
		public TabLayouting TabLayouting;

		// Token: 0x0400011C RID: 284
		public List<TabGroupAttribute> Tabs;

		// Token: 0x0200009E RID: 158
		[Conditional("UNITY_EDITOR")]
		public class TabSubGroupAttribute : PropertyGroupAttribute
		{
			// Token: 0x060001D9 RID: 473 RVA: 0x00004666 File Offset: 0x00002866
			public TabSubGroupAttribute(TabGroupAttribute tab, string groupId, float order) : base(groupId, order)
			{
				this.Tab = tab;
			}

			// Token: 0x060001DA RID: 474 RVA: 0x00004678 File Offset: 0x00002878
			protected override void CombineValuesWith(PropertyGroupAttribute other)
			{
				TabGroupAttribute.TabSubGroupAttribute tabSubGroupAttribute = other as TabGroupAttribute.TabSubGroupAttribute;
				if (tabSubGroupAttribute != null)
				{
					if (this.Tab.TextColor == null)
					{
						this.Tab.TextColor = tabSubGroupAttribute.Tab.TextColor;
					}
					if (this.Tab.Icon == SdfIconType.None)
					{
						this.Tab.Icon = tabSubGroupAttribute.Tab.Icon;
					}
					if (this.Tab.TabName == null)
					{
						this.Tab.TabName = tabSubGroupAttribute.Tab.TabName;
					}
				}
			}

			// Token: 0x040008C5 RID: 2245
			public TabGroupAttribute Tab;
		}
	}
}

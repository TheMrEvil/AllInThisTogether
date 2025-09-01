using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000003 RID: 3
	[Conditional("UNITY_EDITOR")]
	public class AssetSelectorAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C9 File Offset: 0x000002C9
		// (set) Token: 0x06000002 RID: 2 RVA: 0x0000207C File Offset: 0x0000027C
		public string Paths
		{
			get
			{
				if (this.SearchInFolders != null)
				{
					return string.Join(",", this.SearchInFolders);
				}
				return null;
			}
			set
			{
				this.SearchInFolders = (from x in value.Split(new char[]
				{
					'|'
				})
				select x.Trim().Trim(new char[]
				{
					'/',
					'\\'
				})).ToArray<string>();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E5 File Offset: 0x000002E5
		public AssetSelectorAttribute()
		{
		}

		// Token: 0x04000007 RID: 7
		public bool IsUniqueList = true;

		// Token: 0x04000008 RID: 8
		public bool DrawDropdownForListElements = true;

		// Token: 0x04000009 RID: 9
		public bool DisableListAddButtonBehaviour;

		// Token: 0x0400000A RID: 10
		public bool ExcludeExistingValuesInList;

		// Token: 0x0400000B RID: 11
		public bool ExpandAllMenuItems = true;

		// Token: 0x0400000C RID: 12
		public bool FlattenTreeView;

		// Token: 0x0400000D RID: 13
		public int DropdownWidth;

		// Token: 0x0400000E RID: 14
		public int DropdownHeight;

		// Token: 0x0400000F RID: 15
		public string DropdownTitle;

		// Token: 0x04000010 RID: 16
		public string[] SearchInFolders;

		// Token: 0x04000011 RID: 17
		public string Filter;

		// Token: 0x0200009D RID: 157
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060001D6 RID: 470 RVA: 0x0000463D File Offset: 0x0000283D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x00004580 File Offset: 0x00002780
			public <>c()
			{
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x00004649 File Offset: 0x00002849
			internal string <set_Paths>b__12_0(string x)
			{
				return x.Trim().Trim(new char[]
				{
					'/',
					'\\'
				});
			}

			// Token: 0x040008C3 RID: 2243
			public static readonly AssetSelectorAttribute.<>c <>9 = new AssetSelectorAttribute.<>c();

			// Token: 0x040008C4 RID: 2244
			public static Func<string, string> <>9__12_0;
		}
	}
}

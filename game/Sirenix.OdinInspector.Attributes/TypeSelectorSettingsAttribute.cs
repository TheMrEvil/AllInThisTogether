using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000078 RID: 120
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class TypeSelectorSettingsAttribute : Attribute
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00003E92 File Offset: 0x00002092
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00003E9F File Offset: 0x0000209F
		public bool ShowNoneItem
		{
			get
			{
				return this.showNoneItem.GetValueOrDefault();
			}
			set
			{
				this.showNoneItem = new bool?(value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00003EAD File Offset: 0x000020AD
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00003EBA File Offset: 0x000020BA
		public bool ShowCategories
		{
			get
			{
				return this.showCategories.GetValueOrDefault();
			}
			set
			{
				this.showCategories = new bool?(value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00003EC8 File Offset: 0x000020C8
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00003ED5 File Offset: 0x000020D5
		public bool PreferNamespaces
		{
			get
			{
				return this.preferNamespaces.GetValueOrDefault();
			}
			set
			{
				this.preferNamespaces = new bool?(value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00003EE3 File Offset: 0x000020E3
		public bool ShowNoneItemIsSet
		{
			get
			{
				return this.showNoneItem != null;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003EF0 File Offset: 0x000020F0
		public bool ShowCategoriesIsSet
		{
			get
			{
				return this.showCategories != null;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00003EFD File Offset: 0x000020FD
		public bool PreferNamespacesIsSet
		{
			get
			{
				return this.preferNamespaces != null;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00002102 File Offset: 0x00000302
		public TypeSelectorSettingsAttribute()
		{
		}

		// Token: 0x04000155 RID: 341
		public const string FILTER_TYPES_FUNCTION_NAMED_VALUE = "type";

		// Token: 0x04000156 RID: 342
		public string FilterTypesFunction;

		// Token: 0x04000157 RID: 343
		private bool? showNoneItem;

		// Token: 0x04000158 RID: 344
		private bool? showCategories;

		// Token: 0x04000159 RID: 345
		private bool? preferNamespaces;
	}
}

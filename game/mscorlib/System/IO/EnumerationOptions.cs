using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x02000B30 RID: 2864
	public class EnumerationOptions
	{
		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x0600673E RID: 26430 RVA: 0x0015FF74 File Offset: 0x0015E174
		internal static EnumerationOptions Compatible
		{
			[CompilerGenerated]
			get
			{
				return EnumerationOptions.<Compatible>k__BackingField;
			}
		} = new EnumerationOptions
		{
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x0015FF7B File Offset: 0x0015E17B
		private static EnumerationOptions CompatibleRecursive
		{
			[CompilerGenerated]
			get
			{
				return EnumerationOptions.<CompatibleRecursive>k__BackingField;
			}
		} = new EnumerationOptions
		{
			RecurseSubdirectories = true,
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06006740 RID: 26432 RVA: 0x0015FF82 File Offset: 0x0015E182
		internal static EnumerationOptions Default
		{
			[CompilerGenerated]
			get
			{
				return EnumerationOptions.<Default>k__BackingField;
			}
		} = new EnumerationOptions();

		// Token: 0x06006741 RID: 26433 RVA: 0x0015FF89 File Offset: 0x0015E189
		public EnumerationOptions()
		{
			this.IgnoreInaccessible = true;
			this.AttributesToSkip = (FileAttributes.Hidden | FileAttributes.System);
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x0015FF9F File Offset: 0x0015E19F
		internal static EnumerationOptions FromSearchOption(SearchOption searchOption)
		{
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", "Enum value was out of legal range.");
			}
			if (searchOption != SearchOption.AllDirectories)
			{
				return EnumerationOptions.Compatible;
			}
			return EnumerationOptions.CompatibleRecursive;
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06006743 RID: 26435 RVA: 0x0015FFC7 File Offset: 0x0015E1C7
		// (set) Token: 0x06006744 RID: 26436 RVA: 0x0015FFCF File Offset: 0x0015E1CF
		public bool RecurseSubdirectories
		{
			[CompilerGenerated]
			get
			{
				return this.<RecurseSubdirectories>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RecurseSubdirectories>k__BackingField = value;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06006745 RID: 26437 RVA: 0x0015FFD8 File Offset: 0x0015E1D8
		// (set) Token: 0x06006746 RID: 26438 RVA: 0x0015FFE0 File Offset: 0x0015E1E0
		public bool IgnoreInaccessible
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreInaccessible>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreInaccessible>k__BackingField = value;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06006747 RID: 26439 RVA: 0x0015FFE9 File Offset: 0x0015E1E9
		// (set) Token: 0x06006748 RID: 26440 RVA: 0x0015FFF1 File Offset: 0x0015E1F1
		public int BufferSize
		{
			[CompilerGenerated]
			get
			{
				return this.<BufferSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BufferSize>k__BackingField = value;
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x0015FFFA File Offset: 0x0015E1FA
		// (set) Token: 0x0600674A RID: 26442 RVA: 0x00160002 File Offset: 0x0015E202
		public FileAttributes AttributesToSkip
		{
			[CompilerGenerated]
			get
			{
				return this.<AttributesToSkip>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AttributesToSkip>k__BackingField = value;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x0600674B RID: 26443 RVA: 0x0016000B File Offset: 0x0015E20B
		// (set) Token: 0x0600674C RID: 26444 RVA: 0x00160013 File Offset: 0x0015E213
		public MatchType MatchType
		{
			[CompilerGenerated]
			get
			{
				return this.<MatchType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MatchType>k__BackingField = value;
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x0600674D RID: 26445 RVA: 0x0016001C File Offset: 0x0015E21C
		// (set) Token: 0x0600674E RID: 26446 RVA: 0x00160024 File Offset: 0x0015E224
		public MatchCasing MatchCasing
		{
			[CompilerGenerated]
			get
			{
				return this.<MatchCasing>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MatchCasing>k__BackingField = value;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x0600674F RID: 26447 RVA: 0x0016002D File Offset: 0x0015E22D
		// (set) Token: 0x06006750 RID: 26448 RVA: 0x00160035 File Offset: 0x0015E235
		public bool ReturnSpecialDirectories
		{
			[CompilerGenerated]
			get
			{
				return this.<ReturnSpecialDirectories>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReturnSpecialDirectories>k__BackingField = value;
			}
		}

		// Token: 0x06006751 RID: 26449 RVA: 0x00160040 File Offset: 0x0015E240
		// Note: this type is marked as 'beforefieldinit'.
		static EnumerationOptions()
		{
		}

		// Token: 0x04003C26 RID: 15398
		[CompilerGenerated]
		private static readonly EnumerationOptions <Compatible>k__BackingField;

		// Token: 0x04003C27 RID: 15399
		[CompilerGenerated]
		private static readonly EnumerationOptions <CompatibleRecursive>k__BackingField;

		// Token: 0x04003C28 RID: 15400
		[CompilerGenerated]
		private static readonly EnumerationOptions <Default>k__BackingField;

		// Token: 0x04003C29 RID: 15401
		[CompilerGenerated]
		private bool <RecurseSubdirectories>k__BackingField;

		// Token: 0x04003C2A RID: 15402
		[CompilerGenerated]
		private bool <IgnoreInaccessible>k__BackingField;

		// Token: 0x04003C2B RID: 15403
		[CompilerGenerated]
		private int <BufferSize>k__BackingField;

		// Token: 0x04003C2C RID: 15404
		[CompilerGenerated]
		private FileAttributes <AttributesToSkip>k__BackingField;

		// Token: 0x04003C2D RID: 15405
		[CompilerGenerated]
		private MatchType <MatchType>k__BackingField;

		// Token: 0x04003C2E RID: 15406
		[CompilerGenerated]
		private MatchCasing <MatchCasing>k__BackingField;

		// Token: 0x04003C2F RID: 15407
		[CompilerGenerated]
		private bool <ReturnSpecialDirectories>k__BackingField;
	}
}

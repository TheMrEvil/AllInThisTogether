using System;

namespace System.IO
{
	/// <summary>Changes that might occur to a file or directory.</summary>
	// Token: 0x02000528 RID: 1320
	[Flags]
	public enum WatcherChangeTypes
	{
		/// <summary>The creation, deletion, change, or renaming of a file or folder.</summary>
		// Token: 0x040016FF RID: 5887
		All = 15,
		/// <summary>The change of a file or folder. The types of changes include: changes to size, attributes, security settings, last write, and last access time.</summary>
		// Token: 0x04001700 RID: 5888
		Changed = 4,
		/// <summary>The creation of a file or folder.</summary>
		// Token: 0x04001701 RID: 5889
		Created = 1,
		/// <summary>The deletion of a file or folder.</summary>
		// Token: 0x04001702 RID: 5890
		Deleted = 2,
		/// <summary>The renaming of a file or folder.</summary>
		// Token: 0x04001703 RID: 5891
		Renamed = 8
	}
}

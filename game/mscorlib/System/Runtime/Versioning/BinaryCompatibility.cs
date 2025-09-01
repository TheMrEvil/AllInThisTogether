using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000645 RID: 1605
	internal static class BinaryCompatibility
	{
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000040F7 File Offset: 0x000022F7
		public static bool TargetsAtLeast_Desktop_V4_5_2
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000D1455 File Offset: 0x000CF655
		// Note: this type is marked as 'beforefieldinit'.
		static BinaryCompatibility()
		{
		}

		// Token: 0x04002713 RID: 10003
		public static readonly bool TargetsAtLeast_Desktop_V4_5 = true;

		// Token: 0x04002714 RID: 10004
		public static readonly bool TargetsAtLeast_Desktop_V4_5_1 = true;
	}
}

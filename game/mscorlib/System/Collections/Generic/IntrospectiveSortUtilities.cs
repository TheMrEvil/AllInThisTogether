using System;

namespace System.Collections.Generic
{
	// Token: 0x02000ABB RID: 2747
	internal static class IntrospectiveSortUtilities
	{
		// Token: 0x06006241 RID: 25153 RVA: 0x001488E4 File Offset: 0x00146AE4
		internal static int FloorLog2PlusOne(int n)
		{
			int num = 0;
			while (n >= 1)
			{
				num++;
				n /= 2;
			}
			return num;
		}

		// Token: 0x06006242 RID: 25154 RVA: 0x00148903 File Offset: 0x00146B03
		internal static void ThrowOrIgnoreBadComparer(object comparer)
		{
			throw new ArgumentException(SR.Format("Unable to sort because the IComparer.Compare() method returns inconsistent results. Either a value does not compare equal to itself, or one value repeatedly compared to another value yields different results. IComparer: '{0}'.", comparer));
		}

		// Token: 0x04003A31 RID: 14897
		internal const int IntrosortSizeThreshold = 16;
	}
}

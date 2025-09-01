using System;

namespace System.Net
{
	// Token: 0x02000569 RID: 1385
	internal static class RangeValidationHelpers
	{
		// Token: 0x06002CEC RID: 11500 RVA: 0x00099E50 File Offset: 0x00098050
		public static bool ValidateRange(int actual, int fromAllowed, int toAllowed)
		{
			return actual >= fromAllowed && actual <= toAllowed;
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x00099E60 File Offset: 0x00098060
		public static void ValidateSegment(ArraySegment<byte> segment)
		{
			if (segment.Array == null)
			{
				throw new ArgumentNullException("segment");
			}
			if (segment.Offset < 0 || segment.Count < 0 || segment.Count > segment.Array.Length - segment.Offset)
			{
				throw new ArgumentOutOfRangeException("segment");
			}
		}
	}
}

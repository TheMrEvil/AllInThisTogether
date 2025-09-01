using System;

namespace System.Globalization
{
	// Token: 0x02000966 RID: 2406
	internal struct HebrewNumberParsingContext
	{
		// Token: 0x06005540 RID: 21824 RVA: 0x0011DEB7 File Offset: 0x0011C0B7
		public HebrewNumberParsingContext(int result)
		{
			this.state = HebrewNumber.HS.Start;
			this.result = result;
		}

		// Token: 0x0400348E RID: 13454
		internal HebrewNumber.HS state;

		// Token: 0x0400348F RID: 13455
		internal int result;
	}
}

using System;

namespace System.Globalization
{
	// Token: 0x02000963 RID: 2403
	internal readonly struct DaylightTimeStruct
	{
		// Token: 0x0600553E RID: 21822 RVA: 0x0011DE6D File Offset: 0x0011C06D
		public DaylightTimeStruct(DateTime start, DateTime end, TimeSpan delta)
		{
			this.Start = start;
			this.End = end;
			this.Delta = delta;
		}

		// Token: 0x04003487 RID: 13447
		public readonly DateTime Start;

		// Token: 0x04003488 RID: 13448
		public readonly DateTime End;

		// Token: 0x04003489 RID: 13449
		public readonly TimeSpan Delta;
	}
}

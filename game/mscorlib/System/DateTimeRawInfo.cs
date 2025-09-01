using System;

namespace System
{
	// Token: 0x02000129 RID: 297
	internal struct DateTimeRawInfo
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x0002F51E File Offset: 0x0002D71E
		internal unsafe void Init(int* numberBuffer)
		{
			this.month = -1;
			this.year = -1;
			this.dayOfWeek = -1;
			this.era = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
			this.fraction = -1.0;
			this.num = numberBuffer;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002F55C File Offset: 0x0002D75C
		internal unsafe void AddNumber(int value)
		{
			ref int ptr = ref *this.num;
			int num = this.numCount;
			this.numCount = num + 1;
			*(ref ptr + (IntPtr)num * 4) = value;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002F586 File Offset: 0x0002D786
		internal unsafe int GetNumber(int index)
		{
			return this.num[index];
		}

		// Token: 0x04001155 RID: 4437
		private unsafe int* num;

		// Token: 0x04001156 RID: 4438
		internal int numCount;

		// Token: 0x04001157 RID: 4439
		internal int month;

		// Token: 0x04001158 RID: 4440
		internal int year;

		// Token: 0x04001159 RID: 4441
		internal int dayOfWeek;

		// Token: 0x0400115A RID: 4442
		internal int era;

		// Token: 0x0400115B RID: 4443
		internal DateTimeParse.TM timeMark;

		// Token: 0x0400115C RID: 4444
		internal double fraction;

		// Token: 0x0400115D RID: 4445
		internal bool hasSameDateAndTimeSeparators;
	}
}

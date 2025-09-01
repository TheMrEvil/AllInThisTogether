using System;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000146 RID: 326
	[BurstCompatible]
	internal struct RingControl
	{
		// Token: 0x06000BC9 RID: 3017 RVA: 0x00023308 File Offset: 0x00021508
		internal RingControl(int capacity)
		{
			this.Capacity = capacity;
			this.Current = 0;
			this.Write = 0;
			this.Read = 0;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00023326 File Offset: 0x00021526
		internal void Reset()
		{
			this.Current = 0;
			this.Write = 0;
			this.Read = 0;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00023340 File Offset: 0x00021540
		internal int Distance(int from, int to)
		{
			int num = to - from;
			if (num >= 0)
			{
				return num;
			}
			return this.Capacity - math.abs(num);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00023364 File Offset: 0x00021564
		internal int Available()
		{
			return this.Distance(this.Read, this.Current);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00023378 File Offset: 0x00021578
		internal int Reserve(int count)
		{
			int num = this.Distance(this.Write, this.Read) - 1;
			int num2 = (num < 0) ? (this.Capacity - 1) : num;
			count = ((math.abs(count) - num2 < 0) ? count : num2);
			this.Write = (this.Write + count) % this.Capacity;
			return count;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000233D4 File Offset: 0x000215D4
		internal int Commit(int count)
		{
			int num = this.Distance(this.Current, this.Write);
			count = ((math.abs(count) - num < 0) ? count : num);
			this.Current = (this.Current + count) % this.Capacity;
			return count;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002341C File Offset: 0x0002161C
		internal int Consume(int count)
		{
			int num = this.Distance(this.Read, this.Current);
			count = ((math.abs(count) - num < 0) ? count : num);
			this.Read = (this.Read + count) % this.Capacity;
			return count;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00023463 File Offset: 0x00021663
		internal int Length
		{
			get
			{
				return this.Distance(this.Read, this.Write);
			}
		}

		// Token: 0x040003CA RID: 970
		internal readonly int Capacity;

		// Token: 0x040003CB RID: 971
		internal int Current;

		// Token: 0x040003CC RID: 972
		internal int Write;

		// Token: 0x040003CD RID: 973
		internal int Read;
	}
}

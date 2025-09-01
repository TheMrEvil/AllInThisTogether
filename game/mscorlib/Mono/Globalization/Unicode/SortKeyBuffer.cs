using System;
using System.Globalization;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000079 RID: 121
	internal class SortKeyBuffer
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000259F File Offset: 0x0000079F
		public SortKeyBuffer(int lcid)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009650 File Offset: 0x00007850
		public void Reset()
		{
			this.l1 = (this.l2 = (this.l3 = (this.l4s = (this.l4t = (this.l4k = (this.l4w = (this.l5 = 0)))))));
			this.frenchSorted = false;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000096AC File Offset: 0x000078AC
		internal void ClearBuffer()
		{
			this.l1b = (this.l2b = (this.l3b = (this.l4sb = (this.l4tb = (this.l4kb = (this.l4wb = (this.l5b = null)))))));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009700 File Offset: 0x00007900
		internal void Initialize(CompareOptions options, int lcid, string s, bool frenchSort)
		{
			this.source = s;
			this.lcid = lcid;
			this.options = options;
			int length = s.Length;
			this.processLevel2 = ((options & CompareOptions.IgnoreNonSpace) == CompareOptions.None);
			this.frenchSort = frenchSort;
			if (this.l1b == null || this.l1b.Length < length)
			{
				this.l1b = new byte[length * 2 + 10];
			}
			if (this.processLevel2 && (this.l2b == null || this.l2b.Length < length))
			{
				this.l2b = new byte[length + 10];
			}
			if (this.l3b == null || this.l3b.Length < length)
			{
				this.l3b = new byte[length + 10];
			}
			if (this.l4sb == null)
			{
				this.l4sb = new byte[10];
			}
			if (this.l4tb == null)
			{
				this.l4tb = new byte[10];
			}
			if (this.l4kb == null)
			{
				this.l4kb = new byte[10];
			}
			if (this.l4wb == null)
			{
				this.l4wb = new byte[10];
			}
			if (this.l5b == null)
			{
				this.l5b = new byte[10];
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009818 File Offset: 0x00007A18
		internal void AppendCJKExtension(byte lv1msb, byte lv1lsb)
		{
			this.AppendBufferPrimitive(254, ref this.l1b, ref this.l1);
			this.AppendBufferPrimitive(byte.MaxValue, ref this.l1b, ref this.l1);
			this.AppendBufferPrimitive(lv1msb, ref this.l1b, ref this.l1);
			this.AppendBufferPrimitive(lv1lsb, ref this.l1b, ref this.l1);
			if (this.processLevel2)
			{
				this.AppendBufferPrimitive(2, ref this.l2b, ref this.l2);
			}
			this.AppendBufferPrimitive(2, ref this.l3b, ref this.l3);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000098A8 File Offset: 0x00007AA8
		internal void AppendKana(byte category, byte lv1, byte lv2, byte lv3, bool isSmallKana, byte markType, bool isKatakana, bool isHalfWidth)
		{
			this.AppendNormal(category, lv1, lv2, lv3);
			this.AppendBufferPrimitive(isSmallKana ? 196 : 228, ref this.l4sb, ref this.l4s);
			this.AppendBufferPrimitive(markType, ref this.l4tb, ref this.l4t);
			this.AppendBufferPrimitive(isKatakana ? 196 : 228, ref this.l4kb, ref this.l4k);
			this.AppendBufferPrimitive(isHalfWidth ? 196 : 228, ref this.l4wb, ref this.l4w);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009940 File Offset: 0x00007B40
		internal void AppendNormal(byte category, byte lv1, byte lv2, byte lv3)
		{
			if (lv2 == 0)
			{
				lv2 = 2;
			}
			if (lv3 == 0)
			{
				lv3 = 2;
			}
			if (category == 6 && (this.options & CompareOptions.StringSort) == CompareOptions.None)
			{
				this.AppendLevel5(category, lv1);
				return;
			}
			if (this.processLevel2 && category == 1 && this.l1 > 0)
			{
				byte b = lv2;
				byte[] array = this.l2b;
				int num = this.l2 - 1;
				this.l2 = num;
				lv2 = b + array[num];
				byte[] array2 = this.l3b;
				num = this.l3 - 1;
				this.l3 = num;
				lv3 = array2[num];
			}
			if (category != 1)
			{
				this.AppendBufferPrimitive(category, ref this.l1b, ref this.l1);
				this.AppendBufferPrimitive(lv1, ref this.l1b, ref this.l1);
			}
			if (this.processLevel2)
			{
				this.AppendBufferPrimitive(lv2, ref this.l2b, ref this.l2);
			}
			this.AppendBufferPrimitive(lv3, ref this.l3b, ref this.l3);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009A1C File Offset: 0x00007C1C
		private void AppendLevel5(byte category, byte lv1)
		{
			int num = (this.l2 + 1) % 8192;
			this.AppendBufferPrimitive((byte)(num / 64 + 128), ref this.l5b, ref this.l5);
			this.AppendBufferPrimitive((byte)(num % 64 * 4 + 3), ref this.l5b, ref this.l5);
			this.AppendBufferPrimitive(category, ref this.l5b, ref this.l5);
			this.AppendBufferPrimitive(lv1, ref this.l5b, ref this.l5);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009A98 File Offset: 0x00007C98
		private void AppendBufferPrimitive(byte value, ref byte[] buf, ref int bidx)
		{
			byte[] array = buf;
			int num = bidx;
			bidx = num + 1;
			array[num] = value;
			if (bidx == buf.Length)
			{
				byte[] array2 = new byte[bidx * 2];
				Array.Copy(buf, array2, buf.Length);
				buf = array2;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009AD3 File Offset: 0x00007CD3
		public SortKey GetResultAndReset()
		{
			SortKey result = this.GetResult();
			this.Reset();
			return result;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009AE4 File Offset: 0x00007CE4
		private int GetOptimizedLength(byte[] data, int len, byte defaultValue)
		{
			int num = -1;
			for (int i = 0; i < len; i++)
			{
				if (data[i] != defaultValue)
				{
					num = i;
				}
			}
			return num + 1;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009B0C File Offset: 0x00007D0C
		public SortKey GetResult()
		{
			if (this.source.Length == 0)
			{
				return new SortKey(this.lcid, this.source, new byte[0], this.options, 0, 0, 0, 0, 0, 0, 0, 0);
			}
			if (this.frenchSort && !this.frenchSorted && this.l2b != null)
			{
				int num = 0;
				while (num < this.l2b.Length && this.l2b[num] != 0)
				{
					num++;
				}
				Array.Reverse<byte>(this.l2b, 0, num);
				this.frenchSorted = true;
			}
			this.l2 = this.GetOptimizedLength(this.l2b, this.l2, 2);
			this.l3 = this.GetOptimizedLength(this.l3b, this.l3, 2);
			bool flag = this.l4s > 0;
			this.l4s = this.GetOptimizedLength(this.l4sb, this.l4s, 228);
			this.l4t = this.GetOptimizedLength(this.l4tb, this.l4t, 3);
			this.l4k = this.GetOptimizedLength(this.l4kb, this.l4k, 228);
			this.l4w = this.GetOptimizedLength(this.l4wb, this.l4w, 228);
			this.l5 = this.GetOptimizedLength(this.l5b, this.l5, 2);
			int num2 = this.l1 + this.l2 + this.l3 + this.l5 + 5;
			int num3 = this.l4s + this.l4t + this.l4k + this.l4w;
			if (flag)
			{
				num2 += num3 + 4;
			}
			byte[] array = new byte[num2];
			Array.Copy(this.l1b, array, this.l1);
			array[this.l1] = 1;
			int num4 = this.l1 + 1;
			if (this.l2 > 0)
			{
				Array.Copy(this.l2b, 0, array, num4, this.l2);
			}
			num4 += this.l2;
			array[num4++] = 1;
			if (this.l3 > 0)
			{
				Array.Copy(this.l3b, 0, array, num4, this.l3);
			}
			num4 += this.l3;
			array[num4++] = 1;
			if (flag)
			{
				Array.Copy(this.l4sb, 0, array, num4, this.l4s);
				num4 += this.l4s;
				array[num4++] = byte.MaxValue;
				Array.Copy(this.l4tb, 0, array, num4, this.l4t);
				num4 += this.l4t;
				array[num4++] = 2;
				Array.Copy(this.l4kb, 0, array, num4, this.l4k);
				num4 += this.l4k;
				array[num4++] = byte.MaxValue;
				Array.Copy(this.l4wb, 0, array, num4, this.l4w);
				num4 += this.l4w;
				array[num4++] = byte.MaxValue;
			}
			array[num4++] = 1;
			if (this.l5 > 0)
			{
				Array.Copy(this.l5b, 0, array, num4, this.l5);
			}
			num4 += this.l5;
			array[num4++] = 0;
			return new SortKey(this.lcid, this.source, array, this.options, this.l1, this.l2, this.l3, this.l4s, this.l4t, this.l4k, this.l4w, this.l5);
		}

		// Token: 0x04000E80 RID: 3712
		private byte[] l1b;

		// Token: 0x04000E81 RID: 3713
		private byte[] l2b;

		// Token: 0x04000E82 RID: 3714
		private byte[] l3b;

		// Token: 0x04000E83 RID: 3715
		private byte[] l4sb;

		// Token: 0x04000E84 RID: 3716
		private byte[] l4tb;

		// Token: 0x04000E85 RID: 3717
		private byte[] l4kb;

		// Token: 0x04000E86 RID: 3718
		private byte[] l4wb;

		// Token: 0x04000E87 RID: 3719
		private byte[] l5b;

		// Token: 0x04000E88 RID: 3720
		private string source;

		// Token: 0x04000E89 RID: 3721
		private int l1;

		// Token: 0x04000E8A RID: 3722
		private int l2;

		// Token: 0x04000E8B RID: 3723
		private int l3;

		// Token: 0x04000E8C RID: 3724
		private int l4s;

		// Token: 0x04000E8D RID: 3725
		private int l4t;

		// Token: 0x04000E8E RID: 3726
		private int l4k;

		// Token: 0x04000E8F RID: 3727
		private int l4w;

		// Token: 0x04000E90 RID: 3728
		private int l5;

		// Token: 0x04000E91 RID: 3729
		private int lcid;

		// Token: 0x04000E92 RID: 3730
		private CompareOptions options;

		// Token: 0x04000E93 RID: 3731
		private bool processLevel2;

		// Token: 0x04000E94 RID: 3732
		private bool frenchSort;

		// Token: 0x04000E95 RID: 3733
		private bool frenchSorted;
	}
}

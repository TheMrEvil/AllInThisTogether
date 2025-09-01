using System;
using System.IO;
using System.Text;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200007C RID: 124
	internal sealed class ByteBuffer
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x00013CDA File Offset: 0x00011EDA
		internal ByteBuffer(int initialCapacity)
		{
			this.buffer = new byte[initialCapacity];
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00013CEE File Offset: 0x00011EEE
		private ByteBuffer(byte[] wrap, int length)
		{
			this.buffer = wrap;
			this.pos = length;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00013D04 File Offset: 0x00011F04
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00013D0C File Offset: 0x00011F0C
		internal int Position
		{
			get
			{
				return this.pos;
			}
			set
			{
				if (value > this.Length || value > this.buffer.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.__length = Math.Max(this.__length, this.pos);
				this.pos = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00013D46 File Offset: 0x00011F46
		internal int Length
		{
			get
			{
				return Math.Max(this.pos, this.__length);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00013D5C File Offset: 0x00011F5C
		internal void Insert(int count)
		{
			if (count > 0)
			{
				int length = this.Length;
				int num = this.buffer.Length - length;
				if (num < count)
				{
					this.Grow(count - num);
				}
				Buffer.BlockCopy(this.buffer, this.pos, this.buffer, this.pos + count, length - this.pos);
				this.__length = Math.Max(this.__length, this.pos) + count;
				return;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00013DDC File Offset: 0x00011FDC
		private void Grow(int minGrow)
		{
			byte[] dst = new byte[Math.Max(this.buffer.Length + minGrow, this.buffer.Length * 2)];
			Buffer.BlockCopy(this.buffer, 0, dst, 0, this.buffer.Length);
			this.buffer = dst;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00013E28 File Offset: 0x00012028
		internal int GetInt32AtCurrentPosition()
		{
			return (int)this.buffer[this.pos] + ((int)this.buffer[this.pos + 1] << 8) + ((int)this.buffer[this.pos + 2] << 16) + ((int)this.buffer[this.pos + 3] << 24);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00013E7A File Offset: 0x0001207A
		internal byte GetByteAtCurrentPosition()
		{
			return this.buffer[this.pos];
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00013E8C File Offset: 0x0001208C
		internal int GetCompressedUIntLength()
		{
			int num = (int)(this.buffer[this.pos] & 192);
			if (num == 128)
			{
				return 2;
			}
			if (num != 192)
			{
				return 1;
			}
			return 4;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00013EC4 File Offset: 0x000120C4
		internal void Write(byte[] value)
		{
			if (this.pos + value.Length > this.buffer.Length)
			{
				this.Grow(value.Length);
			}
			Buffer.BlockCopy(value, 0, this.buffer, this.pos, value.Length);
			this.pos += value.Length;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00013F14 File Offset: 0x00012114
		internal void Write(byte value)
		{
			if (this.pos == this.buffer.Length)
			{
				this.Grow(1);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = value;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00013F51 File Offset: 0x00012151
		internal void Write(sbyte value)
		{
			this.Write((byte)value);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00013F5B File Offset: 0x0001215B
		internal void Write(ushort value)
		{
			this.Write((short)value);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00013F68 File Offset: 0x00012168
		internal void Write(short value)
		{
			if (this.pos + 2 > this.buffer.Length)
			{
				this.Grow(2);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00013FC4 File Offset: 0x000121C4
		internal void Write(uint value)
		{
			this.Write((int)value);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00013FD0 File Offset: 0x000121D0
		internal void Write(int value)
		{
			if (this.pos + 4 > this.buffer.Length)
			{
				this.Grow(4);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array4[num] = (byte)(value >> 24);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00014066 File Offset: 0x00012266
		internal void Write(ulong value)
		{
			this.Write((long)value);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00014070 File Offset: 0x00012270
		internal void Write(long value)
		{
			if (this.pos + 8 > this.buffer.Length)
			{
				this.Grow(8);
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array4[num] = (byte)(value >> 24);
			byte[] array5 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array5[num] = (byte)(value >> 32);
			byte[] array6 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array6[num] = (byte)(value >> 40);
			byte[] array7 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array7[num] = (byte)(value >> 48);
			byte[] array8 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			array8[num] = (byte)(value >> 56);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001417A File Offset: 0x0001237A
		internal void Write(float value)
		{
			this.Write(SingleConverter.SingleToInt32Bits(value));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00014188 File Offset: 0x00012388
		internal void Write(double value)
		{
			this.Write(BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00014198 File Offset: 0x00012398
		internal void Write(string str)
		{
			if (str == null)
			{
				this.Write(byte.MaxValue);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			this.WriteCompressedUInt(bytes.Length);
			this.Write(bytes);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000141D0 File Offset: 0x000123D0
		internal void WriteCompressedUInt(int value)
		{
			if (value <= 127)
			{
				this.Write((byte)value);
				return;
			}
			if (value <= 16383)
			{
				this.Write((byte)(128 | value >> 8));
				this.Write((byte)value);
				return;
			}
			this.Write((byte)(192 | value >> 24));
			this.Write((byte)(value >> 16));
			this.Write((byte)(value >> 8));
			this.Write((byte)value);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001423C File Offset: 0x0001243C
		internal void WriteCompressedInt(int value)
		{
			if (value >= 0)
			{
				this.WriteCompressedUInt(value << 1);
				return;
			}
			if (value >= -64)
			{
				value = ((value << 1 & 127) | 1);
				this.Write((byte)value);
				return;
			}
			if (value >= -8192)
			{
				value = ((value << 1 & 16383) | 1);
				this.Write((byte)(128 | value >> 8));
				this.Write((byte)value);
				return;
			}
			value = ((value << 1 & 536870911) | 1);
			this.Write((byte)(192 | value >> 24));
			this.Write((byte)(value >> 16));
			this.Write((byte)(value >> 8));
			this.Write((byte)value);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000142D8 File Offset: 0x000124D8
		internal void Write(ByteBuffer bb)
		{
			if (this.pos + bb.Length > this.buffer.Length)
			{
				this.Grow(bb.Length);
			}
			Buffer.BlockCopy(bb.buffer, 0, this.buffer, this.pos, bb.Length);
			this.pos += bb.Length;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00014339 File Offset: 0x00012539
		internal void WriteTo(Stream stream)
		{
			stream.Write(this.buffer, 0, this.Length);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001434E File Offset: 0x0001254E
		internal void Clear()
		{
			this.pos = 0;
			this.__length = 0;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00014360 File Offset: 0x00012560
		internal void Align(int alignment)
		{
			if (this.pos + alignment > this.buffer.Length)
			{
				this.Grow(alignment);
			}
			int num = this.pos + alignment - 1 & ~(alignment - 1);
			while (this.pos < num)
			{
				byte[] array = this.buffer;
				int num2 = this.pos;
				this.pos = num2 + 1;
				array[num2] = 0;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000143BC File Offset: 0x000125BC
		internal void WriteTypeDefOrRefEncoded(int token)
		{
			int num = token >> 24;
			if (num == 1)
			{
				this.WriteCompressedUInt((token & 16777215) << 2 | 1);
				return;
			}
			if (num == 2)
			{
				this.WriteCompressedUInt((token & 16777215) << 2 | 0);
				return;
			}
			if (num != 27)
			{
				throw new InvalidOperationException();
			}
			this.WriteCompressedUInt((token & 16777215) << 2 | 2);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00014418 File Offset: 0x00012618
		internal byte[] ToArray()
		{
			int length = this.Length;
			byte[] array = new byte[length];
			Buffer.BlockCopy(this.buffer, 0, array, 0, length);
			return array;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00014443 File Offset: 0x00012643
		internal static ByteBuffer Wrap(byte[] buf)
		{
			return new ByteBuffer(buf, buf.Length);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001444E File Offset: 0x0001264E
		internal static ByteBuffer Wrap(byte[] buf, int length)
		{
			return new ByteBuffer(buf, length);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00014458 File Offset: 0x00012658
		internal bool Match(int pos, ByteBuffer bb2, int pos2, int len)
		{
			for (int i = 0; i < len; i++)
			{
				if (this.buffer[pos + i] != bb2.buffer[pos2 + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001448C File Offset: 0x0001268C
		internal int Hash()
		{
			int num = 0;
			int length = this.Length;
			for (int i = 0; i < length; i++)
			{
				num *= 37;
				num ^= (int)this.buffer[i];
			}
			return num;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000144BF File Offset: 0x000126BF
		internal ByteReader GetBlob(int offset)
		{
			return ByteReader.FromBlob(this.buffer, offset);
		}

		// Token: 0x04000281 RID: 641
		private byte[] buffer;

		// Token: 0x04000282 RID: 642
		private int pos;

		// Token: 0x04000283 RID: 643
		private int __length;
	}
}

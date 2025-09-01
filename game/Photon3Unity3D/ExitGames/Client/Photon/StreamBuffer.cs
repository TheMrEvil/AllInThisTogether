using System;
using System.IO;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000037 RID: 55
	public class StreamBuffer
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x000171D8 File Offset: 0x000153D8
		public StreamBuffer(int size = 0)
		{
			this.buf = new byte[size];
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000171EE File Offset: 0x000153EE
		public StreamBuffer(byte[] buf)
		{
			this.buf = buf;
			this.len = buf.Length;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00017208 File Offset: 0x00015408
		public byte[] ToArray()
		{
			byte[] array = new byte[this.len];
			Buffer.BlockCopy(this.buf, 0, array, 0, this.len);
			return array;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001723C File Offset: 0x0001543C
		public byte[] ToArrayFromPos()
		{
			int num = this.len - this.pos;
			bool flag = num <= 0;
			byte[] result;
			if (flag)
			{
				result = new byte[0];
			}
			else
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(this.buf, this.pos, array, 0, num);
				result = array;
			}
			return result;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00017290 File Offset: 0x00015490
		public void Compact()
		{
			long num = (long)(this.Length - this.Position);
			bool flag = num > 0L;
			if (flag)
			{
				Buffer.BlockCopy(this.buf, this.Position, this.buf, 0, (int)num);
			}
			this.Position = 0;
			this.SetLength(num);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000172E4 File Offset: 0x000154E4
		public byte[] GetBuffer()
		{
			return this.buf;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000172FC File Offset: 0x000154FC
		public byte[] GetBufferAndAdvance(int length, out int offset)
		{
			offset = this.Position;
			this.Position += length;
			return this.buf;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0001732C File Offset: 0x0001552C
		public bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00017340 File Offset: 0x00015540
		public bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00017354 File Offset: 0x00015554
		public bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00017368 File Offset: 0x00015568
		public int Length
		{
			get
			{
				return this.len;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00017380 File Offset: 0x00015580
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00017398 File Offset: 0x00015598
		public int Position
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
				bool flag = this.len < this.pos;
				if (flag)
				{
					this.len = this.pos;
					this.CheckSize(this.len);
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000173DC File Offset: 0x000155DC
		public int Available
		{
			get
			{
				int num = this.len - this.pos;
				return (num < 0) ? 0 : num;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00017404 File Offset: 0x00015604
		public void Flush()
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00017408 File Offset: 0x00015608
		public long Seek(long offset, SeekOrigin origin)
		{
			int num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = (int)offset;
				break;
			case SeekOrigin.Current:
				num = this.pos + (int)offset;
				break;
			case SeekOrigin.End:
				num = this.len + (int)offset;
				break;
			default:
				throw new ArgumentException("Invalid seek origin");
			}
			bool flag = num < 0;
			if (flag)
			{
				throw new ArgumentException("Seek before begin");
			}
			bool flag2 = num > this.len;
			if (flag2)
			{
				throw new ArgumentException("Seek after end");
			}
			this.pos = num;
			return (long)this.pos;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001749C File Offset: 0x0001569C
		public void SetLength(long value)
		{
			this.len = (int)value;
			this.CheckSize(this.len);
			bool flag = this.pos > this.len;
			if (flag)
			{
				this.pos = this.len;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000174DF File Offset: 0x000156DF
		public void SetCapacityMinimum(int neededSize)
		{
			this.CheckSize(neededSize);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000174EC File Offset: 0x000156EC
		public int Read(byte[] buffer, int dstOffset, int count)
		{
			int num = this.len - this.pos;
			bool flag = num <= 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = count > num;
				if (flag2)
				{
					count = num;
				}
				Buffer.BlockCopy(this.buf, this.pos, buffer, dstOffset, count);
				this.pos += count;
				result = count;
			}
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00017550 File Offset: 0x00015750
		public void Write(byte[] buffer, int srcOffset, int count)
		{
			int num = this.pos + count;
			this.CheckSize(num);
			bool flag = num > this.len;
			if (flag)
			{
				this.len = num;
			}
			Buffer.BlockCopy(buffer, srcOffset, this.buf, this.pos, count);
			this.pos = num;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000175A4 File Offset: 0x000157A4
		public byte ReadByte()
		{
			bool flag = this.pos >= this.len;
			if (flag)
			{
				throw new EndOfStreamException("SteamBuffer.ReadByte() failed. pos:" + this.pos.ToString() + " len:" + this.len.ToString());
			}
			byte[] array = this.buf;
			int num = this.pos;
			this.pos = num + 1;
			return array[num];
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00017610 File Offset: 0x00015810
		public void WriteByte(byte value)
		{
			bool flag = this.pos >= this.len;
			if (flag)
			{
				this.len = this.pos + 1;
				this.CheckSize(this.len);
			}
			byte[] array = this.buf;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = value;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001766C File Offset: 0x0001586C
		public void WriteBytes(byte v0, byte v1)
		{
			int num = this.pos + 2;
			bool flag = this.len < num;
			if (flag)
			{
				this.len = num;
				this.CheckSize(this.len);
			}
			byte[] array = this.buf;
			int num2 = this.pos;
			this.pos = num2 + 1;
			array[num2] = v0;
			byte[] array2 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array2[num2] = v1;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000176D8 File Offset: 0x000158D8
		public void WriteBytes(byte v0, byte v1, byte v2)
		{
			int num = this.pos + 3;
			bool flag = this.len < num;
			if (flag)
			{
				this.len = num;
				this.CheckSize(this.len);
			}
			byte[] array = this.buf;
			int num2 = this.pos;
			this.pos = num2 + 1;
			array[num2] = v0;
			byte[] array2 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array2[num2] = v1;
			byte[] array3 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array3[num2] = v2;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00017760 File Offset: 0x00015960
		public void WriteBytes(byte v0, byte v1, byte v2, byte v3)
		{
			int num = this.pos + 4;
			bool flag = this.len < num;
			if (flag)
			{
				this.len = num;
				this.CheckSize(this.len);
			}
			byte[] array = this.buf;
			int num2 = this.pos;
			this.pos = num2 + 1;
			array[num2] = v0;
			byte[] array2 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array2[num2] = v1;
			byte[] array3 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array3[num2] = v2;
			byte[] array4 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array4[num2] = v3;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00017800 File Offset: 0x00015A00
		public void WriteBytes(byte v0, byte v1, byte v2, byte v3, byte v4, byte v5, byte v6, byte v7)
		{
			int num = this.pos + 8;
			bool flag = this.len < num;
			if (flag)
			{
				this.len = num;
				this.CheckSize(this.len);
			}
			byte[] array = this.buf;
			int num2 = this.pos;
			this.pos = num2 + 1;
			array[num2] = v0;
			byte[] array2 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array2[num2] = v1;
			byte[] array3 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array3[num2] = v2;
			byte[] array4 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array4[num2] = v3;
			byte[] array5 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array5[num2] = v4;
			byte[] array6 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array6[num2] = v5;
			byte[] array7 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array7[num2] = v6;
			byte[] array8 = this.buf;
			num2 = this.pos;
			this.pos = num2 + 1;
			array8[num2] = v7;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00017908 File Offset: 0x00015B08
		private bool CheckSize(int size)
		{
			bool flag = size <= this.buf.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = this.buf.Length;
				bool flag2 = num == 0;
				if (flag2)
				{
					num = 1;
				}
				while (size > num)
				{
					num *= 2;
				}
				byte[] dst = new byte[num];
				Buffer.BlockCopy(this.buf, 0, dst, 0, this.buf.Length);
				this.buf = dst;
				result = true;
			}
			return result;
		}

		// Token: 0x040001A4 RID: 420
		private const int DefaultInitialSize = 0;

		// Token: 0x040001A5 RID: 421
		private int pos;

		// Token: 0x040001A6 RID: 422
		private int len;

		// Token: 0x040001A7 RID: 423
		private byte[] buf;
	}
}

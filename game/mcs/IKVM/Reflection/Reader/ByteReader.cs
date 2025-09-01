using System;
using System.Text;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000092 RID: 146
	internal sealed class ByteReader
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x00018EF6 File Offset: 0x000170F6
		internal ByteReader(byte[] buffer, int offset, int length)
		{
			this.buffer = buffer;
			this.pos = offset;
			this.end = this.pos + length;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00018F1C File Offset: 0x0001711C
		internal static ByteReader FromBlob(byte[] blobHeap, int blob)
		{
			ByteReader byteReader = new ByteReader(blobHeap, blob, 4);
			int num = byteReader.ReadCompressedUInt();
			byteReader.pos += num;
			return byteReader;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00018F46 File Offset: 0x00017146
		internal int Length
		{
			get
			{
				return this.end - this.pos;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00018F55 File Offset: 0x00017155
		internal byte PeekByte()
		{
			if (this.pos == this.end)
			{
				throw new BadImageFormatException();
			}
			return this.buffer[this.pos];
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00018F78 File Offset: 0x00017178
		internal byte ReadByte()
		{
			if (this.pos == this.end)
			{
				throw new BadImageFormatException();
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			return array[num];
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00018FB4 File Offset: 0x000171B4
		internal byte[] ReadBytes(int count)
		{
			if (count < 0)
			{
				throw new BadImageFormatException();
			}
			if (this.end - this.pos < count)
			{
				throw new BadImageFormatException();
			}
			byte[] array = new byte[count];
			Buffer.BlockCopy(this.buffer, this.pos, array, 0, count);
			this.pos += count;
			return array;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001900C File Offset: 0x0001720C
		internal int ReadCompressedUInt()
		{
			byte b = this.ReadByte();
			if (b <= 127)
			{
				return (int)b;
			}
			if ((b & 192) == 128)
			{
				byte b2 = this.ReadByte();
				return (int)(b & 63) << 8 | (int)b2;
			}
			byte b3 = this.ReadByte();
			byte b4 = this.ReadByte();
			byte b5 = this.ReadByte();
			return ((int)(b & 63) << 24) + ((int)b3 << 16) + ((int)b4 << 8) + (int)b5;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00019070 File Offset: 0x00017270
		internal int ReadCompressedInt()
		{
			byte b = this.PeekByte();
			int num = this.ReadCompressedUInt();
			if ((num & 1) == 0)
			{
				return num >> 1;
			}
			int num2 = (int)(b & 192);
			if (num2 == 0 || num2 == 64)
			{
				return (num >> 1) - 64;
			}
			if (num2 != 128)
			{
				return (num >> 1) - 268435456;
			}
			return (num >> 1) - 8192;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000190C8 File Offset: 0x000172C8
		internal string ReadString()
		{
			if (this.PeekByte() == 255)
			{
				this.pos++;
				return null;
			}
			int num = this.ReadCompressedUInt();
			string @string = Encoding.UTF8.GetString(this.buffer, this.pos, num);
			this.pos += num;
			return @string;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001911E File Offset: 0x0001731E
		internal char ReadChar()
		{
			return (char)this.ReadInt16();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00019127 File Offset: 0x00017327
		internal sbyte ReadSByte()
		{
			return (sbyte)this.ReadByte();
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00019130 File Offset: 0x00017330
		internal short ReadInt16()
		{
			if (this.end - this.pos < 2)
			{
				throw new BadImageFormatException();
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			int num2 = array[num];
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			byte b = array2[num];
			return (short)(num2 | (int)b << 8);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001911E File Offset: 0x0001731E
		internal ushort ReadUInt16()
		{
			return (ushort)this.ReadInt16();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001918C File Offset: 0x0001738C
		internal int ReadInt32()
		{
			if (this.end - this.pos < 4)
			{
				throw new BadImageFormatException();
			}
			byte[] array = this.buffer;
			int num = this.pos;
			this.pos = num + 1;
			int num2 = array[num];
			byte[] array2 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			byte b = array2[num];
			byte[] array3 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			byte b2 = array3[num];
			byte[] array4 = this.buffer;
			num = this.pos;
			this.pos = num + 1;
			byte b3 = array4[num];
			return num2 | (int)b << 8 | (int)b2 << 16 | (int)b3 << 24;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00019220 File Offset: 0x00017420
		internal uint ReadUInt32()
		{
			return (uint)this.ReadInt32();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00019228 File Offset: 0x00017428
		internal long ReadInt64()
		{
			long num = (long)((ulong)this.ReadUInt32());
			ulong num2 = (ulong)this.ReadUInt32();
			return num | (long)((long)num2 << 32);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00019249 File Offset: 0x00017449
		internal ulong ReadUInt64()
		{
			return (ulong)this.ReadInt64();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00019251 File Offset: 0x00017451
		internal float ReadSingle()
		{
			return SingleConverter.Int32BitsToSingle(this.ReadInt32());
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001925E File Offset: 0x0001745E
		internal double ReadDouble()
		{
			return BitConverter.Int64BitsToDouble(this.ReadInt64());
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001926B File Offset: 0x0001746B
		internal ByteReader Slice(int length)
		{
			if (this.end - this.pos < length)
			{
				throw new BadImageFormatException();
			}
			ByteReader result = new ByteReader(this.buffer, this.pos, length);
			this.pos += length;
			return result;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000192A3 File Offset: 0x000174A3
		internal void Align(int alignment)
		{
			alignment--;
			this.pos = (this.pos + alignment & ~alignment);
		}

		// Token: 0x04000304 RID: 772
		private byte[] buffer;

		// Token: 0x04000305 RID: 773
		private int pos;

		// Token: 0x04000306 RID: 774
		private int end;
	}
}

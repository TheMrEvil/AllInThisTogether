using System;
using System.IO;
using System.Text;

namespace System
{
	// Token: 0x02000254 RID: 596
	internal class TermInfoReader
	{
		// Token: 0x06001B9E RID: 7070 RVA: 0x00067638 File Offset: 0x00065838
		public TermInfoReader(string term, string filename)
		{
			using (FileStream fileStream = File.OpenRead(filename))
			{
				long length = fileStream.Length;
				if (length > 4096L)
				{
					throw new Exception("File must be smaller than 4K");
				}
				this.buffer = new byte[(int)length];
				if (fileStream.Read(this.buffer, 0, this.buffer.Length) != this.buffer.Length)
				{
					throw new Exception("Short read");
				}
				this.ReadHeader(this.buffer, ref this.booleansOffset);
				this.ReadNames(this.buffer, ref this.booleansOffset);
			}
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000676E4 File Offset: 0x000658E4
		public TermInfoReader(string term, byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.buffer = buffer;
			this.ReadHeader(buffer, ref this.booleansOffset);
			this.ReadNames(buffer, ref this.booleansOffset);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0006771B File Offset: 0x0006591B
		private void DetermineVersion(short magic)
		{
			if (magic == 282)
			{
				this.intOffset = 2;
				return;
			}
			if (magic == 542)
			{
				this.intOffset = 4;
				return;
			}
			throw new Exception(string.Format("Magic number is unexpected: {0}", magic));
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00067754 File Offset: 0x00065954
		private void ReadHeader(byte[] buffer, ref int position)
		{
			short @int = this.GetInt16(buffer, position);
			position += 2;
			this.DetermineVersion(@int);
			this.GetInt16(buffer, position);
			position += 2;
			this.boolSize = (int)this.GetInt16(buffer, position);
			position += 2;
			this.numSize = (int)this.GetInt16(buffer, position);
			position += 2;
			this.strOffsets = (int)this.GetInt16(buffer, position);
			position += 2;
			this.GetInt16(buffer, position);
			position += 2;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x000677D8 File Offset: 0x000659D8
		private void ReadNames(byte[] buffer, ref int position)
		{
			string @string = this.GetString(buffer, position);
			position += @string.Length + 1;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000677FC File Offset: 0x000659FC
		public bool Get(TermInfoBooleans boolean)
		{
			if (boolean < TermInfoBooleans.AutoLeftMargin || boolean >= TermInfoBooleans.Last || boolean >= (TermInfoBooleans)this.boolSize)
			{
				return false;
			}
			int num = this.booleansOffset;
			num = (int)(num + boolean);
			return this.buffer[num] > 0;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00067838 File Offset: 0x00065A38
		public int Get(TermInfoNumbers number)
		{
			if (number < TermInfoNumbers.Columns || number >= TermInfoNumbers.Last || number > (TermInfoNumbers)this.numSize)
			{
				return -1;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num = (int)(num + number * (TermInfoNumbers)this.intOffset);
			return (int)this.GetInt16(this.buffer, num);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0006788C File Offset: 0x00065A8C
		public string Get(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += this.numSize * this.intOffset;
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetString(this.buffer, num + this.strOffsets * 2 + @int);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0006790C File Offset: 0x00065B0C
		public byte[] GetStringBytes(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += this.numSize * this.intOffset;
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetStringBytes(this.buffer, num + this.strOffsets * 2 + @int);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0006798C File Offset: 0x00065B8C
		private short GetInt16(byte[] buffer, int offset)
		{
			int num = (int)buffer[offset];
			int num2 = (int)buffer[offset + 1];
			if (num == 255 && num2 == 255)
			{
				return -1;
			}
			return (short)(num + num2 * 256);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000679C0 File Offset: 0x00065BC0
		private string GetString(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			return Encoding.ASCII.GetString(buffer, offset, num);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000679F0 File Offset: 0x00065BF0
		private byte[] GetStringBytes(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			byte[] array = new byte[num];
			Buffer.InternalBlockCopy(buffer, offset, array, 0, num);
			return array;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00067A24 File Offset: 0x00065C24
		internal static string Escape(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if (char.IsControl(c))
				{
					stringBuilder.AppendFormat("\\x{0:X2}", (int)c);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040017FB RID: 6139
		private int boolSize;

		// Token: 0x040017FC RID: 6140
		private int numSize;

		// Token: 0x040017FD RID: 6141
		private int strOffsets;

		// Token: 0x040017FE RID: 6142
		private byte[] buffer;

		// Token: 0x040017FF RID: 6143
		private int booleansOffset;

		// Token: 0x04001800 RID: 6144
		private int intOffset;
	}
}

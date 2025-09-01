using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Xml
{
	// Token: 0x0200008A RID: 138
	internal class BufferedWrite
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x0001F77B File Offset: 0x0001D97B
		internal BufferedWrite() : this(256)
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001F788 File Offset: 0x0001D988
		internal BufferedWrite(int initialSize)
		{
			this.buffer = new byte[initialSize];
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001F79C File Offset: 0x0001D99C
		private void EnsureBuffer(int count)
		{
			int num = this.buffer.Length;
			if (count > num - this.offset)
			{
				int num2 = num;
				while (num2 != 2147483647)
				{
					num2 = ((num2 < 1073741823) ? (num2 * 2) : int.MaxValue);
					if (count <= num2 - this.offset)
					{
						byte[] dst = new byte[num2];
						Buffer.BlockCopy(this.buffer, 0, dst, 0, this.offset);
						this.buffer = dst;
						return;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Write buffer overflow.")));
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001F81C File Offset: 0x0001DA1C
		internal int Length
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001F824 File Offset: 0x0001DA24
		internal byte[] GetBuffer()
		{
			return this.buffer;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001F82C File Offset: 0x0001DA2C
		internal void Reset()
		{
			this.offset = 0;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001F835 File Offset: 0x0001DA35
		internal void Write(byte[] value)
		{
			this.Write(value, 0, value.Length);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001F842 File Offset: 0x0001DA42
		internal void Write(byte[] value, int index, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, index, this.buffer, this.offset, count);
			this.offset += count;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001F86D File Offset: 0x0001DA6D
		internal void Write(string value)
		{
			this.Write(value, 0, value.Length);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001F880 File Offset: 0x0001DA80
		internal void Write(string value, int index, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[index + i];
				if (c > 'ÿ')
				{
					string name = "MIME header has an invalid character ('{0}', {1} in hexadecimal value).";
					object[] array = new object[2];
					array[0] = c;
					int num = 1;
					int num2 = (int)c;
					array[num] = num2.ToString("X", CultureInfo.InvariantCulture);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString(name, array)));
				}
				this.buffer[this.offset + i] = (byte)c;
			}
			this.offset += count;
		}

		// Token: 0x04000366 RID: 870
		private byte[] buffer;

		// Token: 0x04000367 RID: 871
		private int offset;
	}
}

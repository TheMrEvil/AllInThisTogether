using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x0200081C RID: 2076
	internal sealed class BufferBuilder
	{
		// Token: 0x06004209 RID: 16905 RVA: 0x000E44D7 File Offset: 0x000E26D7
		internal BufferBuilder() : this(256)
		{
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x000E44E4 File Offset: 0x000E26E4
		internal BufferBuilder(int initialSize)
		{
			this._buffer = new byte[initialSize];
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x000E44F8 File Offset: 0x000E26F8
		private void EnsureBuffer(int count)
		{
			if (count > this._buffer.Length - this._offset)
			{
				byte[] array = new byte[(this._buffer.Length * 2 > this._buffer.Length + count) ? (this._buffer.Length * 2) : (this._buffer.Length + count)];
				Buffer.BlockCopy(this._buffer, 0, array, 0, this._offset);
				this._buffer = array;
			}
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x000E4564 File Offset: 0x000E2764
		internal void Append(byte value)
		{
			this.EnsureBuffer(1);
			byte[] buffer = this._buffer;
			int offset = this._offset;
			this._offset = offset + 1;
			buffer[offset] = value;
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x000E4591 File Offset: 0x000E2791
		internal void Append(byte[] value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x000E459E File Offset: 0x000E279E
		internal void Append(byte[] value, int offset, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, offset, this._buffer, this._offset, count);
			this._offset += count;
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x000E45C9 File Offset: 0x000E27C9
		internal void Append(string value)
		{
			this.Append(value, false);
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x000E45D3 File Offset: 0x000E27D3
		internal void Append(string value, bool allowUnicode)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			this.Append(value, 0, value.Length, allowUnicode);
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x000E45F0 File Offset: 0x000E27F0
		internal void Append(string value, int offset, int count, bool allowUnicode)
		{
			if (allowUnicode)
			{
				int byteCount = Encoding.UTF8.GetByteCount(value, offset, count);
				this.EnsureBuffer(byteCount);
				Encoding.UTF8.GetBytes(value, offset, count, this._buffer, this._offset);
				this._offset += byteCount;
				return;
			}
			this.Append(value, offset, count);
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x000E4648 File Offset: 0x000E2848
		internal void Append(string value, int offset, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[offset + i];
				if (c > 'ÿ')
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", c));
				}
				this._buffer[this._offset + i] = (byte)c;
			}
			this._offset += count;
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x000E46AF File Offset: 0x000E28AF
		internal int Length
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000E46B7 File Offset: 0x000E28B7
		internal byte[] GetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000E46BF File Offset: 0x000E28BF
		internal void Reset()
		{
			this._offset = 0;
		}

		// Token: 0x04002810 RID: 10256
		private byte[] _buffer;

		// Token: 0x04002811 RID: 10257
		private int _offset;
	}
}

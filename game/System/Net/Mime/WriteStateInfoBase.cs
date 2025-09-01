using System;

namespace System.Net.Mime
{
	// Token: 0x02000815 RID: 2069
	internal class WriteStateInfoBase
	{
		// Token: 0x060041E6 RID: 16870 RVA: 0x000E3AE0 File Offset: 0x000E1CE0
		internal WriteStateInfoBase()
		{
			this._header = Array.Empty<byte>();
			this._footer = Array.Empty<byte>();
			this._maxLineLength = 70;
			this._buffer = new byte[1024];
			this._currentLineLength = 0;
			this._currentBufferUsed = 0;
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000E3B2F File Offset: 0x000E1D2F
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength) : this(bufferSize, header, footer, maxLineLength, 0)
		{
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000E3B3D File Offset: 0x000E1D3D
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
		{
			this._buffer = new byte[bufferSize];
			this._header = header;
			this._footer = footer;
			this._maxLineLength = maxLineLength;
			this._currentLineLength = mimeHeaderLength;
			this._currentBufferUsed = 0;
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x000E3B76 File Offset: 0x000E1D76
		internal int FooterLength
		{
			get
			{
				return this._footer.Length;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x000E3B80 File Offset: 0x000E1D80
		internal byte[] Footer
		{
			get
			{
				return this._footer;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x060041EB RID: 16875 RVA: 0x000E3B88 File Offset: 0x000E1D88
		internal byte[] Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x000E3B90 File Offset: 0x000E1D90
		internal byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x000E3B98 File Offset: 0x000E1D98
		internal int Length
		{
			get
			{
				return this._currentBufferUsed;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x060041EE RID: 16878 RVA: 0x000E3BA0 File Offset: 0x000E1DA0
		internal int CurrentLineLength
		{
			get
			{
				return this._currentLineLength;
			}
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000E3BA8 File Offset: 0x000E1DA8
		private void EnsureSpaceInBuffer(int moreBytes)
		{
			int num = this.Buffer.Length;
			while (this._currentBufferUsed + moreBytes >= num)
			{
				num *= 2;
			}
			if (num > this.Buffer.Length)
			{
				byte[] array = new byte[num];
				this._buffer.CopyTo(array, 0);
				this._buffer = array;
			}
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000E3BF8 File Offset: 0x000E1DF8
		internal void Append(byte aByte)
		{
			this.EnsureSpaceInBuffer(1);
			byte[] buffer = this.Buffer;
			int currentBufferUsed = this._currentBufferUsed;
			this._currentBufferUsed = currentBufferUsed + 1;
			buffer[currentBufferUsed] = aByte;
			this._currentLineLength++;
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000E3C33 File Offset: 0x000E1E33
		internal void Append(params byte[] bytes)
		{
			this.EnsureSpaceInBuffer(bytes.Length);
			bytes.CopyTo(this._buffer, this.Length);
			this._currentLineLength += bytes.Length;
			this._currentBufferUsed += bytes.Length;
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000E3C70 File Offset: 0x000E1E70
		internal void AppendCRLF(bool includeSpace)
		{
			this.AppendFooter();
			this.Append(new byte[]
			{
				13,
				10
			});
			this._currentLineLength = 0;
			if (includeSpace)
			{
				this.Append(32);
			}
			this.AppendHeader();
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000E3CA6 File Offset: 0x000E1EA6
		internal void AppendHeader()
		{
			if (this.Header != null && this.Header.Length != 0)
			{
				this.Append(this.Header);
			}
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000E3CC5 File Offset: 0x000E1EC5
		internal void AppendFooter()
		{
			if (this.Footer != null && this.Footer.Length != 0)
			{
				this.Append(this.Footer);
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x000E3CE4 File Offset: 0x000E1EE4
		internal int MaxLineLength
		{
			get
			{
				return this._maxLineLength;
			}
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000E3CEC File Offset: 0x000E1EEC
		internal void Reset()
		{
			this._currentBufferUsed = 0;
			this._currentLineLength = 0;
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000E3CFC File Offset: 0x000E1EFC
		internal void BufferFlushed()
		{
			this._currentBufferUsed = 0;
		}

		// Token: 0x04002809 RID: 10249
		protected readonly byte[] _header;

		// Token: 0x0400280A RID: 10250
		protected readonly byte[] _footer;

		// Token: 0x0400280B RID: 10251
		protected readonly int _maxLineLength;

		// Token: 0x0400280C RID: 10252
		protected byte[] _buffer;

		// Token: 0x0400280D RID: 10253
		protected int _currentLineLength;

		// Token: 0x0400280E RID: 10254
		protected int _currentBufferUsed;

		// Token: 0x0400280F RID: 10255
		protected const int DefaultBufferSize = 1024;
	}
}

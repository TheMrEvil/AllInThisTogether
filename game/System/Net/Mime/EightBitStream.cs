using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x020007FC RID: 2044
	internal class EightBitStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x0600413C RID: 16700 RVA: 0x000E0D80 File Offset: 0x000DEF80
		private WriteStateInfoBase WriteState
		{
			get
			{
				WriteStateInfoBase result;
				if ((result = this._writeState) == null)
				{
					result = (this._writeState = new WriteStateInfoBase());
				}
				return result;
			}
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000E0DA5 File Offset: 0x000DEFA5
		internal EightBitStream(Stream stream) : base(stream)
		{
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000E0DAE File Offset: 0x000DEFAE
		internal EightBitStream(Stream stream, bool shouldEncodeLeadingDots) : this(stream)
		{
			this._shouldEncodeLeadingDots = shouldEncodeLeadingDots;
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x000E0DC0 File Offset: 0x000DEFC0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			IAsyncResult result;
			if (this._shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				result = base.BeginWrite(this.WriteState.Buffer, 0, this.WriteState.Length, callback, state);
			}
			else
			{
				result = base.BeginWrite(buffer, offset, count, callback, state);
			}
			return result;
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x000E0E47 File Offset: 0x000DF047
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.EndWrite(asyncResult);
			this.WriteState.BufferFlushed();
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x000E0E5C File Offset: 0x000DF05C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
				return;
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x000E0EE4 File Offset: 0x000DF0E4
		private void EncodeLines(byte[] buffer, int offset, int count)
		{
			int num = offset;
			while (num < offset + count && num < buffer.Length)
			{
				if (buffer[num] == 13 && num + 1 < offset + count && buffer[num + 1] == 10)
				{
					this.WriteState.AppendCRLF(false);
					num++;
				}
				else if (this.WriteState.CurrentLineLength == 0 && buffer[num] == 46)
				{
					this.WriteState.Append(46);
					this.WriteState.Append(buffer[num]);
				}
				else
				{
					this.WriteState.Append(buffer[num]);
				}
				num++;
			}
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x000075E1 File Offset: 0x000057E1
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x0000829A File Offset: 0x0000649A
		public int DecodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x0000829A File Offset: 0x0000649A
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x0000829A File Offset: 0x0000649A
		public string GetEncodedString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002798 RID: 10136
		private WriteStateInfoBase _writeState;

		// Token: 0x04002799 RID: 10137
		private bool _shouldEncodeLeadingDots;
	}
}

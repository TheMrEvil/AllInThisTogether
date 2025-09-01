using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000053 RID: 83
	internal sealed class DummyStream : Stream
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x000107EF File Offset: 0x0000E9EF
		public DummyStream()
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000107F7 File Offset: 0x0000E9F7
		private void DontDoIt()
		{
			throw new Exception(SR.GetString("Internal Error"));
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00010808 File Offset: 0x0000EA08
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00010810 File Offset: 0x0000EA10
		public override long Position
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00010808 File Offset: 0x0000EA08
		public override long Length
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00010810 File Offset: 0x0000EA10
		public override void SetLength(long value)
		{
			this._size = value;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00010819 File Offset: 0x0000EA19
		public override long Seek(long value, SeekOrigin loc)
		{
			this.DontDoIt();
			return -1L;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00007EED File Offset: 0x000060ED
		public override void Flush()
		{
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00010823 File Offset: 0x0000EA23
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.DontDoIt();
			return -1;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001082C File Offset: 0x0000EA2C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._size += (long)count;
		}

		// Token: 0x04000539 RID: 1337
		private long _size;
	}
}

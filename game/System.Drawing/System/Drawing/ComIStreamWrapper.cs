using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Drawing
{
	// Token: 0x02000068 RID: 104
	internal sealed class ComIStreamWrapper : IStream
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000ACF7 File Offset: 0x00008EF7
		internal ComIStreamWrapper(Stream stream)
		{
			this.baseStream = stream;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000AD10 File Offset: 0x00008F10
		private void SetSizeToPosition()
		{
			if (this.position != -1L)
			{
				if (this.position > this.baseStream.Length)
				{
					this.baseStream.SetLength(this.position);
				}
				this.baseStream.Position = this.position;
				this.position = -1L;
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000AD64 File Offset: 0x00008F64
		public void Read(byte[] pv, int cb, IntPtr pcbRead)
		{
			int val = 0;
			if (cb != 0)
			{
				this.SetSizeToPosition();
				val = this.baseStream.Read(pv, 0, cb);
			}
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbRead, val);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000AD9F File Offset: 0x00008F9F
		public void Write(byte[] pv, int cb, IntPtr pcbWritten)
		{
			if (cb != 0)
			{
				this.SetSizeToPosition();
				this.baseStream.Write(pv, 0, cb);
			}
			if (pcbWritten != IntPtr.Zero)
			{
				Marshal.WriteInt32(pcbWritten, cb);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
		{
			long length = this.baseStream.Length;
			long num;
			switch (dwOrigin)
			{
			case 0:
				num = dlibMove;
				break;
			case 1:
				if (this.position == -1L)
				{
					num = this.baseStream.Position + dlibMove;
				}
				else
				{
					num = this.position + dlibMove;
				}
				break;
			case 2:
				num = length + dlibMove;
				break;
			default:
				throw new ExternalException(null, -2147287039);
			}
			if (num > length)
			{
				this.position = num;
			}
			else
			{
				this.baseStream.Position = num;
				this.position = -1L;
			}
			if (plibNewPosition != IntPtr.Zero)
			{
				Marshal.WriteInt64(plibNewPosition, num);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000AE6B File Offset: 0x0000906B
		public void SetSize(long libNewSize)
		{
			this.baseStream.SetLength(libNewSize);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000AE7C File Offset: 0x0000907C
		public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
		{
			long num = 0L;
			if (cb != 0L)
			{
				int num2;
				if (cb < 4096L)
				{
					num2 = (int)cb;
				}
				else
				{
					num2 = 4096;
				}
				byte[] array = new byte[num2];
				this.SetSizeToPosition();
				int num3;
				while ((num3 = this.baseStream.Read(array, 0, num2)) != 0)
				{
					pstm.Write(array, num3, IntPtr.Zero);
					num += (long)num3;
					if (num >= cb)
					{
						break;
					}
					if (cb - num < 4096L)
					{
						num2 = (int)(cb - num);
					}
				}
			}
			if (pcbRead != IntPtr.Zero)
			{
				Marshal.WriteInt64(pcbRead, num);
			}
			if (pcbWritten != IntPtr.Zero)
			{
				Marshal.WriteInt64(pcbWritten, num);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000AF14 File Offset: 0x00009114
		public void Commit(int grfCommitFlags)
		{
			this.baseStream.Flush();
			this.SetSizeToPosition();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000AF27 File Offset: 0x00009127
		public void Revert()
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000AF27 File Offset: 0x00009127
		public void LockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000AF27 File Offset: 0x00009127
		public void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000AF34 File Offset: 0x00009134
		public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
		{
			pstatstg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			pstatstg.cbSize = this.baseStream.Length;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000AF4E File Offset: 0x0000914E
		public void Clone(out IStream ppstm)
		{
			ppstm = null;
			throw new ExternalException(null, -2147287039);
		}

		// Token: 0x04000456 RID: 1110
		private const int STG_E_INVALIDFUNCTION = -2147287039;

		// Token: 0x04000457 RID: 1111
		private readonly Stream baseStream;

		// Token: 0x04000458 RID: 1112
		private long position = -1L;
	}
}

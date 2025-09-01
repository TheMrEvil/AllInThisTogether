using System;

namespace WebSocketSharp.Net
{
	// Token: 0x02000035 RID: 53
	internal class Chunk
	{
		// Token: 0x060003AA RID: 938 RVA: 0x00016A92 File Offset: 0x00014C92
		public Chunk(byte[] data)
		{
			this._data = data;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00016AA4 File Offset: 0x00014CA4
		public int ReadLeft
		{
			get
			{
				return this._data.Length - this._offset;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00016AC8 File Offset: 0x00014CC8
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this._data.Length - this._offset;
			bool flag = num == 0;
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
				Buffer.BlockCopy(this._data, this._offset, buffer, offset, count);
				this._offset += count;
				result = count;
			}
			return result;
		}

		// Token: 0x0400018A RID: 394
		private byte[] _data;

		// Token: 0x0400018B RID: 395
		private int _offset;
	}
}

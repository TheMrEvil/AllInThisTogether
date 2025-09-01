using System;
using System.Runtime.InteropServices;

namespace Internal.Cryptography
{
	// Token: 0x020000CB RID: 203
	internal struct PinAndClear : IDisposable
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00017CE0 File Offset: 0x00015EE0
		internal static PinAndClear Track(byte[] data)
		{
			return new PinAndClear
			{
				_gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned),
				_data = data
			};
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00017D0C File Offset: 0x00015F0C
		public void Dispose()
		{
			Array.Clear(this._data, 0, this._data.Length);
			this._gcHandle.Free();
		}

		// Token: 0x04000FE8 RID: 4072
		private byte[] _data;

		// Token: 0x04000FE9 RID: 4073
		private GCHandle _gcHandle;
	}
}

using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004B RID: 75
	internal sealed class LongNormalizer : Normalizer
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x000102DC File Offset: 0x0000E4DC
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((long)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00010328 File Offset: 0x0000E528
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt64(array, 0));
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001037B File Offset: 0x0000E57B
		internal override int Size
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public LongNormalizer()
		{
		}
	}
}

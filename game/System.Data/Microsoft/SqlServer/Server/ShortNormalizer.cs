using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000047 RID: 71
	internal sealed class ShortNormalizer : Normalizer
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00010094 File Offset: 0x0000E294
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((short)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000100E0 File Offset: 0x0000E2E0
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[2];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt16(array, 0));
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00010133 File Offset: 0x0000E333
		internal override int Size
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public ShortNormalizer()
		{
		}
	}
}

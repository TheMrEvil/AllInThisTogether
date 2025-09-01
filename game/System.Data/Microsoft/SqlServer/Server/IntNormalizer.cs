using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000049 RID: 73
	internal sealed class IntNormalizer : Normalizer
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x000101B8 File Offset: 0x0000E3B8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((int)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00010204 File Offset: 0x0000E404
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt32(array, 0));
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00010257 File Offset: 0x0000E457
		internal override int Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public IntNormalizer()
		{
		}
	}
}

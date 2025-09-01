using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004D RID: 77
	internal sealed class FloatNormalizer : Normalizer
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00010400 File Offset: 0x0000E600
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			float num = (float)base.GetValue(fi, obj);
			byte[] bytes = BitConverter.GetBytes(num);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				if ((bytes[0] & 128) == 0)
				{
					byte[] array = bytes;
					int num2 = 0;
					array[num2] ^= 128;
				}
				else if (num < 0f)
				{
					base.FlipAllBits(bytes);
				}
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00010468 File Offset: 0x0000E668
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				if ((array[0] & 128) > 0)
				{
					byte[] array2 = array;
					int num = 0;
					array2[num] ^= 128;
				}
				else
				{
					base.FlipAllBits(array);
				}
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToSingle(array, 0));
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00010257 File Offset: 0x0000E457
		internal override int Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public FloatNormalizer()
		{
		}
	}
}

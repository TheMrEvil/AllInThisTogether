using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200021E RID: 542
	[NativeHeader("Runtime/Scripting/TextAsset.h")]
	public class TextAsset : Object
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600177E RID: 6014
		public extern byte[] bytes { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600177F RID: 6015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] GetPreviewBytes(int maxByteCount);

		// Token: 0x06001780 RID: 6016
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstance([Writable] TextAsset self, string text);

		// Token: 0x06001781 RID: 6017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetDataPtr();

		// Token: 0x06001782 RID: 6018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern long GetDataSize();

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x00025F64 File Offset: 0x00024164
		public string text
		{
			get
			{
				return TextAsset.DecodeString(this.bytes);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00025F81 File Offset: 0x00024181
		public long dataSize
		{
			get
			{
				return this.GetDataSize();
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00025F8C File Offset: 0x0002418C
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00025FA4 File Offset: 0x000241A4
		public TextAsset() : this(TextAsset.CreateOptions.CreateNativeObject, null)
		{
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00025FB0 File Offset: 0x000241B0
		public TextAsset(string text) : this(TextAsset.CreateOptions.CreateNativeObject, text)
		{
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00025FBC File Offset: 0x000241BC
		internal TextAsset(TextAsset.CreateOptions options, string text)
		{
			bool flag = options == TextAsset.CreateOptions.CreateNativeObject;
			if (flag)
			{
				TextAsset.Internal_CreateInstance(this, text);
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00025FE4 File Offset: 0x000241E4
		public unsafe NativeArray<T> GetData<T>() where T : struct
		{
			long dataSize = this.GetDataSize();
			long num = (long)UnsafeUtility.SizeOf<T>();
			bool flag = dataSize % num != 0L;
			if (flag)
			{
				throw new ArgumentException(string.Format("Type passed to {0} can't capture the asset data. Data size is {1} which is not a multiple of type size {2}", "GetData", dataSize, num));
			}
			long num2 = dataSize / num;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataPtr(), (int)num2, Allocator.None);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00026050 File Offset: 0x00024250
		internal string GetPreview(int maxChars)
		{
			return TextAsset.DecodeString(this.GetPreviewBytes(maxChars * 4));
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00026070 File Offset: 0x00024270
		internal static string DecodeString(byte[] bytes)
		{
			int num = TextAsset.EncodingUtility.encodingLookup.Length;
			int i = 0;
			int num2;
			while (i < num)
			{
				byte[] key = TextAsset.EncodingUtility.encodingLookup[i].Key;
				num2 = key.Length;
				bool flag = bytes.Length >= num2;
				if (flag)
				{
					for (int j = 0; j < num2; j++)
					{
						bool flag2 = key[j] != bytes[j];
						if (flag2)
						{
							num2 = -1;
						}
					}
					bool flag3 = num2 < 0;
					if (!flag3)
					{
						try
						{
							Encoding value = TextAsset.EncodingUtility.encodingLookup[i].Value;
							return value.GetString(bytes, num2, bytes.Length - num2);
						}
						catch
						{
						}
					}
				}
				IL_A2:
				i++;
				continue;
				goto IL_A2;
			}
			num2 = 0;
			Encoding targetEncoding = TextAsset.EncodingUtility.targetEncoding;
			return targetEncoding.GetString(bytes, num2, bytes.Length - num2);
		}

		// Token: 0x0200021F RID: 543
		internal enum CreateOptions
		{
			// Token: 0x04000810 RID: 2064
			None,
			// Token: 0x04000811 RID: 2065
			CreateNativeObject
		}

		// Token: 0x02000220 RID: 544
		private static class EncodingUtility
		{
			// Token: 0x0600178C RID: 6028 RVA: 0x0002615C File Offset: 0x0002435C
			static EncodingUtility()
			{
				Encoding encoding = new UTF32Encoding(true, true, true);
				Encoding encoding2 = new UTF32Encoding(false, true, true);
				Encoding encoding3 = new UnicodeEncoding(true, true, true);
				Encoding encoding4 = new UnicodeEncoding(false, true, true);
				Encoding encoding5 = new UTF8Encoding(true, true);
				TextAsset.EncodingUtility.encodingLookup = new KeyValuePair<byte[], Encoding>[]
				{
					new KeyValuePair<byte[], Encoding>(encoding.GetPreamble(), encoding),
					new KeyValuePair<byte[], Encoding>(encoding2.GetPreamble(), encoding2),
					new KeyValuePair<byte[], Encoding>(encoding3.GetPreamble(), encoding3),
					new KeyValuePair<byte[], Encoding>(encoding4.GetPreamble(), encoding4),
					new KeyValuePair<byte[], Encoding>(encoding5.GetPreamble(), encoding5)
				};
			}

			// Token: 0x04000812 RID: 2066
			internal static readonly KeyValuePair<byte[], Encoding>[] encodingLookup;

			// Token: 0x04000813 RID: 2067
			internal static readonly Encoding targetEncoding = Encoding.GetEncoding(Encoding.UTF8.CodePage, new EncoderReplacementFallback("�"), new DecoderReplacementFallback("�"));
		}
	}
}

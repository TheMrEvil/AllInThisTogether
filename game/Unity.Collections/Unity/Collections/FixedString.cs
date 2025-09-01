using System;

namespace Unity.Collections
{
	// Token: 0x020000A2 RID: 162
	[BurstCompatible]
	public static class FixedString
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000D38C File Offset: 0x0000B58C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000D468 File Offset: 0x0000B668
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000D530 File Offset: 0x0000B730
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000D5A0 File Offset: 0x0000B7A0
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000D610 File Offset: 0x0000B810
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000D680 File Offset: 0x0000B880
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000D74C File Offset: 0x0000B94C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000D828 File Offset: 0x0000BA28
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000D884 File Offset: 0x0000BA84
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000D940 File Offset: 0x0000BB40
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000D99C File Offset: 0x0000BB9C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000DA58 File Offset: 0x0000BC58
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000DB38 File Offset: 0x0000BD38
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000DC08 File Offset: 0x0000BE08
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000DE2C File Offset: 0x0000C02C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000DE9C File Offset: 0x0000C09C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000DF5C File Offset: 0x0000C15C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000E01C File Offset: 0x0000C21C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000E068 File Offset: 0x0000C268
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000E144 File Offset: 0x0000C344
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000E20C File Offset: 0x0000C40C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000E27C File Offset: 0x0000C47C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000E35C File Offset: 0x0000C55C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000E3BC File Offset: 0x0000C5BC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000E428 File Offset: 0x0000C628
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000E498 File Offset: 0x0000C698
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000E504 File Offset: 0x0000C704
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000E560 File Offset: 0x0000C760
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000E61C File Offset: 0x0000C81C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000E678 File Offset: 0x0000C878
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000E720 File Offset: 0x0000C920
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000E780 File Offset: 0x0000C980
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000E828 File Offset: 0x0000CA28
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000E888 File Offset: 0x0000CA88
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000E948 File Offset: 0x0000CB48
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000E994 File Offset: 0x0000CB94
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000EA50 File Offset: 0x0000CC50
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000EB44 File Offset: 0x0000CD44
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000EB90 File Offset: 0x0000CD90
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg3);
			ref result.AppendFormat(formatString, arg0, arg1, arg2, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000EC88 File Offset: 0x0000CE88
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000ED68 File Offset: 0x0000CF68
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000EE38 File Offset: 0x0000D038
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000EEAC File Offset: 0x0000D0AC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000EF1C File Offset: 0x0000D11C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000EF7C File Offset: 0x0000D17C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000F05C File Offset: 0x0000D25C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000F12C File Offset: 0x0000D32C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000F18C File Offset: 0x0000D38C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000F24C File Offset: 0x0000D44C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000F298 File Offset: 0x0000D498
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000F308 File Offset: 0x0000D508
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000F37C File Offset: 0x0000D57C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000F44C File Offset: 0x0000D64C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000F534 File Offset: 0x0000D734
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000F60C File Offset: 0x0000D80C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000F67C File Offset: 0x0000D87C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000F760 File Offset: 0x0000D960
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000F820 File Offset: 0x0000DA20
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000F884 File Offset: 0x0000DA84
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000F934 File Offset: 0x0000DB34
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000FA14 File Offset: 0x0000DC14
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000FA84 File Offset: 0x0000DC84
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000FB54 File Offset: 0x0000DD54
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000FC38 File Offset: 0x0000DE38
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000FC98 File Offset: 0x0000DE98
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000FD08 File Offset: 0x0000DF08
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000FD78 File Offset: 0x0000DF78
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000FE48 File Offset: 0x0000E048
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000FF08 File Offset: 0x0000E108
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000FF68 File Offset: 0x0000E168
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00010014 File Offset: 0x0000E214
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00010074 File Offset: 0x0000E274
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000100D4 File Offset: 0x0000E2D4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00010120 File Offset: 0x0000E320
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00010180 File Offset: 0x0000E380
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000101E4 File Offset: 0x0000E3E4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00010244 File Offset: 0x0000E444
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00010294 File Offset: 0x0000E494
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000102F4 File Offset: 0x0000E4F4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00010354 File Offset: 0x0000E554
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000103B4 File Offset: 0x0000E5B4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00010400 File Offset: 0x0000E600
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001044C File Offset: 0x0000E64C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001049C File Offset: 0x0000E69C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000104E8 File Offset: 0x0000E6E8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg3, '.');
			ref result.AppendFormat(formatString, arg0, arg1, arg2, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00010524 File Offset: 0x0000E724
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00010590 File Offset: 0x0000E790
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00010600 File Offset: 0x0000E800
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001066C File Offset: 0x0000E86C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000106C8 File Offset: 0x0000E8C8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00010738 File Offset: 0x0000E938
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000107A8 File Offset: 0x0000E9A8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00010818 File Offset: 0x0000EA18
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00010878 File Offset: 0x0000EA78
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000108E4 File Offset: 0x0000EAE4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00010954 File Offset: 0x0000EB54
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000109C0 File Offset: 0x0000EBC0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00010A1C File Offset: 0x0000EC1C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00010A78 File Offset: 0x0000EC78
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00010B34 File Offset: 0x0000ED34
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00010B80 File Offset: 0x0000ED80
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00010C60 File Offset: 0x0000EE60
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00010CD0 File Offset: 0x0000EED0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00010D30 File Offset: 0x0000EF30
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00010DA0 File Offset: 0x0000EFA0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00010E14 File Offset: 0x0000F014
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00010E84 File Offset: 0x0000F084
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00010F54 File Offset: 0x0000F154
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00011034 File Offset: 0x0000F234
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00011094 File Offset: 0x0000F294
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000110F4 File Offset: 0x0000F2F4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00011154 File Offset: 0x0000F354
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000111B4 File Offset: 0x0000F3B4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00011200 File Offset: 0x0000F400
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001126C File Offset: 0x0000F46C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000112DC File Offset: 0x0000F4DC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00011348 File Offset: 0x0000F548
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000113A4 File Offset: 0x0000F5A4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00011414 File Offset: 0x0000F614
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00011484 File Offset: 0x0000F684
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000114F4 File Offset: 0x0000F6F4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00011554 File Offset: 0x0000F754
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000115C0 File Offset: 0x0000F7C0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00011630 File Offset: 0x0000F830
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			ref fixedString32Bytes4.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, fixedString32Bytes4);
			return result;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001169C File Offset: 0x0000F89C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000116F8 File Offset: 0x0000F8F8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00011754 File Offset: 0x0000F954
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000117B4 File Offset: 0x0000F9B4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00011810 File Offset: 0x0000FA10
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001185C File Offset: 0x0000FA5C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000118B8 File Offset: 0x0000FAB8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00011918 File Offset: 0x0000FB18
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00011974 File Offset: 0x0000FB74
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000119C0 File Offset: 0x0000FBC0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00011A20 File Offset: 0x0000FC20
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00011A80 File Offset: 0x0000FC80
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00011B2C File Offset: 0x0000FD2C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00011B88 File Offset: 0x0000FD88
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00011C44 File Offset: 0x0000FE44
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00011C90 File Offset: 0x0000FE90
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00011CDC File Offset: 0x0000FEDC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00011D28 File Offset: 0x0000FF28
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg3);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00011D74 File Offset: 0x0000FF74
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg3);
			ref result.AppendFormat(formatString, arg0, arg1, arg2, fixedString32Bytes);
			return result;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00011E0C File Offset: 0x0001000C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00011E68 File Offset: 0x00010068
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00011EC4 File Offset: 0x000100C4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00011F10 File Offset: 0x00010110
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00011F6C File Offset: 0x0001016C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00011FCC File Offset: 0x000101CC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00012028 File Offset: 0x00010228
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00012074 File Offset: 0x00010274
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000120D0 File Offset: 0x000102D0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001212C File Offset: 0x0001032C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00012188 File Offset: 0x00010388
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000121D4 File Offset: 0x000103D4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00012220 File Offset: 0x00010420
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001226C File Offset: 0x0001046C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000122B8 File Offset: 0x000104B8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, arg3);
			return result;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000122F0 File Offset: 0x000104F0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001234C File Offset: 0x0001054C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000123AC File Offset: 0x000105AC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00012408 File Offset: 0x00010608
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00012454 File Offset: 0x00010654
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000124B4 File Offset: 0x000106B4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00012514 File Offset: 0x00010714
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00012574 File Offset: 0x00010774
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000125C4 File Offset: 0x000107C4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00012620 File Offset: 0x00010820
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00012680 File Offset: 0x00010880
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000126DC File Offset: 0x000108DC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00012728 File Offset: 0x00010928
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00012774 File Offset: 0x00010974
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000127C4 File Offset: 0x000109C4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00012810 File Offset: 0x00010A10
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2, '.');
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, arg3);
			return result;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001284C File Offset: 0x00010A4C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000128A8 File Offset: 0x00010AA8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00012904 File Offset: 0x00010B04
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00012960 File Offset: 0x00010B60
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000129AC File Offset: 0x00010BAC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00012A08 File Offset: 0x00010C08
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00012A68 File Offset: 0x00010C68
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00012AC4 File Offset: 0x00010CC4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00012B10 File Offset: 0x00010D10
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00012B6C File Offset: 0x00010D6C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00012BC8 File Offset: 0x00010DC8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3, arg3);
			return result;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00012C24 File Offset: 0x00010E24
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00012C70 File Offset: 0x00010E70
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00012CBC File Offset: 0x00010EBC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00012D08 File Offset: 0x00010F08
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2, arg3);
			return result;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00012D54 File Offset: 0x00010F54
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes, arg3);
			return result;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00012D8C File Offset: 0x00010F8C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00012DD8 File Offset: 0x00010FD8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00012E24 File Offset: 0x00011024
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00012E70 File Offset: 0x00011070
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, arg3);
			return result;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00012EA8 File Offset: 0x000110A8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00012EF4 File Offset: 0x000110F4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00012F44 File Offset: 0x00011144
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00012F90 File Offset: 0x00011190
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, arg3);
			return result;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00012FCC File Offset: 0x000111CC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00013018 File Offset: 0x00011218
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00013064 File Offset: 0x00011264
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2, arg3);
			return result;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000130B0 File Offset: 0x000112B0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2, arg3);
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000130E8 File Offset: 0x000112E8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, arg3);
			return result;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00013120 File Offset: 0x00011320
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, arg3);
			return result;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001315C File Offset: 0x0001135C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2, arg3);
			return result;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00013194 File Offset: 0x00011394
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3, T4>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, T4 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes where T4 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes result = default(FixedString512Bytes);
			ref result.AppendFormat(formatString, arg0, arg1, arg2, arg3);
			return result;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000131BC File Offset: 0x000113BC
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00013214 File Offset: 0x00011414
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00013270 File Offset: 0x00011470
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000132C8 File Offset: 0x000114C8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, int arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00013310 File Offset: 0x00011510
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001336C File Offset: 0x0001156C
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000133C8 File Offset: 0x000115C8
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00013424 File Offset: 0x00011624
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, float arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00013470 File Offset: 0x00011670
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000134C8 File Offset: 0x000116C8
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00013524 File Offset: 0x00011724
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, int arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001357C File Offset: 0x0001177C
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, string arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000135C4 File Offset: 0x000117C4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, T1 arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001360C File Offset: 0x0001180C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, T1 arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00013658 File Offset: 0x00011858
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, T1 arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000136A0 File Offset: 0x000118A0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000136D8 File Offset: 0x000118D8
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00013734 File Offset: 0x00011934
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00013790 File Offset: 0x00011990
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000137EC File Offset: 0x000119EC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, int arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00013838 File Offset: 0x00011A38
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00013894 File Offset: 0x00011A94
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000138F4 File Offset: 0x00011AF4
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00013950 File Offset: 0x00011B50
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, float arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001399C File Offset: 0x00011B9C
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000139F8 File Offset: 0x00011BF8
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00013A54 File Offset: 0x00011C54
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, float arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00013AB0 File Offset: 0x00011CB0
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, string arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00013AFC File Offset: 0x00011CFC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, T1 arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00013B48 File Offset: 0x00011D48
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, T1 arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00013B94 File Offset: 0x00011D94
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, T1 arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00013BE0 File Offset: 0x00011DE0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2, '.');
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00013C18 File Offset: 0x00011E18
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00013C70 File Offset: 0x00011E70
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00013CCC File Offset: 0x00011ECC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00013D24 File Offset: 0x00011F24
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, int arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00013D6C File Offset: 0x00011F6C
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00013DC8 File Offset: 0x00011FC8
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00013E24 File Offset: 0x00012024
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00013E80 File Offset: 0x00012080
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, float arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00013ECC File Offset: 0x000120CC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00013F24 File Offset: 0x00012124
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00013F80 File Offset: 0x00012180
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, string arg2)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			ref fixedString32Bytes3.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, fixedString32Bytes3);
			return result;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00013FD8 File Offset: 0x000121D8
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, string arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00014020 File Offset: 0x00012220
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, T1 arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00014068 File Offset: 0x00012268
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, T1 arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000140B4 File Offset: 0x000122B4
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, T1 arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg2);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000140FC File Offset: 0x000122FC
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg2);
			ref result.AppendFormat(formatString, arg0, arg1, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00014134 File Offset: 0x00012334
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, int arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001417C File Offset: 0x0001237C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, int arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000141C8 File Offset: 0x000123C8
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, int arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00014210 File Offset: 0x00012410
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, int arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2);
			return result;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00014248 File Offset: 0x00012448
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, float arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00014294 File Offset: 0x00012494
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, float arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000142E0 File Offset: 0x000124E0
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, float arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001432C File Offset: 0x0001252C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, float arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2);
			return result;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00014364 File Offset: 0x00012564
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, string arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000143AC File Offset: 0x000125AC
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, string arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000143F8 File Offset: 0x000125F8
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, string arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2, arg2);
			return result;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00014440 File Offset: 0x00012640
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, string arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes, arg2);
			return result;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00014478 File Offset: 0x00012678
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, int arg0, T1 arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2);
			return result;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000144B0 File Offset: 0x000126B0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, float arg0, T1 arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2);
			return result;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000144E8 File Offset: 0x000126E8
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, string arg0, T1 arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1, arg2);
			return result;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00014520 File Offset: 0x00012720
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2, T3>(FixedString128Bytes formatString, T1 arg0, T2 arg1, T3 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			ref result.AppendFormat(formatString, arg0, arg1, arg2);
			return result;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00014548 File Offset: 0x00012748
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00014590 File Offset: 0x00012790
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000145D8 File Offset: 0x000127D8
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00014620 File Offset: 0x00012820
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, int arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00014654 File Offset: 0x00012854
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001469C File Offset: 0x0001289C
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000146E8 File Offset: 0x000128E8
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00014730 File Offset: 0x00012930
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, float arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1, '.');
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00014768 File Offset: 0x00012968
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000147B0 File Offset: 0x000129B0
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000147F8 File Offset: 0x000129F8
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			ref fixedString32Bytes2.Append(arg1);
			ref result.AppendFormat(formatString, fixedString32Bytes, fixedString32Bytes2);
			return result;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00014840 File Offset: 0x00012A40
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, string arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg1);
			ref result.AppendFormat(formatString, arg0, fixedString32Bytes);
			return result;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00014874 File Offset: 0x00012A74
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, T1 arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1);
			return result;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000148A8 File Offset: 0x00012AA8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, T1 arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1);
			return result;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000148E0 File Offset: 0x00012AE0
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, T1 arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			ref result.AppendFormat(formatString, fixedString32Bytes, arg1);
			return result;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00014914 File Offset: 0x00012B14
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			ref result.AppendFormat(formatString, arg0, arg1);
			return result;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00014938 File Offset: 0x00012B38
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			ref result.AppendFormat(formatString, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001496C File Offset: 0x00012B6C
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0, '.');
			ref result.AppendFormat(formatString, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000149A0 File Offset: 0x00012BA0
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0)
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			ref fixedString32Bytes.Append(arg0);
			ref result.AppendFormat(formatString, fixedString32Bytes);
			return result;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000149D4 File Offset: 0x00012BD4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes result = default(FixedString128Bytes);
			ref result.AppendFormat(formatString, arg0);
			return result;
		}
	}
}

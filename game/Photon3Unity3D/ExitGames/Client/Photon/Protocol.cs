using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200002C RID: 44
	public class Protocol
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x0000D31C File Offset: 0x0000B51C
		public static bool TryRegisterType(Type type, byte typeCode, SerializeMethod serializeFunction, DeserializeMethod deserializeFunction)
		{
			bool flag = Protocol.CodeDict.ContainsKey(typeCode) || Protocol.TypeDict.ContainsKey(type);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				CustomType value = new CustomType(type, typeCode, serializeFunction, deserializeFunction);
				Protocol.CodeDict.Add(typeCode, value);
				Protocol.TypeDict.Add(type, value);
				result = true;
			}
			return result;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000D378 File Offset: 0x0000B578
		public static bool TryRegisterType(Type type, byte typeCode, SerializeStreamMethod serializeFunction, DeserializeStreamMethod deserializeFunction)
		{
			bool flag = Protocol.CodeDict.ContainsKey(typeCode) || Protocol.TypeDict.ContainsKey(type);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				CustomType value = new CustomType(type, typeCode, serializeFunction, deserializeFunction);
				Protocol.CodeDict.Add(typeCode, value);
				Protocol.TypeDict.Add(type, value);
				result = true;
			}
			return result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		[Obsolete]
		public static byte[] Serialize(object obj)
		{
			bool flag = Protocol.ProtocolDefault == null;
			if (flag)
			{
				Protocol.ProtocolDefault = new Protocol16();
			}
			IProtocol protocolDefault = Protocol.ProtocolDefault;
			byte[] result;
			lock (protocolDefault)
			{
				result = Protocol.ProtocolDefault.Serialize(obj);
			}
			return result;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D438 File Offset: 0x0000B638
		[Obsolete]
		public static object Deserialize(byte[] serializedData)
		{
			bool flag = Protocol.ProtocolDefault == null;
			if (flag)
			{
				Protocol.ProtocolDefault = new Protocol16();
			}
			IProtocol protocolDefault = Protocol.ProtocolDefault;
			object result;
			lock (protocolDefault)
			{
				result = Protocol.ProtocolDefault.Deserialize(serializedData);
			}
			return result;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D49C File Offset: 0x0000B69C
		public static void Serialize(short value, byte[] target, ref int targetOffset)
		{
			int num = targetOffset;
			targetOffset = num + 1;
			target[num] = (byte)(value >> 8);
			num = targetOffset;
			targetOffset = num + 1;
			target[num] = (byte)value;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public static void Serialize(int value, byte[] target, ref int targetOffset)
		{
			int num = targetOffset;
			targetOffset = num + 1;
			target[num] = (byte)(value >> 24);
			num = targetOffset;
			targetOffset = num + 1;
			target[num] = (byte)(value >> 16);
			num = targetOffset;
			targetOffset = num + 1;
			target[num] = (byte)(value >> 8);
			num = targetOffset;
			targetOffset = num + 1;
			target[num] = (byte)value;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D514 File Offset: 0x0000B714
		public static void Serialize(float value, byte[] target, ref int targetOffset)
		{
			float[] obj = Protocol.memFloatBlock;
			lock (obj)
			{
				Protocol.memFloatBlock[0] = value;
				Buffer.BlockCopy(Protocol.memFloatBlock, 0, target, targetOffset, 4);
			}
			bool isLittleEndian = BitConverter.IsLittleEndian;
			if (isLittleEndian)
			{
				byte b = target[targetOffset];
				byte b2 = target[targetOffset + 1];
				target[targetOffset] = target[targetOffset + 3];
				target[targetOffset + 1] = target[targetOffset + 2];
				target[targetOffset + 2] = b2;
				target[targetOffset + 3] = b;
			}
			targetOffset += 4;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		public static void Deserialize(out int value, byte[] source, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			int num2 = (int)source[num] << 24;
			num = offset;
			offset = num + 1;
			int num3 = num2 | (int)source[num] << 16;
			num = offset;
			offset = num + 1;
			int num4 = num3 | (int)source[num] << 8;
			num = offset;
			offset = num + 1;
			value = (num4 | (int)source[num]);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		public static void Deserialize(out short value, byte[] source, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			byte b = (byte)(source[num] << 8);
			num = offset;
			offset = num + 1;
			value = (short)(b | source[num]);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D624 File Offset: 0x0000B824
		public static void Deserialize(out float value, byte[] source, ref int offset)
		{
			bool isLittleEndian = BitConverter.IsLittleEndian;
			if (isLittleEndian)
			{
				byte[] obj = Protocol.memDeserialize;
				lock (obj)
				{
					byte[] array = Protocol.memDeserialize;
					byte[] array2 = array;
					int num = 3;
					int num2 = offset;
					offset = num2 + 1;
					array2[num] = source[num2];
					byte[] array3 = array;
					int num3 = 2;
					num2 = offset;
					offset = num2 + 1;
					array3[num3] = source[num2];
					byte[] array4 = array;
					int num4 = 1;
					num2 = offset;
					offset = num2 + 1;
					array4[num4] = source[num2];
					byte[] array5 = array;
					int num5 = 0;
					num2 = offset;
					offset = num2 + 1;
					array5[num5] = source[num2];
					value = BitConverter.ToSingle(array, 0);
				}
			}
			else
			{
				value = BitConverter.ToSingle(source, offset);
				offset += 4;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public Protocol()
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D6DD File Offset: 0x0000B8DD
		// Note: this type is marked as 'beforefieldinit'.
		static Protocol()
		{
		}

		// Token: 0x04000178 RID: 376
		internal static readonly Dictionary<Type, CustomType> TypeDict = new Dictionary<Type, CustomType>();

		// Token: 0x04000179 RID: 377
		internal static readonly Dictionary<byte, CustomType> CodeDict = new Dictionary<byte, CustomType>();

		// Token: 0x0400017A RID: 378
		private static IProtocol ProtocolDefault;

		// Token: 0x0400017B RID: 379
		private static readonly float[] memFloatBlock = new float[1];

		// Token: 0x0400017C RID: 380
		private static readonly byte[] memDeserialize = new byte[4];
	}
}

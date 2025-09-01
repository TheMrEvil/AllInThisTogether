using System;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Photon.Realtime
{
	// Token: 0x02000004 RID: 4
	internal static class CustomTypesUnity
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000266C File Offset: 0x0000086C
		internal static void Register()
		{
			PhotonPeer.RegisterType(typeof(Vector2), 87, new SerializeStreamMethod(CustomTypesUnity.SerializeVector2), new DeserializeStreamMethod(CustomTypesUnity.DeserializeVector2));
			PhotonPeer.RegisterType(typeof(Vector3), 86, new SerializeStreamMethod(CustomTypesUnity.SerializeVector3), new DeserializeStreamMethod(CustomTypesUnity.DeserializeVector3));
			PhotonPeer.RegisterType(typeof(Quaternion), 81, new SerializeStreamMethod(CustomTypesUnity.SerializeQuaternion), new DeserializeStreamMethod(CustomTypesUnity.DeserializeQuaternion));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026F8 File Offset: 0x000008F8
		private static short SerializeVector3(StreamBuffer outStream, object customobject)
		{
			Vector3 vector = (Vector3)customobject;
			int num = 0;
			byte[] obj = CustomTypesUnity.memVector3;
			lock (obj)
			{
				byte[] array = CustomTypesUnity.memVector3;
				Protocol.Serialize(vector.x, array, ref num);
				Protocol.Serialize(vector.y, array, ref num);
				Protocol.Serialize(vector.z, array, ref num);
				outStream.Write(array, 0, 12);
			}
			return 12;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000277C File Offset: 0x0000097C
		private static object DeserializeVector3(StreamBuffer inStream, short length)
		{
			Vector3 vector = default(Vector3);
			if (length != 12)
			{
				return vector;
			}
			byte[] obj = CustomTypesUnity.memVector3;
			lock (obj)
			{
				inStream.Read(CustomTypesUnity.memVector3, 0, 12);
				int num = 0;
				Protocol.Deserialize(out vector.x, CustomTypesUnity.memVector3, ref num);
				Protocol.Deserialize(out vector.y, CustomTypesUnity.memVector3, ref num);
				Protocol.Deserialize(out vector.z, CustomTypesUnity.memVector3, ref num);
			}
			return vector;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000281C File Offset: 0x00000A1C
		private static short SerializeVector2(StreamBuffer outStream, object customobject)
		{
			Vector2 vector = (Vector2)customobject;
			byte[] obj = CustomTypesUnity.memVector2;
			lock (obj)
			{
				byte[] array = CustomTypesUnity.memVector2;
				int num = 0;
				Protocol.Serialize(vector.x, array, ref num);
				Protocol.Serialize(vector.y, array, ref num);
				outStream.Write(array, 0, 8);
			}
			return 8;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000288C File Offset: 0x00000A8C
		private static object DeserializeVector2(StreamBuffer inStream, short length)
		{
			Vector2 vector = default(Vector2);
			if (length != 8)
			{
				return vector;
			}
			byte[] obj = CustomTypesUnity.memVector2;
			lock (obj)
			{
				inStream.Read(CustomTypesUnity.memVector2, 0, 8);
				int num = 0;
				Protocol.Deserialize(out vector.x, CustomTypesUnity.memVector2, ref num);
				Protocol.Deserialize(out vector.y, CustomTypesUnity.memVector2, ref num);
			}
			return vector;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002914 File Offset: 0x00000B14
		private static short SerializeQuaternion(StreamBuffer outStream, object customobject)
		{
			Quaternion quaternion = (Quaternion)customobject;
			byte[] obj = CustomTypesUnity.memQuarternion;
			lock (obj)
			{
				byte[] array = CustomTypesUnity.memQuarternion;
				int num = 0;
				Protocol.Serialize(quaternion.w, array, ref num);
				Protocol.Serialize(quaternion.x, array, ref num);
				Protocol.Serialize(quaternion.y, array, ref num);
				Protocol.Serialize(quaternion.z, array, ref num);
				outStream.Write(array, 0, 16);
			}
			return 16;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000029A4 File Offset: 0x00000BA4
		private static object DeserializeQuaternion(StreamBuffer inStream, short length)
		{
			Quaternion identity = Quaternion.identity;
			if (length != 16)
			{
				return identity;
			}
			byte[] obj = CustomTypesUnity.memQuarternion;
			lock (obj)
			{
				inStream.Read(CustomTypesUnity.memQuarternion, 0, 16);
				int num = 0;
				Protocol.Deserialize(out identity.w, CustomTypesUnity.memQuarternion, ref num);
				Protocol.Deserialize(out identity.x, CustomTypesUnity.memQuarternion, ref num);
				Protocol.Deserialize(out identity.y, CustomTypesUnity.memQuarternion, ref num);
				Protocol.Deserialize(out identity.z, CustomTypesUnity.memQuarternion, ref num);
			}
			return identity;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A54 File Offset: 0x00000C54
		// Note: this type is marked as 'beforefieldinit'.
		static CustomTypesUnity()
		{
		}

		// Token: 0x0400001F RID: 31
		private const int SizeV2 = 8;

		// Token: 0x04000020 RID: 32
		private const int SizeV3 = 12;

		// Token: 0x04000021 RID: 33
		private const int SizeQuat = 16;

		// Token: 0x04000022 RID: 34
		public static readonly byte[] memVector3 = new byte[12];

		// Token: 0x04000023 RID: 35
		public static readonly byte[] memVector2 = new byte[8];

		// Token: 0x04000024 RID: 36
		public static readonly byte[] memQuarternion = new byte[16];
	}
}

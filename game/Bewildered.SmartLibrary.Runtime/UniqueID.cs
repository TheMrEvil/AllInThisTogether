using System;
using UnityEngine;

namespace Bewildered.SmartLibrary
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public struct UniqueID : ISerializationCallbackReceiver
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000206B File Offset: 0x0000026B
		public UniqueID(string id)
		{
			this._guid = new Guid(id);
			this._serializedGuid = this._guid.ToByteArray();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000208A File Offset: 0x0000028A
		public UniqueID(byte[] b)
		{
			this._guid = new Guid(b);
			this._serializedGuid = b;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A0 File Offset: 0x000002A0
		public static UniqueID NewUniqueId()
		{
			Guid guid = Guid.NewGuid();
			return new UniqueID
			{
				_guid = guid,
				_serializedGuid = guid.ToByteArray()
			};
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020D2 File Offset: 0x000002D2
		public static bool operator ==(UniqueID lhs, UniqueID rhs)
		{
			return lhs._guid == rhs._guid;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E5 File Offset: 0x000002E5
		public static bool operator !=(UniqueID lhs, UniqueID rhs)
		{
			return lhs._guid != rhs._guid;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F8 File Offset: 0x000002F8
		public override bool Equals(object obj)
		{
			if (obj is UniqueID)
			{
				UniqueID uniqueID = (UniqueID)obj;
				return this._guid.Equals(uniqueID._guid);
			}
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002127 File Offset: 0x00000327
		public override int GetHashCode()
		{
			return this._guid.GetHashCode();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213A File Offset: 0x0000033A
		public override string ToString()
		{
			return this._guid.ToString();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214D File Offset: 0x0000034D
		public byte[] ToByteArray()
		{
			return this._serializedGuid;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002155 File Offset: 0x00000355
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (this._guid != Guid.Empty)
			{
				this._serializedGuid = this._guid.ToByteArray();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000217A File Offset: 0x0000037A
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this._serializedGuid != null && this._serializedGuid.Length == 16)
			{
				this._guid = new Guid(this._serializedGuid);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A4 File Offset: 0x000003A4
		// Note: this type is marked as 'beforefieldinit'.
		static UniqueID()
		{
		}

		// Token: 0x04000002 RID: 2
		private Guid _guid;

		// Token: 0x04000003 RID: 3
		[SerializeField]
		private byte[] _serializedGuid;

		// Token: 0x04000004 RID: 4
		public static readonly UniqueID Empty = new UniqueID
		{
			_guid = Guid.Empty,
			_serializedGuid = null
		};
	}
}

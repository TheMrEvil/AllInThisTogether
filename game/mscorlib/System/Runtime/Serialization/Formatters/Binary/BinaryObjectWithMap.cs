using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200069F RID: 1695
	internal sealed class BinaryObjectWithMap : IStreamable
	{
		// Token: 0x06003E52 RID: 15954 RVA: 0x0000259F File Offset: 0x0000079F
		internal BinaryObjectWithMap()
		{
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x000D7095 File Offset: 0x000D5295
		internal BinaryObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000D70A4 File Offset: 0x000D52A4
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, int assemId)
		{
			this.objectId = objectId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMap;
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000D70E0 File Offset: 0x000D52E0
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000D7154 File Offset: 0x000D5354
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump()
		{
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000D71CC File Offset: 0x000D53CC
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				for (int i = 0; i < this.numMembers; i++)
				{
				}
				BinaryHeaderEnum binaryHeaderEnum = this.binaryHeaderEnum;
			}
		}

		// Token: 0x04002863 RID: 10339
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04002864 RID: 10340
		internal int objectId;

		// Token: 0x04002865 RID: 10341
		internal string name;

		// Token: 0x04002866 RID: 10342
		internal int numMembers;

		// Token: 0x04002867 RID: 10343
		internal string[] memberNames;

		// Token: 0x04002868 RID: 10344
		internal int assemId;
	}
}

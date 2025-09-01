using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000C0 RID: 192
	internal class DataContractPairKey
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x000301C0 File Offset: 0x0002E3C0
		public DataContractPairKey(object object1, object object2)
		{
			this.object1 = object1;
			this.object2 = object2;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000301D8 File Offset: 0x0002E3D8
		public override bool Equals(object other)
		{
			DataContractPairKey dataContractPairKey = other as DataContractPairKey;
			return dataContractPairKey != null && ((dataContractPairKey.object1 == this.object1 && dataContractPairKey.object2 == this.object2) || (dataContractPairKey.object1 == this.object2 && dataContractPairKey.object2 == this.object1));
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003022D File Offset: 0x0002E42D
		public override int GetHashCode()
		{
			return this.object1.GetHashCode() ^ this.object2.GetHashCode();
		}

		// Token: 0x0400048B RID: 1163
		private object object1;

		// Token: 0x0400048C RID: 1164
		private object object2;
	}
}

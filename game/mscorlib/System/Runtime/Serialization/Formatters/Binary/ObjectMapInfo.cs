using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006AB RID: 1707
	internal sealed class ObjectMapInfo
	{
		// Token: 0x06003EE7 RID: 16103 RVA: 0x000D9661 File Offset: 0x000D7861
		internal ObjectMapInfo(int objectId, int numMembers, string[] memberNames, Type[] memberTypes)
		{
			this.objectId = objectId;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.memberTypes = memberTypes;
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x000D9688 File Offset: 0x000D7888
		internal bool isCompatible(int numMembers, string[] memberNames, Type[] memberTypes)
		{
			bool result = true;
			if (this.numMembers == numMembers)
			{
				for (int i = 0; i < numMembers; i++)
				{
					if (!this.memberNames[i].Equals(memberNames[i]))
					{
						result = false;
						break;
					}
					if (memberTypes != null && this.memberTypes[i] != memberTypes[i])
					{
						result = false;
						break;
					}
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040028EB RID: 10475
		internal int objectId;

		// Token: 0x040028EC RID: 10476
		private int numMembers;

		// Token: 0x040028ED RID: 10477
		private string[] memberNames;

		// Token: 0x040028EE RID: 10478
		private Type[] memberTypes;
	}
}

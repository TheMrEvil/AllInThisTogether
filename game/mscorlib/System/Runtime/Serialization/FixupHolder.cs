using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000663 RID: 1635
	[Serializable]
	internal class FixupHolder
	{
		// Token: 0x06003D39 RID: 15673 RVA: 0x000D42A6 File Offset: 0x000D24A6
		internal FixupHolder(long id, object fixupInfo, int fixupType)
		{
			this.m_id = id;
			this.m_fixupInfo = fixupInfo;
			this.m_fixupType = fixupType;
		}

		// Token: 0x04002763 RID: 10083
		internal const int ArrayFixup = 1;

		// Token: 0x04002764 RID: 10084
		internal const int MemberFixup = 2;

		// Token: 0x04002765 RID: 10085
		internal const int DelayedFixup = 4;

		// Token: 0x04002766 RID: 10086
		internal long m_id;

		// Token: 0x04002767 RID: 10087
		internal object m_fixupInfo;

		// Token: 0x04002768 RID: 10088
		internal int m_fixupType;
	}
}

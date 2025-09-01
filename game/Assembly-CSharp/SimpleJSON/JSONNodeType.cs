using System;

namespace SimpleJSON
{
	// Token: 0x02000392 RID: 914
	public enum JSONNodeType
	{
		// Token: 0x04001EB4 RID: 7860
		Array = 1,
		// Token: 0x04001EB5 RID: 7861
		Object,
		// Token: 0x04001EB6 RID: 7862
		String,
		// Token: 0x04001EB7 RID: 7863
		Number,
		// Token: 0x04001EB8 RID: 7864
		NullValue,
		// Token: 0x04001EB9 RID: 7865
		Boolean,
		// Token: 0x04001EBA RID: 7866
		None,
		// Token: 0x04001EBB RID: 7867
		Custom = 255
	}
}

using System;

namespace Unity.Burst
{
	// Token: 0x02000013 RID: 19
	public interface IFunctionPointer
	{
		// Token: 0x060000A4 RID: 164
		[Obsolete("This method will be removed in a future version of Burst")]
		IFunctionPointer FromIntPtr(IntPtr ptr);
	}
}

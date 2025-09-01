using System;

namespace Mono.CSharp
{
	// Token: 0x020001A0 RID: 416
	public interface IMemoryLocation
	{
		// Token: 0x06001623 RID: 5667
		void AddressOf(EmitContext ec, AddressOp mode);
	}
}

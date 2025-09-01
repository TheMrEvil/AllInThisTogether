using System;

namespace Parse.Abstractions.Infrastructure.Data
{
	// Token: 0x0200009E RID: 158
	public interface IParseDataDecoder
	{
		// Token: 0x060005B8 RID: 1464
		object Decode(object data, IServiceHub serviceHub);
	}
}

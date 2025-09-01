using System;
using System.Collections.Generic;

namespace IKVM.Reflection
{
	// Token: 0x02000071 RID: 113
	public interface ICustomAttributeProvider
	{
		// Token: 0x0600067B RID: 1659
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x0600067C RID: 1660
		IList<CustomAttributeData> __GetCustomAttributes(Type attributeType, bool inherit);
	}
}

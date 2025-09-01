using System;
using System.Collections.Generic;

namespace Sirenix.OdinInspector.Internal
{
	// Token: 0x0200009C RID: 156
	public interface ISubGroupProviderAttribute
	{
		// Token: 0x060001D4 RID: 468
		IList<PropertyGroupAttribute> GetSubGroupAttributes();

		// Token: 0x060001D5 RID: 469
		string RepathMemberAttribute(PropertyGroupAttribute attr);
	}
}

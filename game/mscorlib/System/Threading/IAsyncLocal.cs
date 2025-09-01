using System;

namespace System.Threading
{
	// Token: 0x02000282 RID: 642
	internal interface IAsyncLocal
	{
		// Token: 0x06001D5F RID: 7519
		void OnValueChanged(object previousValue, object currentValue, bool contextChanged);
	}
}

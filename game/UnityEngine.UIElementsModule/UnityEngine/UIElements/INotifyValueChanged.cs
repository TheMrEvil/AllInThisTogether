using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014B RID: 331
	public interface INotifyValueChanged<T>
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000AC2 RID: 2754
		// (set) Token: 0x06000AC3 RID: 2755
		T value { get; set; }

		// Token: 0x06000AC4 RID: 2756
		void SetValueWithoutNotify(T newValue);
	}
}

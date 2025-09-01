using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029B RID: 667
	internal interface IStyleDataGroup<T>
	{
		// Token: 0x060016F7 RID: 5879
		T Copy();

		// Token: 0x060016F8 RID: 5880
		void CopyFrom(ref T other);
	}
}

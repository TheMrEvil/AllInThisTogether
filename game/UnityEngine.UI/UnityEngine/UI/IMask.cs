using System;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x02000016 RID: 22
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Not supported anymore.", true)]
	public interface IMask
	{
		// Token: 0x06000157 RID: 343
		bool Enabled();

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000158 RID: 344
		RectTransform rectTransform { get; }
	}
}

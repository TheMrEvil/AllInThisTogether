using System;

namespace UnityEngine
{
	// Token: 0x0200025A RID: 602
	public struct DrivenRectTransformTracker
	{
		// Token: 0x06001A05 RID: 6661 RVA: 0x0002A200 File Offset: 0x00028400
		internal static bool CanRecordModifications()
		{
			return true;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00004563 File Offset: 0x00002763
		public void Add(Object driver, RectTransform rectTransform, DrivenTransformProperties drivenProperties)
		{
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0002A213 File Offset: 0x00028413
		[Obsolete("revertValues parameter is ignored. Please use Clear() instead.")]
		public void Clear(bool revertValues)
		{
			this.Clear();
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00004563 File Offset: 0x00002763
		public void Clear()
		{
		}
	}
}

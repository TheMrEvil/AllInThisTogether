using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000435 RID: 1077
	[RequiredByNativeCode]
	public interface INotificationReceiver
	{
		// Token: 0x06002582 RID: 9602
		[RequiredByNativeCode]
		void OnNotify(Playable origin, INotification notification, object context);
	}
}

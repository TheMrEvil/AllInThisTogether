using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000237 RID: 567
	[RequiredByNativeCode]
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x0600180A RID: 6154
		[RequiredByNativeCode]
		void OnBeforeSerialize();

		// Token: 0x0600180B RID: 6155
		[RequiredByNativeCode]
		void OnAfterDeserialize();
	}
}

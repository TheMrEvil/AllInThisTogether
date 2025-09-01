using System;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	// Token: 0x020000A4 RID: 164
	internal static class AtomicCompositionExtensions
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x0000C8ED File Offset: 0x0000AAED
		internal static T GetValueAllowNull<T>(this AtomicComposition atomicComposition, T defaultResultAndKey) where T : class
		{
			Assumes.NotNull<T>(defaultResultAndKey);
			return atomicComposition.GetValueAllowNull(defaultResultAndKey, defaultResultAndKey);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000C904 File Offset: 0x0000AB04
		internal static T GetValueAllowNull<T>(this AtomicComposition atomicComposition, object key, T defaultResult)
		{
			T result;
			if (atomicComposition != null && atomicComposition.TryGetValue<T>(key, out result))
			{
				return result;
			}
			return defaultResult;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000C922 File Offset: 0x0000AB22
		internal static void AddRevertActionAllowNull(this AtomicComposition atomicComposition, Action action)
		{
			Assumes.NotNull<Action>(action);
			if (atomicComposition == null)
			{
				action();
				return;
			}
			atomicComposition.AddRevertAction(action);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000C93B File Offset: 0x0000AB3B
		internal static void AddCompleteActionAllowNull(this AtomicComposition atomicComposition, Action action)
		{
			Assumes.NotNull<Action>(action);
			if (atomicComposition == null)
			{
				action();
				return;
			}
			atomicComposition.AddCompleteAction(action);
		}
	}
}

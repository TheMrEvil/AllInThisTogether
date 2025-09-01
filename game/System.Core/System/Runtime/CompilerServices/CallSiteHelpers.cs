using System;
using System.Dynamic;
using System.Reflection;

namespace System.Runtime.CompilerServices
{
	/// <summary>Class that contains helper methods for DLR CallSites.</summary>
	// Token: 0x020002E0 RID: 736
	public static class CallSiteHelpers
	{
		/// <summary>Checks if a <see cref="T:System.Reflection.MethodBase" /> is internally used by DLR and should not be displayed on the language code's stack.</summary>
		/// <param name="mb">The input <see cref="T:System.Reflection.MethodBase" /></param>
		/// <returns>True if the input <see cref="T:System.Reflection.MethodBase" /> is internally used by DLR and should not be displayed on the language code's stack. Otherwise, false.</returns>
		// Token: 0x06001660 RID: 5728 RVA: 0x0004BC70 File Offset: 0x00049E70
		public static bool IsInternalFrame(MethodBase mb)
		{
			return (mb.Name == "CallSite.Target" && mb.GetType() != CallSiteHelpers.s_knownNonDynamicMethodType) || mb.DeclaringType == typeof(UpdateDelegates);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0004BCBD File Offset: 0x00049EBD
		// Note: this type is marked as 'beforefieldinit'.
		static CallSiteHelpers()
		{
		}

		// Token: 0x04000B53 RID: 2899
		private static readonly Type s_knownNonDynamicMethodType = typeof(object).GetMethod("ToString").GetType();
	}
}

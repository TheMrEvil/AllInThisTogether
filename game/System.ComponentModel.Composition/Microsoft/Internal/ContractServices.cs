using System;
using System.ComponentModel.Composition.Primitives;

namespace Microsoft.Internal
{
	// Token: 0x02000006 RID: 6
	internal static class ContractServices
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002158 File Offset: 0x00000358
		public static bool TryCast(Type contractType, object value, out object result)
		{
			if (value == null)
			{
				result = null;
				return true;
			}
			if (contractType.IsInstanceOfType(value))
			{
				result = value;
				return true;
			}
			if (typeof(Delegate).IsAssignableFrom(contractType))
			{
				ExportedDelegate exportedDelegate = value as ExportedDelegate;
				if (exportedDelegate != null)
				{
					result = exportedDelegate.CreateDelegate(contractType.UnderlyingSystemType);
					return result != null;
				}
			}
			result = null;
			return false;
		}
	}
}

using System;
using System.Security;

namespace System.Runtime.Versioning
{
	// Token: 0x0200063C RID: 1596
	internal static class MultitargetingHelpers
	{
		// Token: 0x06003C26 RID: 15398 RVA: 0x000D10C4 File Offset: 0x000CF2C4
		internal static string GetAssemblyQualifiedName(Type type, Func<Type, string> converter)
		{
			string text = null;
			if (type != null)
			{
				if (converter != null)
				{
					try
					{
						text = converter(type);
					}
					catch (Exception ex)
					{
						if (MultitargetingHelpers.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (text == null)
				{
					text = type.AssemblyQualifiedName;
				}
			}
			return text;
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x000D1110 File Offset: 0x000CF310
		private static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x000D113D File Offset: 0x000CF33D
		private static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || MultitargetingHelpers.IsCriticalException(ex);
		}
	}
}

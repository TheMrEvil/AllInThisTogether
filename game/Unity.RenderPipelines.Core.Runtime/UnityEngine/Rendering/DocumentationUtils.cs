using System;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x02000076 RID: 118
	public static class DocumentationUtils
	{
		// Token: 0x060003BA RID: 954 RVA: 0x00011624 File Offset: 0x0000F824
		public static string GetHelpURL<TEnum>(TEnum mask = default(TEnum)) where TEnum : struct, IConvertible
		{
			HelpURLAttribute helpURLAttribute = (HelpURLAttribute)mask.GetType().GetCustomAttributes(typeof(HelpURLAttribute), false).FirstOrDefault<object>();
			if (helpURLAttribute != null)
			{
				return string.Format("{0}#{1}", helpURLAttribute.URL, mask);
			}
			return string.Empty;
		}
	}
}

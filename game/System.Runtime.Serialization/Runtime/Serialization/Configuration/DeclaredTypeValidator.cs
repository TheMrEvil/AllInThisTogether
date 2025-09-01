using System;
using System.Configuration;

namespace System.Runtime.Serialization.Configuration
{
	// Token: 0x020001A3 RID: 419
	internal class DeclaredTypeValidator : ConfigurationValidatorBase
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x000540CB File Offset: 0x000522CB
		public override bool CanValidate(Type type)
		{
			return typeof(string) == type;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000540E0 File Offset: 0x000522E0
		public override void Validate(object value)
		{
			string text = (string)value;
			if (text.StartsWith(Globals.TypeOfObject.FullName, StringComparison.Ordinal))
			{
				Type type = Type.GetType(text, false);
				if (type != null && Globals.TypeOfObject.Equals(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("Known type configuration specifies System.Object."));
				}
			}
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00054135 File Offset: 0x00052335
		public DeclaredTypeValidator()
		{
		}
	}
}

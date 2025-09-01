using System;

namespace Febucci.UI.Core
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00006C7F File Offset: 0x00004E7F
		public DefaultValueAttribute(string variableName, float variableValue)
		{
			this.variableName = variableName;
			this.variableValue = variableValue;
		}

		// Token: 0x040000F3 RID: 243
		public readonly string variableName;

		// Token: 0x040000F4 RID: 244
		public readonly float variableValue;
	}
}

using System;

namespace Mono.CSharp
{
	// Token: 0x0200026B RID: 619
	public interface IParameterData
	{
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001E5B RID: 7771
		Expression DefaultValue { get; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001E5C RID: 7772
		bool HasExtensionMethodModifier { get; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001E5D RID: 7773
		bool HasDefaultValue { get; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001E5E RID: 7774
		Parameter.Modifier ModFlags { get; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001E5F RID: 7775
		string Name { get; }
	}
}

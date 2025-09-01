using System;

namespace Mono.CSharp
{
	// Token: 0x0200026F RID: 623
	public class ParametersImported : AParametersCollection
	{
		// Token: 0x06001EA4 RID: 7844 RVA: 0x00096C1F File Offset: 0x00094E1F
		public ParametersImported(IParameterData[] parameters, TypeSpec[] types, bool hasArglist, bool hasParams)
		{
			this.parameters = parameters;
			this.types = types;
			this.has_arglist = hasArglist;
			this.has_params = hasParams;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00096C44 File Offset: 0x00094E44
		public ParametersImported(IParameterData[] param, TypeSpec[] types, bool hasParams)
		{
			this.parameters = param;
			this.types = types;
			this.has_params = hasParams;
		}
	}
}

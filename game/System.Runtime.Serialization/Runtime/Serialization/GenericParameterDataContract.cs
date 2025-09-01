using System;
using System.Collections.Generic;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x020000E1 RID: 225
	internal sealed class GenericParameterDataContract : DataContract
	{
		// Token: 0x06000CD0 RID: 3280 RVA: 0x00034633 File Offset: 0x00032833
		[SecuritySafeCritical]
		internal GenericParameterDataContract(Type type) : base(new GenericParameterDataContract.GenericParameterDataContractCriticalHelper(type))
		{
			this.helper = (base.Helper as GenericParameterDataContract.GenericParameterDataContractCriticalHelper);
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00034652 File Offset: 0x00032852
		internal int ParameterPosition
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ParameterPosition;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x000066E8 File Offset: 0x000048E8
		internal override bool IsBuiltInDataContract
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003465F File Offset: 0x0003285F
		internal override DataContract BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
		{
			return paramContracts[this.ParameterPosition];
		}

		// Token: 0x04000559 RID: 1369
		[SecurityCritical]
		private GenericParameterDataContract.GenericParameterDataContractCriticalHelper helper;

		// Token: 0x020000E2 RID: 226
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class GenericParameterDataContractCriticalHelper : DataContract.DataContractCriticalHelper
		{
			// Token: 0x06000CD4 RID: 3284 RVA: 0x00034669 File Offset: 0x00032869
			internal GenericParameterDataContractCriticalHelper(Type type) : base(type)
			{
				base.SetDataContractName(DataContract.GetStableName(type));
				this.parameterPosition = type.GenericParameterPosition;
			}

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0003468A File Offset: 0x0003288A
			internal int ParameterPosition
			{
				get
				{
					return this.parameterPosition;
				}
			}

			// Token: 0x0400055A RID: 1370
			private int parameterPosition;
		}
	}
}

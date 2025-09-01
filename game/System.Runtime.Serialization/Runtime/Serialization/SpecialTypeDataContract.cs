using System;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x0200012B RID: 299
	internal sealed class SpecialTypeDataContract : DataContract
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x0003CB4E File Offset: 0x0003AD4E
		[SecuritySafeCritical]
		public SpecialTypeDataContract(Type type) : base(new SpecialTypeDataContract.SpecialTypeDataContractCriticalHelper(type))
		{
			this.helper = (base.Helper as SpecialTypeDataContract.SpecialTypeDataContractCriticalHelper);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003CB6D File Offset: 0x0003AD6D
		[SecuritySafeCritical]
		public SpecialTypeDataContract(Type type, XmlDictionaryString name, XmlDictionaryString ns) : base(new SpecialTypeDataContract.SpecialTypeDataContractCriticalHelper(type, name, ns))
		{
			this.helper = (base.Helper as SpecialTypeDataContract.SpecialTypeDataContractCriticalHelper);
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x000066E8 File Offset: 0x000048E8
		internal override bool IsBuiltInDataContract
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400067F RID: 1663
		[SecurityCritical]
		private SpecialTypeDataContract.SpecialTypeDataContractCriticalHelper helper;

		// Token: 0x0200012C RID: 300
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SpecialTypeDataContractCriticalHelper : DataContract.DataContractCriticalHelper
		{
			// Token: 0x06000EF2 RID: 3826 RVA: 0x0003CB8E File Offset: 0x0003AD8E
			internal SpecialTypeDataContractCriticalHelper(Type type) : base(type)
			{
			}

			// Token: 0x06000EF3 RID: 3827 RVA: 0x00036E54 File Offset: 0x00035054
			internal SpecialTypeDataContractCriticalHelper(Type type, XmlDictionaryString name, XmlDictionaryString ns) : base(type)
			{
				base.SetDataContractName(name, ns);
			}
		}
	}
}

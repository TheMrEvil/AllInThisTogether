using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000128 RID: 296
	internal struct ScopedKnownTypes
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003CA7C File Offset: 0x0003AC7C
		internal void Push(Dictionary<XmlQualifiedName, DataContract> dataContractDictionary)
		{
			if (this.dataContractDictionaries == null)
			{
				this.dataContractDictionaries = new Dictionary<XmlQualifiedName, DataContract>[4];
			}
			else if (this.count == this.dataContractDictionaries.Length)
			{
				Array.Resize<Dictionary<XmlQualifiedName, DataContract>>(ref this.dataContractDictionaries, this.dataContractDictionaries.Length * 2);
			}
			Dictionary<XmlQualifiedName, DataContract>[] array = this.dataContractDictionaries;
			int num = this.count;
			this.count = num + 1;
			array[num] = dataContractDictionary;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003CADD File Offset: 0x0003ACDD
		internal void Pop()
		{
			this.count--;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
		internal DataContract GetDataContract(XmlQualifiedName qname)
		{
			for (int i = this.count - 1; i >= 0; i--)
			{
				DataContract result;
				if (this.dataContractDictionaries[i].TryGetValue(qname, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x04000679 RID: 1657
		internal Dictionary<XmlQualifiedName, DataContract>[] dataContractDictionaries;

		// Token: 0x0400067A RID: 1658
		private int count;
	}
}

using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000568 RID: 1384
	internal class ProviderData
	{
		// Token: 0x06003663 RID: 13923 RVA: 0x000C46C8 File Offset: 0x000C28C8
		public void CopyFrom(ProviderData other)
		{
			if (this.Ref == null)
			{
				this.Ref = other.Ref;
			}
			if (this.Id == null)
			{
				this.Id = other.Id;
			}
			if (this.Type == null)
			{
				this.Type = other.Type;
			}
			foreach (object obj in other.CustomProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
				{
					this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
			if (other.CustomData != null)
			{
				if (this.CustomData == null)
				{
					this.CustomData = new ArrayList();
				}
				foreach (object obj2 in other.CustomData)
				{
					SinkProviderData value = (SinkProviderData)obj2;
					this.CustomData.Add(value);
				}
			}
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000C47F0 File Offset: 0x000C29F0
		public ProviderData()
		{
		}

		// Token: 0x04002549 RID: 9545
		internal string Ref;

		// Token: 0x0400254A RID: 9546
		internal string Type;

		// Token: 0x0400254B RID: 9547
		internal string Id;

		// Token: 0x0400254C RID: 9548
		internal Hashtable CustomProperties = new Hashtable();

		// Token: 0x0400254D RID: 9549
		internal IList CustomData;
	}
}

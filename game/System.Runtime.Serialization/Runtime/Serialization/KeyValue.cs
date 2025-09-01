using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000B2 RID: 178
	[DataContract(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
	internal struct KeyValue<K, V>
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x0002B23C File Offset: 0x0002943C
		internal KeyValue(K key, V value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002B24C File Offset: 0x0002944C
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x0002B254 File Offset: 0x00029454
		[DataMember(IsRequired = true)]
		public K Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002B25D File Offset: 0x0002945D
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0002B265 File Offset: 0x00029465
		[DataMember(IsRequired = true)]
		public V Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04000425 RID: 1061
		private K key;

		// Token: 0x04000426 RID: 1062
		private V value;
	}
}

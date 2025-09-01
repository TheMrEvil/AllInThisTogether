using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200049D RID: 1181
	internal struct StorageDescriptor
	{
		// Token: 0x06002E36 RID: 11830 RVA: 0x0010F2CC File Offset: 0x0010D4CC
		public static StorageDescriptor None()
		{
			return default(StorageDescriptor);
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x0010F2E4 File Offset: 0x0010D4E4
		public static StorageDescriptor Stack(Type itemStorageType, bool isCached)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Stack,
				itemStorageType = itemStorageType,
				isCached = isCached
			};
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x0010F314 File Offset: 0x0010D514
		public static StorageDescriptor Parameter(int paramIndex, Type itemStorageType, bool isCached)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Parameter,
				locationObject = paramIndex,
				itemStorageType = itemStorageType,
				isCached = isCached
			};
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x0010F350 File Offset: 0x0010D550
		public static StorageDescriptor Local(LocalBuilder loc, Type itemStorageType, bool isCached)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Local,
				locationObject = loc,
				itemStorageType = itemStorageType,
				isCached = isCached
			};
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x0010F388 File Offset: 0x0010D588
		public static StorageDescriptor Current(LocalBuilder locIter, Type itemStorageType)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Current,
				locationObject = locIter,
				itemStorageType = itemStorageType
			};
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x0010F3B8 File Offset: 0x0010D5B8
		public static StorageDescriptor Global(MethodInfo methGlobal, Type itemStorageType, bool isCached)
		{
			return new StorageDescriptor
			{
				location = ItemLocation.Global,
				locationObject = methGlobal,
				itemStorageType = itemStorageType,
				isCached = isCached
			};
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x0010F3EE File Offset: 0x0010D5EE
		public StorageDescriptor ToStack()
		{
			return StorageDescriptor.Stack(this.itemStorageType, this.isCached);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x0010F401 File Offset: 0x0010D601
		public StorageDescriptor ToLocal(LocalBuilder loc)
		{
			return StorageDescriptor.Local(loc, this.itemStorageType, this.isCached);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x0010F418 File Offset: 0x0010D618
		public StorageDescriptor ToStorageType(Type itemStorageType)
		{
			StorageDescriptor result = this;
			result.itemStorageType = itemStorageType;
			return result;
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x0010F435 File Offset: 0x0010D635
		public ItemLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x0010F43D File Offset: 0x0010D63D
		public int ParameterLocation
		{
			get
			{
				return (int)this.locationObject;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x0010F44A File Offset: 0x0010D64A
		public LocalBuilder LocalLocation
		{
			get
			{
				return this.locationObject as LocalBuilder;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002E42 RID: 11842 RVA: 0x0010F44A File Offset: 0x0010D64A
		public LocalBuilder CurrentLocation
		{
			get
			{
				return this.locationObject as LocalBuilder;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x0010F457 File Offset: 0x0010D657
		public MethodInfo GlobalLocation
		{
			get
			{
				return this.locationObject as MethodInfo;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x0010F464 File Offset: 0x0010D664
		public bool IsCached
		{
			get
			{
				return this.isCached;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x0010F46C File Offset: 0x0010D66C
		public Type ItemStorageType
		{
			get
			{
				return this.itemStorageType;
			}
		}

		// Token: 0x0400249B RID: 9371
		private ItemLocation location;

		// Token: 0x0400249C RID: 9372
		private object locationObject;

		// Token: 0x0400249D RID: 9373
		private Type itemStorageType;

		// Token: 0x0400249E RID: 9374
		private bool isCached;
	}
}

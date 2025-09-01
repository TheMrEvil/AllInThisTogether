using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	// Token: 0x0200004C RID: 76
	internal static class MetadataServices
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00005F27 File Offset: 0x00004127
		public static IDictionary<string, object> AsReadOnly(this IDictionary<string, object> metadata)
		{
			if (metadata == null)
			{
				return MetadataServices.EmptyMetadata;
			}
			if (metadata is ReadOnlyDictionary<string, object>)
			{
				return metadata;
			}
			return new ReadOnlyDictionary<string, object>(metadata);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00005F44 File Offset: 0x00004144
		public static T GetValue<T>(this IDictionary<string, object> metadata, string key)
		{
			Assumes.NotNull<IDictionary<string, object>, string>(metadata, "metadata");
			object obj = true;
			if (!metadata.TryGetValue(key, out obj))
			{
				return default(T);
			}
			if (obj is T)
			{
				return (T)((object)obj);
			}
			return default(T);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005F90 File Offset: 0x00004190
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataServices()
		{
		}

		// Token: 0x040000D5 RID: 213
		public static readonly IDictionary<string, object> EmptyMetadata = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>(0));
	}
}

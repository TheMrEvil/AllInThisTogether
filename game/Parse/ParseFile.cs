using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Infrastructure.Utilities;
using Parse.Platform.Files;

namespace Parse
{
	// Token: 0x0200000A RID: 10
	public class ParseFile : IJsonConvertible
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002363 File Offset: 0x00000563
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000236B File Offset: 0x0000056B
		internal FileState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<State>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002374 File Offset: 0x00000574
		internal Stream DataStream
		{
			[CompilerGenerated]
			get
			{
				return this.<DataStream>k__BackingField;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000237C File Offset: 0x0000057C
		internal TaskQueue TaskQueue
		{
			[CompilerGenerated]
			get
			{
				return this.<TaskQueue>k__BackingField;
			}
		} = new TaskQueue();

		// Token: 0x06000021 RID: 33 RVA: 0x00002384 File Offset: 0x00000584
		internal ParseFile(string name, Uri uri, string mimeType = null)
		{
			this.State = new FileState
			{
				Name = name,
				Location = uri,
				MediaType = mimeType
			};
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023B7 File Offset: 0x000005B7
		public ParseFile(string name, byte[] data, string mimeType = null) : this(name, new MemoryStream(data), mimeType)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023C7 File Offset: 0x000005C7
		public ParseFile(string name, Stream data, string mimeType = null)
		{
			this.State = new FileState
			{
				Name = name,
				MediaType = mimeType
			};
			this.DataStream = data;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023FA File Offset: 0x000005FA
		public bool IsDirty
		{
			get
			{
				return this.State.Location == null;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000240D File Offset: 0x0000060D
		[ParseFieldName("name")]
		public string Name
		{
			get
			{
				return this.State.Name;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000241A File Offset: 0x0000061A
		public string MimeType
		{
			get
			{
				return this.State.MediaType;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002427 File Offset: 0x00000627
		[ParseFieldName("url")]
		public Uri Url
		{
			get
			{
				return this.State.SecureLocation;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002434 File Offset: 0x00000634
		IDictionary<string, object> IJsonConvertible.ConvertToJSON()
		{
			if (this.IsDirty)
			{
				throw new InvalidOperationException("ParseFile must be saved before it can be serialized.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["__type"] = "File";
			dictionary["name"] = this.Name;
			dictionary["url"] = this.Url.AbsoluteUri;
			return dictionary;
		}

		// Token: 0x04000007 RID: 7
		[CompilerGenerated]
		private FileState <State>k__BackingField;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		private readonly Stream <DataStream>k__BackingField;

		// Token: 0x04000009 RID: 9
		[CompilerGenerated]
		private readonly TaskQueue <TaskQueue>k__BackingField;
	}
}

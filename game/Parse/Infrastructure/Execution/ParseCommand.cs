using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure.Execution
{
	// Token: 0x02000060 RID: 96
	public class ParseCommand : WebRequest
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000F3A6 File Offset: 0x0000D5A6
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000F3AE File Offset: 0x0000D5AE
		public IDictionary<string, object> DataObject
		{
			[CompilerGenerated]
			get
			{
				return this.<DataObject>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DataObject>k__BackingField = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000F3FD File Offset: 0x0000D5FD
		public override Stream Data
		{
			get
			{
				Stream result;
				if ((result = base.Data) == null)
				{
					result = (base.Data = ((this.DataObject != null) ? new MemoryStream(Encoding.UTF8.GetBytes(JsonUtilities.Encode(this.DataObject))) : null));
				}
				return result;
			}
			set
			{
				base.Data = value;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000F406 File Offset: 0x0000D606
		public ParseCommand(string relativeUri, string method, string sessionToken = null, IList<KeyValuePair<string, string>> headers = null, IDictionary<string, object> data = null) : this(relativeUri, method, sessionToken, headers, null, (data != null) ? "application/json" : null)
		{
			this.DataObject = data;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000F428 File Offset: 0x0000D628
		public ParseCommand(string relativeUri, string method, string sessionToken = null, IList<KeyValuePair<string, string>> headers = null, Stream stream = null, string contentType = null)
		{
			base.Path = relativeUri;
			base.Method = method;
			this.Data = stream;
			base.Headers = new List<KeyValuePair<string, string>>(headers ?? Enumerable.Empty<KeyValuePair<string, string>>());
			if (!string.IsNullOrEmpty(sessionToken))
			{
				base.Headers.Add(new KeyValuePair<string, string>("X-Parse-Session-Token", sessionToken));
			}
			if (!string.IsNullOrEmpty(contentType))
			{
				base.Headers.Add(new KeyValuePair<string, string>("Content-Type", contentType));
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		public ParseCommand(ParseCommand other)
		{
			base.Resource = other.Resource;
			base.Path = other.Path;
			base.Method = other.Method;
			this.DataObject = other.DataObject;
			base.Headers = new List<KeyValuePair<string, string>>(other.Headers);
			this.Data = other.Data;
		}

		// Token: 0x040000E0 RID: 224
		[CompilerGenerated]
		private IDictionary<string, object> <DataObject>k__BackingField;
	}
}

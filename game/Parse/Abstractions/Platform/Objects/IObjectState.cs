using System;
using System.Collections;
using System.Collections.Generic;
using Parse.Platform.Objects;

namespace Parse.Abstractions.Platform.Objects
{
	// Token: 0x0200007D RID: 125
	public interface IObjectState : IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000514 RID: 1300
		bool IsNew { get; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000515 RID: 1301
		string ClassName { get; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000516 RID: 1302
		string ObjectId { get; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000517 RID: 1303
		DateTime? UpdatedAt { get; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000518 RID: 1304
		DateTime? CreatedAt { get; }

		// Token: 0x1700018F RID: 399
		object this[string key]
		{
			get;
		}

		// Token: 0x0600051A RID: 1306
		bool ContainsKey(string key);

		// Token: 0x0600051B RID: 1307
		IObjectState MutatedClone(Action<MutableObjectState> func);
	}
}

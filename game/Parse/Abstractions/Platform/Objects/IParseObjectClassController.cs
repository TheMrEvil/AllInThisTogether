using System;
using System.Collections.Generic;
using Parse.Abstractions.Infrastructure;

namespace Parse.Abstractions.Platform.Objects
{
	// Token: 0x0200007E RID: 126
	public interface IParseObjectClassController
	{
		// Token: 0x0600051C RID: 1308
		string GetClassName(Type type);

		// Token: 0x0600051D RID: 1309
		Type GetType(string className);

		// Token: 0x0600051E RID: 1310
		bool GetClassMatch(string className, Type type);

		// Token: 0x0600051F RID: 1311
		void AddValid(Type type);

		// Token: 0x06000520 RID: 1312
		void RemoveClass(Type type);

		// Token: 0x06000521 RID: 1313
		void AddRegisterHook(Type type, Action action);

		// Token: 0x06000522 RID: 1314
		ParseObject Instantiate(string className, IServiceHub serviceHub);

		// Token: 0x06000523 RID: 1315
		IDictionary<string, string> GetPropertyMappings(string className);

		// Token: 0x06000524 RID: 1316
		void AddIntrinsic();
	}
}

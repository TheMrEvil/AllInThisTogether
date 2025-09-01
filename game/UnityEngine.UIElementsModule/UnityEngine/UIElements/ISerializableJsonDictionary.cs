using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000044 RID: 68
	internal interface ISerializableJsonDictionary
	{
		// Token: 0x060001AB RID: 427
		void Set<T>(string key, T value) where T : class;

		// Token: 0x060001AC RID: 428
		T Get<T>(string key) where T : class;

		// Token: 0x060001AD RID: 429
		T GetScriptable<T>(string key) where T : ScriptableObject;

		// Token: 0x060001AE RID: 430
		void Overwrite(object obj, string key);

		// Token: 0x060001AF RID: 431
		bool ContainsKey(string key);

		// Token: 0x060001B0 RID: 432
		void OnBeforeSerialize();

		// Token: 0x060001B1 RID: 433
		void OnAfterDeserialize();
	}
}

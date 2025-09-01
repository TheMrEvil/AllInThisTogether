using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x020000E4 RID: 228
	internal class HybridObjectCache
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x0000222F File Offset: 0x0000042F
		internal HybridObjectCache()
		{
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000351F0 File Offset: 0x000333F0
		internal void Add(string id, object obj)
		{
			if (this.objectDictionary == null)
			{
				this.objectDictionary = new Dictionary<string, object>();
			}
			object obj2;
			if (this.objectDictionary.TryGetValue(id, out obj2))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Invalid XML encountered. The same Id value '{0}' is defined more than once. Multiple objects cannot be deserialized using the same Id.", new object[]
				{
					id
				})));
			}
			this.objectDictionary.Add(id, obj);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003524C File Offset: 0x0003344C
		internal void Remove(string id)
		{
			if (this.objectDictionary != null)
			{
				this.objectDictionary.Remove(id);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00035264 File Offset: 0x00033464
		internal object GetObject(string id)
		{
			if (this.referencedObjectDictionary == null)
			{
				this.referencedObjectDictionary = new Dictionary<string, object>();
				this.referencedObjectDictionary.Add(id, null);
			}
			else if (!this.referencedObjectDictionary.ContainsKey(id))
			{
				this.referencedObjectDictionary.Add(id, null);
			}
			if (this.objectDictionary != null)
			{
				object result;
				this.objectDictionary.TryGetValue(id, out result);
				return result;
			}
			return null;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000352C8 File Offset: 0x000334C8
		internal bool IsObjectReferenced(string id)
		{
			return this.referencedObjectDictionary != null && this.referencedObjectDictionary.ContainsKey(id);
		}

		// Token: 0x04000613 RID: 1555
		private Dictionary<string, object> objectDictionary;

		// Token: 0x04000614 RID: 1556
		private Dictionary<string, object> referencedObjectDictionary;
	}
}

using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x0200026E RID: 622
	internal class LocalScope
	{
		// Token: 0x060017B0 RID: 6064 RVA: 0x0008B33D File Offset: 0x0008953D
		public LocalScope()
		{
			this.locals = new Dictionary<string, LocalBuilder>();
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0008B350 File Offset: 0x00089550
		public LocalScope(LocalScope parent) : this()
		{
			this.parent = parent;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0008B35F File Offset: 0x0008955F
		public void Add(string key, LocalBuilder value)
		{
			this.locals.Add(key, value);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0008B36E File Offset: 0x0008956E
		public bool ContainsKey(string key)
		{
			return this.locals.ContainsKey(key) || (this.parent != null && this.parent.ContainsKey(key));
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0008B396 File Offset: 0x00089596
		public bool TryGetValue(string key, out LocalBuilder value)
		{
			if (this.locals.TryGetValue(key, out value))
			{
				return true;
			}
			if (this.parent != null)
			{
				return this.parent.TryGetValue(key, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x17000450 RID: 1104
		public LocalBuilder this[string key]
		{
			get
			{
				LocalBuilder result;
				this.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this.locals[key] = value;
			}
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0008B3EC File Offset: 0x000895EC
		public void AddToFreeLocals(Dictionary<Tuple<Type, string>, Queue<LocalBuilder>> freeLocals)
		{
			foreach (KeyValuePair<string, LocalBuilder> keyValuePair in this.locals)
			{
				Tuple<Type, string> key = new Tuple<Type, string>(keyValuePair.Value.LocalType, keyValuePair.Key);
				Queue<LocalBuilder> queue;
				if (freeLocals.TryGetValue(key, out queue))
				{
					queue.Enqueue(keyValuePair.Value);
				}
				else
				{
					queue = new Queue<LocalBuilder>();
					queue.Enqueue(keyValuePair.Value);
					freeLocals.Add(key, queue);
				}
			}
		}

		// Token: 0x0400187B RID: 6267
		public readonly LocalScope parent;

		// Token: 0x0400187C RID: 6268
		private readonly Dictionary<string, LocalBuilder> locals;
	}
}

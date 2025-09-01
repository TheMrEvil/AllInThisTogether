using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Control;

namespace Parse.Platform.Objects
{
	// Token: 0x0200002E RID: 46
	public class MutableObjectState : IObjectState, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000096FF File Offset: 0x000078FF
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00009707 File Offset: 0x00007907
		public bool IsNew
		{
			[CompilerGenerated]
			get
			{
				return this.<IsNew>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsNew>k__BackingField = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00009710 File Offset: 0x00007910
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00009718 File Offset: 0x00007918
		public string ClassName
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ClassName>k__BackingField = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009721 File Offset: 0x00007921
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00009729 File Offset: 0x00007929
		public string ObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.<ObjectId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ObjectId>k__BackingField = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00009732 File Offset: 0x00007932
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000973A File Offset: 0x0000793A
		public DateTime? UpdatedAt
		{
			[CompilerGenerated]
			get
			{
				return this.<UpdatedAt>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UpdatedAt>k__BackingField = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00009743 File Offset: 0x00007943
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000974B File Offset: 0x0000794B
		public DateTime? CreatedAt
		{
			[CompilerGenerated]
			get
			{
				return this.<CreatedAt>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CreatedAt>k__BackingField = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00009754 File Offset: 0x00007954
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000975C File Offset: 0x0000795C
		public IDictionary<string, object> ServerData
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ServerData>k__BackingField = value;
			}
		} = new Dictionary<string, object>();

		// Token: 0x17000088 RID: 136
		public object this[string key]
		{
			get
			{
				return this.ServerData[key];
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009773 File Offset: 0x00007973
		public bool ContainsKey(string key)
		{
			return this.ServerData.ContainsKey(key);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009784 File Offset: 0x00007984
		public void Apply(IDictionary<string, IParseFieldOperation> operationSet)
		{
			foreach (KeyValuePair<string, IParseFieldOperation> keyValuePair in operationSet)
			{
				object oldValue;
				this.ServerData.TryGetValue(keyValuePair.Key, out oldValue);
				object obj = keyValuePair.Value.Apply(oldValue, keyValuePair.Key);
				if (obj != ParseDeleteOperation.Token)
				{
					this.ServerData[keyValuePair.Key] = obj;
				}
				else
				{
					this.ServerData.Remove(keyValuePair.Key);
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009820 File Offset: 0x00007A20
		public void Apply(IObjectState other)
		{
			this.IsNew = other.IsNew;
			if (other.ObjectId != null)
			{
				this.ObjectId = other.ObjectId;
			}
			if (other.UpdatedAt != null)
			{
				this.UpdatedAt = other.UpdatedAt;
			}
			if (other.CreatedAt != null)
			{
				this.CreatedAt = other.CreatedAt;
			}
			foreach (KeyValuePair<string, object> keyValuePair in other)
			{
				this.ServerData[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000098D4 File Offset: 0x00007AD4
		public IObjectState MutatedClone(Action<MutableObjectState> func)
		{
			MutableObjectState mutableObjectState = this.MutableClone();
			func(mutableObjectState);
			return mutableObjectState;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000098F0 File Offset: 0x00007AF0
		protected virtual MutableObjectState MutableClone()
		{
			MutableObjectState mutableObjectState = new MutableObjectState();
			mutableObjectState.IsNew = this.IsNew;
			mutableObjectState.ClassName = this.ClassName;
			mutableObjectState.ObjectId = this.ObjectId;
			mutableObjectState.CreatedAt = this.CreatedAt;
			mutableObjectState.UpdatedAt = this.UpdatedAt;
			mutableObjectState.ServerData = this.ToDictionary((KeyValuePair<string, object> t) => t.Key, (KeyValuePair<string, object> t) => t.Value);
			return mutableObjectState;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009988 File Offset: 0x00007B88
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return this.ServerData.GetEnumerator();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009995 File Offset: 0x00007B95
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000999D File Offset: 0x00007B9D
		public MutableObjectState()
		{
		}

		// Token: 0x04000056 RID: 86
		[CompilerGenerated]
		private bool <IsNew>k__BackingField;

		// Token: 0x04000057 RID: 87
		[CompilerGenerated]
		private string <ClassName>k__BackingField;

		// Token: 0x04000058 RID: 88
		[CompilerGenerated]
		private string <ObjectId>k__BackingField;

		// Token: 0x04000059 RID: 89
		[CompilerGenerated]
		private DateTime? <UpdatedAt>k__BackingField;

		// Token: 0x0400005A RID: 90
		[CompilerGenerated]
		private DateTime? <CreatedAt>k__BackingField;

		// Token: 0x0400005B RID: 91
		[CompilerGenerated]
		private IDictionary<string, object> <ServerData>k__BackingField;

		// Token: 0x020000FC RID: 252
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006C9 RID: 1737 RVA: 0x00014F6A File Offset: 0x0001316A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x00014F76 File Offset: 0x00013176
			public <>c()
			{
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x00014F7E File Offset: 0x0001317E
			internal string <MutableClone>b__30_0(KeyValuePair<string, object> t)
			{
				return t.Key;
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x00014F87 File Offset: 0x00013187
			internal object <MutableClone>b__30_1(KeyValuePair<string, object> t)
			{
				return t.Value;
			}

			// Token: 0x04000204 RID: 516
			public static readonly MutableObjectState.<>c <>9 = new MutableObjectState.<>c();

			// Token: 0x04000205 RID: 517
			public static Func<KeyValuePair<string, object>, string> <>9__30_0;

			// Token: 0x04000206 RID: 518
			public static Func<KeyValuePair<string, object>, object> <>9__30_1;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000396 RID: 918
	public class JSONObject : JSONNode
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000B640B File Offset: 0x000B460B
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x000B6413 File Offset: 0x000B4613
		public override bool Inline
		{
			get
			{
				return this.inline;
			}
			set
			{
				this.inline = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000B641C File Offset: 0x000B461C
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Object;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x000B641F File Offset: 0x000B461F
		public override bool IsObject
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000B6422 File Offset: 0x000B4622
		public override JSONNode.Enumerator GetEnumerator()
		{
			return new JSONNode.Enumerator(this.m_Dict.GetEnumerator());
		}

		// Token: 0x170001D1 RID: 465
		public override JSONNode this[string aKey]
		{
			get
			{
				if (this.m_Dict.ContainsKey(aKey))
				{
					return this.m_Dict[aKey];
				}
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				if (value == null)
				{
					value = JSONNull.CreateOrGet();
				}
				if (this.m_Dict.ContainsKey(aKey))
				{
					this.m_Dict[aKey] = value;
					return;
				}
				this.m_Dict.Add(aKey, value);
			}
		}

		// Token: 0x170001D2 RID: 466
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_Dict.Count)
				{
					return null;
				}
				return this.m_Dict.ElementAt(aIndex).Value;
			}
			set
			{
				if (value == null)
				{
					value = JSONNull.CreateOrGet();
				}
				if (aIndex < 0 || aIndex >= this.m_Dict.Count)
				{
					return;
				}
				string key = this.m_Dict.ElementAt(aIndex).Key;
				this.m_Dict[key] = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x000B651E File Offset: 0x000B471E
		public override int Count
		{
			get
			{
				return this.m_Dict.Count;
			}
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000B652C File Offset: 0x000B472C
		public override void Add(string aKey, JSONNode aItem)
		{
			if (aItem == null)
			{
				aItem = JSONNull.CreateOrGet();
			}
			if (aKey == null)
			{
				this.m_Dict.Add(Guid.NewGuid().ToString(), aItem);
				return;
			}
			if (this.m_Dict.ContainsKey(aKey))
			{
				this.m_Dict[aKey] = aItem;
				return;
			}
			this.m_Dict.Add(aKey, aItem);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000B6595 File Offset: 0x000B4795
		public override JSONNode Remove(string aKey)
		{
			if (!this.m_Dict.ContainsKey(aKey))
			{
				return null;
			}
			JSONNode result = this.m_Dict[aKey];
			this.m_Dict.Remove(aKey);
			return result;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000B65C0 File Offset: 0x000B47C0
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_Dict.Count)
			{
				return null;
			}
			KeyValuePair<string, JSONNode> keyValuePair = this.m_Dict.ElementAt(aIndex);
			this.m_Dict.Remove(keyValuePair.Key);
			return keyValuePair.Value;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000B6608 File Offset: 0x000B4808
		public override JSONNode Remove(JSONNode aNode)
		{
			JSONNode result;
			try
			{
				KeyValuePair<string, JSONNode> keyValuePair = (from k in this.m_Dict
				where k.Value == aNode
				select k).First<KeyValuePair<string, JSONNode>>();
				this.m_Dict.Remove(keyValuePair.Key);
				result = aNode;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000B6674 File Offset: 0x000B4874
		public override JSONNode Clone()
		{
			JSONObject jsonobject = new JSONObject();
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				jsonobject.Add(keyValuePair.Key, keyValuePair.Value.Clone());
			}
			return jsonobject;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000B66E0 File Offset: 0x000B48E0
		public override bool HasKey(string aKey)
		{
			return this.m_Dict.ContainsKey(aKey);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000B66F0 File Offset: 0x000B48F0
		public override JSONNode GetValueOrDefault(string aKey, JSONNode aDefault)
		{
			JSONNode result;
			if (this.m_Dict.TryGetValue(aKey, out result))
			{
				return result;
			}
			return aDefault;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000B6710 File Offset: 0x000B4910
		public override IEnumerable<JSONNode> Children
		{
			get
			{
				foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
				{
					yield return keyValuePair.Value;
				}
				Dictionary<string, JSONNode>.Enumerator enumerator = default(Dictionary<string, JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000B6720 File Offset: 0x000B4920
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append('{');
			bool flag = true;
			if (this.inline)
			{
				aMode = JSONTextMode.Compact;
			}
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				if (!flag)
				{
					aSB.Append(',');
				}
				flag = false;
				if (aMode == JSONTextMode.Indent)
				{
					aSB.AppendLine();
				}
				if (aMode == JSONTextMode.Indent)
				{
					aSB.Append(' ', aIndent + aIndentInc);
				}
				aSB.Append('"').Append(JSONNode.Escape(keyValuePair.Key)).Append('"');
				if (aMode == JSONTextMode.Compact)
				{
					aSB.Append(':');
				}
				else
				{
					aSB.Append(" : ");
				}
				keyValuePair.Value.WriteToStringBuilder(aSB, aIndent + aIndentInc, aIndentInc, aMode);
			}
			if (aMode == JSONTextMode.Indent)
			{
				aSB.AppendLine().Append(' ', aIndent);
			}
			aSB.Append('}');
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000B6820 File Offset: 0x000B4A20
		public JSONObject()
		{
		}

		// Token: 0x04001EC5 RID: 7877
		private Dictionary<string, JSONNode> m_Dict = new Dictionary<string, JSONNode>();

		// Token: 0x04001EC6 RID: 7878
		private bool inline;

		// Token: 0x02000690 RID: 1680
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x06002803 RID: 10243 RVA: 0x000D7833 File Offset: 0x000D5A33
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x06002804 RID: 10244 RVA: 0x000D783B File Offset: 0x000D5A3B
			internal bool <Remove>b__0(KeyValuePair<string, JSONNode> k)
			{
				return k.Value == this.aNode;
			}

			// Token: 0x04002C1D RID: 11293
			public JSONNode aNode;
		}

		// Token: 0x02000691 RID: 1681
		[CompilerGenerated]
		private sealed class <get_Children>d__26 : IEnumerable<JSONNode>, IEnumerable, IEnumerator<JSONNode>, IEnumerator, IDisposable
		{
			// Token: 0x06002805 RID: 10245 RVA: 0x000D784F File Offset: 0x000D5A4F
			[DebuggerHidden]
			public <get_Children>d__26(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06002806 RID: 10246 RVA: 0x000D786C File Offset: 0x000D5A6C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06002807 RID: 10247 RVA: 0x000D78A4 File Offset: 0x000D5AA4
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					JSONObject jsonobject = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = jsonobject.m_Dict.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = default(Dictionary<string, JSONNode>.Enumerator);
						result = false;
					}
					else
					{
						KeyValuePair<string, JSONNode> keyValuePair = enumerator.Current;
						this.<>2__current = keyValuePair.Value;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06002808 RID: 10248 RVA: 0x000D7954 File Offset: 0x000D5B54
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator).Dispose();
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06002809 RID: 10249 RVA: 0x000D796E File Offset: 0x000D5B6E
			JSONNode IEnumerator<JSONNode>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600280A RID: 10250 RVA: 0x000D7976 File Offset: 0x000D5B76
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x0600280B RID: 10251 RVA: 0x000D797D File Offset: 0x000D5B7D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600280C RID: 10252 RVA: 0x000D7988 File Offset: 0x000D5B88
			[DebuggerHidden]
			IEnumerator<JSONNode> IEnumerable<JSONNode>.GetEnumerator()
			{
				JSONObject.<get_Children>d__26 <get_Children>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Children>d__ = this;
				}
				else
				{
					<get_Children>d__ = new JSONObject.<get_Children>d__26(0);
					<get_Children>d__.<>4__this = this;
				}
				return <get_Children>d__;
			}

			// Token: 0x0600280D RID: 10253 RVA: 0x000D79CB File Offset: 0x000D5BCB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SimpleJSON.JSONNode>.GetEnumerator();
			}

			// Token: 0x04002C1E RID: 11294
			private int <>1__state;

			// Token: 0x04002C1F RID: 11295
			private JSONNode <>2__current;

			// Token: 0x04002C20 RID: 11296
			private int <>l__initialThreadId;

			// Token: 0x04002C21 RID: 11297
			public JSONObject <>4__this;

			// Token: 0x04002C22 RID: 11298
			private Dictionary<string, JSONNode>.Enumerator <>7__wrap1;
		}
	}
}

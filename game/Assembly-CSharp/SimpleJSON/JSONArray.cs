using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000395 RID: 917
	public class JSONArray : JSONNode
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x000B61AB File Offset: 0x000B43AB
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x000B61B3 File Offset: 0x000B43B3
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x000B61BC File Offset: 0x000B43BC
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Array;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x000B61BF File Offset: 0x000B43BF
		public override bool IsArray
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000B61C2 File Offset: 0x000B43C2
		public override JSONNode.Enumerator GetEnumerator()
		{
			return new JSONNode.Enumerator(this.m_List.GetEnumerator());
		}

		// Token: 0x170001CA RID: 458
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					return new JSONLazyCreator(this);
				}
				return this.m_List[aIndex];
			}
			set
			{
				if (value == null)
				{
					value = JSONNull.CreateOrGet();
				}
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					this.m_List.Add(value);
					return;
				}
				this.m_List[aIndex] = value;
			}
		}

		// Token: 0x170001CB RID: 459
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				if (value == null)
				{
					value = JSONNull.CreateOrGet();
				}
				this.m_List.Add(value);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x000B625F File Offset: 0x000B445F
		public override int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000B626C File Offset: 0x000B446C
		public override void Add(string aKey, JSONNode aItem)
		{
			if (aItem == null)
			{
				aItem = JSONNull.CreateOrGet();
			}
			this.m_List.Add(aItem);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000B628A File Offset: 0x000B448A
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_List.Count)
			{
				return null;
			}
			JSONNode result = this.m_List[aIndex];
			this.m_List.RemoveAt(aIndex);
			return result;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x000B62B8 File Offset: 0x000B44B8
		public override JSONNode Remove(JSONNode aNode)
		{
			this.m_List.Remove(aNode);
			return aNode;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000B62C8 File Offset: 0x000B44C8
		public override JSONNode Clone()
		{
			JSONArray jsonarray = new JSONArray();
			jsonarray.m_List.Capacity = this.m_List.Capacity;
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (jsonnode != null)
				{
					jsonarray.Add(jsonnode.Clone());
				}
				else
				{
					jsonarray.Add(null);
				}
			}
			return jsonarray;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x000B6350 File Offset: 0x000B4550
		public override IEnumerable<JSONNode> Children
		{
			get
			{
				foreach (JSONNode jsonnode in this.m_List)
				{
					yield return jsonnode;
				}
				List<JSONNode>.Enumerator enumerator = default(List<JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000B6360 File Offset: 0x000B4560
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append('[');
			int count = this.m_List.Count;
			if (this.inline)
			{
				aMode = JSONTextMode.Compact;
			}
			for (int i = 0; i < count; i++)
			{
				if (i > 0)
				{
					aSB.Append(',');
				}
				if (aMode == JSONTextMode.Indent)
				{
					aSB.AppendLine();
				}
				if (aMode == JSONTextMode.Indent)
				{
					aSB.Append(' ', aIndent + aIndentInc);
				}
				this.m_List[i].WriteToStringBuilder(aSB, aIndent + aIndentInc, aIndentInc, aMode);
			}
			if (aMode == JSONTextMode.Indent)
			{
				aSB.AppendLine().Append(' ', aIndent);
			}
			aSB.Append(']');
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000B63F8 File Offset: 0x000B45F8
		public JSONArray()
		{
		}

		// Token: 0x04001EC3 RID: 7875
		private List<JSONNode> m_List = new List<JSONNode>();

		// Token: 0x04001EC4 RID: 7876
		private bool inline;

		// Token: 0x0200068F RID: 1679
		[CompilerGenerated]
		private sealed class <get_Children>d__23 : IEnumerable<JSONNode>, IEnumerable, IEnumerator<JSONNode>, IEnumerator, IDisposable
		{
			// Token: 0x060027FA RID: 10234 RVA: 0x000D76B3 File Offset: 0x000D58B3
			[DebuggerHidden]
			public <get_Children>d__23(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060027FB RID: 10235 RVA: 0x000D76D0 File Offset: 0x000D58D0
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

			// Token: 0x060027FC RID: 10236 RVA: 0x000D7708 File Offset: 0x000D5908
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					JSONArray jsonarray = this;
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
						enumerator = jsonarray.m_List.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = default(List<JSONNode>.Enumerator);
						result = false;
					}
					else
					{
						JSONNode jsonnode = enumerator.Current;
						this.<>2__current = jsonnode;
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

			// Token: 0x060027FD RID: 10237 RVA: 0x000D77B4 File Offset: 0x000D59B4
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator).Dispose();
			}

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x060027FE RID: 10238 RVA: 0x000D77CE File Offset: 0x000D59CE
			JSONNode IEnumerator<JSONNode>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060027FF RID: 10239 RVA: 0x000D77D6 File Offset: 0x000D59D6
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x06002800 RID: 10240 RVA: 0x000D77DD File Offset: 0x000D59DD
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002801 RID: 10241 RVA: 0x000D77E8 File Offset: 0x000D59E8
			[DebuggerHidden]
			IEnumerator<JSONNode> IEnumerable<JSONNode>.GetEnumerator()
			{
				JSONArray.<get_Children>d__23 <get_Children>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Children>d__ = this;
				}
				else
				{
					<get_Children>d__ = new JSONArray.<get_Children>d__23(0);
					<get_Children>d__.<>4__this = this;
				}
				return <get_Children>d__;
			}

			// Token: 0x06002802 RID: 10242 RVA: 0x000D782B File Offset: 0x000D5A2B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SimpleJSON.JSONNode>.GetEnumerator();
			}

			// Token: 0x04002C18 RID: 11288
			private int <>1__state;

			// Token: 0x04002C19 RID: 11289
			private JSONNode <>2__current;

			// Token: 0x04002C1A RID: 11290
			private int <>l__initialThreadId;

			// Token: 0x04002C1B RID: 11291
			public JSONArray <>4__this;

			// Token: 0x04002C1C RID: 11292
			private List<JSONNode>.Enumerator <>7__wrap1;
		}
	}
}

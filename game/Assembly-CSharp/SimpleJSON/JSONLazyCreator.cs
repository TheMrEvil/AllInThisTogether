using System;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x0200039B RID: 923
	internal class JSONLazyCreator : JSONNode
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000B6BD7 File Offset: 0x000B4DD7
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.None;
			}
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000B6BDC File Offset: 0x000B4DDC
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000B6BF2 File Offset: 0x000B4DF2
		public JSONLazyCreator(JSONNode aNode)
		{
			this.m_Node = aNode;
			this.m_Key = null;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000B6C08 File Offset: 0x000B4E08
		public JSONLazyCreator(JSONNode aNode, string aKey)
		{
			this.m_Node = aNode;
			this.m_Key = aKey;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000B6C1E File Offset: 0x000B4E1E
		private T Set<T>(T aVal) where T : JSONNode
		{
			if (this.m_Key == null)
			{
				this.m_Node.Add(aVal);
			}
			else
			{
				this.m_Node.Add(this.m_Key, aVal);
			}
			this.m_Node = null;
			return aVal;
		}

		// Token: 0x170001E6 RID: 486
		public override JSONNode this[int aIndex]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.Set<JSONArray>(new JSONArray()).Add(value);
			}
		}

		// Token: 0x170001E7 RID: 487
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				this.Set<JSONObject>(new JSONObject()).Add(aKey, value);
			}
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000B6C92 File Offset: 0x000B4E92
		public override void Add(JSONNode aItem)
		{
			this.Set<JSONArray>(new JSONArray()).Add(aItem);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000B6CA5 File Offset: 0x000B4EA5
		public override void Add(string aKey, JSONNode aItem)
		{
			this.Set<JSONObject>(new JSONObject()).Add(aKey, aItem);
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000B6CB9 File Offset: 0x000B4EB9
		public static bool operator ==(JSONLazyCreator a, object b)
		{
			return b == null || a == b;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000B6CC4 File Offset: 0x000B4EC4
		public static bool operator !=(JSONLazyCreator a, object b)
		{
			return !(a == b);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x000B6CD0 File Offset: 0x000B4ED0
		public override bool Equals(object obj)
		{
			return obj == null || this == obj;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000B6CDB File Offset: 0x000B4EDB
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x000B6CDE File Offset: 0x000B4EDE
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x000B6CF6 File Offset: 0x000B4EF6
		public override int AsInt
		{
			get
			{
				this.Set<JSONNumber>(new JSONNumber(0.0));
				return 0;
			}
			set
			{
				this.Set<JSONNumber>(new JSONNumber((double)value));
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x000B6D06 File Offset: 0x000B4F06
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x000B6D22 File Offset: 0x000B4F22
		public override float AsFloat
		{
			get
			{
				this.Set<JSONNumber>(new JSONNumber(0.0));
				return 0f;
			}
			set
			{
				this.Set<JSONNumber>(new JSONNumber((double)value));
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x000B6D32 File Offset: 0x000B4F32
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x000B6D52 File Offset: 0x000B4F52
		public override double AsDouble
		{
			get
			{
				this.Set<JSONNumber>(new JSONNumber(0.0));
				return 0.0;
			}
			set
			{
				this.Set<JSONNumber>(new JSONNumber(value));
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x000B6D61 File Offset: 0x000B4F61
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x000B6D94 File Offset: 0x000B4F94
		public override long AsLong
		{
			get
			{
				if (JSONNode.longAsString)
				{
					this.Set<JSONString>(new JSONString("0"));
				}
				else
				{
					this.Set<JSONNumber>(new JSONNumber(0.0));
				}
				return 0L;
			}
			set
			{
				if (JSONNode.longAsString)
				{
					this.Set<JSONString>(new JSONString(value.ToString()));
					return;
				}
				this.Set<JSONNumber>(new JSONNumber((double)value));
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000B6DBF File Offset: 0x000B4FBF
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x000B6DCF File Offset: 0x000B4FCF
		public override bool AsBool
		{
			get
			{
				this.Set<JSONBool>(new JSONBool(false));
				return false;
			}
			set
			{
				this.Set<JSONBool>(new JSONBool(value));
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x000B6DDE File Offset: 0x000B4FDE
		public override JSONArray AsArray
		{
			get
			{
				return this.Set<JSONArray>(new JSONArray());
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x000B6DEB File Offset: 0x000B4FEB
		public override JSONObject AsObject
		{
			get
			{
				return this.Set<JSONObject>(new JSONObject());
			}
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000B6DF8 File Offset: 0x000B4FF8
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append("null");
		}

		// Token: 0x04001ECC RID: 7884
		private JSONNode m_Node;

		// Token: 0x04001ECD RID: 7885
		private string m_Key;
	}
}

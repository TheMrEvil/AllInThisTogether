using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000394 RID: 916
	public abstract class JSONNode
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06001DD4 RID: 7636
		public abstract JSONNodeType Tag { get; }

		// Token: 0x170001AF RID: 431
		public virtual JSONNode this[int aIndex]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001B0 RID: 432
		public virtual JSONNode this[string aKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000B588C File Offset: 0x000B3A8C
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x000B5893 File Offset: 0x000B3A93
		public virtual string Value
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000B5895 File Offset: 0x000B3A95
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x000B5898 File Offset: 0x000B3A98
		public virtual bool IsNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000B589B File Offset: 0x000B3A9B
		public virtual bool IsString
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x000B589E File Offset: 0x000B3A9E
		public virtual bool IsBoolean
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000B58A1 File Offset: 0x000B3AA1
		public virtual bool IsNull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x000B58A4 File Offset: 0x000B3AA4
		public virtual bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000B58A7 File Offset: 0x000B3AA7
		public virtual bool IsObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000B58AA File Offset: 0x000B3AAA
		// (set) Token: 0x06001DE3 RID: 7651 RVA: 0x000B58AD File Offset: 0x000B3AAD
		public virtual bool Inline
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000B58AF File Offset: 0x000B3AAF
		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000B58B1 File Offset: 0x000B3AB1
		public virtual void Add(JSONNode aItem)
		{
			this.Add("", aItem);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000B58BF File Offset: 0x000B3ABF
		public virtual JSONNode Remove(string aKey)
		{
			return null;
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000B58C2 File Offset: 0x000B3AC2
		public virtual JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000B58C5 File Offset: 0x000B3AC5
		public virtual JSONNode Remove(JSONNode aNode)
		{
			return aNode;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000B58C8 File Offset: 0x000B3AC8
		public virtual JSONNode Clone()
		{
			return null;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x000B58CB File Offset: 0x000B3ACB
		public virtual IEnumerable<JSONNode> Children
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x000B58D4 File Offset: 0x000B3AD4
		public IEnumerable<JSONNode> DeepChildren
		{
			get
			{
				foreach (JSONNode jsonnode in this.Children)
				{
					foreach (JSONNode jsonnode2 in jsonnode.DeepChildren)
					{
						yield return jsonnode2;
					}
					IEnumerator<JSONNode> enumerator2 = null;
				}
				IEnumerator<JSONNode> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000B58E4 File Offset: 0x000B3AE4
		public virtual bool HasKey(string aKey)
		{
			return false;
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x000B58E7 File Offset: 0x000B3AE7
		public virtual JSONNode GetValueOrDefault(string aKey, JSONNode aDefault)
		{
			return aDefault;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000B58EC File Offset: 0x000B3AEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.WriteToStringBuilder(stringBuilder, 0, 0, JSONTextMode.Compact);
			return stringBuilder.ToString();
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000B5910 File Offset: 0x000B3B10
		public virtual string ToString(int aIndent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.WriteToStringBuilder(stringBuilder, 0, aIndent, JSONTextMode.Indent);
			return stringBuilder.ToString();
		}

		// Token: 0x06001DF0 RID: 7664
		internal abstract void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode);

		// Token: 0x06001DF1 RID: 7665
		public abstract JSONNode.Enumerator GetEnumerator();

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000B5933 File Offset: 0x000B3B33
		public IEnumerable<KeyValuePair<string, JSONNode>> Linq
		{
			get
			{
				return new JSONNode.LinqEnumerator(this);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000B593B File Offset: 0x000B3B3B
		public JSONNode.KeyEnumerator Keys
		{
			get
			{
				return new JSONNode.KeyEnumerator(this.GetEnumerator());
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x000B5948 File Offset: 0x000B3B48
		public JSONNode.ValueEnumerator Values
		{
			get
			{
				return new JSONNode.ValueEnumerator(this.GetEnumerator());
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x000B5958 File Offset: 0x000B3B58
		// (set) Token: 0x06001DF6 RID: 7670 RVA: 0x000B5993 File Offset: 0x000B3B93
		public virtual double AsDouble
		{
			get
			{
				double result = 0.0;
				if (double.TryParse(this.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
				{
					return result;
				}
				return 0.0;
			}
			set
			{
				this.Value = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x000B59A7 File Offset: 0x000B3BA7
		// (set) Token: 0x06001DF8 RID: 7672 RVA: 0x000B59B0 File Offset: 0x000B3BB0
		public virtual int AsInt
		{
			get
			{
				return (int)this.AsDouble;
			}
			set
			{
				this.AsDouble = (double)value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x000B59BA File Offset: 0x000B3BBA
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x000B59C3 File Offset: 0x000B3BC3
		public virtual float AsFloat
		{
			get
			{
				return (float)this.AsDouble;
			}
			set
			{
				this.AsDouble = (double)value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000B59D0 File Offset: 0x000B3BD0
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x000B59FE File Offset: 0x000B3BFE
		public virtual bool AsBool
		{
			get
			{
				bool result = false;
				if (bool.TryParse(this.Value, out result))
				{
					return result;
				}
				return !string.IsNullOrEmpty(this.Value);
			}
			set
			{
				this.Value = (value ? "true" : "false");
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000B5A18 File Offset: 0x000B3C18
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x000B5A3B File Offset: 0x000B3C3B
		public virtual long AsLong
		{
			get
			{
				long result = 0L;
				if (long.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0L;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x000B5A4A File Offset: 0x000B3C4A
		public virtual JSONArray AsArray
		{
			get
			{
				return this as JSONArray;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x000B5A52 File Offset: 0x000B3C52
		public virtual JSONObject AsObject
		{
			get
			{
				return this as JSONObject;
			}
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x000B5A5A File Offset: 0x000B3C5A
		public static implicit operator JSONNode(string s)
		{
			return new JSONString(s);
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000B5A62 File Offset: 0x000B3C62
		public static implicit operator string(JSONNode d)
		{
			if (!(d == null))
			{
				return d.Value;
			}
			return null;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000B5A75 File Offset: 0x000B3C75
		public static implicit operator JSONNode(double n)
		{
			return new JSONNumber(n);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000B5A7D File Offset: 0x000B3C7D
		public static implicit operator double(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsDouble;
			}
			return 0.0;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000B5A98 File Offset: 0x000B3C98
		public static implicit operator JSONNode(float n)
		{
			return new JSONNumber((double)n);
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x000B5AA1 File Offset: 0x000B3CA1
		public static implicit operator float(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsFloat;
			}
			return 0f;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000B5AB8 File Offset: 0x000B3CB8
		public static implicit operator JSONNode(int n)
		{
			return new JSONNumber((double)n);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000B5AC1 File Offset: 0x000B3CC1
		public static implicit operator int(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsInt;
			}
			return 0;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000B5AD4 File Offset: 0x000B3CD4
		public static implicit operator JSONNode(long n)
		{
			if (JSONNode.longAsString)
			{
				return new JSONString(n.ToString());
			}
			return new JSONNumber((double)n);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000B5AF1 File Offset: 0x000B3CF1
		public static implicit operator long(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsLong;
			}
			return 0L;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x000B5B05 File Offset: 0x000B3D05
		public static implicit operator JSONNode(bool b)
		{
			return new JSONBool(b);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000B5B0D File Offset: 0x000B3D0D
		public static implicit operator bool(JSONNode d)
		{
			return !(d == null) && d.AsBool;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000B5B20 File Offset: 0x000B3D20
		public static implicit operator JSONNode(KeyValuePair<string, JSONNode> aKeyValue)
		{
			return aKeyValue.Value;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000B5B2C File Offset: 0x000B3D2C
		public static bool operator ==(JSONNode a, object b)
		{
			if (a == b)
			{
				return true;
			}
			bool flag = a is JSONNull || a == null || a is JSONLazyCreator;
			bool flag2 = b is JSONNull || b == null || b is JSONLazyCreator;
			return (flag && flag2) || (!flag && a.Equals(b));
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000B5B82 File Offset: 0x000B3D82
		public static bool operator !=(JSONNode a, object b)
		{
			return !(a == b);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000B5B8E File Offset: 0x000B3D8E
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000B5B94 File Offset: 0x000B3D94
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x000B5B9C File Offset: 0x000B3D9C
		internal static StringBuilder EscapeBuilder
		{
			get
			{
				if (JSONNode.m_EscapeBuilder == null)
				{
					JSONNode.m_EscapeBuilder = new StringBuilder();
				}
				return JSONNode.m_EscapeBuilder;
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000B5BB4 File Offset: 0x000B3DB4
		internal static string Escape(string aText)
		{
			StringBuilder escapeBuilder = JSONNode.EscapeBuilder;
			escapeBuilder.Length = 0;
			if (escapeBuilder.Capacity < aText.Length + aText.Length / 10)
			{
				escapeBuilder.Capacity = aText.Length + aText.Length / 10;
			}
			int i = 0;
			while (i < aText.Length)
			{
				char c = aText[i];
				switch (c)
				{
				case '\b':
					escapeBuilder.Append("\\b");
					break;
				case '\t':
					escapeBuilder.Append("\\t");
					break;
				case '\n':
					escapeBuilder.Append("\\n");
					break;
				case '\v':
					goto IL_E2;
				case '\f':
					escapeBuilder.Append("\\f");
					break;
				case '\r':
					escapeBuilder.Append("\\r");
					break;
				default:
					if (c != '"')
					{
						if (c != '\\')
						{
							goto IL_E2;
						}
						escapeBuilder.Append("\\\\");
					}
					else
					{
						escapeBuilder.Append("\\\"");
					}
					break;
				}
				IL_121:
				i++;
				continue;
				IL_E2:
				if (c < ' ' || (JSONNode.forceASCII && c > '\u007f'))
				{
					ushort num = (ushort)c;
					escapeBuilder.Append("\\u").Append(num.ToString("X4"));
					goto IL_121;
				}
				escapeBuilder.Append(c);
				goto IL_121;
			}
			string result = escapeBuilder.ToString();
			escapeBuilder.Length = 0;
			return result;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000B5D04 File Offset: 0x000B3F04
		private static JSONNode ParseElement(string token, bool quoted)
		{
			if (quoted)
			{
				return token;
			}
			string a = token.ToLower();
			if (a == "false" || a == "true")
			{
				return a == "true";
			}
			if (a == "null")
			{
				return JSONNull.CreateOrGet();
			}
			double n;
			if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out n))
			{
				return n;
			}
			return token;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000B5D84 File Offset: 0x000B3F84
		public static JSONNode Parse(string aJSON)
		{
			Stack<JSONNode> stack = new Stack<JSONNode>();
			JSONNode jsonnode = null;
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			string aKey = "";
			bool flag = false;
			bool flag2 = false;
			while (i < aJSON.Length)
			{
				char c = aJSON[i];
				if (c <= '/')
				{
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
						case '\r':
							goto IL_3C7;
						case '\v':
						case '\f':
							goto IL_3B9;
						default:
							if (c != ' ')
							{
								goto IL_3B9;
							}
							break;
						}
						if (flag)
						{
							stringBuilder.Append(aJSON[i]);
						}
					}
					else if (c != '"')
					{
						if (c != ',')
						{
							if (c != '/')
							{
								goto IL_3B9;
							}
							if (JSONNode.allowLineComments && !flag && i + 1 < aJSON.Length && aJSON[i + 1] == '/')
							{
								while (++i < aJSON.Length && aJSON[i] != '\n')
								{
									if (aJSON[i] == '\r')
									{
										break;
									}
								}
							}
							else
							{
								stringBuilder.Append(aJSON[i]);
							}
						}
						else if (flag)
						{
							stringBuilder.Append(aJSON[i]);
						}
						else
						{
							if (stringBuilder.Length > 0 || flag2)
							{
								jsonnode.Add(aKey, JSONNode.ParseElement(stringBuilder.ToString(), flag2));
							}
							aKey = "";
							stringBuilder.Length = 0;
							flag2 = false;
						}
					}
					else
					{
						flag = !flag;
						flag2 = (flag2 || flag);
					}
				}
				else
				{
					if (c <= ']')
					{
						if (c != ':')
						{
							switch (c)
							{
							case '[':
								if (flag)
								{
									stringBuilder.Append(aJSON[i]);
									goto IL_3C7;
								}
								stack.Push(new JSONArray());
								if (jsonnode != null)
								{
									jsonnode.Add(aKey, stack.Peek());
								}
								aKey = "";
								stringBuilder.Length = 0;
								jsonnode = stack.Peek();
								goto IL_3C7;
							case '\\':
								i++;
								if (flag)
								{
									char c2 = aJSON[i];
									if (c2 <= 'f')
									{
										if (c2 == 'b')
										{
											stringBuilder.Append('\b');
											goto IL_3C7;
										}
										if (c2 == 'f')
										{
											stringBuilder.Append('\f');
											goto IL_3C7;
										}
									}
									else
									{
										if (c2 == 'n')
										{
											stringBuilder.Append('\n');
											goto IL_3C7;
										}
										switch (c2)
										{
										case 'r':
											stringBuilder.Append('\r');
											goto IL_3C7;
										case 't':
											stringBuilder.Append('\t');
											goto IL_3C7;
										case 'u':
										{
											string s = aJSON.Substring(i + 1, 4);
											stringBuilder.Append((char)int.Parse(s, NumberStyles.AllowHexSpecifier));
											i += 4;
											goto IL_3C7;
										}
										}
									}
									stringBuilder.Append(c2);
									goto IL_3C7;
								}
								goto IL_3C7;
							case ']':
								break;
							default:
								goto IL_3B9;
							}
						}
						else
						{
							if (flag)
							{
								stringBuilder.Append(aJSON[i]);
								goto IL_3C7;
							}
							aKey = stringBuilder.ToString();
							stringBuilder.Length = 0;
							flag2 = false;
							goto IL_3C7;
						}
					}
					else if (c != '{')
					{
						if (c != '}')
						{
							if (c != '﻿')
							{
								goto IL_3B9;
							}
							goto IL_3C7;
						}
					}
					else
					{
						if (flag)
						{
							stringBuilder.Append(aJSON[i]);
							goto IL_3C7;
						}
						stack.Push(new JSONObject());
						if (jsonnode != null)
						{
							jsonnode.Add(aKey, stack.Peek());
						}
						aKey = "";
						stringBuilder.Length = 0;
						jsonnode = stack.Peek();
						goto IL_3C7;
					}
					if (flag)
					{
						stringBuilder.Append(aJSON[i]);
					}
					else
					{
						if (stack.Count == 0)
						{
							throw new Exception("JSON Parse: Too many closing brackets");
						}
						stack.Pop();
						if (stringBuilder.Length > 0 || flag2)
						{
							jsonnode.Add(aKey, JSONNode.ParseElement(stringBuilder.ToString(), flag2));
						}
						flag2 = false;
						aKey = "";
						stringBuilder.Length = 0;
						if (stack.Count > 0)
						{
							jsonnode = stack.Peek();
						}
					}
				}
				IL_3C7:
				i++;
				continue;
				IL_3B9:
				stringBuilder.Append(aJSON[i]);
				goto IL_3C7;
			}
			if (flag)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			if (jsonnode == null)
			{
				return JSONNode.ParseElement(stringBuilder.ToString(), flag2);
			}
			return jsonnode;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000B618F File Offset: 0x000B438F
		protected JSONNode()
		{
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000B6197 File Offset: 0x000B4397
		// Note: this type is marked as 'beforefieldinit'.
		static JSONNode()
		{
		}

		// Token: 0x04001EBF RID: 7871
		public static bool forceASCII = false;

		// Token: 0x04001EC0 RID: 7872
		public static bool longAsString = false;

		// Token: 0x04001EC1 RID: 7873
		public static bool allowLineComments = true;

		// Token: 0x04001EC2 RID: 7874
		[ThreadStatic]
		private static StringBuilder m_EscapeBuilder;

		// Token: 0x02000689 RID: 1673
		public struct Enumerator
		{
			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x060027CF RID: 10191 RVA: 0x000D7204 File Offset: 0x000D5404
			public bool IsValid
			{
				get
				{
					return this.type > JSONNode.Enumerator.Type.None;
				}
			}

			// Token: 0x060027D0 RID: 10192 RVA: 0x000D720F File Offset: 0x000D540F
			public Enumerator(List<JSONNode>.Enumerator aArrayEnum)
			{
				this.type = JSONNode.Enumerator.Type.Array;
				this.m_Object = default(Dictionary<string, JSONNode>.Enumerator);
				this.m_Array = aArrayEnum;
			}

			// Token: 0x060027D1 RID: 10193 RVA: 0x000D722B File Offset: 0x000D542B
			public Enumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
			{
				this.type = JSONNode.Enumerator.Type.Object;
				this.m_Object = aDictEnum;
				this.m_Array = default(List<JSONNode>.Enumerator);
			}

			// Token: 0x170003BA RID: 954
			// (get) Token: 0x060027D2 RID: 10194 RVA: 0x000D7248 File Offset: 0x000D5448
			public KeyValuePair<string, JSONNode> Current
			{
				get
				{
					if (this.type == JSONNode.Enumerator.Type.Array)
					{
						return new KeyValuePair<string, JSONNode>(string.Empty, this.m_Array.Current);
					}
					if (this.type == JSONNode.Enumerator.Type.Object)
					{
						return this.m_Object.Current;
					}
					return new KeyValuePair<string, JSONNode>(string.Empty, null);
				}
			}

			// Token: 0x060027D3 RID: 10195 RVA: 0x000D7294 File Offset: 0x000D5494
			public bool MoveNext()
			{
				if (this.type == JSONNode.Enumerator.Type.Array)
				{
					return this.m_Array.MoveNext();
				}
				return this.type == JSONNode.Enumerator.Type.Object && this.m_Object.MoveNext();
			}

			// Token: 0x04002C08 RID: 11272
			private JSONNode.Enumerator.Type type;

			// Token: 0x04002C09 RID: 11273
			private Dictionary<string, JSONNode>.Enumerator m_Object;

			// Token: 0x04002C0A RID: 11274
			private List<JSONNode>.Enumerator m_Array;

			// Token: 0x020006C5 RID: 1733
			private enum Type
			{
				// Token: 0x04002CED RID: 11501
				None,
				// Token: 0x04002CEE RID: 11502
				Array,
				// Token: 0x04002CEF RID: 11503
				Object
			}
		}

		// Token: 0x0200068A RID: 1674
		public struct ValueEnumerator
		{
			// Token: 0x060027D4 RID: 10196 RVA: 0x000D72C1 File Offset: 0x000D54C1
			public ValueEnumerator(List<JSONNode>.Enumerator aArrayEnum)
			{
				this = new JSONNode.ValueEnumerator(new JSONNode.Enumerator(aArrayEnum));
			}

			// Token: 0x060027D5 RID: 10197 RVA: 0x000D72CF File Offset: 0x000D54CF
			public ValueEnumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
			{
				this = new JSONNode.ValueEnumerator(new JSONNode.Enumerator(aDictEnum));
			}

			// Token: 0x060027D6 RID: 10198 RVA: 0x000D72DD File Offset: 0x000D54DD
			public ValueEnumerator(JSONNode.Enumerator aEnumerator)
			{
				this.m_Enumerator = aEnumerator;
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x060027D7 RID: 10199 RVA: 0x000D72E8 File Offset: 0x000D54E8
			public JSONNode Current
			{
				get
				{
					KeyValuePair<string, JSONNode> keyValuePair = this.m_Enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x060027D8 RID: 10200 RVA: 0x000D7308 File Offset: 0x000D5508
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x060027D9 RID: 10201 RVA: 0x000D7315 File Offset: 0x000D5515
			public JSONNode.ValueEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04002C0B RID: 11275
			private JSONNode.Enumerator m_Enumerator;
		}

		// Token: 0x0200068B RID: 1675
		public struct KeyEnumerator
		{
			// Token: 0x060027DA RID: 10202 RVA: 0x000D731D File Offset: 0x000D551D
			public KeyEnumerator(List<JSONNode>.Enumerator aArrayEnum)
			{
				this = new JSONNode.KeyEnumerator(new JSONNode.Enumerator(aArrayEnum));
			}

			// Token: 0x060027DB RID: 10203 RVA: 0x000D732B File Offset: 0x000D552B
			public KeyEnumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
			{
				this = new JSONNode.KeyEnumerator(new JSONNode.Enumerator(aDictEnum));
			}

			// Token: 0x060027DC RID: 10204 RVA: 0x000D7339 File Offset: 0x000D5539
			public KeyEnumerator(JSONNode.Enumerator aEnumerator)
			{
				this.m_Enumerator = aEnumerator;
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x060027DD RID: 10205 RVA: 0x000D7344 File Offset: 0x000D5544
			public string Current
			{
				get
				{
					KeyValuePair<string, JSONNode> keyValuePair = this.m_Enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x060027DE RID: 10206 RVA: 0x000D7364 File Offset: 0x000D5564
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x060027DF RID: 10207 RVA: 0x000D7371 File Offset: 0x000D5571
			public JSONNode.KeyEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04002C0C RID: 11276
			private JSONNode.Enumerator m_Enumerator;
		}

		// Token: 0x0200068C RID: 1676
		public class LinqEnumerator : IEnumerator<KeyValuePair<string, JSONNode>>, IEnumerator, IDisposable, IEnumerable<KeyValuePair<string, JSONNode>>, IEnumerable
		{
			// Token: 0x060027E0 RID: 10208 RVA: 0x000D7379 File Offset: 0x000D5579
			internal LinqEnumerator(JSONNode aNode)
			{
				this.m_Node = aNode;
				if (this.m_Node != null)
				{
					this.m_Enumerator = this.m_Node.GetEnumerator();
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x060027E1 RID: 10209 RVA: 0x000D73A7 File Offset: 0x000D55A7
			public KeyValuePair<string, JSONNode> Current
			{
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x060027E2 RID: 10210 RVA: 0x000D73B4 File Offset: 0x000D55B4
			object IEnumerator.Current
			{
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x060027E3 RID: 10211 RVA: 0x000D73C6 File Offset: 0x000D55C6
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x060027E4 RID: 10212 RVA: 0x000D73D3 File Offset: 0x000D55D3
			public void Dispose()
			{
				this.m_Node = null;
				this.m_Enumerator = default(JSONNode.Enumerator);
			}

			// Token: 0x060027E5 RID: 10213 RVA: 0x000D73E8 File Offset: 0x000D55E8
			public IEnumerator<KeyValuePair<string, JSONNode>> GetEnumerator()
			{
				return new JSONNode.LinqEnumerator(this.m_Node);
			}

			// Token: 0x060027E6 RID: 10214 RVA: 0x000D73F5 File Offset: 0x000D55F5
			public void Reset()
			{
				if (this.m_Node != null)
				{
					this.m_Enumerator = this.m_Node.GetEnumerator();
				}
			}

			// Token: 0x060027E7 RID: 10215 RVA: 0x000D7416 File Offset: 0x000D5616
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new JSONNode.LinqEnumerator(this.m_Node);
			}

			// Token: 0x04002C0D RID: 11277
			private JSONNode m_Node;

			// Token: 0x04002C0E RID: 11278
			private JSONNode.Enumerator m_Enumerator;
		}

		// Token: 0x0200068D RID: 1677
		[CompilerGenerated]
		private sealed class <get_Children>d__42 : IEnumerable<JSONNode>, IEnumerable, IEnumerator<JSONNode>, IEnumerator, IDisposable
		{
			// Token: 0x060027E8 RID: 10216 RVA: 0x000D7423 File Offset: 0x000D5623
			[DebuggerHidden]
			public <get_Children>d__42(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060027E9 RID: 10217 RVA: 0x000D743D File Offset: 0x000D563D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060027EA RID: 10218 RVA: 0x000D7440 File Offset: 0x000D5640
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x060027EB RID: 10219 RVA: 0x000D7461 File Offset: 0x000D5661
			JSONNode IEnumerator<JSONNode>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060027EC RID: 10220 RVA: 0x000D7469 File Offset: 0x000D5669
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x060027ED RID: 10221 RVA: 0x000D7470 File Offset: 0x000D5670
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060027EE RID: 10222 RVA: 0x000D7478 File Offset: 0x000D5678
			[DebuggerHidden]
			IEnumerator<JSONNode> IEnumerable<JSONNode>.GetEnumerator()
			{
				JSONNode.<get_Children>d__42 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new JSONNode.<get_Children>d__42(0);
				}
				return result;
			}

			// Token: 0x060027EF RID: 10223 RVA: 0x000D74AF File Offset: 0x000D56AF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SimpleJSON.JSONNode>.GetEnumerator();
			}

			// Token: 0x04002C0F RID: 11279
			private int <>1__state;

			// Token: 0x04002C10 RID: 11280
			private JSONNode <>2__current;

			// Token: 0x04002C11 RID: 11281
			private int <>l__initialThreadId;
		}

		// Token: 0x0200068E RID: 1678
		[CompilerGenerated]
		private sealed class <get_DeepChildren>d__44 : IEnumerable<JSONNode>, IEnumerable, IEnumerator<JSONNode>, IEnumerator, IDisposable
		{
			// Token: 0x060027F0 RID: 10224 RVA: 0x000D74B7 File Offset: 0x000D56B7
			[DebuggerHidden]
			public <get_DeepChildren>d__44(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060027F1 RID: 10225 RVA: 0x000D74D4 File Offset: 0x000D56D4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060027F2 RID: 10226 RVA: 0x000D752C File Offset: 0x000D572C
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					JSONNode jsonnode = this;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = jsonnode.Children.GetEnumerator();
						this.<>1__state = -3;
						goto IL_A7;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_8D:
					if (enumerator2.MoveNext())
					{
						JSONNode jsonnode2 = enumerator2.Current;
						this.<>2__current = jsonnode2;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_A7:
					if (enumerator.MoveNext())
					{
						JSONNode jsonnode3 = enumerator.Current;
						enumerator2 = jsonnode3.DeepChildren.GetEnumerator();
						this.<>1__state = -4;
						goto IL_8D;
					}
					this.<>m__Finally1();
					enumerator = null;
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060027F3 RID: 10227 RVA: 0x000D7618 File Offset: 0x000D5818
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x060027F4 RID: 10228 RVA: 0x000D7634 File Offset: 0x000D5834
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x060027F5 RID: 10229 RVA: 0x000D7651 File Offset: 0x000D5851
			JSONNode IEnumerator<JSONNode>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060027F6 RID: 10230 RVA: 0x000D7659 File Offset: 0x000D5859
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x060027F7 RID: 10231 RVA: 0x000D7660 File Offset: 0x000D5860
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060027F8 RID: 10232 RVA: 0x000D7668 File Offset: 0x000D5868
			[DebuggerHidden]
			IEnumerator<JSONNode> IEnumerable<JSONNode>.GetEnumerator()
			{
				JSONNode.<get_DeepChildren>d__44 <get_DeepChildren>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_DeepChildren>d__ = this;
				}
				else
				{
					<get_DeepChildren>d__ = new JSONNode.<get_DeepChildren>d__44(0);
					<get_DeepChildren>d__.<>4__this = this;
				}
				return <get_DeepChildren>d__;
			}

			// Token: 0x060027F9 RID: 10233 RVA: 0x000D76AB File Offset: 0x000D58AB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SimpleJSON.JSONNode>.GetEnumerator();
			}

			// Token: 0x04002C12 RID: 11282
			private int <>1__state;

			// Token: 0x04002C13 RID: 11283
			private JSONNode <>2__current;

			// Token: 0x04002C14 RID: 11284
			private int <>l__initialThreadId;

			// Token: 0x04002C15 RID: 11285
			public JSONNode <>4__this;

			// Token: 0x04002C16 RID: 11286
			private IEnumerator<JSONNode> <>7__wrap1;

			// Token: 0x04002C17 RID: 11287
			private IEnumerator<JSONNode> <>7__wrap2;
		}
	}
}

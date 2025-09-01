using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000017 RID: 23
	public class ParameterDictionary : IEnumerable<KeyValuePair<byte, object>>, IEnumerable
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x0000871F File Offset: 0x0000691F
		public ParameterDictionary()
		{
			this.paramDict = new NonAllocDictionary<byte, object>(29U);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008741 File Offset: 0x00006941
		public ParameterDictionary(int capacity)
		{
			this.paramDict = new NonAllocDictionary<byte, object>((uint)capacity);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008764 File Offset: 0x00006964
		public static implicit operator NonAllocDictionary<byte, object>(ParameterDictionary value)
		{
			return value.paramDict;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000877C File Offset: 0x0000697C
		IEnumerator<KeyValuePair<byte, object>> IEnumerable<KeyValuePair<byte, object>>.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<byte, object>>)this.paramDict).GetEnumerator();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000879C File Offset: 0x0000699C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<byte, object>>)this.paramDict).GetEnumerator();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000087BC File Offset: 0x000069BC
		public NonAllocDictionary<byte, object>.PairIterator GetEnumerator()
		{
			return this.paramDict.GetEnumerator();
		}

		// Token: 0x17000032 RID: 50
		public object this[byte key]
		{
			get
			{
				object obj = this.paramDict[key];
				StructWrapper<object> structWrapper = obj as StructWrapper<object>;
				bool flag = structWrapper == null;
				object result;
				if (flag)
				{
					result = obj;
				}
				else
				{
					result = structWrapper;
				}
				return result;
			}
			set
			{
				this.paramDict[key] = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00008824 File Offset: 0x00006A24
		public int Count
		{
			get
			{
				return this.paramDict.Count;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008841 File Offset: 0x00006A41
		public void Clear()
		{
			this.wrapperPools.Clear();
			this.paramDict.Clear();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000885C File Offset: 0x00006A5C
		public void Add(byte code, string value)
		{
			bool flag = this.paramDict.ContainsKey(code);
			if (flag)
			{
				Debug.LogWarning(code.ToString() + " already exists as key in ParameterDictionary");
			}
			this.paramDict[code] = value;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000088A4 File Offset: 0x00006AA4
		public void Add(byte code, Hashtable value)
		{
			bool flag = this.paramDict.ContainsKey(code);
			if (flag)
			{
				Debug.LogWarning(code.ToString() + " already exists as key in ParameterDictionary");
			}
			this.paramDict[code] = value;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000088EC File Offset: 0x00006AEC
		public void Add(byte code, byte value)
		{
			bool flag = this.paramDict.ContainsKey(code);
			if (flag)
			{
				Debug.LogError(code.ToString() + " already exists as key in ParameterDictionary");
			}
			StructWrapper<byte> value2 = StructWrapperPools.mappedByteWrappers[(int)value];
			this.paramDict[code] = value2;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000893C File Offset: 0x00006B3C
		public void Add(byte code, bool value)
		{
			bool flag = this.paramDict.ContainsKey(code);
			if (flag)
			{
				Debug.LogError(code.ToString() + " already exists as key in ParameterDictionary");
			}
			StructWrapper<bool> value2 = StructWrapperPools.mappedBoolWrappers[value ? 1 : 0];
			this.paramDict[code] = value2;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008990 File Offset: 0x00006B90
		public void Add(byte code, short value)
		{
			bool flag = this.paramDict.ContainsKey(code);
			if (flag)
			{
				Debug.LogWarning(code.ToString() + " already exists as key in ParameterDictionary");
			}
			this.paramDict[code] = value;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000089DC File Offset: 0x00006BDC
		public void Add(byte code, int value)
		{
			bool flag = this.paramDict.ContainsKey(code);
			if (flag)
			{
				Debug.LogWarning(code.ToString() + " already exists as key in ParameterDictionary");
			}
			this.paramDict[code] = value;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008A28 File Offset: 0x00006C28
		public void Add(byte code, long value)
		{
			bool flag = this.paramDict.ContainsKey(code);
			if (flag)
			{
				Debug.LogWarning(code.ToString() + " already exists as key in ParameterDictionary");
			}
			this.paramDict[code] = value;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008A74 File Offset: 0x00006C74
		public void Add(byte code, object value)
		{
			bool flag = this.paramDict.ContainsKey(code);
			if (flag)
			{
				Debug.LogWarning(code.ToString() + " already exists as key in ParameterDictionary");
			}
			this.paramDict[code] = value;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00008ABC File Offset: 0x00006CBC
		public T Unwrap<T>(byte key)
		{
			object obj = this.paramDict[key];
			return obj.Unwrap<T>();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public T Get<T>(byte key)
		{
			object obj = this.paramDict[key];
			return obj.Get<T>();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008B0C File Offset: 0x00006D0C
		public bool ContainsKey(byte key)
		{
			return this.paramDict.ContainsKey(key);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008B2C File Offset: 0x00006D2C
		public object TryGetObject(byte key)
		{
			object obj;
			bool flag = this.paramDict.TryGetValue(key, out obj);
			object result;
			if (flag)
			{
				result = obj;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008B58 File Offset: 0x00006D58
		public bool TryGetValue(byte key, out object value)
		{
			return this.paramDict.TryGetValue(key, out value);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008B78 File Offset: 0x00006D78
		public bool TryGetValue<T>(byte key, out T value) where T : struct
		{
			object obj;
			bool flag = this.paramDict.TryGetValue(key, out obj);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				value = default(T);
				result = false;
			}
			else
			{
				StructWrapper<T> structWrapper = obj as StructWrapper<T>;
				bool flag3 = structWrapper != null;
				if (flag3)
				{
					value = structWrapper.value;
				}
				else
				{
					StructWrapper<object> structWrapper2 = obj as StructWrapper<object>;
					bool flag4 = structWrapper2 != null;
					if (flag4)
					{
						value = (T)((object)structWrapper2.value);
					}
					else
					{
						value = (T)((object)obj);
					}
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008C0C File Offset: 0x00006E0C
		public string ToStringFull(bool includeTypes = true)
		{
			string result;
			if (includeTypes)
			{
				result = string.Format("(ParameterDictionary){0}", SupportClass.DictionaryToString(this.paramDict, includeTypes));
			}
			else
			{
				result = SupportClass.DictionaryToString(this.paramDict, includeTypes);
			}
			return result;
		}

		// Token: 0x040000BF RID: 191
		public readonly NonAllocDictionary<byte, object> paramDict;

		// Token: 0x040000C0 RID: 192
		public readonly StructWrapperPools wrapperPools = new StructWrapperPools();
	}
}

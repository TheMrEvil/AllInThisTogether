using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000005 RID: 5
	public class Hashtable : Dictionary<object, object>
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002F5C File Offset: 0x0000115C
		public static object GetBoxedByte(byte value)
		{
			return Hashtable.boxedByte[(int)value];
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002F78 File Offset: 0x00001178
		static Hashtable()
		{
			int num = 256;
			Hashtable.boxedByte = new object[num];
			for (int i = 0; i < num; i++)
			{
				Hashtable.boxedByte[i] = (byte)i;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002FB5 File Offset: 0x000011B5
		public Hashtable()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002FBF File Offset: 0x000011BF
		public Hashtable(int x) : base(x)
		{
		}

		// Token: 0x1700000B RID: 11
		public new object this[object key]
		{
			get
			{
				object result = null;
				base.TryGetValue(key, out result);
				return result;
			}
			set
			{
				base[key] = value;
			}
		}

		// Token: 0x1700000C RID: 12
		public object this[byte key]
		{
			get
			{
				object result = null;
				base.TryGetValue(Hashtable.boxedByte[(int)key], out result);
				return result;
			}
			set
			{
				base[Hashtable.boxedByte[(int)key]] = value;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000302F File Offset: 0x0000122F
		public void Add(byte k, object v)
		{
			base.Add(Hashtable.boxedByte[(int)k], v);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003041 File Offset: 0x00001241
		public void Remove(byte k)
		{
			base.Remove(Hashtable.boxedByte[(int)k]);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003054 File Offset: 0x00001254
		public bool ContainsKey(byte key)
		{
			return base.ContainsKey(Hashtable.boxedByte[(int)key]);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003074 File Offset: 0x00001274
		public new DictionaryEntryEnumerator GetEnumerator()
		{
			return new DictionaryEntryEnumerator(base.GetEnumerator());
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003094 File Offset: 0x00001294
		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (object obj in base.Keys)
			{
				bool flag = obj == null || this[obj] == null;
				if (flag)
				{
					List<string> list2 = list;
					string str = (obj != null) ? obj.ToString() : null;
					string str2 = "=";
					object obj2 = this[obj];
					list2.Add(str + str2 + ((obj2 != null) ? obj2.ToString() : null));
				}
				else
				{
					List<string> list3 = list;
					string[] array = new string[8];
					array[0] = "(";
					int num = 1;
					Type type = obj.GetType();
					array[num] = ((type != null) ? type.ToString() : null);
					array[2] = ")";
					array[3] = ((obj != null) ? obj.ToString() : null);
					array[4] = "=(";
					int num2 = 5;
					Type type2 = this[obj].GetType();
					array[num2] = ((type2 != null) ? type2.ToString() : null);
					array[6] = ")";
					int num3 = 7;
					object obj3 = this[obj];
					array[num3] = ((obj3 != null) ? obj3.ToString() : null);
					list3.Add(string.Concat(array));
				}
			}
			return string.Join(", ", list.ToArray());
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000031DC File Offset: 0x000013DC
		public object Clone()
		{
			return new Dictionary<object, object>(this);
		}

		// Token: 0x04000013 RID: 19
		internal static readonly object[] boxedByte;
	}
}

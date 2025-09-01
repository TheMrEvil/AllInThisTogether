using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000005 RID: 5
	public static class Extensions
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002A7C File Offset: 0x00000C7C
		public static void Merge(this IDictionary target, IDictionary addHash)
		{
			if (addHash == null || target.Equals(addHash))
			{
				return;
			}
			foreach (object key in addHash.Keys)
			{
				target[key] = addHash[key];
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public static void MergeStringKeys(this IDictionary target, IDictionary addHash)
		{
			if (addHash == null || target.Equals(addHash))
			{
				return;
			}
			foreach (object obj in addHash.Keys)
			{
				if (obj is string)
				{
					target[obj] = addHash[obj];
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B54 File Offset: 0x00000D54
		public static string ToStringFull(this IDictionary origin)
		{
			return SupportClass.DictionaryToString(origin, false);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B60 File Offset: 0x00000D60
		public static string ToStringFull<T>(this List<T> data)
		{
			if (data == null)
			{
				return "null";
			}
			string[] array = new string[data.Count];
			for (int i = 0; i < data.Count; i++)
			{
				object obj = data[i];
				array[i] = ((obj != null) ? obj.ToString() : "null");
			}
			return string.Join(", ", array);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static string ToStringFull(this object[] data)
		{
			if (data == null)
			{
				return "null";
			}
			string[] array = new string[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				object obj = data[i];
				array[i] = ((obj != null) ? obj.ToString() : "null");
			}
			return string.Join(", ", array);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C10 File Offset: 0x00000E10
		public static ExitGames.Client.Photon.Hashtable StripToStringKeys(this IDictionary original)
		{
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			if (original != null)
			{
				foreach (object obj in original.Keys)
				{
					if (obj is string)
					{
						hashtable[obj] = original[obj];
					}
				}
			}
			return hashtable;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C80 File Offset: 0x00000E80
		public static ExitGames.Client.Photon.Hashtable StripToStringKeys(this ExitGames.Client.Photon.Hashtable original)
		{
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			if (original != null)
			{
				foreach (DictionaryEntry dictionaryEntry in original)
				{
					if (dictionaryEntry.Key is string)
					{
						hashtable[dictionaryEntry.Key] = original[dictionaryEntry.Key];
					}
				}
			}
			return hashtable;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002CFC File Offset: 0x00000EFC
		public static void StripKeysWithNullValues(this IDictionary original)
		{
			List<object> obj = Extensions.keysWithNullValue;
			lock (obj)
			{
				Extensions.keysWithNullValue.Clear();
				foreach (object obj2 in original)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					if (dictionaryEntry.Value == null)
					{
						Extensions.keysWithNullValue.Add(dictionaryEntry.Key);
					}
				}
				for (int i = 0; i < Extensions.keysWithNullValue.Count; i++)
				{
					object key = Extensions.keysWithNullValue[i];
					original.Remove(key);
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public static void StripKeysWithNullValues(this ExitGames.Client.Photon.Hashtable original)
		{
			List<object> obj = Extensions.keysWithNullValue;
			lock (obj)
			{
				Extensions.keysWithNullValue.Clear();
				foreach (DictionaryEntry dictionaryEntry in original)
				{
					if (dictionaryEntry.Value == null)
					{
						Extensions.keysWithNullValue.Add(dictionaryEntry.Key);
					}
				}
				for (int i = 0; i < Extensions.keysWithNullValue.Count; i++)
				{
					object key = Extensions.keysWithNullValue[i];
					original.Remove(key);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002E8C File Offset: 0x0000108C
		public static bool Contains(this int[] target, int nr)
		{
			if (target == null)
			{
				return false;
			}
			for (int i = 0; i < target.Length; i++)
			{
				if (target[i] == nr)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002EB5 File Offset: 0x000010B5
		// Note: this type is marked as 'beforefieldinit'.
		static Extensions()
		{
		}

		// Token: 0x04000025 RID: 37
		private static readonly List<object> keysWithNullValue = new List<object>();
	}
}

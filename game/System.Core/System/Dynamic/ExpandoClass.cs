using System;
using System.Collections.Generic;

namespace System.Dynamic
{
	// Token: 0x0200030C RID: 780
	internal class ExpandoClass
	{
		// Token: 0x06001773 RID: 6003 RVA: 0x0004E1ED File Offset: 0x0004C3ED
		internal ExpandoClass()
		{
			this._hashCode = 6551;
			this._keys = Array.Empty<string>();
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0004E20B File Offset: 0x0004C40B
		internal ExpandoClass(string[] keys, int hashCode)
		{
			this._hashCode = hashCode;
			this._keys = keys;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0004E224 File Offset: 0x0004C424
		internal ExpandoClass FindNewClass(string newKey)
		{
			int hashCode = this._hashCode ^ newKey.GetHashCode();
			ExpandoClass result;
			lock (this)
			{
				List<WeakReference> transitionList = this.GetTransitionList(hashCode);
				for (int i = 0; i < transitionList.Count; i++)
				{
					ExpandoClass expandoClass = transitionList[i].Target as ExpandoClass;
					if (expandoClass == null)
					{
						transitionList.RemoveAt(i);
						i--;
					}
					else if (string.Equals(expandoClass._keys[expandoClass._keys.Length - 1], newKey, StringComparison.Ordinal))
					{
						return expandoClass;
					}
				}
				string[] array = new string[this._keys.Length + 1];
				Array.Copy(this._keys, 0, array, 0, this._keys.Length);
				array[this._keys.Length] = newKey;
				ExpandoClass expandoClass2 = new ExpandoClass(array, hashCode);
				transitionList.Add(new WeakReference(expandoClass2));
				result = expandoClass2;
			}
			return result;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0004E320 File Offset: 0x0004C520
		private List<WeakReference> GetTransitionList(int hashCode)
		{
			if (this._transitions == null)
			{
				this._transitions = new Dictionary<int, List<WeakReference>>();
			}
			List<WeakReference> result;
			if (!this._transitions.TryGetValue(hashCode, out result))
			{
				result = (this._transitions[hashCode] = new List<WeakReference>());
			}
			return result;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0004E364 File Offset: 0x0004C564
		internal int GetValueIndex(string name, bool caseInsensitive, ExpandoObject obj)
		{
			if (caseInsensitive)
			{
				return this.GetValueIndexCaseInsensitive(name, obj);
			}
			return this.GetValueIndexCaseSensitive(name);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0004E37C File Offset: 0x0004C57C
		internal int GetValueIndexCaseSensitive(string name)
		{
			for (int i = 0; i < this._keys.Length; i++)
			{
				if (string.Equals(this._keys[i], name, StringComparison.Ordinal))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0004E3B0 File Offset: 0x0004C5B0
		private int GetValueIndexCaseInsensitive(string name, ExpandoObject obj)
		{
			int num = -1;
			object lockObject = obj.LockObject;
			lock (lockObject)
			{
				for (int i = this._keys.Length - 1; i >= 0; i--)
				{
					if (string.Equals(this._keys[i], name, StringComparison.OrdinalIgnoreCase) && !obj.IsDeletedMember(i))
					{
						if (num != -1)
						{
							return -2;
						}
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0004E430 File Offset: 0x0004C630
		internal string[] Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0004E438 File Offset: 0x0004C638
		// Note: this type is marked as 'beforefieldinit'.
		static ExpandoClass()
		{
		}

		// Token: 0x04000B96 RID: 2966
		private readonly string[] _keys;

		// Token: 0x04000B97 RID: 2967
		private readonly int _hashCode;

		// Token: 0x04000B98 RID: 2968
		private Dictionary<int, List<WeakReference>> _transitions;

		// Token: 0x04000B99 RID: 2969
		private const int EmptyHashCode = 6551;

		// Token: 0x04000B9A RID: 2970
		internal static readonly ExpandoClass Empty = new ExpandoClass();
	}
}

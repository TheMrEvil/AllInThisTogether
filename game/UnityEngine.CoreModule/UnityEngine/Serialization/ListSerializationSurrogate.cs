using System;
using System.Collections;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x020002D0 RID: 720
	internal class ListSerializationSurrogate : ISerializationSurrogate
	{
		// Token: 0x06001DD9 RID: 7641 RVA: 0x00030958 File Offset: 0x0002EB58
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			IList list = (IList)obj;
			info.AddValue("_size", list.Count);
			info.AddValue("_items", ListSerializationSurrogate.ArrayFromGenericList(list));
			info.AddValue("_version", 0);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000309A0 File Offset: 0x0002EBA0
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			IList list = (IList)Activator.CreateInstance(obj.GetType());
			int @int = info.GetInt32("_size");
			bool flag = @int == 0;
			object result;
			if (flag)
			{
				result = list;
			}
			else
			{
				IEnumerator enumerator = ((IEnumerable)info.GetValue("_items", typeof(IEnumerable))).GetEnumerator();
				for (int i = 0; i < @int; i++)
				{
					bool flag2 = !enumerator.MoveNext();
					if (flag2)
					{
						throw new InvalidOperationException();
					}
					list.Add(enumerator.Current);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00030A3C File Offset: 0x0002EC3C
		private static Array ArrayFromGenericList(IList list)
		{
			Array array = Array.CreateInstance(list.GetType().GetGenericArguments()[0], list.Count);
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00002072 File Offset: 0x00000272
		public ListSerializationSurrogate()
		{
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00030A71 File Offset: 0x0002EC71
		// Note: this type is marked as 'beforefieldinit'.
		static ListSerializationSurrogate()
		{
		}

		// Token: 0x040009B6 RID: 2486
		public static readonly ISerializationSurrogate Default = new ListSerializationSurrogate();
	}
}

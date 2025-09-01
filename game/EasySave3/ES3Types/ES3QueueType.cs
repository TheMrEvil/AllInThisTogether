using System;
using System.Collections;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000023 RID: 35
	[Preserve]
	public class ES3QueueType : ES3CollectionType
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00008394 File Offset: 0x00006594
		public ES3QueueType(Type type) : base(type)
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000083A0 File Offset: 0x000065A0
		public override void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			IEnumerable enumerable = (ICollection)obj;
			if (this.elementType == null)
			{
				throw new ArgumentNullException("ES3Type argument cannot be null.");
			}
			int num = 0;
			foreach (object value in enumerable)
			{
				writer.StartWriteCollectionItem(num);
				writer.Write(value, this.elementType, memberReferenceMode);
				writer.EndWriteCollectionItem(num);
				num++;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008424 File Offset: 0x00006624
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008430 File Offset: 0x00006630
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			int num = 0;
			Queue<T> queue = (Queue<T>)obj;
			foreach (T t in queue)
			{
				num++;
				if (!reader.StartReadCollectionItem())
				{
					break;
				}
				reader.ReadInto<T>(t, this.elementType);
				if (reader.EndReadCollectionItem())
				{
					break;
				}
				if (num == queue.Count)
				{
					throw new IndexOutOfRangeException("The collection we are loading is longer than the collection provided as a parameter.");
				}
			}
			if (num != queue.Count)
			{
				throw new IndexOutOfRangeException("The collection we are loading is shorter than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000084EC File Offset: 0x000066EC
		public override object Read(ES3Reader reader)
		{
			IList list = (IList)ES3Reflection.CreateInstance(ES3Reflection.MakeGenericType(typeof(List<>), this.elementType.type));
			if (reader.StartReadCollection())
			{
				return null;
			}
			while (reader.StartReadCollectionItem())
			{
				list.Add(reader.Read<object>(this.elementType));
				if (reader.EndReadCollectionItem())
				{
					break;
				}
			}
			reader.EndReadCollection();
			return ES3Reflection.CreateInstance(this.type, new object[]
			{
				list
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008568 File Offset: 0x00006768
		public override void ReadInto(ES3Reader reader, object obj)
		{
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			int num = 0;
			ICollection collection = (ICollection)obj;
			foreach (object obj2 in collection)
			{
				num++;
				if (!reader.StartReadCollectionItem())
				{
					break;
				}
				reader.ReadInto<object>(obj2, this.elementType);
				if (reader.EndReadCollectionItem())
				{
					break;
				}
				if (num == collection.Count)
				{
					throw new IndexOutOfRangeException("The collection we are loading is longer than the collection provided as a parameter.");
				}
			}
			if (num != collection.Count)
			{
				throw new IndexOutOfRangeException("The collection we are loading is shorter than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}
	}
}

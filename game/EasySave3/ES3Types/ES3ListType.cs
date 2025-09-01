using System;
using System.Collections;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000021 RID: 33
	[Preserve]
	public class ES3ListType : ES3CollectionType
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00007FFE File Offset: 0x000061FE
		public ES3ListType(Type type) : base(type)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008007 File Offset: 0x00006207
		public ES3ListType(Type type, ES3Type elementType) : base(type, elementType)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008014 File Offset: 0x00006214
		public override void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			if (obj == null)
			{
				writer.WriteNull();
				return;
			}
			IEnumerable enumerable = (IList)obj;
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

		// Token: 0x0600021F RID: 543 RVA: 0x000080A0 File Offset: 0x000062A0
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000080A9 File Offset: 0x000062A9
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			this.ReadICollectionInto(reader, (ICollection)obj, this.elementType);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000080C0 File Offset: 0x000062C0
		public override object Read(ES3Reader reader)
		{
			IList list = (IList)ES3Reflection.CreateInstance(this.type);
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
			return list;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008114 File Offset: 0x00006314
		public override void ReadInto(ES3Reader reader, object obj)
		{
			IList list = (IList)obj;
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			int num = 0;
			foreach (object obj2 in list)
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
				if (num == list.Count)
				{
					throw new IndexOutOfRangeException("The collection we are loading is longer than the collection provided as a parameter.");
				}
			}
			if (num != list.Count)
			{
				throw new IndexOutOfRangeException("The collection we are loading is shorter than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}
	}
}

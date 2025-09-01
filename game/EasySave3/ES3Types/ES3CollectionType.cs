using System;
using System.Collections;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200001D RID: 29
	[Preserve]
	public abstract class ES3CollectionType : ES3Type
	{
		// Token: 0x060001FD RID: 509
		public abstract object Read(ES3Reader reader);

		// Token: 0x060001FE RID: 510
		public abstract void ReadInto(ES3Reader reader, object obj);

		// Token: 0x060001FF RID: 511
		public abstract void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode);

		// Token: 0x06000200 RID: 512 RVA: 0x00007880 File Offset: 0x00005A80
		public ES3CollectionType(Type type) : base(type)
		{
			this.elementType = ES3TypeMgr.GetOrCreateES3Type(ES3Reflection.GetElementTypes(type)[0], false);
			this.isCollection = true;
			if (this.elementType == null)
			{
				this.isUnsupported = true;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000078B3 File Offset: 0x00005AB3
		public ES3CollectionType(Type type, ES3Type elementType) : base(type)
		{
			this.elementType = elementType;
			this.isCollection = true;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000078CA File Offset: 0x00005ACA
		[Preserve]
		public override void Write(object obj, ES3Writer writer)
		{
			this.Write(obj, writer, ES3.ReferenceMode.ByRefAndValue);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000078D5 File Offset: 0x00005AD5
		protected virtual bool ReadICollection<T>(ES3Reader reader, ICollection<T> collection, ES3Type elementType)
		{
			if (reader.StartReadCollection())
			{
				return false;
			}
			while (reader.StartReadCollectionItem())
			{
				collection.Add(reader.Read<T>(elementType));
				if (reader.EndReadCollectionItem())
				{
					break;
				}
			}
			reader.EndReadCollection();
			return true;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007905 File Offset: 0x00005B05
		protected virtual void ReadICollectionInto<T>(ES3Reader reader, ICollection<T> collection, ES3Type elementType)
		{
			this.ReadICollectionInto<T>(reader, collection, elementType);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007910 File Offset: 0x00005B10
		[Preserve]
		protected virtual void ReadICollectionInto(ES3Reader reader, ICollection collection, ES3Type elementType)
		{
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			int num = 0;
			foreach (object obj in collection)
			{
				num++;
				if (!reader.StartReadCollectionItem())
				{
					break;
				}
				reader.ReadInto<object>(obj, elementType);
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

		// Token: 0x0400005C RID: 92
		public ES3Type elementType;
	}
}

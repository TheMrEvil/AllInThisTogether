using System;
using System.Collections.Generic;
using ES3Internal;

namespace ES3Types
{
	// Token: 0x0200001B RID: 27
	public class ES33DArrayType : ES3CollectionType
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x000073A5 File Offset: 0x000055A5
		public ES33DArrayType(Type type) : base(type)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000073B0 File Offset: 0x000055B0
		public override void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			Array array = (Array)obj;
			if (this.elementType == null)
			{
				throw new ArgumentNullException("ES3Type argument cannot be null.");
			}
			for (int i = 0; i < array.GetLength(0); i++)
			{
				writer.StartWriteCollectionItem(i);
				writer.StartWriteCollection();
				for (int j = 0; j < array.GetLength(1); j++)
				{
					writer.StartWriteCollectionItem(j);
					writer.StartWriteCollection();
					for (int k = 0; k < array.GetLength(2); k++)
					{
						writer.StartWriteCollectionItem(k);
						writer.Write(array.GetValue(i, j, k), this.elementType, memberReferenceMode);
						writer.EndWriteCollectionItem(k);
					}
					writer.EndWriteCollection();
					writer.EndWriteCollectionItem(j);
				}
				writer.EndWriteCollection();
				writer.EndWriteCollectionItem(i);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000746B File Offset: 0x0000566B
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007474 File Offset: 0x00005674
		public override object Read(ES3Reader reader)
		{
			if (reader.StartReadCollection())
			{
				return null;
			}
			List<object> list = new List<object>();
			int num = 0;
			int num2 = 0;
			while (reader.StartReadCollectionItem())
			{
				reader.StartReadCollection();
				num++;
				while (reader.StartReadCollectionItem())
				{
					this.ReadICollection<object>(reader, list, this.elementType);
					num2++;
					if (reader.EndReadCollectionItem())
					{
						break;
					}
				}
				reader.EndReadCollection();
				if (reader.EndReadCollectionItem())
				{
					break;
				}
			}
			reader.EndReadCollection();
			num2 /= num;
			int num3 = list.Count / num2 / num;
			Array array = ES3Reflection.ArrayCreateInstance(this.elementType.type, new int[]
			{
				num,
				num2,
				num3
			});
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < num3; k++)
					{
						array.SetValue(list[i * (num2 * num3) + j * num3 + k], i, j, k);
					}
				}
			}
			return array;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007565 File Offset: 0x00005765
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			this.ReadInto(reader, obj);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007570 File Offset: 0x00005770
		public override void ReadInto(ES3Reader reader, object obj)
		{
			Array array = (Array)obj;
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			bool flag = false;
			for (int i = 0; i < array.GetLength(0); i++)
			{
				bool flag2 = false;
				if (!reader.StartReadCollectionItem())
				{
					throw new IndexOutOfRangeException("The collection we are loading is smaller than the collection provided as a parameter.");
				}
				reader.StartReadCollection();
				for (int j = 0; j < array.GetLength(1); j++)
				{
					bool flag3 = false;
					if (!reader.StartReadCollectionItem())
					{
						throw new IndexOutOfRangeException("The collection we are loading is smaller than the collection provided as a parameter.");
					}
					reader.StartReadCollection();
					for (int k = 0; k < array.GetLength(2); k++)
					{
						if (!reader.StartReadCollectionItem())
						{
							throw new IndexOutOfRangeException("The collection we are loading is smaller than the collection provided as a parameter.");
						}
						reader.ReadInto<object>(array.GetValue(i, j, k), this.elementType);
						flag3 = reader.EndReadCollectionItem();
					}
					if (!flag3)
					{
						throw new IndexOutOfRangeException("The collection we are loading is larger than the collection provided as a parameter.");
					}
					reader.EndReadCollection();
					flag2 = reader.EndReadCollectionItem();
				}
				if (!flag2)
				{
					throw new IndexOutOfRangeException("The collection we are loading is larger than the collection provided as a parameter.");
				}
				reader.EndReadCollection();
				flag = reader.EndReadCollectionItem();
			}
			if (!flag)
			{
				throw new IndexOutOfRangeException("The collection we are loading is larger than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}
	}
}

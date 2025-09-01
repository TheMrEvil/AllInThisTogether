using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000024 RID: 36
	[Preserve]
	public class ES3StackType : ES3CollectionType
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00008624 File Offset: 0x00006824
		public ES3StackType(Type type) : base(type)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008630 File Offset: 0x00006830
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

		// Token: 0x06000233 RID: 563 RVA: 0x000086B4 File Offset: 0x000068B4
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000086C0 File Offset: 0x000068C0
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			int num = 0;
			Stack<T> stack = (Stack<T>)obj;
			foreach (T t in stack)
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
				if (num == stack.Count)
				{
					throw new IndexOutOfRangeException("The collection we are loading is longer than the collection provided as a parameter.");
				}
			}
			if (num != stack.Count)
			{
				throw new IndexOutOfRangeException("The collection we are loading is shorter than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000877C File Offset: 0x0000697C
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
			ES3Reflection.GetMethods(list.GetType(), "Reverse").FirstOrDefault((MethodInfo t) => !t.IsStatic).Invoke(list, new object[0]);
			return ES3Reflection.CreateInstance(this.type, new object[]
			{
				list
			});
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008838 File Offset: 0x00006A38
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

		// Token: 0x02000105 RID: 261
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005A1 RID: 1441 RVA: 0x00020557 File Offset: 0x0001E757
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x00020563 File Offset: 0x0001E763
			public <>c()
			{
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x0002056B File Offset: 0x0001E76B
			internal bool <Read>b__4_0(MethodInfo t)
			{
				return !t.IsStatic;
			}

			// Token: 0x040001F7 RID: 503
			public static readonly ES3StackType.<>c <>9 = new ES3StackType.<>c();

			// Token: 0x040001F8 RID: 504
			public static Func<MethodInfo, bool> <>9__4_0;
		}
	}
}

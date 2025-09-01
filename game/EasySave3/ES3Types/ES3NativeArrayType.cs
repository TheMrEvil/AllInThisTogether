using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ES3Internal;
using Unity.Collections;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000022 RID: 34
	[Preserve]
	public class ES3NativeArrayType : ES3CollectionType
	{
		// Token: 0x06000223 RID: 547 RVA: 0x000081D0 File Offset: 0x000063D0
		public ES3NativeArrayType(Type type) : base(type)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000081D9 File Offset: 0x000063D9
		public ES3NativeArrayType(Type type, ES3Type elementType) : base(type, elementType)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000081E4 File Offset: 0x000063E4
		public override void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			if (this.elementType == null)
			{
				throw new ArgumentNullException("ES3Type argument cannot be null.");
			}
			IEnumerable enumerable = (IEnumerable)obj;
			int num = 0;
			foreach (object value in enumerable)
			{
				writer.StartWriteCollectionItem(num);
				writer.Write(value, this.elementType, memberReferenceMode);
				writer.EndWriteCollectionItem(num);
				num++;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008268 File Offset: 0x00006468
		public override object Read(ES3Reader reader)
		{
			Array array = this.ReadAsArray(reader);
			return ES3Reflection.CreateInstance(this.type, new object[]
			{
				array,
				Allocator.Persistent
			});
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000829B File Offset: 0x0000649B
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000082A4 File Offset: 0x000064A4
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			this.ReadInto(reader, obj);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000082B0 File Offset: 0x000064B0
		public override void ReadInto(ES3Reader reader, object obj)
		{
			Array array = this.ReadAsArray(reader);
			ES3Reflection.GetMethods(this.type, "CopyFrom").First((MethodInfo m) => ES3Reflection.TypeIsArray(m.GetParameters()[0].GetType())).Invoke(obj, new object[]
			{
				array
			});
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000830C File Offset: 0x0000650C
		private Array ReadAsArray(ES3Reader reader)
		{
			List<object> list = new List<object>();
			if (!this.ReadICollection<object>(reader, list, this.elementType))
			{
				return null;
			}
			Array array = ES3Reflection.ArrayCreateInstance(this.elementType.type, list.Count);
			int num = 0;
			foreach (object value in list)
			{
				array.SetValue(value, num);
				num++;
			}
			return array;
		}

		// Token: 0x02000104 RID: 260
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600059E RID: 1438 RVA: 0x0002052F File Offset: 0x0001E72F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x0002053B File Offset: 0x0001E73B
			public <>c()
			{
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00020543 File Offset: 0x0001E743
			internal bool <ReadInto>b__6_0(MethodInfo m)
			{
				return ES3Reflection.TypeIsArray(m.GetParameters()[0].GetType());
			}

			// Token: 0x040001F5 RID: 501
			public static readonly ES3NativeArrayType.<>c <>9 = new ES3NativeArrayType.<>c();

			// Token: 0x040001F6 RID: 502
			public static Func<MethodInfo, bool> <>9__6_0;
		}
	}
}

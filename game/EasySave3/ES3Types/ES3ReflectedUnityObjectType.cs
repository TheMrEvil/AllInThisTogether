using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200005D RID: 93
	[Preserve]
	internal class ES3ReflectedUnityObjectType : ES3UnityObjectType
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000AB2A File Offset: 0x00008D2A
		public ES3ReflectedUnityObjectType(Type type) : base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000AB41 File Offset: 0x00008D41
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000AB4C File Offset: 0x00008D4C
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			object obj = ES3Reflection.CreateInstance(this.type);
			base.ReadProperties(reader, obj);
			return obj;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000AB6F File Offset: 0x00008D6F
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			base.ReadProperties(reader, obj);
		}
	}
}

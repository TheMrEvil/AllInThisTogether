using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200005A RID: 90
	[Preserve]
	internal class ES3ReflectedObjectType : ES3ObjectType
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000A877 File Offset: 0x00008A77
		public ES3ReflectedObjectType(Type type) : base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A88E File Offset: 0x00008A8E
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A898 File Offset: 0x00008A98
		protected override object ReadObject<T>(ES3Reader reader)
		{
			object obj = ES3Reflection.CreateInstance(this.type);
			base.ReadProperties(reader, obj);
			return obj;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A8BB File Offset: 0x00008ABB
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			base.ReadProperties(reader, obj);
		}
	}
}

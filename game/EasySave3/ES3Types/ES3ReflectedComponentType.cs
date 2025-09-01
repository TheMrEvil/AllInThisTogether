using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000059 RID: 89
	[Preserve]
	internal class ES3ReflectedComponentType : ES3ComponentType
	{
		// Token: 0x060002CB RID: 715 RVA: 0x0000A84B File Offset: 0x00008A4B
		public ES3ReflectedComponentType(Type type) : base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A862 File Offset: 0x00008A62
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A86C File Offset: 0x00008A6C
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			base.ReadProperties(reader, obj);
		}
	}
}

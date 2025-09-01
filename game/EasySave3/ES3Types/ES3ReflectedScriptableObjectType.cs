using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200005B RID: 91
	[Preserve]
	internal class ES3ReflectedScriptableObjectType : ES3ScriptableObjectType
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000A8C6 File Offset: 0x00008AC6
		public ES3ReflectedScriptableObjectType(Type type) : base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A8DD File Offset: 0x00008ADD
		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A8E7 File Offset: 0x00008AE7
		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			base.ReadProperties(reader, obj);
		}
	}
}

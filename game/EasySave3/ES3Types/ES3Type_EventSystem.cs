using System;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000063 RID: 99
	[Preserve]
	public class ES3Type_EventSystem : ES3ComponentType
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000BD54 File Offset: 0x00009F54
		public ES3Type_EventSystem() : base(typeof(EventSystem))
		{
			ES3Type_EventSystem.Instance = this;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000BD6C File Offset: 0x00009F6C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000BD70 File Offset: 0x00009F70
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				reader.Skip();
			}
		}

		// Token: 0x040000B2 RID: 178
		public static ES3Type Instance;
	}
}

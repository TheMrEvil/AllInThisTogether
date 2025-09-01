using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000068 RID: 104
	[Preserve]
	[ES3Properties(new string[]
	{
		"sharedMesh"
	})]
	public class ES3Type_MeshFilter : ES3ComponentType
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000C669 File Offset: 0x0000A869
		public ES3Type_MeshFilter() : base(typeof(MeshFilter))
		{
			ES3Type_MeshFilter.Instance = this;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000C684 File Offset: 0x0000A884
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			MeshFilter meshFilter = (MeshFilter)obj;
			writer.WritePropertyByRef("sharedMesh", meshFilter.sharedMesh);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			MeshFilter meshFilter = (MeshFilter)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "sharedMesh")
					{
						meshFilter.sharedMesh = reader.Read<Mesh>(ES3Type_Mesh.Instance);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x040000B7 RID: 183
		public static ES3Type Instance;
	}
}

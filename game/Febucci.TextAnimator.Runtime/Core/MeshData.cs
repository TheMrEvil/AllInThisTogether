using System;
using System.Text;
using UnityEngine;

namespace Febucci.UI.Core
{
	// Token: 0x0200003D RID: 61
	public struct MeshData : IEquatable<MeshData>
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00006B64 File Offset: 0x00004D64
		public bool Equals(MeshData other)
		{
			for (int i = 0; i < this.positions.Length; i++)
			{
				if (this.positions[i] != other.positions[i])
				{
					return false;
				}
			}
			for (int j = 0; j < this.colors.Length; j++)
			{
				if (!this.colors[j].Equals(other.colors[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.positions.Length; i++)
			{
				stringBuilder.Append(this.positions[i].ToString());
				stringBuilder.Append(" ");
				stringBuilder.Append(this.colors[i].ToString());
				stringBuilder.Append(" - ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000EE RID: 238
		public Vector3[] positions;

		// Token: 0x040000EF RID: 239
		public Color32[] colors;
	}
}

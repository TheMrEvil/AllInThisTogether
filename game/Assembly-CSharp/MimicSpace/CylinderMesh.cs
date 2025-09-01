using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MimicSpace
{
	// Token: 0x020003DA RID: 986
	public class CylinderMesh : MonoBehaviour
	{
		// Token: 0x0600201B RID: 8219 RVA: 0x000BE934 File Offset: 0x000BCB34
		private void Start()
		{
			this.myLine = base.GetComponent<LineRenderer>();
			this.myFilter = base.GetComponent<MeshFilter>();
			this.mesh = new Mesh();
			this.myFilter.sharedMesh = this.mesh;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000BE96A File Offset: 0x000BCB6A
		private IEnumerator WaitIt()
		{
			yield return new WaitForSeconds(1f);
			this.BuildMesh();
			yield break;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000BE97C File Offset: 0x000BCB7C
		private void BuildMesh()
		{
			this.mesh.Clear();
			this.angle = 360f / (float)this.verticeCount;
			List<Vector3> list = new List<Vector3>();
			List<int> list2 = new List<int>();
			for (int i = 0; i < this.myLine.positionCount - 1; i++)
			{
				Vector3 normalized = (this.myLine.GetPosition(i + 1) - this.myLine.GetPosition(i)).normalized;
				Vector3 vector = Vector3.Cross(normalized, Vector3.up) * (this.myLine.widthCurve.Evaluate((float)i / (float)this.myLine.positionCount) / 2f);
				Quaternion rotation = Quaternion.AngleAxis(this.angle, normalized);
				for (int j = 0; j < this.verticeCount; j++)
				{
					list.Add(base.transform.InverseTransformPoint(vector + this.myLine.GetPosition(i)));
					vector = rotation * vector;
				}
			}
			list.Add(base.transform.InverseTransformPoint(this.myLine.GetPosition(this.myLine.positionCount - 1)));
			for (int k = 0; k < this.myLine.positionCount - 2; k++)
			{
				for (int l = 0; l < this.verticeCount; l++)
				{
					list2.Add(k * this.verticeCount + l);
					list2.Add(k * this.verticeCount + 1 + l);
					list2.Add((k + 1) * this.verticeCount + l);
					list2.Add((k + 1) * this.verticeCount + l);
					list2.Add(k * this.verticeCount + 1 + l);
					list2.Add((k + 1) * this.verticeCount + l + 1);
				}
			}
			int num = (this.myLine.positionCount - 2) * this.verticeCount;
			for (int m = 1; m < this.verticeCount; m++)
			{
				list2.Add(num + m - 1);
				list2.Add(num + m);
				list2.Add(list.Count - 1);
			}
			this.mesh.vertices = list.ToArray();
			this.mesh.triangles = list2.ToArray();
			this.mesh.RecalculateNormals();
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000BEBE0 File Offset: 0x000BCDE0
		private void Update()
		{
			this.BuildMesh();
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000BEBE8 File Offset: 0x000BCDE8
		public CylinderMesh()
		{
		}

		// Token: 0x04002068 RID: 8296
		private LineRenderer myLine;

		// Token: 0x04002069 RID: 8297
		public int verticeCount;

		// Token: 0x0400206A RID: 8298
		public float angle;

		// Token: 0x0400206B RID: 8299
		private MeshFilter myFilter;

		// Token: 0x0400206C RID: 8300
		private Mesh mesh;

		// Token: 0x020006A6 RID: 1702
		[CompilerGenerated]
		private sealed class <WaitIt>d__6 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600283F RID: 10303 RVA: 0x000D84C1 File Offset: 0x000D66C1
			[DebuggerHidden]
			public <WaitIt>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06002840 RID: 10304 RVA: 0x000D84D0 File Offset: 0x000D66D0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002841 RID: 10305 RVA: 0x000D84D4 File Offset: 0x000D66D4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				CylinderMesh cylinderMesh = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = new WaitForSeconds(1f);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				cylinderMesh.BuildMesh();
				return false;
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x06002842 RID: 10306 RVA: 0x000D8526 File Offset: 0x000D6726
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002843 RID: 10307 RVA: 0x000D852E File Offset: 0x000D672E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x06002844 RID: 10308 RVA: 0x000D8535 File Offset: 0x000D6735
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C7D RID: 11389
			private int <>1__state;

			// Token: 0x04002C7E RID: 11390
			private object <>2__current;

			// Token: 0x04002C7F RID: 11391
			public CylinderMesh <>4__this;
		}
	}
}

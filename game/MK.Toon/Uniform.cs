using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x0200003B RID: 59
	public class Uniform
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003B68 File Offset: 0x00001D68
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003B70 File Offset: 0x00001D70
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003B78 File Offset: 0x00001D78
		public Uniform(string name)
		{
			this._name = name;
			this._id = Shader.PropertyToID(name);
		}

		// Token: 0x0400018D RID: 397
		protected string _name;

		// Token: 0x0400018E RID: 398
		protected int _id;
	}
}

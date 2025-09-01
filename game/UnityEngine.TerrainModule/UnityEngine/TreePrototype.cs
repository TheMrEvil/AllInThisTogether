using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TreePrototype
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000024BC File Offset: 0x000006BC
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000024D4 File Offset: 0x000006D4
		public GameObject prefab
		{
			get
			{
				return this.m_Prefab;
			}
			set
			{
				this.m_Prefab = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000024E0 File Offset: 0x000006E0
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000024F8 File Offset: 0x000006F8
		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002504 File Offset: 0x00000704
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000251C File Offset: 0x0000071C
		public int navMeshLod
		{
			get
			{
				return this.m_NavMeshLod;
			}
			set
			{
				this.m_NavMeshLod = value;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002526 File Offset: 0x00000726
		public TreePrototype()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002530 File Offset: 0x00000730
		public TreePrototype(TreePrototype other)
		{
			this.prefab = other.prefab;
			this.bendFactor = other.bendFactor;
			this.navMeshLod = other.navMeshLod;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002564 File Offset: 0x00000764
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreePrototype);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002584 File Offset: 0x00000784
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000259C File Offset: 0x0000079C
		private bool Equals(TreePrototype other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = other == this;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = base.GetType() != other.GetType();
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = this.prefab == other.prefab && this.bendFactor == other.bendFactor && this.navMeshLod == other.navMeshLod;
						result = flag4;
					}
				}
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002615 File Offset: 0x00000815
		internal bool Validate(out string errorMessage)
		{
			return TreePrototype.ValidateTreePrototype(this, out errorMessage);
		}

		// Token: 0x06000099 RID: 153
		[FreeFunction("TerrainDataScriptingInterface::ValidateTreePrototype")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ValidateTreePrototype([NotNull("ArgumentNullException")] TreePrototype prototype, out string errorMessage);

		// Token: 0x0400001B RID: 27
		internal GameObject m_Prefab;

		// Token: 0x0400001C RID: 28
		internal float m_BendFactor;

		// Token: 0x0400001D RID: 29
		internal int m_NavMeshLod;
	}
}

using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000067 RID: 103
	internal static class MeshUtilities
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00010B04 File Offset: 0x0000ED04
		static MeshUtilities()
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00010B58 File Offset: 0x0000ED58
		internal static Mesh GetColliderMesh(Collider collider)
		{
			Type type = collider.GetType();
			if (type == typeof(MeshCollider))
			{
				return ((MeshCollider)collider).sharedMesh;
			}
			return MeshUtilities.GetPrimitive(MeshUtilities.s_ColliderPrimitives[type]);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00010B9C File Offset: 0x0000ED9C
		internal static Mesh GetPrimitive(PrimitiveType primitiveType)
		{
			Mesh builtinMesh;
			if (!MeshUtilities.s_Primitives.TryGetValue(primitiveType, out builtinMesh))
			{
				builtinMesh = MeshUtilities.GetBuiltinMesh(primitiveType);
				MeshUtilities.s_Primitives.Add(primitiveType, builtinMesh);
			}
			return builtinMesh;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00010BCC File Offset: 0x0000EDCC
		private static Mesh GetBuiltinMesh(PrimitiveType primitiveType)
		{
			GameObject gameObject = GameObject.CreatePrimitive(primitiveType);
			Mesh sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
			RuntimeUtilities.Destroy(gameObject);
			return sharedMesh;
		}

		// Token: 0x0400021D RID: 541
		private static Dictionary<PrimitiveType, Mesh> s_Primitives = new Dictionary<PrimitiveType, Mesh>();

		// Token: 0x0400021E RID: 542
		private static Dictionary<Type, PrimitiveType> s_ColliderPrimitives = new Dictionary<Type, PrimitiveType>
		{
			{
				typeof(BoxCollider),
				PrimitiveType.Cube
			},
			{
				typeof(SphereCollider),
				PrimitiveType.Sphere
			},
			{
				typeof(CapsuleCollider),
				PrimitiveType.Capsule
			}
		};
	}
}

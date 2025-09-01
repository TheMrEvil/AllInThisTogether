using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Physics/RaycastHit.h")]
	[NativeHeader("PhysicsScriptingClasses.h")]
	[NativeHeader("Runtime/Interfaces/IRaycast.h")]
	[UsedByNativeCode]
	public struct RaycastHit
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00004A88 File Offset: 0x00002C88
		public Collider collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00004AAC File Offset: 0x00002CAC
		public int colliderInstanceID
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00004AC4 File Offset: 0x00002CC4
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00004ADC File Offset: 0x00002CDC
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00004AE8 File Offset: 0x00002CE8
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00004B00 File Offset: 0x00002D00
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00004B0C File Offset: 0x00002D0C
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00004B56 File Offset: 0x00002D56
		public Vector3 barycentricCoordinate
		{
			get
			{
				return new Vector3(1f - (this.m_UV.y + this.m_UV.x), this.m_UV.x, this.m_UV.y);
			}
			set
			{
				this.m_UV = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00004B68 File Offset: 0x00002D68
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00004B80 File Offset: 0x00002D80
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00004B8C File Offset: 0x00002D8C
		public int triangleIndex
		{
			get
			{
				return (int)this.m_FaceID;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00004BA4 File Offset: 0x00002DA4
		[FreeFunction]
		private static Vector2 CalculateRaycastTexCoord(Collider collider, Vector2 uv, Vector3 pos, uint face, int textcoord)
		{
			Vector2 result;
			RaycastHit.CalculateRaycastTexCoord_Injected(collider, ref uv, ref pos, face, textcoord, out result);
			return result;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public Vector2 textureCoord
		{
			get
			{
				return RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 0);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public Vector2 textureCoord2
		{
			get
			{
				return RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00004C24 File Offset: 0x00002E24
		public Transform transform
		{
			get
			{
				Rigidbody rigidbody = this.rigidbody;
				bool flag = rigidbody != null;
				Transform result;
				if (flag)
				{
					result = rigidbody.transform;
				}
				else
				{
					bool flag2 = this.collider != null;
					if (flag2)
					{
						result = this.collider.transform;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00004C70 File Offset: 0x00002E70
		public Rigidbody rigidbody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedRigidbody : null;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public ArticulationBody articulationBody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedArticulationBody : null;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public Vector2 lightmapCoord
		{
			get
			{
				Vector2 vector = RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				bool flag = this.collider.GetComponent<Renderer>() != null;
				if (flag)
				{
					Vector4 lightmapScaleOffset = this.collider.GetComponent<Renderer>().lightmapScaleOffset;
					vector.x = vector.x * lightmapScaleOffset.x + lightmapScaleOffset.z;
					vector.y = vector.y * lightmapScaleOffset.y + lightmapScaleOffset.w;
				}
				return vector;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00004D60 File Offset: 0x00002F60
		[Obsolete("Use textureCoord2 instead. (UnityUpgradable) -> textureCoord2")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Vector2 textureCoord1
		{
			get
			{
				return this.textureCoord2;
			}
		}

		// Token: 0x06000235 RID: 565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateRaycastTexCoord_Injected(Collider collider, ref Vector2 uv, ref Vector3 pos, uint face, int textcoord, out Vector2 ret);

		// Token: 0x040000AE RID: 174
		[NativeName("point")]
		internal Vector3 m_Point;

		// Token: 0x040000AF RID: 175
		[NativeName("normal")]
		internal Vector3 m_Normal;

		// Token: 0x040000B0 RID: 176
		[NativeName("faceID")]
		internal uint m_FaceID;

		// Token: 0x040000B1 RID: 177
		[NativeName("distance")]
		internal float m_Distance;

		// Token: 0x040000B2 RID: 178
		[NativeName("uv")]
		internal Vector2 m_UV;

		// Token: 0x040000B3 RID: 179
		[NativeName("collider")]
		internal int m_Collider;
	}
}

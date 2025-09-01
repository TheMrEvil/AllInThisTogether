using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Unity.AI.Navigation
{
	// Token: 0x02000007 RID: 7
	[ExecuteAlways]
	[DefaultExecutionOrder(-102)]
	[AddComponentMenu("Navigation/NavMeshSurface", 30)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.0/manual/NavMeshSurface.html")]
	public class NavMeshSurface : MonoBehaviour
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002584 File Offset: 0x00000784
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000258C File Offset: 0x0000078C
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002595 File Offset: 0x00000795
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000259D File Offset: 0x0000079D
		public CollectObjects collectObjects
		{
			get
			{
				return this.m_CollectObjects;
			}
			set
			{
				this.m_CollectObjects = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000025A6 File Offset: 0x000007A6
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000025AE File Offset: 0x000007AE
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000025B7 File Offset: 0x000007B7
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000025BF File Offset: 0x000007BF
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000025C8 File Offset: 0x000007C8
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000025D0 File Offset: 0x000007D0
		public LayerMask layerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000025D9 File Offset: 0x000007D9
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000025E1 File Offset: 0x000007E1
		public NavMeshCollectGeometry useGeometry
		{
			get
			{
				return this.m_UseGeometry;
			}
			set
			{
				this.m_UseGeometry = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000025EA File Offset: 0x000007EA
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000025F2 File Offset: 0x000007F2
		public int defaultArea
		{
			get
			{
				return this.m_DefaultArea;
			}
			set
			{
				this.m_DefaultArea = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000025FB File Offset: 0x000007FB
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002603 File Offset: 0x00000803
		public bool ignoreNavMeshAgent
		{
			get
			{
				return this.m_IgnoreNavMeshAgent;
			}
			set
			{
				this.m_IgnoreNavMeshAgent = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000260C File Offset: 0x0000080C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002614 File Offset: 0x00000814
		public bool ignoreNavMeshObstacle
		{
			get
			{
				return this.m_IgnoreNavMeshObstacle;
			}
			set
			{
				this.m_IgnoreNavMeshObstacle = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000261D File Offset: 0x0000081D
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002625 File Offset: 0x00000825
		public bool overrideTileSize
		{
			get
			{
				return this.m_OverrideTileSize;
			}
			set
			{
				this.m_OverrideTileSize = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000262E File Offset: 0x0000082E
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002636 File Offset: 0x00000836
		public int tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000263F File Offset: 0x0000083F
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002647 File Offset: 0x00000847
		public bool overrideVoxelSize
		{
			get
			{
				return this.m_OverrideVoxelSize;
			}
			set
			{
				this.m_OverrideVoxelSize = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002650 File Offset: 0x00000850
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002658 File Offset: 0x00000858
		public float voxelSize
		{
			get
			{
				return this.m_VoxelSize;
			}
			set
			{
				this.m_VoxelSize = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002661 File Offset: 0x00000861
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002669 File Offset: 0x00000869
		public float minRegionArea
		{
			get
			{
				return this.m_MinRegionArea;
			}
			set
			{
				this.m_MinRegionArea = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002672 File Offset: 0x00000872
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000267A File Offset: 0x0000087A
		public bool buildHeightMesh
		{
			get
			{
				return this.m_BuildHeightMesh;
			}
			set
			{
				this.m_BuildHeightMesh = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002683 File Offset: 0x00000883
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000268B File Offset: 0x0000088B
		public NavMeshData navMeshData
		{
			get
			{
				return this.m_NavMeshData;
			}
			set
			{
				this.m_NavMeshData = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002694 File Offset: 0x00000894
		internal NavMeshDataInstance navMeshDataInstance
		{
			get
			{
				return this.m_NavMeshDataInstance;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000269C File Offset: 0x0000089C
		public static List<NavMeshSurface> activeSurfaces
		{
			get
			{
				return NavMeshSurface.s_NavMeshSurfaces;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000026A4 File Offset: 0x000008A4
		private Bounds GetInflatedBounds()
		{
			NavMeshBuildSettings settingsByID = NavMesh.GetSettingsByID(this.m_AgentTypeID);
			float num = (settingsByID.agentTypeID != -1) ? settingsByID.agentRadius : 0f;
			Bounds result = new Bounds(this.center, this.size);
			result.Expand(new Vector3(num, 0f, num));
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026FD File Offset: 0x000008FD
		private void OnEnable()
		{
			NavMeshSurface.Register(this);
			this.AddData();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000270B File Offset: 0x0000090B
		private void OnDisable()
		{
			this.RemoveData();
			NavMeshSurface.Unregister(this);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000271C File Offset: 0x0000091C
		public void AddData()
		{
			if (this.m_NavMeshDataInstance.valid)
			{
				return;
			}
			if (this.m_NavMeshData != null)
			{
				this.m_NavMeshDataInstance = NavMesh.AddNavMeshData(this.m_NavMeshData, base.transform.position, base.transform.rotation);
				this.m_NavMeshDataInstance.owner = this;
			}
			this.m_LastPosition = base.transform.position;
			this.m_LastRotation = base.transform.rotation;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000279A File Offset: 0x0000099A
		public void RemoveData()
		{
			this.m_NavMeshDataInstance.Remove();
			this.m_NavMeshDataInstance = default(NavMeshDataInstance);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000027B4 File Offset: 0x000009B4
		public NavMeshBuildSettings GetBuildSettings()
		{
			NavMeshBuildSettings settingsByID = NavMesh.GetSettingsByID(this.m_AgentTypeID);
			if (settingsByID.agentTypeID == -1)
			{
				Debug.LogWarning("No build settings for agent type ID " + this.agentTypeID.ToString(), this);
				settingsByID.agentTypeID = this.m_AgentTypeID;
			}
			if (this.overrideTileSize)
			{
				settingsByID.overrideTileSize = true;
				settingsByID.tileSize = this.tileSize;
			}
			if (this.overrideVoxelSize)
			{
				settingsByID.overrideVoxelSize = true;
				settingsByID.voxelSize = this.voxelSize;
			}
			settingsByID.minRegionArea = this.minRegionArea;
			return settingsByID;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000284C File Offset: 0x00000A4C
		public void BuildNavMesh()
		{
			List<NavMeshBuildSource> sources = this.CollectSources();
			Bounds localBounds = new Bounds(this.m_Center, NavMeshSurface.Abs(this.m_Size));
			if (this.m_CollectObjects != CollectObjects.Volume)
			{
				localBounds = this.CalculateWorldBounds(sources);
			}
			NavMeshData navMeshData = NavMeshBuilder.BuildNavMeshData(this.GetBuildSettings(), sources, localBounds, base.transform.position, base.transform.rotation);
			if (navMeshData != null)
			{
				navMeshData.name = base.gameObject.name;
				this.RemoveData();
				this.m_NavMeshData = navMeshData;
				if (base.isActiveAndEnabled)
				{
					this.AddData();
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000028E4 File Offset: 0x00000AE4
		public AsyncOperation UpdateNavMesh(NavMeshData data)
		{
			List<NavMeshBuildSource> sources = this.CollectSources();
			Bounds localBounds = new Bounds(this.m_Center, NavMeshSurface.Abs(this.m_Size));
			if (this.m_CollectObjects != CollectObjects.Volume)
			{
				localBounds = this.CalculateWorldBounds(sources);
			}
			return NavMeshBuilder.UpdateNavMeshDataAsync(data, this.GetBuildSettings(), sources, localBounds);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002930 File Offset: 0x00000B30
		private static void Register(NavMeshSurface surface)
		{
			if (NavMeshSurface.s_NavMeshSurfaces.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshSurface.UpdateActive));
			}
			if (!NavMeshSurface.s_NavMeshSurfaces.Contains(surface))
			{
				NavMeshSurface.s_NavMeshSurfaces.Add(surface);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002981 File Offset: 0x00000B81
		private static void Unregister(NavMeshSurface surface)
		{
			NavMeshSurface.s_NavMeshSurfaces.Remove(surface);
			if (NavMeshSurface.s_NavMeshSurfaces.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshSurface.UpdateActive));
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000029BC File Offset: 0x00000BBC
		private static void UpdateActive()
		{
			for (int i = 0; i < NavMeshSurface.s_NavMeshSurfaces.Count; i++)
			{
				NavMeshSurface.s_NavMeshSurfaces[i].UpdateDataIfTransformChanged();
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000029F0 File Offset: 0x00000BF0
		private void AppendModifierVolumes(ref List<NavMeshBuildSource> sources)
		{
			List<NavMeshModifierVolume> list;
			if (this.m_CollectObjects == CollectObjects.Children)
			{
				list = new List<NavMeshModifierVolume>(base.GetComponentsInChildren<NavMeshModifierVolume>());
				list.RemoveAll((NavMeshModifierVolume x) => !x.isActiveAndEnabled);
			}
			else
			{
				list = NavMeshModifierVolume.activeModifiers;
			}
			foreach (NavMeshModifierVolume navMeshModifierVolume in list)
			{
				if ((this.m_LayerMask & 1 << navMeshModifierVolume.gameObject.layer) != 0 && navMeshModifierVolume.AffectsAgentType(this.m_AgentTypeID))
				{
					Vector3 pos = navMeshModifierVolume.transform.TransformPoint(navMeshModifierVolume.center);
					Vector3 lossyScale = navMeshModifierVolume.transform.lossyScale;
					Vector3 size = new Vector3(navMeshModifierVolume.size.x * Mathf.Abs(lossyScale.x), navMeshModifierVolume.size.y * Mathf.Abs(lossyScale.y), navMeshModifierVolume.size.z * Mathf.Abs(lossyScale.z));
					NavMeshBuildSource item = default(NavMeshBuildSource);
					item.shape = NavMeshBuildSourceShape.ModifierBox;
					item.transform = Matrix4x4.TRS(pos, navMeshModifierVolume.transform.rotation, Vector3.one);
					item.size = size;
					item.area = navMeshModifierVolume.area;
					sources.Add(item);
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B78 File Offset: 0x00000D78
		private List<NavMeshBuildSource> CollectSources()
		{
			List<NavMeshBuildSource> list = new List<NavMeshBuildSource>();
			List<NavMeshBuildMarkup> list2 = new List<NavMeshBuildMarkup>();
			List<NavMeshModifier> list3;
			if (this.m_CollectObjects == CollectObjects.Children)
			{
				list3 = new List<NavMeshModifier>(base.GetComponentsInChildren<NavMeshModifier>());
				list3.RemoveAll((NavMeshModifier x) => !x.isActiveAndEnabled);
			}
			else
			{
				list3 = NavMeshModifier.activeModifiers;
			}
			foreach (NavMeshModifier navMeshModifier in list3)
			{
				if ((this.m_LayerMask & 1 << navMeshModifier.gameObject.layer) != 0 && navMeshModifier.AffectsAgentType(this.m_AgentTypeID))
				{
					list2.Add(new NavMeshBuildMarkup
					{
						root = navMeshModifier.transform,
						overrideArea = navMeshModifier.overrideArea,
						area = navMeshModifier.area,
						ignoreFromBuild = navMeshModifier.ignoreFromBuild
					});
				}
			}
			if (this.m_CollectObjects == CollectObjects.All)
			{
				NavMeshBuilder.CollectSources(null, this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, list2, list);
			}
			else if (this.m_CollectObjects == CollectObjects.Children)
			{
				NavMeshBuilder.CollectSources(base.transform, this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, list2, list);
			}
			else if (this.m_CollectObjects == CollectObjects.Volume)
			{
				NavMeshBuilder.CollectSources(NavMeshSurface.GetWorldBounds(Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one), this.GetInflatedBounds()), this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, list2, list);
			}
			if (this.m_IgnoreNavMeshAgent)
			{
				list.RemoveAll((NavMeshBuildSource x) => x.component != null && x.component.gameObject.GetComponent<NavMeshAgent>() != null);
			}
			if (this.m_IgnoreNavMeshObstacle)
			{
				list.RemoveAll((NavMeshBuildSource x) => x.component != null && x.component.gameObject.GetComponent<NavMeshObstacle>() != null);
			}
			this.AppendModifierVolumes(ref list);
			return list;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D9C File Offset: 0x00000F9C
		private static Vector3 Abs(Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DC4 File Offset: 0x00000FC4
		private static Bounds GetWorldBounds(Matrix4x4 mat, Bounds bounds)
		{
			Vector3 a = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.right));
			Vector3 a2 = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.up));
			Vector3 a3 = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.forward));
			Vector3 center = mat.MultiplyPoint(bounds.center);
			Vector3 size = a * bounds.size.x + a2 * bounds.size.y + a3 * bounds.size.z;
			return new Bounds(center, size);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E5C File Offset: 0x0000105C
		private Bounds CalculateWorldBounds(List<NavMeshBuildSource> sources)
		{
			Matrix4x4 inverse = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one).inverse;
			Bounds result = default(Bounds);
			foreach (NavMeshBuildSource navMeshBuildSource in sources)
			{
				switch (navMeshBuildSource.shape)
				{
				case NavMeshBuildSourceShape.Mesh:
				{
					Mesh mesh = navMeshBuildSource.sourceObject as Mesh;
					result.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, mesh.bounds));
					break;
				}
				case NavMeshBuildSourceShape.Terrain:
				{
					TerrainData terrainData = navMeshBuildSource.sourceObject as TerrainData;
					result.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, new Bounds(0.5f * terrainData.size, terrainData.size)));
					break;
				}
				case NavMeshBuildSourceShape.Box:
				case NavMeshBuildSourceShape.Sphere:
				case NavMeshBuildSourceShape.Capsule:
				case NavMeshBuildSourceShape.ModifierBox:
					result.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, new Bounds(Vector3.zero, navMeshBuildSource.size)));
					break;
				}
			}
			result.Expand(0.1f);
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FB4 File Offset: 0x000011B4
		private bool HasTransformChanged()
		{
			return this.m_LastPosition != base.transform.position || this.m_LastRotation != base.transform.rotation;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002FEB File Offset: 0x000011EB
		private void UpdateDataIfTransformChanged()
		{
			if (this.HasTransformChanged())
			{
				this.RemoveData();
				this.AddData();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003004 File Offset: 0x00001204
		public NavMeshSurface()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003091 File Offset: 0x00001291
		// Note: this type is marked as 'beforefieldinit'.
		static NavMeshSurface()
		{
		}

		// Token: 0x0400001F RID: 31
		[SerializeField]
		private int m_AgentTypeID;

		// Token: 0x04000020 RID: 32
		[SerializeField]
		private CollectObjects m_CollectObjects;

		// Token: 0x04000021 RID: 33
		[SerializeField]
		private Vector3 m_Size = new Vector3(10f, 10f, 10f);

		// Token: 0x04000022 RID: 34
		[SerializeField]
		private Vector3 m_Center = new Vector3(0f, 2f, 0f);

		// Token: 0x04000023 RID: 35
		[SerializeField]
		private LayerMask m_LayerMask = -1;

		// Token: 0x04000024 RID: 36
		[SerializeField]
		private NavMeshCollectGeometry m_UseGeometry;

		// Token: 0x04000025 RID: 37
		[SerializeField]
		private int m_DefaultArea;

		// Token: 0x04000026 RID: 38
		[SerializeField]
		private bool m_IgnoreNavMeshAgent = true;

		// Token: 0x04000027 RID: 39
		[SerializeField]
		private bool m_IgnoreNavMeshObstacle = true;

		// Token: 0x04000028 RID: 40
		[SerializeField]
		private bool m_OverrideTileSize;

		// Token: 0x04000029 RID: 41
		[SerializeField]
		private int m_TileSize = 256;

		// Token: 0x0400002A RID: 42
		[SerializeField]
		private bool m_OverrideVoxelSize;

		// Token: 0x0400002B RID: 43
		[SerializeField]
		private float m_VoxelSize;

		// Token: 0x0400002C RID: 44
		[SerializeField]
		private float m_MinRegionArea = 2f;

		// Token: 0x0400002D RID: 45
		[FormerlySerializedAs("m_BakedNavMeshData")]
		[SerializeField]
		private NavMeshData m_NavMeshData;

		// Token: 0x0400002E RID: 46
		[SerializeField]
		private bool m_BuildHeightMesh;

		// Token: 0x0400002F RID: 47
		private NavMeshDataInstance m_NavMeshDataInstance;

		// Token: 0x04000030 RID: 48
		private Vector3 m_LastPosition = Vector3.zero;

		// Token: 0x04000031 RID: 49
		private Quaternion m_LastRotation = Quaternion.identity;

		// Token: 0x04000032 RID: 50
		private static readonly List<NavMeshSurface> s_NavMeshSurfaces = new List<NavMeshSurface>();

		// Token: 0x02000008 RID: 8
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600006B RID: 107 RVA: 0x0000309D File Offset: 0x0000129D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600006C RID: 108 RVA: 0x000030A9 File Offset: 0x000012A9
			public <>c()
			{
			}

			// Token: 0x0600006D RID: 109 RVA: 0x000030B1 File Offset: 0x000012B1
			internal bool <AppendModifierVolumes>b__83_0(NavMeshModifierVolume x)
			{
				return !x.isActiveAndEnabled;
			}

			// Token: 0x0600006E RID: 110 RVA: 0x000030BC File Offset: 0x000012BC
			internal bool <CollectSources>b__84_0(NavMeshModifier x)
			{
				return !x.isActiveAndEnabled;
			}

			// Token: 0x0600006F RID: 111 RVA: 0x000030C7 File Offset: 0x000012C7
			internal bool <CollectSources>b__84_1(NavMeshBuildSource x)
			{
				return x.component != null && x.component.gameObject.GetComponent<NavMeshAgent>() != null;
			}

			// Token: 0x06000070 RID: 112 RVA: 0x000030F1 File Offset: 0x000012F1
			internal bool <CollectSources>b__84_2(NavMeshBuildSource x)
			{
				return x.component != null && x.component.gameObject.GetComponent<NavMeshObstacle>() != null;
			}

			// Token: 0x04000033 RID: 51
			public static readonly NavMeshSurface.<>c <>9 = new NavMeshSurface.<>c();

			// Token: 0x04000034 RID: 52
			public static Predicate<NavMeshModifierVolume> <>9__83_0;

			// Token: 0x04000035 RID: 53
			public static Predicate<NavMeshModifier> <>9__84_0;

			// Token: 0x04000036 RID: 54
			public static Predicate<NavMeshBuildSource> <>9__84_1;

			// Token: 0x04000037 RID: 55
			public static Predicate<NavMeshBuildSource> <>9__84_2;
		}
	}
}

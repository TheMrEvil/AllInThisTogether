using System;
using System.Collections.Generic;
using DamageNumbersPro.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DamageNumbersPro
{
	// Token: 0x0200000A RID: 10
	public abstract class DamageNumber : MonoBehaviour
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000288D File Offset: 0x00000A8D
		private void Start()
		{
			this.GetReferencesIfNecessary();
			if (this.enablePooling && this.disableOnSceneLoad)
			{
				SceneManager.sceneLoaded += this.OnSceneLoaded;
			}
			this.Restart();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028BC File Offset: 0x00000ABC
		private void Update()
		{
			if (this.performRestart)
			{
				this.Restart();
				this.performRestart = false;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028D3 File Offset: 0x00000AD3
		private void LateUpdate()
		{
			if (!this.performRestart)
			{
				this.UpdateScaleAnd3D(false);
			}
			this.OnLateUpdate();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028EC File Offset: 0x00000AEC
		public void UpdateDamageNumber(float delta, float time)
		{
			if (!base.isActiveAndEnabled)
			{
				this.startTime += delta;
				this.startLifeTime += delta;
				this.absorbStartTime += delta;
				this.destructionStartTime += delta;
				return;
			}
			if (DNPUpdater.vectorsNeedUpdate)
			{
				DNPUpdater.UpdateVectors(base.transform);
			}
			if (this.IsAlive(time))
			{
				this.HandleFadeIn(delta);
			}
			else
			{
				this.HandleFadeOut(delta);
			}
			this.OnUpdate(delta);
			if (this.enableLerp)
			{
				this.HandleLerp(delta);
			}
			if (this.enableVelocity)
			{
				this.HandleVelocity(delta);
			}
			if (this.enableFollowing)
			{
				this.HandleFollowing(delta);
			}
			if (this.enableRotateOverTime)
			{
				this.HandleRotateOverTime(delta, time);
				this.UpdateRotationZ();
			}
			if (this.enableCombination)
			{
				this.HandleCombination(delta, time);
			}
			if (this.enableDestruction)
			{
				this.HandleDestruction(time);
			}
			this.finalPosition = this.position;
			if (this.enableShaking)
			{
				this.finalPosition = this.ApplyShake(this.finalPosition, this.shakeSettings, time);
			}
			this.SetPosition(this.finalPosition);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A08 File Offset: 0x00000C08
		public DamageNumber Spawn()
		{
			DamageNumber damageNumber = null;
			int instanceID = base.GetInstanceID();
			if (this.enablePooling && this.PoolAvailable(instanceID))
			{
				using (HashSet<DamageNumber>.Enumerator enumerator = DamageNumber.pools[instanceID].GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						damageNumber = enumerator.Current;
					}
				}
				DamageNumber.pools[instanceID].Remove(damageNumber);
			}
			else
			{
				damageNumber = UnityEngine.Object.Instantiate<GameObject>(base.gameObject).GetComponent<DamageNumber>();
				if (this.enablePooling)
				{
					damageNumber.originalPrefab = this;
				}
			}
			damageNumber.gameObject.SetActive(true);
			damageNumber.OnPreSpawn();
			if (this.enablePooling)
			{
				damageNumber.SetPoolingID(instanceID);
				damageNumber.destroyAfterSpawning = false;
			}
			return damageNumber;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public DamageNumber Spawn(Vector3 newPosition)
		{
			DamageNumber damageNumber = this.Spawn();
			damageNumber.SetPosition(newPosition);
			return damageNumber;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002AE3 File Offset: 0x00000CE3
		public DamageNumber Spawn(Vector3 newPosition, float newNumber)
		{
			DamageNumber damageNumber = this.Spawn(newPosition);
			damageNumber.enableNumber = true;
			damageNumber.number = newNumber;
			return damageNumber;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AFA File Offset: 0x00000CFA
		public DamageNumber Spawn(Vector3 newPosition, float newNumber, Transform followedTransform)
		{
			DamageNumber damageNumber = this.Spawn(newPosition, newNumber);
			damageNumber.SetFollowedTarget(followedTransform);
			return damageNumber;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B0B File Offset: 0x00000D0B
		public DamageNumber Spawn(Vector3 newPosition, Transform followedTransform)
		{
			DamageNumber damageNumber = this.Spawn(newPosition);
			damageNumber.SetFollowedTarget(followedTransform);
			return damageNumber;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B1B File Offset: 0x00000D1B
		public DamageNumber Spawn(Vector3 newPosition, string newText)
		{
			DamageNumber damageNumber = this.Spawn(newPosition);
			damageNumber.enableNumber = false;
			damageNumber.enableLeftText = true;
			damageNumber.leftText = newText;
			damageNumber.enableNumber = false;
			return damageNumber;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B40 File Offset: 0x00000D40
		public DamageNumber Spawn(Vector3 newPosition, string newText, Transform followedTransform)
		{
			DamageNumber damageNumber = this.Spawn(newPosition, newText);
			damageNumber.SetFollowedTarget(followedTransform);
			return damageNumber;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B51 File Offset: 0x00000D51
		public DamageNumber SpawnGUI(RectTransform rectParent, Vector2 anchoredPosition)
		{
			DamageNumber damageNumber = this.Spawn();
			damageNumber.SetAnchoredPosition(rectParent, anchoredPosition);
			return damageNumber;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B61 File Offset: 0x00000D61
		public DamageNumber SpawnGUI(RectTransform rectParent, RectTransform rectPosition, Vector2 anchoredPosition)
		{
			DamageNumber damageNumber = this.Spawn();
			damageNumber.SetAnchoredPosition(rectParent, rectPosition, anchoredPosition);
			return damageNumber;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B72 File Offset: 0x00000D72
		public DamageNumber SpawnGUI(RectTransform rectParent, Vector2 anchoredPosition, float newNumber)
		{
			DamageNumber damageNumber = this.SpawnGUI(rectParent, anchoredPosition);
			damageNumber.enableNumber = true;
			damageNumber.number = newNumber;
			return damageNumber;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B8A File Offset: 0x00000D8A
		public DamageNumber SpawnGUI(RectTransform rectParent, RectTransform rectPosition, Vector2 anchoredPosition, float newNumber)
		{
			DamageNumber damageNumber = this.SpawnGUI(rectParent, rectPosition, anchoredPosition);
			damageNumber.enableNumber = true;
			damageNumber.number = newNumber;
			return damageNumber;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public DamageNumber SpawnGUI(RectTransform rectParent, Vector2 anchoredPosition, string newText)
		{
			DamageNumber damageNumber = this.SpawnGUI(rectParent, anchoredPosition);
			damageNumber.enableNumber = false;
			damageNumber.enableLeftText = true;
			damageNumber.leftText = newText;
			return damageNumber;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public DamageNumber SpawnGUI(RectTransform rectParent, RectTransform rectPosition, Vector2 anchoredPosition, string newText)
		{
			DamageNumber damageNumber = this.SpawnGUI(rectParent, rectPosition, anchoredPosition);
			damageNumber.enableNumber = false;
			damageNumber.enableLeftText = true;
			damageNumber.leftText = newText;
			return damageNumber;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public void SetFollowedTarget(Transform followedTransform)
		{
			this.enableFollowing = true;
			this.followedTarget = followedTransform;
			this.spamGroup += followedTransform.GetInstanceID().ToString();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C20 File Offset: 0x00000E20
		public void SetColor(Color newColor)
		{
			this.GetReferencesIfNecessary();
			TMP_Text[] textMeshs = this.GetTextMeshs();
			for (int i = 0; i < textMeshs.Length; i++)
			{
				textMeshs[i].color = newColor;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C54 File Offset: 0x00000E54
		public void SetGradientColor(VertexGradient newGradient)
		{
			this.GetReferencesIfNecessary();
			foreach (TMP_Text tmp_Text in this.GetTextMeshs())
			{
				tmp_Text.enableVertexGradient = true;
				tmp_Text.colorGradient = newGradient;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C8C File Offset: 0x00000E8C
		public void SetRandomColor(Color from, Color to)
		{
			this.SetColor(Color.Lerp(from, to, UnityEngine.Random.value));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public void SetRandomColor(Gradient gradient)
		{
			this.SetColor(gradient.Evaluate(UnityEngine.Random.value));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public void SetGradientColor(Color topLeft, Color topRight, Color bottomLeft, Color bottomRight)
		{
			this.SetGradientColor(new VertexGradient
			{
				topLeft = topLeft,
				topRight = topRight,
				bottomLeft = bottomLeft,
				bottomRight = bottomRight
			});
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public void SetFontMaterial(TMP_FontAsset font)
		{
			this.GetReferencesIfNecessary();
			TMP_Text[] textMeshs = this.GetTextMeshs();
			for (int i = 0; i < textMeshs.Length; i++)
			{
				textMeshs[i].font = font;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D28 File Offset: 0x00000F28
		public TMP_FontAsset GetFontMaterial()
		{
			this.GetReferencesIfNecessary();
			foreach (TMP_Text tmp_Text in this.GetTextMeshs())
			{
				if (tmp_Text.font != null)
				{
					return tmp_Text.font;
				}
			}
			return null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D6C File Offset: 0x00000F6C
		public void SetScale(float newScale)
		{
			Transform transform = base.transform;
			Vector3 localScale = new Vector3(newScale, newScale, newScale);
			transform.localScale = localScale;
			this.originalScale = localScale;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D96 File Offset: 0x00000F96
		public virtual Vector3 GetUpVector()
		{
			return DNPUpdater.upVector;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D9D File Offset: 0x00000F9D
		public virtual Vector3 GetRightVector()
		{
			return DNPUpdater.rightVector;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public virtual Vector3 GetFreshUpVector()
		{
			return base.transform.up;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DB1 File Offset: 0x00000FB1
		public virtual Vector3 GetFreshRightVector()
		{
			return base.transform.right;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public virtual void GetReferences()
		{
			this.baseAlpha = 1f;
			this.textMeshPro = base.transform.Find("TMP").GetComponent<TextMeshPro>();
			this.textMeshRenderer = this.textMeshPro.GetComponent<MeshRenderer>();
			this.transformA = base.transform.Find("MeshA");
			this.transformB = base.transform.Find("MeshB");
			this.meshRendererA = this.transformA.GetComponent<MeshRenderer>();
			this.meshRendererB = this.transformB.GetComponent<MeshRenderer>();
			this.meshFilterA = this.transformA.GetComponent<MeshFilter>();
			this.meshFilterB = this.transformB.GetComponent<MeshFilter>();
			this.subMeshRenderers = new List<Tuple<MeshRenderer, MeshRenderer>>();
			this.subMeshFilters = new List<Tuple<MeshFilter, MeshFilter>>();
			Transform transform = this.meshRendererA.transform;
			Transform transform2 = this.meshRendererB.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				Transform child2 = transform2.GetChild(i);
				this.subMeshRenderers.Add(new Tuple<MeshRenderer, MeshRenderer>(child.GetComponent<MeshRenderer>(), child2.GetComponent<MeshRenderer>()));
				this.subMeshFilters.Add(new Tuple<MeshFilter, MeshFilter>(child.GetComponent<MeshFilter>(), child2.GetComponent<MeshFilter>()));
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EFE File Offset: 0x000010FE
		public virtual void GetReferencesIfNecessary()
		{
			if (this.textMeshPro == null || this.subMeshRenderers == null)
			{
				this.GetReferences();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F1C File Offset: 0x0000111C
		public void FadeOut()
		{
			this.permanent = false;
			this.startLifeTime = -1000f;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F30 File Offset: 0x00001130
		public void FadeIn()
		{
			this.currentFade = 0f;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F3D File Offset: 0x0000113D
		public virtual TMP_Text[] GetTextMeshs()
		{
			return new TMP_Text[]
			{
				this.textMeshPro
			};
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F4E File Offset: 0x0000114E
		public virtual TMP_Text GetTextMesh()
		{
			return this.textMeshPro;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F56 File Offset: 0x00001156
		public virtual Material[] GetSharedMaterials()
		{
			return this.textMeshRenderer.sharedMaterials;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F63 File Offset: 0x00001163
		public virtual Material[] GetMaterials()
		{
			return this.textMeshRenderer.materials;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F70 File Offset: 0x00001170
		public virtual Material GetSharedMaterial()
		{
			return this.textMeshRenderer.sharedMaterial;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F7D File Offset: 0x0000117D
		public virtual Material GetMaterial()
		{
			return this.textMeshRenderer.material;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F8A File Offset: 0x0000118A
		public virtual bool IsAlive(float time)
		{
			return this.permanent || time - this.startLifeTime < this.currentLifetime;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void DestroyDNP()
		{
			if (!this.enablePooling || !(this.originalPrefab != null))
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			if (DamageNumber.pools == null)
			{
				DamageNumber.pools = new Dictionary<int, HashSet<DamageNumber>>();
			}
			if (!DamageNumber.pools.ContainsKey(this.poolingID))
			{
				DamageNumber.pools.Add(this.poolingID, new HashSet<DamageNumber>());
			}
			if (DamageNumber.activeInstances != null && DamageNumber.activeInstances.Contains(this))
			{
				DamageNumber.activeInstances.Remove(this);
			}
			DNPUpdater.UnregisterPopup(this.unscaledTime, this.updateDelay, this);
			this.RemoveFromDictionary();
			if (DamageNumber.pools[this.poolingID].Count < this.poolSize)
			{
				this.PreparePooling();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000307C File Offset: 0x0000127C
		public virtual void CheckAndEnable3D()
		{
			Camera camera = Camera.main;
			if (camera == null)
			{
				camera = Camera.current;
			}
			if (camera != null && !camera.orthographic)
			{
				this.enable3DGame = true;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030B6 File Offset: 0x000012B6
		public virtual bool IsMesh()
		{
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000030BC File Offset: 0x000012BC
		public static GameObject NewMesh(string tmName, Transform parent)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = tmName;
			gameObject.layer = 6;
			MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
			gameObject.AddComponent<MeshFilter>();
			meshRenderer.receiveShadows = false;
			meshRenderer.allowOcclusionWhenDynamic = false;
			meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			meshRenderer.lightProbeUsage = LightProbeUsage.Off;
			meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
			Transform transform = gameObject.transform;
			transform.SetParent(parent, true);
			transform.localPosition = Vector3.zero;
			transform.localScale = Vector3.one;
			transform.localEulerAngles = Vector3.zero;
			return gameObject;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000313C File Offset: 0x0000133C
		public void PrewarmPool()
		{
			if (this.enablePooling)
			{
				int instanceID = base.GetInstanceID();
				if (DamageNumber.pools == null)
				{
					DamageNumber.pools = new Dictionary<int, HashSet<DamageNumber>>();
				}
				if (!DamageNumber.pools.ContainsKey(instanceID))
				{
					DamageNumber.pools.Add(instanceID, new HashSet<DamageNumber>());
				}
				int num = this.poolSize - DamageNumber.pools[instanceID].Count;
				if ((float)num > (float)this.poolSize * 0.5f)
				{
					for (int i = 0; i < num; i++)
					{
						this.Spawn(new Vector3(-9999f, -9999f, 0f)).destroyAfterSpawning = true;
					}
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031E0 File Offset: 0x000013E0
		public static void ClearPooled(DNPType type = DNPType.All)
		{
			if (DamageNumber.pools != null)
			{
				foreach (KeyValuePair<int, HashSet<DamageNumber>> keyValuePair in DamageNumber.pools)
				{
					if (keyValuePair.Value != null)
					{
						foreach (DamageNumber damageNumber in keyValuePair.Value)
						{
							if (type == DNPType.All || damageNumber.IsMesh() == (type == DNPType.Mesh))
							{
								UnityEngine.Object.Destroy(damageNumber.gameObject);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003294 File Offset: 0x00001494
		public static void ClearActive(DNPType type = DNPType.All, bool allowPooling = true, bool ignorePermanent = true)
		{
			if (allowPooling)
			{
				List<DamageNumber> list = new List<DamageNumber>();
				foreach (DamageNumber damageNumber in DamageNumber.activeInstances)
				{
					if (damageNumber != null)
					{
						list.Add(damageNumber);
					}
				}
				using (List<DamageNumber>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DamageNumber damageNumber2 = enumerator2.Current;
						if ((!ignorePermanent || !damageNumber2.permanent) && (type == DNPType.All || damageNumber2.IsMesh() == (type == DNPType.Mesh)))
						{
							damageNumber2.DestroyDNP();
						}
					}
					return;
				}
			}
			foreach (DamageNumber damageNumber3 in DamageNumber.activeInstances)
			{
				if (damageNumber3 != null && (!ignorePermanent || !damageNumber3.permanent) && (type == DNPType.All || damageNumber3.IsMesh() == (type == DNPType.Mesh)))
				{
					UnityEngine.Object.Destroy(damageNumber3.gameObject);
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033C4 File Offset: 0x000015C4
		public static void FadeOutActive(DNPType type = DNPType.All, bool ignorePermanent = true)
		{
			foreach (DamageNumber damageNumber in DamageNumber.activeInstances)
			{
				if (damageNumber != null && (!ignorePermanent || !damageNumber.permanent) && (type == DNPType.All || damageNumber.IsMesh() == (type == DNPType.Mesh)))
				{
					damageNumber.FadeOut();
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003438 File Offset: 0x00001638
		protected void Restart()
		{
			if (DamageNumber.activeInstances == null)
			{
				DamageNumber.activeInstances = new HashSet<DamageNumber>();
			}
			if (!DamageNumber.activeInstances.Contains(this))
			{
				DamageNumber.activeInstances.Add(this);
			}
			DNPUpdater.RegisterPopup(this.unscaledTime, this.updateDelay, this);
			if (this.originalScale.x < 0.02f)
			{
				this.originalScale = base.transform.localScale;
			}
			this.transformA.localScale = (this.transformB.localScale = Vector3.one);
			this.OnStart();
			if (this.IsMesh())
			{
				if (DamageNumber.fallbackDictionary == null)
				{
					DamageNumber.fallbackDictionary = new Dictionary<TMP_FontAsset, GameObject>();
				}
				TMP_FontAsset fontMaterial = this.GetFontMaterial();
				if (!DamageNumber.fallbackDictionary.ContainsKey(fontMaterial) && fontMaterial != null)
				{
					bool flag = fontMaterial.fallbackFontAssetTable != null && fontMaterial.fallbackFontAssetTable.Count > 0;
					if (fontMaterial.isMultiAtlasTexturesEnabled || flag)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.textMeshPro.gameObject);
						gameObject.transform.localScale = Vector3.zero;
						gameObject.SetActive(true);
						gameObject.hideFlags = HideFlags.HideAndDontSave;
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
						string text = char.ConvertFromUtf32((int)fontMaterial.characterTable[0].unicode);
						if (fontMaterial.isMultiAtlasTexturesEnabled)
						{
							foreach (TMP_Character tmp_Character in fontMaterial.characterTable)
							{
								if (tmp_Character != null && tmp_Character.unicode > 0U)
								{
									text += char.ConvertFromUtf32((int)tmp_Character.unicode);
								}
							}
						}
						if (flag)
						{
							for (int i = 0; i < fontMaterial.fallbackFontAssetTable.Count; i++)
							{
								TMP_FontAsset tmp_FontAsset = fontMaterial.fallbackFontAssetTable[i];
								if (tmp_FontAsset != null && tmp_FontAsset.characterTable != null)
								{
									bool flag2 = false;
									foreach (TMP_Character tmp_Character2 in tmp_FontAsset.characterTable)
									{
										if (tmp_Character2 != null && this.AddFallbackCharacterToString(ref text, tmp_Character2.unicode, fontMaterial, i))
										{
											flag2 = true;
											break;
										}
									}
									if (!flag2 && tmp_FontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic)
									{
										int num = 0;
										while (num < 40959 && (!tmp_FontAsset.TryAddCharacters(new uint[]
										{
											(uint)num
										}, false) || !this.AddFallbackCharacterToString(ref text, (uint)num, fontMaterial, i)))
										{
											num++;
										}
									}
								}
							}
						}
						gameObject.GetComponent<TextMeshPro>().text = text;
						DamageNumber.fallbackDictionary.Add(fontMaterial, gameObject);
					}
					else
					{
						DamageNumber.fallbackDictionary.Add(fontMaterial, null);
					}
				}
			}
			float time = this.unscaledTime ? Time.unscaledTime : Time.time;
			this.Initialize(time);
			if (this.spamGroup != "")
			{
				this.TryCombination(time);
				this.TryDestruction(time);
				this.TryCollision();
				this.TryPush();
			}
			this.firstFrameScale = true;
			if (this.destroyAfterSpawning)
			{
				this.destroyAfterSpawning = false;
				this.startLifeTime = -100f;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003774 File Offset: 0x00001974
		private bool AddFallbackCharacterToString(ref string textString, uint unicode, TMP_FontAsset mainFontAsset, int fallbackIndex)
		{
			if (unicode < 1U)
			{
				return false;
			}
			if (!mainFontAsset.characterLookupTable.ContainsKey(unicode) && (mainFontAsset.atlasPopulationMode != AtlasPopulationMode.Dynamic || !mainFontAsset.TryAddCharacters(new uint[]
			{
				unicode
			}, false)))
			{
				bool flag = true;
				for (int i = 0; i < fallbackIndex; i++)
				{
					TMP_FontAsset tmp_FontAsset = mainFontAsset.fallbackFontAssetTable[i];
					if (tmp_FontAsset != null && (tmp_FontAsset.characterLookupTable.ContainsKey(unicode) || (tmp_FontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic && tmp_FontAsset.TryAddCharacters(new uint[]
					{
						unicode
					}, false))))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					textString += char.ConvertFromUtf32((int)unicode);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000381C File Offset: 0x00001A1C
		private void Initialize(float time)
		{
			this.numberScale = (this.destructionScale = (this.combinationScale = (this.currentFollowSpeed = 1f)));
			this.baseAlpha = 1f;
			this.finalPosition = (this.position = this.GetPosition());
			this.startTime = time;
			this.startLifeTime = time;
			this.currentLifetime = this.lifetime;
			this.isFadingOut = false;
			if (this.enable3DGame)
			{
				if (this.cameraOverride != null)
				{
					this.targetCamera = this.cameraOverride;
				}
				else if (Camera.main != null)
				{
					this.targetCamera = Camera.main.transform;
				}
				else if (Camera.current != null)
				{
					this.targetCamera = Camera.current.transform;
				}
				if (this.scaleWithFov)
				{
					if (this.fovCamera != null)
					{
						this.targetFovCamera = this.fovCamera;
					}
					else if (Camera.main != null)
					{
						this.targetFovCamera = Camera.main;
					}
					else if (Camera.current != null)
					{
						this.targetFovCamera = Camera.current;
					}
				}
			}
			if (this.enableOrthographicScaling)
			{
				if (this.orthographicCamera != null)
				{
					this.targetOrthographicCamera = this.orthographicCamera;
				}
				else if (Camera.main != null)
				{
					this.targetOrthographicCamera = Camera.main;
				}
				else if (Camera.current != null)
				{
					this.targetOrthographicCamera = Camera.current;
				}
			}
			this.UpdateScaleAnd3D(true);
			if (this.enableRotateOverTime)
			{
				this.currentRotationSpeed = UnityEngine.Random.Range(this.minRotationSpeed, this.maxRotationSpeed);
				if (this.rotationSpeedRandomFlip && UnityEngine.Random.value < 0.5f)
				{
					this.currentRotationSpeed *= -1f;
				}
			}
			this.AddToDictionary();
			if (this.enableLerp)
			{
				float num = UnityEngine.Random.Range(this.lerpSettings.minX, this.lerpSettings.maxX) * this.GetPositionFactor();
				if (this.lerpSettings.randomFlip && UnityEngine.Random.value < 0.5f)
				{
					num = -num;
				}
				this.remainingOffset = this.GetFreshRightVector() * num + this.GetFreshUpVector() * (UnityEngine.Random.Range(this.lerpSettings.minY, this.lerpSettings.maxY) * this.GetPositionFactor());
			}
			if (this.enableVelocity)
			{
				this.currentVelocity = new Vector2(UnityEngine.Random.Range(this.velocitySettings.minX, this.velocitySettings.maxX), UnityEngine.Random.Range(this.velocitySettings.minY, this.velocitySettings.maxY)) * this.GetPositionFactor();
				if (this.velocitySettings.randomFlip && UnityEngine.Random.value < 0.5f)
				{
					this.currentVelocity.x = -this.currentVelocity.x;
				}
			}
			if (this.enableStartRotation)
			{
				this.currentRotation = UnityEngine.Random.Range(this.minRotation, this.maxRotation);
				if (this.rotationRandomFlip && UnityEngine.Random.value < 0.5f)
				{
					this.currentRotation *= -1f;
				}
			}
			else
			{
				this.currentRotation = 0f;
			}
			this.currentFade = ((this.durationFadeIn > 0f) ? 0f : 1f);
			this.fadeInSpeed = 1f / Mathf.Max(0.0001f, this.durationFadeIn);
			if (this.enableCrossScaleFadeIn)
			{
				this.currentScaleInOffset = this.crossScaleFadeIn;
				if (this.currentScaleInOffset.x == 0f)
				{
					this.currentScaleInOffset.x = this.currentScaleInOffset.x + 0.001f;
				}
				if (this.currentScaleInOffset.y == 0f)
				{
					this.currentScaleInOffset.y = this.currentScaleInOffset.y + 0.001f;
				}
			}
			this.fadeOutSpeed = 1f / Mathf.Max(0.0001f, this.durationFadeOut);
			if (this.enableCrossScaleFadeOut)
			{
				this.currentScaleOutOffset = this.crossScaleFadeOut;
				if (this.currentScaleOutOffset.x == 0f)
				{
					this.currentScaleOutOffset.x = this.currentScaleOutOffset.x + 0.001f;
				}
				if (this.currentScaleOutOffset.y == 0f)
				{
					this.currentScaleOutOffset.y = this.currentScaleOutOffset.y + 0.001f;
				}
			}
			this.lastTargetPosition = Vector3.zero;
			this.UpdateText();
			this.UpdateRotationZ();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003C88 File Offset: 0x00001E88
		private void PreparePooling()
		{
			DamageNumber.pools[this.poolingID].Add(this);
			base.gameObject.SetActive(false);
			this.performRestart = true;
			base.transform.localScale = this.originalScale;
			this.lastTargetPosition = (this.targetOffset = Vector3.zero);
			this.myAbsorber = null;
			this.permanent = this.originalPrefab.permanent;
			this.spamGroup = this.originalPrefab.spamGroup;
			this.leftText = this.originalPrefab.leftText;
			this.rightText = this.originalPrefab.rightText;
			this.followedTarget = this.originalPrefab.followedTarget;
			this.enableCollision = this.originalPrefab.enableCollision;
			this.enablePush = this.originalPrefab.enablePush;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003D62 File Offset: 0x00001F62
		private bool PoolAvailable(int id)
		{
			return DamageNumber.pools != null && DamageNumber.pools.ContainsKey(id) && DamageNumber.pools[id].Count > 0;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003D90 File Offset: 0x00001F90
		private void SetPoolingID(int id)
		{
			this.poolingID = id;
			if (DamageNumber.pools == null)
			{
				DamageNumber.pools = new Dictionary<int, HashSet<DamageNumber>>();
			}
			if (DamageNumber.poolParent == null)
			{
				GameObject gameObject = new GameObject("Damage Number Pool");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				DamageNumber.poolParent = gameObject.transform;
				DamageNumber.poolParent.localScale = Vector3.one;
				DamageNumber.poolParent.eulerAngles = (DamageNumber.poolParent.position = Vector3.zero);
			}
			base.transform.SetParent(DamageNumber.poolParent, true);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003E18 File Offset: 0x00002018
		public void UpdateText()
		{
			string str = "";
			if (this.enableNumber)
			{
				string text;
				if (this.digitSettings.decimals <= 0)
				{
					bool flag;
					text = this.ProcessIntegers(Mathf.RoundToInt(this.number).ToString(), out flag);
				}
				else
				{
					string text2 = Mathf.RoundToInt(Mathf.Abs(this.number) * Mathf.Pow(10f, (float)this.digitSettings.decimals)).ToString();
					bool flag2 = this.number < 0f;
					int num = this.digitSettings.decimals;
					int length = text2.Length;
					if (length < num)
					{
						for (int i = 0; i < num - length; i++)
						{
							text2 = "0" + text2;
						}
					}
					while (this.digitSettings.hideZeros && text2.EndsWith("0") && num > 0)
					{
						text2 = text2.Substring(0, text2.Length - 1);
						num--;
					}
					string text3 = text2.Substring(0, Mathf.Max(0, text2.Length - num));
					bool flag;
					text3 = this.ProcessIntegers(text3, out flag);
					if (text3 == "")
					{
						text3 = "0";
					}
					int j = text2.Length;
					while (j < num)
					{
						if (this.digitSettings.hideZeros)
						{
							num--;
						}
						else
						{
							text2 += "0";
							j = text2.Length;
						}
					}
					string str2 = text2.Substring(text2.Length - num);
					if (num > 0 && !flag)
					{
						text = text3 + this.digitSettings.decimalChar + str2;
					}
					else
					{
						text = text3;
					}
					if (flag2)
					{
						text = "-" + text;
					}
				}
				str = this.ApplyTextSettings(text, this.numberSettings);
				if (this.enableScaleByNumber)
				{
					this.numberScale = this.scaleByNumberSettings.fromScale + (this.scaleByNumberSettings.toScale - this.scaleByNumberSettings.fromScale) * Mathf.Clamp01((this.number - this.scaleByNumberSettings.fromNumber) / (this.scaleByNumberSettings.toNumber - this.scaleByNumberSettings.fromNumber));
				}
				if (this.enableColorByNumber)
				{
					this.SetColor(this.colorByNumberSettings.colorGradient.Evaluate(Mathf.Clamp01((this.number - this.colorByNumberSettings.fromNumber) / (this.colorByNumberSettings.toNumber - this.colorByNumberSettings.fromNumber))));
				}
			}
			string str3 = "";
			if (this.enableTopText)
			{
				str3 = str3 + this.ApplyTextSettings(this.topText, this.topTextSettings) + Environment.NewLine;
			}
			if (this.enableLeftText)
			{
				str3 += this.ApplyTextSettings(this.leftText, this.leftTextSettings);
			}
			string text4 = "";
			if (this.enableRightText)
			{
				text4 += this.ApplyTextSettings(this.rightText, this.rightTextSettings);
			}
			if (this.enableBottomText)
			{
				text4 = text4 + Environment.NewLine + this.ApplyTextSettings(this.bottomText, this.bottomTextSettings);
			}
			this.GetReferencesIfNecessary();
			Vector3 localScale = base.transform.localScale;
			if (!this.enable3DGame || !this.renderThroughWalls)
			{
				this.renderThroughWallsScale = 1f;
			}
			if (this.lastScaleFactor < 1f)
			{
				this.lastScaleFactor = 1f;
			}
			float num2 = this.renderThroughWallsScale * this.lastScaleFactor;
			if (localScale.x < num2)
			{
				base.transform.localScale = new Vector3(num2, num2, num2);
			}
			this.SetTextString(str3 + str + text4);
			base.transform.localScale = localScale;
			this.colors = new List<Color[]>();
			this.alphas = new List<float[]>();
			for (int k = 0; k < this.meshs.Count; k++)
			{
				Mesh mesh = this.meshs[k];
				if (mesh != null)
				{
					Color[] array = mesh.colors;
					float[] array2 = new float[array.Length];
					for (int l = 0; l < array.Length; l++)
					{
						array2[l] = array[l].a;
					}
					this.alphas.Add(array2);
					this.colors.Add(array);
				}
				else
				{
					this.colors.Add(new Color[0]);
					this.alphas.Add(new float[0]);
				}
			}
			this.UpdateAlpha(this.currentFade);
			this.OnTextUpdate();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000042A4 File Offset: 0x000024A4
		protected virtual void SetTextString(string fullString)
		{
			this.textMeshPro.gameObject.SetActive(true);
			this.textMeshPro.text = fullString;
			this.textMeshPro.ForceMeshUpdate(false, false);
			this.ClearMeshs();
			this.meshs = new List<Mesh>();
			this.meshs.Add(UnityEngine.Object.Instantiate<Mesh>(this.textMeshPro.mesh));
			this.meshFilterA.mesh = (this.meshFilterB.mesh = this.meshs[0]);
			this.meshRendererA.sharedMaterials = (this.meshRendererB.sharedMaterials = this.textMeshRenderer.sharedMaterials);
			int num = 0;
			for (int i = 0; i < this.textMeshPro.transform.childCount; i++)
			{
				MeshRenderer component = this.textMeshPro.transform.GetChild(i).GetComponent<MeshRenderer>();
				if (component != null)
				{
					MeshFilter component2 = component.GetComponent<MeshFilter>();
					if (this.subMeshRenderers.Count <= i)
					{
						GameObject gameObject = DamageNumber.NewMesh("Sub", this.meshRendererA.transform);
						GameObject gameObject2 = DamageNumber.NewMesh("Sub", this.meshRendererB.transform);
						this.subMeshRenderers.Add(new Tuple<MeshRenderer, MeshRenderer>(gameObject.GetComponent<MeshRenderer>(), gameObject2.GetComponent<MeshRenderer>()));
						this.subMeshFilters.Add(new Tuple<MeshFilter, MeshFilter>(gameObject.GetComponent<MeshFilter>(), gameObject2.GetComponent<MeshFilter>()));
					}
					Tuple<MeshRenderer, MeshRenderer> tuple = this.subMeshRenderers[i];
					Tuple<MeshFilter, MeshFilter> tuple2 = this.subMeshFilters[i];
					tuple.Item1.sharedMaterials = (tuple.Item2.sharedMaterials = component.sharedMaterials);
					Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(component2.sharedMesh);
					tuple2.Item1.mesh = (tuple2.Item2.mesh = mesh);
					this.meshs.Add(mesh);
					tuple.Item1.transform.localScale = (tuple.Item2.transform.localScale = Vector3.one);
					tuple.Item1.gameObject.layer = 6;
					tuple.Item2.gameObject.layer = 6;
					num++;
				}
			}
			for (int j = num; j < this.subMeshRenderers.Count; j++)
			{
				this.subMeshRenderers[j].Item1.transform.localScale = (this.subMeshRenderers[j].Item2.transform.localScale = Vector3.zero);
			}
			this.textMeshPro.gameObject.SetActive(false);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000454C File Offset: 0x0000274C
		private string ProcessIntegers(string integers, out bool shortened)
		{
			shortened = false;
			if (this.digitSettings.suffixShorten)
			{
				int num = -1;
				while (integers.Length > this.digitSettings.maxDigits && num < this.digitSettings.suffixes.Count - 1 && integers.Length - this.digitSettings.suffixDigits[num + 1] > 0)
				{
					num++;
					integers = integers.Substring(0, integers.Length - this.digitSettings.suffixDigits[num]);
				}
				if (num >= 0)
				{
					integers += this.digitSettings.suffixes[num];
					shortened = true;
					return integers;
				}
			}
			if (this.digitSettings.dotSeparation && this.digitSettings.dotDistance > 0)
			{
				char[] array = integers.ToCharArray();
				integers = "";
				for (int i = array.Length - 1; i > -1; i--)
				{
					integers = array[i].ToString() + integers;
					if ((array.Length - i) % this.digitSettings.dotDistance == 0 && i > 0)
					{
						integers = this.digitSettings.dotChar + integers;
					}
				}
			}
			return integers;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004674 File Offset: 0x00002874
		private string ApplyTextSettings(string text, TextSettings settings)
		{
			string text2 = text;
			if (text == "")
			{
				return "";
			}
			if (settings.bold)
			{
				text2 = "<b>" + text2 + "</b>";
			}
			if (settings.italic)
			{
				text2 = "<i>" + text2 + "</i>";
			}
			if (settings.underline)
			{
				text2 = "<u>" + text2 + "</u>";
			}
			if (settings.strike)
			{
				text2 = "<s>" + text2 + "</s>";
			}
			if (settings.customColor)
			{
				text2 = string.Concat(new string[]
				{
					"<color=#",
					ColorUtility.ToHtmlStringRGBA(settings.color),
					">",
					text2,
					"</color>"
				});
			}
			if (settings.mark)
			{
				text2 = string.Concat(new string[]
				{
					"<mark=#",
					ColorUtility.ToHtmlStringRGBA(settings.markColor),
					">",
					text2,
					"</mark>"
				});
			}
			if (settings.alpha < 1f)
			{
				text2 = string.Concat(new string[]
				{
					"<alpha=#",
					ColorUtility.ToHtmlStringRGBA(new Color(1f, 1f, 1f, settings.alpha)).Substring(6),
					">",
					text2,
					"<alpha=#FF>"
				});
			}
			if (settings.size > 0f)
			{
				text2 = string.Concat(new string[]
				{
					"<size=+",
					settings.size.ToString().Replace(',', '.'),
					">",
					text2,
					"</size>"
				});
			}
			else if (settings.size < 0f)
			{
				text2 = string.Concat(new string[]
				{
					"<size=-",
					Mathf.Abs(settings.size).ToString().Replace(',', '.'),
					">",
					text2,
					"</size>"
				});
			}
			if (settings.characterSpacing != 0f)
			{
				text2 = string.Concat(new string[]
				{
					"<cspace=",
					settings.characterSpacing.ToString().Replace(',', '.'),
					">",
					text2,
					"</cspace>"
				});
			}
			if (settings.horizontal > 0f)
			{
				string text3 = "<space=" + settings.horizontal.ToString().Replace(',', '.') + "em>";
				text2 = text3 + text2 + text3;
			}
			if (settings.vertical != 0f)
			{
				text2 = string.Concat(new string[]
				{
					"<voffset=",
					settings.vertical.ToString().Replace(',', '.'),
					"em>",
					text2,
					"</voffset>"
				});
			}
			return text2;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000494C File Offset: 0x00002B4C
		private void ClearMeshs()
		{
			if (this.meshs != null)
			{
				if (Application.isPlaying)
				{
					using (List<Mesh>.Enumerator enumerator = this.meshs.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Mesh obj = enumerator.Current;
							UnityEngine.Object.Destroy(obj);
						}
						return;
					}
				}
				foreach (Mesh obj2 in this.meshs)
				{
					UnityEngine.Object.DestroyImmediate(obj2);
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000049EC File Offset: 0x00002BEC
		private void HandleFadeIn(float delta)
		{
			if (this.currentFade < 1f)
			{
				this.currentFade = Mathf.Min(1f, this.currentFade + delta * this.fadeInSpeed);
				this.UpdateFade(this.enableOffsetFadeIn, this.offsetFadeIn, this.enableCrossScaleFadeIn, this.currentScaleInOffset, this.enableScaleFadeIn, this.scaleFadeIn, this.enableShakeFadeIn, this.shakeOffsetFadeIn, this.shakeFrequencyFadeIn);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004A64 File Offset: 0x00002C64
		private void HandleFadeOut(float delta)
		{
			if (!this.isFadingOut)
			{
				this.isFadingOut = true;
				this.OnStop();
			}
			this.currentFade = Mathf.Max(0f, this.currentFade - delta * this.fadeOutSpeed);
			this.UpdateFade(this.enableOffsetFadeOut, this.offsetFadeOut, this.enableCrossScaleFadeOut, this.currentScaleOutOffset, this.enableScaleFadeOut, this.scaleFadeOut, this.enableShakeFadeOut, this.shakeOffsetFadeOut, this.shakeFrequencyFadeOut);
			this.RemoveFromDictionary();
			if (this.currentFade <= 0f)
			{
				this.DestroyDNP();
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004AFC File Offset: 0x00002CFC
		private void UpdateFade(bool enablePositionOffset, Vector2 positionOffset, bool enableScaleOffset, Vector2 scaleOffset, bool enableScale, Vector2 scale, bool enableShake, Vector2 shakeOffset, float shakeFrequency)
		{
			Vector2 vector = Vector2.zero;
			float num = this.currentFade - 1f;
			if (enableShake)
			{
				vector = shakeOffset * Mathf.Sin(num * shakeFrequency) * num;
			}
			if (enablePositionOffset)
			{
				Vector2 b = positionOffset * num;
				this.SetLocalPositionA(vector + b);
				this.SetLocalPositionB(vector - b);
			}
			else
			{
				this.SetLocalPositionA(vector);
				this.SetLocalPositionB(vector);
			}
			if (enableScaleOffset)
			{
				if (enableScale)
				{
					Vector3 localScale = Vector2.Lerp(scaleOffset * scale, Vector2.one, this.currentFade);
					localScale.z = 1f;
					Vector3 localScale2 = Vector2.Lerp(new Vector3(1f / scaleOffset.x, 1f / scaleOffset.y, 1f) * scale, Vector2.one, this.currentFade);
					localScale2.z = 1f;
					this.transformA.localScale = localScale;
					this.transformB.localScale = localScale2;
				}
				else
				{
					Vector3 localScale3 = Vector2.Lerp(scaleOffset, Vector2.one, this.currentFade);
					localScale3.z = 1f;
					Vector3 localScale4 = Vector2.Lerp(new Vector3(1f / scaleOffset.x, 1f / scaleOffset.y, 1f), Vector2.one, this.currentFade);
					localScale4.z = 1f;
					this.transformA.localScale = localScale3;
					this.transformB.localScale = localScale4;
				}
			}
			else if (enableScale)
			{
				Vector3 localScale5 = Vector2.Lerp(scale, Vector2.one, this.currentFade);
				localScale5.z = 1f;
				this.transformA.localScale = (this.transformB.localScale = localScale5);
			}
			this.UpdateAlpha(this.currentFade);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004D08 File Offset: 0x00002F08
		public void UpdateAlpha(float progress)
		{
			float num = progress * progress * this.baseAlpha * this.baseAlpha;
			if (this.meshs != null)
			{
				for (int i = 0; i < this.meshs.Count; i++)
				{
					if (this.colors[i] != null && this.meshs[i] != null)
					{
						Color[] array = this.colors[i];
						float[] array2 = this.alphas[i];
						for (int j = 0; j < array.Length; j++)
						{
							array[j].a = num * array2[j];
						}
						this.meshs[i].colors = array;
					}
				}
			}
			this.OnFade(progress);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004DC8 File Offset: 0x00002FC8
		private void HandleFollowing(float deltaTime)
		{
			if (this.followedTarget == null)
			{
				this.lastTargetPosition = Vector3.zero;
				return;
			}
			if (this.lastTargetPosition != Vector3.zero)
			{
				this.targetOffset += this.followedTarget.position - this.lastTargetPosition;
			}
			this.lastTargetPosition = this.followedTarget.position;
			if (this.followSettings.drag > 0f && this.currentFollowSpeed > 0f)
			{
				this.currentFollowSpeed -= this.followSettings.drag * deltaTime;
				if (this.currentFollowSpeed < 0f)
				{
					this.currentFollowSpeed = 0f;
				}
			}
			Vector3 a = this.targetOffset;
			this.targetOffset = Vector3.Lerp(this.targetOffset, Vector3.zero, deltaTime * this.followSettings.speed * this.currentFollowSpeed);
			this.position += a - this.targetOffset;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004ED8 File Offset: 0x000030D8
		private void HandleLerp(float deltaTime)
		{
			float d = Mathf.Min(1f, deltaTime * this.lerpSettings.speed);
			Vector3 b = this.remainingOffset * d;
			this.remainingOffset -= b;
			this.position += b;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004F30 File Offset: 0x00003130
		private void HandleVelocity(float deltaTime)
		{
			if (this.velocitySettings.dragX > 0f)
			{
				this.currentVelocity.x = Mathf.Lerp(this.currentVelocity.x, 0f, deltaTime * this.velocitySettings.dragX);
			}
			if (this.velocitySettings.dragY > 0f)
			{
				this.currentVelocity.y = Mathf.Lerp(this.currentVelocity.y, 0f, deltaTime * this.velocitySettings.dragY);
			}
			this.currentVelocity.y = this.currentVelocity.y - this.velocitySettings.gravity * deltaTime * this.GetPositionFactor();
			this.position += (this.GetUpVector() * this.currentVelocity.y + this.GetRightVector() * this.currentVelocity.x) * deltaTime;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005028 File Offset: 0x00003228
		private Vector3 ApplyShake(Vector3 vector, ShakeSettings shakeSettings, float time)
		{
			float num = time - this.startTime;
			float d = Mathf.Sin(shakeSettings.frequency * num) * this.GetPositionFactor();
			if (shakeSettings.offset.y != 0f)
			{
				vector += this.GetUpVector() * d * shakeSettings.offset.y;
			}
			if (shakeSettings.offset.x != 0f)
			{
				vector += this.GetRightVector() * d * shakeSettings.offset.x;
			}
			return vector;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000050C0 File Offset: 0x000032C0
		public Vector3 GetTargetPosition()
		{
			return this.position + this.remainingOffset;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000050D3 File Offset: 0x000032D3
		public virtual Vector3 GetPosition()
		{
			return base.transform.position;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000050E0 File Offset: 0x000032E0
		protected virtual void SetLocalPositionA(Vector3 localPosition)
		{
			this.transformA.localPosition = localPosition;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000050EE File Offset: 0x000032EE
		protected virtual void SetLocalPositionB(Vector3 localPosition)
		{
			this.transformB.localPosition = localPosition;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000050FC File Offset: 0x000032FC
		public virtual void SetPosition(Vector3 newPosition)
		{
			base.transform.position = newPosition;
			this.position = newPosition;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005120 File Offset: 0x00003320
		public virtual void SetToMousePosition(RectTransform rectParent, Camera canvasCamera)
		{
			Vector2 anchoredPosition = Vector3.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, Input.mousePosition, canvasCamera, out anchoredPosition);
			this.SetAnchoredPosition(rectParent, anchoredPosition);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005154 File Offset: 0x00003354
		public virtual void SetAnchoredPosition(Transform rectParent, Vector2 anchoredPosition)
		{
			Vector3 localScale = base.transform.localScale;
			Vector3 eulerAngles = base.transform.eulerAngles;
			base.transform.SetParent(rectParent, false);
			base.transform.position = anchoredPosition;
			base.transform.localScale = localScale;
			base.transform.eulerAngles = eulerAngles;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000051B0 File Offset: 0x000033B0
		public virtual void SetAnchoredPosition(Transform rectParent, Transform rectPosition, Vector2 relativeAnchoredPosition)
		{
			Vector3 localScale = base.transform.localScale;
			Vector3 eulerAngles = base.transform.eulerAngles;
			base.transform.SetParent(rectParent, false);
			base.transform.position = rectPosition.position + relativeAnchoredPosition;
			base.transform.localScale = localScale;
			base.transform.eulerAngles = eulerAngles;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005216 File Offset: 0x00003416
		protected virtual float GetPositionFactor()
		{
			return 1f;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000521D File Offset: 0x0000341D
		public void SetSpamGroup(string newSpamGroup)
		{
			this.RemoveFromDictionary();
			this.spamGroup = newSpamGroup;
			this.AddToDictionary();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005234 File Offset: 0x00003434
		private void AddToDictionary()
		{
			if (this.spamGroup != "")
			{
				if (DamageNumber.spamGroupDictionary == null)
				{
					DamageNumber.spamGroupDictionary = new Dictionary<string, HashSet<DamageNumber>>();
				}
				if (!DamageNumber.spamGroupDictionary.ContainsKey(this.spamGroup))
				{
					DamageNumber.spamGroupDictionary.Add(this.spamGroup, new HashSet<DamageNumber>());
				}
				if (!DamageNumber.spamGroupDictionary[this.spamGroup].Contains(this))
				{
					DamageNumber.spamGroupDictionary[this.spamGroup].Add(this);
				}
				this.removedFromDictionary = false;
				return;
			}
			this.removedFromDictionary = true;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000052CC File Offset: 0x000034CC
		private void RemoveFromDictionary()
		{
			if (!this.removedFromDictionary && this.spamGroup != "" && DamageNumber.spamGroupDictionary != null && DamageNumber.spamGroupDictionary.ContainsKey(this.spamGroup) && DamageNumber.spamGroupDictionary[this.spamGroup].Contains(this))
			{
				DamageNumber.spamGroupDictionary[this.spamGroup].Remove(this);
				this.removedFromDictionary = true;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005344 File Offset: 0x00003544
		private void HandleCombination(float delta, float time)
		{
			if (this.myAbsorber != null)
			{
				if (this.myAbsorber.myAbsorber != null)
				{
					this.myAbsorber = this.myAbsorber.myAbsorber;
				}
				if (time - this.startTime < this.combinationSettings.spawnDelay)
				{
					this.absorbStartPosition = this.position;
					this.absorbStartTime = time;
					return;
				}
				this.startLifeTime = time;
				float num = (this.combinationSettings.absorbDuration > 0f) ? ((time - this.absorbStartTime) / this.combinationSettings.absorbDuration) : 1f;
				if (this.combinationSettings.moveToAbsorber)
				{
					this.position = Vector3.Lerp(this.absorbStartPosition, this.myAbsorber.position, num);
				}
				this.combinationScale = this.combinationSettings.scaleCurve.Evaluate(num);
				this.baseAlpha = 1f * this.combinationSettings.alphaCurve.Evaluate(num);
				this.UpdateAlpha(this.currentFade);
				if (this.combinationSettings.instantGain && num > 0f)
				{
					this.GiveNumber(time);
				}
				if (num >= 1f)
				{
					this.GiveNumber(time);
					this.DestroyDNP();
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005480 File Offset: 0x00003680
		private void TryCombination(float time)
		{
			if (!this.enableCombination)
			{
				return;
			}
			this.myAbsorber = null;
			switch (this.combinationSettings.method)
			{
			case CombinationMethod.ABSORB_NEW:
			{
				float num = time + 0.5f;
				DamageNumber damageNumber = null;
				foreach (DamageNumber damageNumber2 in DamageNumber.spamGroupDictionary[this.spamGroup])
				{
					if (damageNumber2 != this && damageNumber2.enableCombination && damageNumber2.myAbsorber == null && damageNumber2.startTime <= num && Vector3.Distance(damageNumber2.GetTargetPosition(), this.GetTargetPosition()) < this.combinationSettings.maxDistance * this.GetPositionFactor())
					{
						num = damageNumber2.startTime;
						damageNumber = damageNumber2;
					}
				}
				if (damageNumber != null)
				{
					this.GetAbsorbed(damageNumber, time);
					return;
				}
				return;
			}
			case CombinationMethod.REPLACE_OLD:
				using (HashSet<DamageNumber>.Enumerator enumerator = DamageNumber.spamGroupDictionary[this.spamGroup].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DamageNumber damageNumber3 = enumerator.Current;
						if (damageNumber3 != this && damageNumber3.enableCombination && Vector3.Distance(damageNumber3.position, this.position) < this.combinationSettings.maxDistance * this.GetPositionFactor())
						{
							if (damageNumber3.myAbsorber == null)
							{
								damageNumber3.startTime = time - 0.01f;
							}
							damageNumber3.GetAbsorbed(this, time);
						}
					}
					return;
				}
				break;
			case CombinationMethod.IS_ALWAYS_ABSORBER:
				break;
			case CombinationMethod.IS_ALWAYS_VICTIM:
				goto IL_230;
			default:
				return;
			}
			using (HashSet<DamageNumber>.Enumerator enumerator = DamageNumber.spamGroupDictionary[this.spamGroup].GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DamageNumber damageNumber4 = enumerator.Current;
					if (damageNumber4 != this && damageNumber4.enableCombination && damageNumber4.combinationSettings.method == CombinationMethod.IS_ALWAYS_VICTIM && Vector3.Distance(damageNumber4.position, this.position) < this.combinationSettings.maxDistance * this.GetPositionFactor())
					{
						if (damageNumber4.myAbsorber == null)
						{
							damageNumber4.startTime = time - 0.01f;
						}
						damageNumber4.GetAbsorbed(this, time);
					}
				}
				return;
			}
			IL_230:
			foreach (DamageNumber damageNumber5 in DamageNumber.spamGroupDictionary[this.spamGroup])
			{
				if (damageNumber5 != this && damageNumber5.enableCombination && damageNumber5.myAbsorber == null && damageNumber5.combinationSettings.method == CombinationMethod.IS_ALWAYS_ABSORBER && Vector3.Distance(damageNumber5.GetTargetPosition(), this.GetTargetPosition()) < this.combinationSettings.maxDistance * this.GetPositionFactor())
				{
					this.GetAbsorbed(damageNumber5, time);
					break;
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005790 File Offset: 0x00003990
		private void GetAbsorbed(DamageNumber otherNumber, float time)
		{
			if (this.myAbsorber != null)
			{
				return;
			}
			otherNumber.enablePush = (otherNumber.enableCollision = false);
			this.myAbsorber = otherNumber;
			this.absorbStartPosition = this.position;
			this.absorbStartTime = time;
			this.givenNumber = false;
			this.myAbsorber.startLifeTime = time;
			if (this.combinationSettings.teleportToAbsorber)
			{
				this.position = otherNumber.position;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005804 File Offset: 0x00003A04
		private void GiveNumber(float time)
		{
			if (!this.givenNumber)
			{
				this.givenNumber = true;
				this.myAbsorber.number += this.number;
				if (this.myAbsorber.myAbsorber == null)
				{
					this.myAbsorber.combinationScale = this.combinationSettings.absorberScaleFactor;
					this.myAbsorber.startTime = time;
					this.myAbsorber.currentLifetime = this.myAbsorber.lifetime + this.combinationSettings.bonusLifetime;
				}
				this.myAbsorber.UpdateText();
				this.myAbsorber.OnAbsorb(this.number, this.myAbsorber.number);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000058B9 File Offset: 0x00003AB9
		protected virtual void OnAbsorb(float number, float newSum)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000058BC File Offset: 0x00003ABC
		private void HandleDestruction(float time)
		{
			if (this.enableDestruction && this.isDestroyed)
			{
				if (time - this.startTime < this.destructionSettings.spawnDelay)
				{
					this.destructionStartTime = time;
					return;
				}
				float num = (this.destructionSettings.duration > 0f) ? ((time - this.destructionStartTime) / this.destructionSettings.duration) : 1f;
				if (num >= 1f)
				{
					this.DestroyDNP();
					return;
				}
				this.baseAlpha = 1f * this.destructionSettings.alphaCurve.Evaluate(num);
				this.UpdateAlpha(this.currentFade);
				this.destructionScale = this.destructionSettings.scaleCurve.Evaluate(num);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000597C File Offset: 0x00003B7C
		private void TryDestruction(float time)
		{
			if (this.enableDestruction)
			{
				this.isDestroyed = false;
				foreach (DamageNumber damageNumber in DamageNumber.spamGroupDictionary[this.spamGroup])
				{
					if (!damageNumber.isDestroyed && damageNumber != this && damageNumber.enableDestruction && Vector3.Distance(damageNumber.GetTargetPosition(), this.GetTargetPosition()) < this.destructionSettings.maxDistance * this.GetPositionFactor())
					{
						damageNumber.isDestroyed = true;
						damageNumber.destructionStartTime = time;
					}
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005A30 File Offset: 0x00003C30
		public void TriggerDestriction()
		{
			this.isDestroyed = true;
			this.destructionStartTime = (this.unscaledTime ? Time.unscaledTime : Time.time);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005A54 File Offset: 0x00003C54
		private void TryCollision()
		{
			foreach (DamageNumber damageNumber in DamageNumber.spamGroupDictionary[this.spamGroup])
			{
				damageNumber.collided = false;
			}
			this.TryCollision(this.GetTargetPosition());
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005ABC File Offset: 0x00003CBC
		private void TryCollision(Vector3 sourcePosition)
		{
			if (this.enableCollision)
			{
				this.collided = true;
				Vector3 targetPosition = this.GetTargetPosition();
				float num = this.collisionSettings.radius * this.simulatedScale * this.GetPositionFactor();
				foreach (DamageNumber damageNumber in DamageNumber.spamGroupDictionary[this.spamGroup])
				{
					if (damageNumber.enableCollision && damageNumber != this)
					{
						Vector3 targetPosition2 = damageNumber.GetTargetPosition();
						Vector3 b = targetPosition2 - targetPosition;
						float magnitude = b.magnitude;
						if (magnitude < num)
						{
							Vector3 lhs = targetPosition2 - sourcePosition + b + this.collisionSettings.desiredDirection * this.GetPositionFactor();
							if (lhs == Vector3.zero)
							{
								lhs = new Vector3(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f);
							}
							damageNumber.remainingOffset += lhs.normalized * (num - magnitude) * this.collisionSettings.pushFactor;
							if (!damageNumber.collided)
							{
								damageNumber.TryCollision(sourcePosition);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005C28 File Offset: 0x00003E28
		private void TryPush()
		{
			foreach (DamageNumber damageNumber in DamageNumber.spamGroupDictionary[this.spamGroup])
			{
				damageNumber.pushed = false;
			}
			this.TryPush(this.GetTargetPosition());
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005C90 File Offset: 0x00003E90
		private void TryPush(Vector3 sourcePosition)
		{
			if (this.enablePush)
			{
				this.pushed = true;
				Vector3 targetPosition = this.GetTargetPosition();
				float num = this.pushSettings.radius * this.simulatedScale * this.GetPositionFactor();
				DamageNumber damageNumber = null;
				float num2 = (float)((this.pushSettings.pushOffset > 0f) ? 1 : -1);
				float num3 = targetPosition.y + 1000f * num2;
				foreach (DamageNumber damageNumber2 in DamageNumber.spamGroupDictionary[this.spamGroup])
				{
					if (damageNumber2.enablePush && !damageNumber2.pushed)
					{
						Vector3 targetPosition2 = damageNumber2.GetTargetPosition();
						if (targetPosition2.y * num2 < num3 * num2 && Vector3.Distance(targetPosition, targetPosition2) < num)
						{
							num3 = targetPosition2.y;
							damageNumber = damageNumber2;
						}
					}
				}
				if (damageNumber != null)
				{
					float num4 = num3 - targetPosition.y;
					float a = this.pushSettings.pushOffset * this.GetPositionFactor() - num4;
					DamageNumber damageNumber3 = damageNumber;
					damageNumber3.remainingOffset.y = damageNumber3.remainingOffset.y + ((num2 > 0f) ? Mathf.Max(a, 0f) : Mathf.Min(a, 0f));
					damageNumber.TryPush(sourcePosition);
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005DEC File Offset: 0x00003FEC
		protected virtual void UpdateRotationZ()
		{
			this.SetRotationZ(this.meshRendererA.transform);
			this.SetRotationZ(this.meshRendererB.transform);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005E10 File Offset: 0x00004010
		protected void SetRotationZ(Transform transformTarget)
		{
			Vector3 localEulerAngles = transformTarget.localEulerAngles;
			localEulerAngles.z = this.currentRotation;
			transformTarget.localEulerAngles = localEulerAngles;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005E38 File Offset: 0x00004038
		private void HandleRotateOverTime(float delta, float time)
		{
			this.currentRotation += this.currentRotationSpeed * delta * this.rotateOverTime.Evaluate((time - this.startTime) / this.currentLifetime);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005E6C File Offset: 0x0000406C
		private void UpdateScaleAnd3D(bool beforeMeshBuild = false)
		{
			Vector3 vector = this.originalScale;
			this.lastScaleFactor = 1f;
			if (this.enableCombination)
			{
				this.combinationScale = Mathf.Lerp(this.combinationScale, 1f, Time.deltaTime * this.combinationSettings.absorberScaleFade);
				this.lastScaleFactor *= this.combinationScale;
			}
			if (this.enableScaleByNumber)
			{
				this.lastScaleFactor *= this.numberScale;
			}
			if (this.enableScaleOverTime)
			{
				float num = this.unscaledTime ? Time.unscaledTime : Time.time;
				vector *= this.scaleOverTime.Evaluate((num - this.startTime) / (this.currentLifetime + this.durationFadeOut));
			}
			if (this.enableDestruction)
			{
				vector *= this.destructionScale;
			}
			if (this.enableOrthographicScaling && !beforeMeshBuild)
			{
				vector *= Mathf.Min(this.maxOrthographicSize, this.targetOrthographicCamera.orthographicSize / this.defaultOrthographicSize);
			}
			if (this.enable3DGame && this.targetCamera != null)
			{
				if (this.faceCameraView)
				{
					if (this.lookAtCamera)
					{
						base.transform.LookAt(base.transform.position + (base.transform.position - this.targetCamera.position));
					}
					else
					{
						base.transform.rotation = this.targetCamera.rotation;
					}
				}
				Vector3 from = default(Vector3);
				float num2 = 0f;
				if (this.consistentScreenSize)
				{
					from = this.finalPosition - this.targetCamera.position;
					num2 = Mathf.Max(0.004f, from.magnitude);
					this.lastScaleFactor *= num2 / this.distanceScalingSettings.baseDistance;
					if (num2 < this.distanceScalingSettings.closeDistance)
					{
						this.lastScaleFactor *= this.distanceScalingSettings.closeScale;
					}
					else if (num2 > this.distanceScalingSettings.farDistance)
					{
						this.lastScaleFactor *= this.distanceScalingSettings.farScale;
					}
					else
					{
						this.lastScaleFactor *= this.distanceScalingSettings.farScale + (this.distanceScalingSettings.closeScale - this.distanceScalingSettings.farScale) * Mathf.Clamp01(1f - (num2 - this.distanceScalingSettings.closeScale) / Mathf.Max(0.01f, this.distanceScalingSettings.farDistance - this.distanceScalingSettings.closeScale));
					}
				}
				if (this.scaleWithFov && !beforeMeshBuild)
				{
					this.lastScaleFactor *= this.targetFovCamera.fieldOfView / this.defaultFov;
				}
				vector *= this.lastScaleFactor;
				this.simulatedScale = vector.x;
				if (this.renderThroughWalls)
				{
					float num3 = 0.3f;
					if (Camera.main != null)
					{
						num3 = Camera.main.nearClipPlane;
					}
					if (!this.consistentScreenSize)
					{
						from = this.finalPosition - this.targetCamera.position;
						num2 = Mathf.Max(0.004f, from.magnitude);
					}
					num3 += 0.0005f * num2 + 0.02f + num3 * 0.02f * Vector3.Angle(from, this.targetCamera.forward);
					base.transform.position = from.normalized * num3 + this.targetCamera.position;
					this.renderThroughWallsScale = num3 / num2;
					vector *= this.renderThroughWallsScale;
				}
			}
			else
			{
				vector *= this.lastScaleFactor;
				this.simulatedScale = vector.x;
			}
			base.transform.localScale = vector;
			if (this.firstFrameScale)
			{
				if (this.durationFadeIn > 0f)
				{
					base.transform.localScale = Vector3.zero;
				}
				this.firstFrameScale = false;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006264 File Offset: 0x00004464
		protected virtual void OnFade(float currentFade)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006266 File Offset: 0x00004466
		protected virtual void OnTextUpdate()
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006268 File Offset: 0x00004468
		protected virtual void OnUpdate(float deltaTime)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000626A File Offset: 0x0000446A
		protected virtual void OnStart()
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000626C File Offset: 0x0000446C
		protected virtual void OnStop()
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000626E File Offset: 0x0000446E
		protected virtual void OnLateUpdate()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006270 File Offset: 0x00004470
		protected virtual void OnPreSpawn()
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006274 File Offset: 0x00004474
		private void OnDestroy()
		{
			if (DamageNumber.activeInstances != null && DamageNumber.activeInstances.Contains(this))
			{
				DamageNumber.activeInstances.Remove(this);
			}
			DNPUpdater.UnregisterPopup(this.unscaledTime, this.updateDelay, this);
			this.RemoveFromDictionary();
			this.ClearMeshs();
			if (this.enablePooling && DamageNumber.pools != null && DamageNumber.pools.ContainsKey(this.poolingID) && DamageNumber.pools[this.poolingID].Contains(this))
			{
				DamageNumber.pools[this.poolingID].Remove(this);
			}
			if (this.enablePooling && this.disableOnSceneLoad)
			{
				SceneManager.sceneLoaded -= this.OnSceneLoaded;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006330 File Offset: 0x00004530
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (this.currentFade > 0f)
			{
				this.DestroyDNP();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006345 File Offset: 0x00004545
		private void Reset()
		{
			if (!Application.isPlaying)
			{
				this.CheckAndEnable3D();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006354 File Offset: 0x00004554
		protected DamageNumber()
		{
		}

		// Token: 0x04000031 RID: 49
		[Tooltip("Damage number will not fade out on it's own.")]
		public bool permanent;

		// Token: 0x04000032 RID: 50
		[Tooltip("The lifetime after which this fades out.")]
		public float lifetime = 2f;

		// Token: 0x04000033 RID: 51
		[Tooltip("Ignores slow motion or game pause.")]
		public bool unscaledTime;

		// Token: 0x04000034 RID: 52
		public bool enable3DGame;

		// Token: 0x04000035 RID: 53
		[Tooltip("Faces the camera at all times.")]
		public bool faceCameraView = true;

		// Token: 0x04000036 RID: 54
		[Tooltip("Uses LookAt(...) instead of the camera rotation.\nThis costs more performance but looks better in VR.\n\nNot recommended for spamming popups.")]
		public bool lookAtCamera;

		// Token: 0x04000037 RID: 55
		[Tooltip("Moves the number close to the camera and scales it down to make it look like it was visible through walls.")]
		public bool renderThroughWalls = true;

		// Token: 0x04000038 RID: 56
		[Tooltip("Keeps the screen-size consistent accross different distances.")]
		public bool consistentScreenSize;

		// Token: 0x04000039 RID: 57
		public DistanceScalingSettings distanceScalingSettings = new DistanceScalingSettings(0f);

		// Token: 0x0400003A RID: 58
		[Tooltip("Scales the popup with the camera's field of view to keep it's size consistent.")]
		public bool scaleWithFov;

		// Token: 0x0400003B RID: 59
		[Tooltip("The default field of view, where the popup will be at it's default scale.")]
		public float defaultFov = 60f;

		// Token: 0x0400003C RID: 60
		[Tooltip("The camera whose field of view the popup will react to.")]
		public Camera fovCamera;

		// Token: 0x0400003D RID: 61
		[Tooltip("Override the camera looked at and scaled for.\nIf this set to None the Main Camera will be used.")]
		public Transform cameraOverride;

		// Token: 0x0400003E RID: 62
		public bool enableNumber = true;

		// Token: 0x0400003F RID: 63
		[Tooltip("The number displayed in the text.\nCan be disabled if you only need text.")]
		public float number = 1f;

		// Token: 0x04000040 RID: 64
		public TextSettings numberSettings = new TextSettings(0f);

		// Token: 0x04000041 RID: 65
		public DigitSettings digitSettings = new DigitSettings(0f);

		// Token: 0x04000042 RID: 66
		[FormerlySerializedAs("enablePrefix")]
		public bool enableLeftText;

		// Token: 0x04000043 RID: 67
		[Tooltip("Text displayed to the left of the number.")]
		[FormerlySerializedAs("prefix")]
		public string leftText = "";

		// Token: 0x04000044 RID: 68
		[FormerlySerializedAs("prefixSettings")]
		public TextSettings leftTextSettings = new TextSettings(0f);

		// Token: 0x04000045 RID: 69
		[FormerlySerializedAs("enableSuffix")]
		public bool enableRightText;

		// Token: 0x04000046 RID: 70
		[Tooltip("Text displayed to the right of the number.")]
		[FormerlySerializedAs("suffix")]
		public string rightText = "";

		// Token: 0x04000047 RID: 71
		[FormerlySerializedAs("suffixSettings")]
		public TextSettings rightTextSettings = new TextSettings(0f);

		// Token: 0x04000048 RID: 72
		public bool enableTopText;

		// Token: 0x04000049 RID: 73
		[Tooltip("Text displayed above the number.")]
		public string topText = "";

		// Token: 0x0400004A RID: 74
		public TextSettings topTextSettings = new TextSettings(0f);

		// Token: 0x0400004B RID: 75
		public bool enableBottomText;

		// Token: 0x0400004C RID: 76
		[Tooltip("Text displayed below the number.")]
		public string bottomText = "";

		// Token: 0x0400004D RID: 77
		public TextSettings bottomTextSettings = new TextSettings(0f);

		// Token: 0x0400004E RID: 78
		public bool enableColorByNumber;

		// Token: 0x0400004F RID: 79
		public ColorByNumberSettings colorByNumberSettings = new ColorByNumberSettings(0f);

		// Token: 0x04000050 RID: 80
		public float durationFadeIn = 0.2f;

		// Token: 0x04000051 RID: 81
		public bool enableOffsetFadeIn = true;

		// Token: 0x04000052 RID: 82
		[Tooltip("TextA and TextB move together from this offset.")]
		public Vector2 offsetFadeIn = new Vector2(0.5f, 0f);

		// Token: 0x04000053 RID: 83
		public bool enableScaleFadeIn = true;

		// Token: 0x04000054 RID: 84
		[Tooltip("Scales in from this scale.")]
		public Vector2 scaleFadeIn = new Vector2(2f, 2f);

		// Token: 0x04000055 RID: 85
		public bool enableCrossScaleFadeIn;

		// Token: 0x04000056 RID: 86
		[Tooltip("Scales TextA in from this scale and TextB from the inverse of this scale.")]
		public Vector2 crossScaleFadeIn = new Vector2(1f, 1.5f);

		// Token: 0x04000057 RID: 87
		public bool enableShakeFadeIn;

		// Token: 0x04000058 RID: 88
		[Tooltip("Shakes in from this offset.")]
		public Vector2 shakeOffsetFadeIn = new Vector2(0f, 1.5f);

		// Token: 0x04000059 RID: 89
		[Tooltip("Shakes in at this frequency.")]
		public float shakeFrequencyFadeIn = 4f;

		// Token: 0x0400005A RID: 90
		public float durationFadeOut = 0.2f;

		// Token: 0x0400005B RID: 91
		public bool enableOffsetFadeOut = true;

		// Token: 0x0400005C RID: 92
		[Tooltip("TextA and TextB move apart to this offset.")]
		public Vector2 offsetFadeOut = new Vector2(0.5f, 0f);

		// Token: 0x0400005D RID: 93
		public bool enableScaleFadeOut;

		// Token: 0x0400005E RID: 94
		[Tooltip("Scales out to this scale.")]
		public Vector2 scaleFadeOut = new Vector2(2f, 2f);

		// Token: 0x0400005F RID: 95
		public bool enableCrossScaleFadeOut;

		// Token: 0x04000060 RID: 96
		[Tooltip("Scales TextA out to this scale and TextB to the inverse of this scale.")]
		public Vector2 crossScaleFadeOut = new Vector2(1f, 1.5f);

		// Token: 0x04000061 RID: 97
		public bool enableShakeFadeOut;

		// Token: 0x04000062 RID: 98
		[Tooltip("Shakes out to this offset.")]
		public Vector2 shakeOffsetFadeOut = new Vector2(0f, 1.5f);

		// Token: 0x04000063 RID: 99
		[Tooltip("Shakes out at this frequency.")]
		public float shakeFrequencyFadeOut = 4f;

		// Token: 0x04000064 RID: 100
		public bool enableLerp = true;

		// Token: 0x04000065 RID: 101
		public LerpSettings lerpSettings = new LerpSettings(0);

		// Token: 0x04000066 RID: 102
		public bool enableVelocity;

		// Token: 0x04000067 RID: 103
		public VelocitySettings velocitySettings = new VelocitySettings(0f);

		// Token: 0x04000068 RID: 104
		public bool enableShaking;

		// Token: 0x04000069 RID: 105
		[Tooltip("Shake settings during idle.")]
		public ShakeSettings shakeSettings = new ShakeSettings(new Vector2(0.005f, 0.005f));

		// Token: 0x0400006A RID: 106
		public bool enableFollowing;

		// Token: 0x0400006B RID: 107
		[Tooltip("Transform that will be followed.\nTries to maintain the position relative to the target.")]
		public Transform followedTarget;

		// Token: 0x0400006C RID: 108
		public FollowSettings followSettings = new FollowSettings(0f);

		// Token: 0x0400006D RID: 109
		public bool enableStartRotation;

		// Token: 0x0400006E RID: 110
		[Tooltip("The minimum z-angle for the random spawn rotation.")]
		public float minRotation = -4f;

		// Token: 0x0400006F RID: 111
		[Tooltip("The maximum z-angle for the random spawn rotation.")]
		public float maxRotation = 4f;

		// Token: 0x04000070 RID: 112
		public bool rotationRandomFlip;

		// Token: 0x04000071 RID: 113
		public bool enableRotateOverTime;

		// Token: 0x04000072 RID: 114
		[Tooltip("The minimum rotation speed for the z-angle.")]
		public float minRotationSpeed = -15f;

		// Token: 0x04000073 RID: 115
		[Tooltip("The maximum rotation speed for the z-angle.")]
		public float maxRotationSpeed = 15f;

		// Token: 0x04000074 RID: 116
		public bool rotationSpeedRandomFlip;

		// Token: 0x04000075 RID: 117
		[Tooltip("Defines rotation speed over lifetime.")]
		public AnimationCurve rotateOverTime = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(0.4f, 1f),
			new Keyframe(0.8f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04000076 RID: 118
		public bool enableScaleByNumber;

		// Token: 0x04000077 RID: 119
		public ScaleByNumberSettings scaleByNumberSettings = new ScaleByNumberSettings(0f);

		// Token: 0x04000078 RID: 120
		public bool enableScaleOverTime;

		// Token: 0x04000079 RID: 121
		[Tooltip("Will scale over it's lifetime using this curve.")]
		public AnimationCurve scaleOverTime = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0.7f)
		});

		// Token: 0x0400007A RID: 122
		public bool enableOrthographicScaling;

		// Token: 0x0400007B RID: 123
		[Tooltip("The base orthographic size of the camera, where the popup will use it's default scale.")]
		public float defaultOrthographicSize = 5f;

		// Token: 0x0400007C RID: 124
		[Tooltip("Popups wont get larger than this value to prevent overlapping, when zooming out.")]
		public float maxOrthographicSize = 1.5f;

		// Token: 0x0400007D RID: 125
		[Tooltip("The camera whose orthographic size will be used to resize the popup. Uses the Main Camera by default.")]
		public Camera orthographicCamera;

		// Token: 0x0400007E RID: 126
		[Tooltip("The group of numbers which will affect each other using the features bellow.")]
		public string spamGroup = "";

		// Token: 0x0400007F RID: 127
		public bool enableCombination;

		// Token: 0x04000080 RID: 128
		public CombinationSettings combinationSettings = new CombinationSettings(0f);

		// Token: 0x04000081 RID: 129
		public bool enableDestruction;

		// Token: 0x04000082 RID: 130
		public DestructionSettings destructionSettings = new DestructionSettings(0f);

		// Token: 0x04000083 RID: 131
		public bool enableCollision;

		// Token: 0x04000084 RID: 132
		public CollisionSettings collisionSettings = new CollisionSettings(0f);

		// Token: 0x04000085 RID: 133
		public bool enablePush;

		// Token: 0x04000086 RID: 134
		public PushSettings pushSettings = new PushSettings(0f);

		// Token: 0x04000087 RID: 135
		public float updateDelay = 0.0125f;

		// Token: 0x04000088 RID: 136
		public bool enablePooling;

		// Token: 0x04000089 RID: 137
		[Tooltip("Maximum of damage numbers stored in pool.")]
		public int poolSize = 50;

		// Token: 0x0400008A RID: 138
		[Tooltip("Pooled damage numbers are not destroyed on load.\nThis option will fade them out on load.\nSo you don't see popups from the previous scene.")]
		public bool disableOnSceneLoad = true;

		// Token: 0x0400008B RID: 139
		public string editorLastFont;

		// Token: 0x0400008C RID: 140
		private TextMeshPro textMeshPro;

		// Token: 0x0400008D RID: 141
		private MeshRenderer textMeshRenderer;

		// Token: 0x0400008E RID: 142
		private MeshRenderer meshRendererA;

		// Token: 0x0400008F RID: 143
		private MeshRenderer meshRendererB;

		// Token: 0x04000090 RID: 144
		private MeshFilter meshFilterA;

		// Token: 0x04000091 RID: 145
		private MeshFilter meshFilterB;

		// Token: 0x04000092 RID: 146
		protected Transform transformA;

		// Token: 0x04000093 RID: 147
		protected Transform transformB;

		// Token: 0x04000094 RID: 148
		private List<Tuple<MeshRenderer, MeshRenderer>> subMeshRenderers;

		// Token: 0x04000095 RID: 149
		private List<Tuple<MeshFilter, MeshFilter>> subMeshFilters;

		// Token: 0x04000096 RID: 150
		protected List<Mesh> meshs;

		// Token: 0x04000097 RID: 151
		protected List<Color[]> colors;

		// Token: 0x04000098 RID: 152
		protected List<float[]> alphas;

		// Token: 0x04000099 RID: 153
		protected float currentFade;

		// Token: 0x0400009A RID: 154
		protected float startTime;

		// Token: 0x0400009B RID: 155
		private float startLifeTime;

		// Token: 0x0400009C RID: 156
		protected float currentLifetime;

		// Token: 0x0400009D RID: 157
		private float fadeInSpeed;

		// Token: 0x0400009E RID: 158
		private float fadeOutSpeed;

		// Token: 0x0400009F RID: 159
		protected float baseAlpha;

		// Token: 0x040000A0 RID: 160
		private Vector2 currentScaleInOffset;

		// Token: 0x040000A1 RID: 161
		private Vector2 currentScaleOutOffset;

		// Token: 0x040000A2 RID: 162
		public Vector3 position;

		// Token: 0x040000A3 RID: 163
		private Vector3 finalPosition;

		// Token: 0x040000A4 RID: 164
		protected Vector3 remainingOffset;

		// Token: 0x040000A5 RID: 165
		protected Vector2 currentVelocity;

		// Token: 0x040000A6 RID: 166
		protected Vector3 originalScale;

		// Token: 0x040000A7 RID: 167
		private float numberScale;

		// Token: 0x040000A8 RID: 168
		private float combinationScale;

		// Token: 0x040000A9 RID: 169
		private float destructionScale;

		// Token: 0x040000AA RID: 170
		private float renderThroughWallsScale = 0.1f;

		// Token: 0x040000AB RID: 171
		private float lastScaleFactor = 1f;

		// Token: 0x040000AC RID: 172
		private bool firstFrameScale;

		// Token: 0x040000AD RID: 173
		private float currentRotationSpeed;

		// Token: 0x040000AE RID: 174
		private float currentRotation;

		// Token: 0x040000AF RID: 175
		private Vector3 lastTargetPosition;

		// Token: 0x040000B0 RID: 176
		private Vector3 targetOffset;

		// Token: 0x040000B1 RID: 177
		private float currentFollowSpeed;

		// Token: 0x040000B2 RID: 178
		private static Dictionary<string, HashSet<DamageNumber>> spamGroupDictionary;

		// Token: 0x040000B3 RID: 179
		private bool removedFromDictionary;

		// Token: 0x040000B4 RID: 180
		private DamageNumber myAbsorber;

		// Token: 0x040000B5 RID: 181
		private bool givenNumber;

		// Token: 0x040000B6 RID: 182
		private float absorbStartTime;

		// Token: 0x040000B7 RID: 183
		private Vector3 absorbStartPosition;

		// Token: 0x040000B8 RID: 184
		private Transform targetCamera;

		// Token: 0x040000B9 RID: 185
		private Camera targetFovCamera;

		// Token: 0x040000BA RID: 186
		private float simulatedScale;

		// Token: 0x040000BB RID: 187
		private float lastFOV;

		// Token: 0x040000BC RID: 188
		private Camera targetOrthographicCamera;

		// Token: 0x040000BD RID: 189
		private bool isDestroyed;

		// Token: 0x040000BE RID: 190
		private float destructionStartTime;

		// Token: 0x040000BF RID: 191
		private bool collided;

		// Token: 0x040000C0 RID: 192
		private bool pushed;

		// Token: 0x040000C1 RID: 193
		private DamageNumber originalPrefab;

		// Token: 0x040000C2 RID: 194
		public static Transform poolParent;

		// Token: 0x040000C3 RID: 195
		private static Dictionary<int, HashSet<DamageNumber>> pools;

		// Token: 0x040000C4 RID: 196
		public static HashSet<DamageNumber> activeInstances;

		// Token: 0x040000C5 RID: 197
		private int poolingID;

		// Token: 0x040000C6 RID: 198
		private bool performRestart;

		// Token: 0x040000C7 RID: 199
		private bool destroyAfterSpawning;

		// Token: 0x040000C8 RID: 200
		private static Dictionary<TMP_FontAsset, GameObject> fallbackDictionary;

		// Token: 0x040000C9 RID: 201
		protected bool isFadingOut;
	}
}

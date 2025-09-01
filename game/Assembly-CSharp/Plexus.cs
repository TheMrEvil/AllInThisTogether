using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000255 RID: 597
public class Plexus : MonoBehaviour
{
	// Token: 0x0600180C RID: 6156 RVA: 0x00096498 File Offset: 0x00094698
	private void Awake()
	{
		this.system = base.GetComponent<ParticleSystem>();
		this.main = this.system.main;
		for (int i = 0; i < this.maxLines; i++)
		{
			LineRenderer item = UnityEngine.Object.Instantiate<LineRenderer>(this.lineTemplate, base.transform, false);
			this.lines.Add(item);
		}
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000964F4 File Offset: 0x000946F4
	private void LateUpdate()
	{
		this.UpdateConnections();
		Gradient colorGradient = this.lineTemplate.colorGradient;
		GradientAlphaKey gradientAlphaKey = new GradientAlphaKey(1f, 0f);
		GradientAlphaKey gradientAlphaKey2 = new GradientAlphaKey(1f, 1f);
		GradientAlphaKey[] array = new GradientAlphaKey[2];
		float num = this.maxDistance * this.maxDistance;
		int maxParticles = this.main.maxParticles;
		if (this.particles == null || this.particles.Length < maxParticles)
		{
			this.particles = new ParticleSystem.Particle[maxParticles];
		}
		this.system.GetParticles(this.particles);
		List<LineRenderer> list = new List<LineRenderer>();
		foreach (KeyValuePair<LineRenderer, Plexus.Connection> keyValuePair in this.connections)
		{
			LineRenderer lineRenderer;
			Plexus.Connection connection;
			keyValuePair.Deconstruct(out lineRenderer, out connection);
			LineRenderer lineRenderer2 = lineRenderer;
			Plexus.Connection connection2 = connection;
			ParticleSystem.Particle particle = this.particles[connection2.start];
			ParticleSystem.Particle particle2 = this.particles[connection2.end];
			if (particle.remainingLifetime < 0f || particle2.remainingLifetime < 0f)
			{
				lineRenderer2.enabled = false;
				list.Add(lineRenderer2);
				Dictionary<int, int> dictionary = this.linkCount;
				int num2 = connection2.start;
				int num3 = dictionary[num2];
				dictionary[num2] = num3 - 1;
				Dictionary<int, int> dictionary2 = this.linkCount;
				num3 = connection2.end;
				num2 = dictionary2[num3];
				dictionary2[num3] = num2 - 1;
			}
			else
			{
				float num4 = Vector3.SqrMagnitude(particle.position - particle2.position);
				connection2.sqrdist = num4;
				if (num4 > num)
				{
					Dictionary<int, int> dictionary3 = this.linkCount;
					int num2 = connection2.start;
					int num3 = dictionary3[num2];
					dictionary3[num2] = num3 - 1;
					Dictionary<int, int> dictionary4 = this.linkCount;
					num3 = connection2.end;
					num2 = dictionary4[num3];
					dictionary4[num3] = num2 - 1;
					list.Add(lineRenderer2);
					lineRenderer2.enabled = false;
				}
				else
				{
					float a = Mathf.Min(particle.remainingLifetime, particle2.remainingLifetime);
					float alpha = Mathf.Lerp(lineRenderer2.colorGradient.alphaKeys[0].alpha, Mathf.Min(a, 1f - num4 / num), Time.deltaTime * 2f);
					float alpha2 = Mathf.Lerp(lineRenderer2.colorGradient.alphaKeys[1].alpha, Mathf.Min(a, 1f - num4 / num), Time.deltaTime * 2f);
					gradientAlphaKey.alpha = alpha;
					gradientAlphaKey2.alpha = alpha2;
					array[0] = gradientAlphaKey;
					array[1] = gradientAlphaKey2;
					colorGradient.SetKeys(colorGradient.colorKeys, array);
					lineRenderer2.colorGradient = colorGradient;
					lineRenderer2.SetPosition(0, particle.position);
					lineRenderer2.SetPosition(1, particle2.position);
					lineRenderer2.enabled = true;
				}
			}
		}
		foreach (LineRenderer key in list)
		{
			this.connections.Remove(key);
		}
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x0009685C File Offset: 0x00094A5C
	private void UpdateConnections()
	{
		int maxParticles = this.main.maxParticles;
		if (this.particles == null || this.particles.Length < maxParticles)
		{
			this.particles = new ParticleSystem.Particle[maxParticles];
		}
		this.system.GetParticles(this.particles);
		int particleCount = this.system.particleCount;
		float num = this.maxDistance * this.maxDistance;
		Vector3 a = Vector3.zero;
		Vector3 b = Vector3.zero;
		Gradient colorGradient = this.lineTemplate.colorGradient;
		new GradientAlphaKey(1f, 0f);
		new GradientAlphaKey(1f, 1f);
		new GradientAlphaKey[2];
		List<LineRenderer> list = new List<LineRenderer>();
		foreach (LineRenderer lineRenderer in this.lines)
		{
			if (!this.connections.ContainsKey(lineRenderer))
			{
				list.Add(lineRenderer);
			}
		}
		int count = list.Count;
		if (list.Count == 0)
		{
			return;
		}
		for (int i = this.ptest; i < Mathf.Min(particleCount, this.ptest + this.sliceCount); i++)
		{
			a = this.particles[i].position;
			for (int j = i + 1; j < particleCount; j++)
			{
				b = this.particles[j].position;
				if (Vector3.SqrMagnitude(a - b) <= num)
				{
					if (!this.linkCount.ContainsKey(i))
					{
						this.linkCount.Add(i, 0);
					}
					if (!this.linkCount.ContainsKey(j))
					{
						this.linkCount.Add(j, 0);
					}
					if (this.linkCount[i] < this.MaxLinksPerNode && this.linkCount[j] < this.MaxLinksPerNode)
					{
						Dictionary<int, int> dictionary = this.linkCount;
						int num2 = i;
						int num3 = dictionary[num2];
						dictionary[num2] = num3 + 1;
						Dictionary<int, int> dictionary2 = this.linkCount;
						num3 = j;
						num2 = dictionary2[num3];
						dictionary2[num3] = num2 + 1;
						LineRenderer key = list[this.lineIndex];
						Plexus.Connection connection = new Plexus.Connection();
						connection.start = i;
						connection.end = j;
						this.connections.Add(key, connection);
						this.lineIndex++;
						if (this.connections.Count >= this.maxLines || this.lineIndex >= count)
						{
							break;
						}
					}
				}
			}
		}
		for (int k = this.lineIndex; k < this.lines.Count; k++)
		{
			this.lines[k].enabled = false;
		}
		this.ptest += this.sliceCount;
		if (this.ptest >= particleCount || this.connections.Count >= this.maxLines)
		{
			this.ptest = 0;
			this.lineIndex = 0;
		}
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x00096B6C File Offset: 0x00094D6C
	public Plexus()
	{
	}

	// Token: 0x040017D1 RID: 6097
	public float maxDistance = 1f;

	// Token: 0x040017D2 RID: 6098
	public int maxLines = 150;

	// Token: 0x040017D3 RID: 6099
	public int sliceCount = 5;

	// Token: 0x040017D4 RID: 6100
	public int MaxLinksPerNode = 3;

	// Token: 0x040017D5 RID: 6101
	private ParticleSystem system;

	// Token: 0x040017D6 RID: 6102
	private ParticleSystem.Particle[] particles;

	// Token: 0x040017D7 RID: 6103
	private ParticleSystem.MainModule main;

	// Token: 0x040017D8 RID: 6104
	public LineRenderer lineTemplate;

	// Token: 0x040017D9 RID: 6105
	public List<LineRenderer> lines = new List<LineRenderer>();

	// Token: 0x040017DA RID: 6106
	private Dictionary<LineRenderer, Plexus.Connection> connections = new Dictionary<LineRenderer, Plexus.Connection>();

	// Token: 0x040017DB RID: 6107
	private Dictionary<int, int> linkCount = new Dictionary<int, int>();

	// Token: 0x040017DC RID: 6108
	private int ptest;

	// Token: 0x040017DD RID: 6109
	private int lineIndex;

	// Token: 0x02000619 RID: 1561
	private class Connection
	{
		// Token: 0x06002743 RID: 10051 RVA: 0x000D5458 File Offset: 0x000D3658
		public Connection()
		{
		}

		// Token: 0x040029CE RID: 10702
		public int start;

		// Token: 0x040029CF RID: 10703
		public int end;

		// Token: 0x040029D0 RID: 10704
		public float sqrdist;
	}
}

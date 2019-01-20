﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace ca.HenrySoftware
{
	public class Gename
	{
		private class Data
		{
			public List<string> Name;
			public List<string> Start;
			public List<string> Middle;
			public List<string> Finish;
			public List<string> Pre;
			public List<string> Post;
		}
		private Data _male;
		private Data _female;
		private char[] _vowel = { 'a', 'e', 'i', 'o', 'u', 'y' };
		private Random _random = new Random(Guid.NewGuid().GetHashCode());
		public Gename()
		{
			Setup();
		}
		public void Setup()
		{
			// m, f, n
			using (var name = new StreamReader(@"Name.csv"))
			{
				while (!name.EndOfStream)
				{
					var line = name.ReadLine();
					var values = line.Split(',');
					Debug.Assert(values.Length == 3);
					var male = values[0];
					if (!string.IsNullOrEmpty(male))
						_male.Name.Add(male);
					var female = values[1];
					if (!string.IsNullOrEmpty(female))
						_female.Name.Add(female);
					var neutral = values[2];
					if (!string.IsNullOrEmpty(neutral))
					{
						_male.Name.Add(neutral);
						_female.Name.Add(neutral);
					}
				}
			}
			// ms, mm, mf, fs, fm, ff, ns, nm, nf
			using (var syllable = new StreamReader(@"Syllable.csv"))
			{
				while (!syllable.EndOfStream)
				{
					var line = syllable.ReadLine();
					var values = line.Split(',');
					Debug.Assert(values.Length == 9);
					var maleStart = values[0];
					if (!string.IsNullOrEmpty(maleStart))
						_male.Start.Add(maleStart);
					var maleMiddle = values[1];
					if (!string.IsNullOrEmpty(maleMiddle))
						_male.Middle.Add(maleMiddle);
					var maleFinish = values[2];
					if (!string.IsNullOrEmpty(maleFinish))
						_male.Finish.Add(maleFinish);
					var femaleStart = values[3];
					if (!string.IsNullOrEmpty(femaleStart))
						_female.Start.Add(femaleStart);
					var femaleMiddle = values[4];
					if (!string.IsNullOrEmpty(femaleMiddle))
						_female.Middle.Add(femaleMiddle);
					var femaleFinish = values[5];
					if (!string.IsNullOrEmpty(femaleFinish))
						_female.Finish.Add(femaleFinish);
					var neutralStart = values[6];
					if (!string.IsNullOrEmpty(neutralStart))
					{
						_male.Start.Add(neutralStart);
						_female.Start.Add(neutralStart);
					}
					var neutralMiddle = values[7];
					if (!string.IsNullOrEmpty(neutralMiddle))
					{
						_male.Middle.Add(neutralMiddle);
						_female.Middle.Add(neutralMiddle);
					}
					var neutralFinish = values[8];
					if (!string.IsNullOrEmpty(neutralFinish))
					{
						_male.Finish.Add(neutralFinish);
						_female.Finish.Add(neutralFinish);
					}
				}
			}
			// mp, mp, fp, fp, np, np
			using (var title = new StreamReader(@"Title.csv"))
			{
				while (!title.EndOfStream)
				{
					var line = title.ReadLine();
					var values = line.Split(',');
					Debug.Assert(values.Length == 6);
					var malePre = values[0];
					if (!string.IsNullOrEmpty(malePre))
						_male.Pre.Add(malePre);
					var malePost = values[0];
					if (!string.IsNullOrEmpty(malePost))
						_male.Post.Add(malePost);
					var femalePre = values[2];
					if (!string.IsNullOrEmpty(femalePre))
						_female.Pre.Add(femalePre);
					var femalePost = values[3];
					if (!string.IsNullOrEmpty(femalePost))
						_female.Post.Add(femalePost);
					var neutralPre = values[4];
					if (!string.IsNullOrEmpty(neutralPre))
					{
						_male.Pre.Add(neutralPre);
						_female.Pre.Add(neutralPre);
					}
					var neutralPost = values[5];
					if (!string.IsNullOrEmpty(neutralPost))
					{
						_male.Post.Add(neutralPost);
						_female.Post.Add(neutralPost);
					}
				}
			}
		}
		public string Name(
			double sex = .5,
			double generated = .5,
			double pre = .333,
			double vowel = .8,
			double middle0 = .3,
			double middle1 = .1,
			double post = .22)
		{
			var name = string.Empty;
			var male = sex > _random.NextDouble();
			if (pre > _random.NextDouble())
			{
				name += male ? _male.Pre[_random.Next(_male.Pre.Count)] : _female.Pre[_random.Next(_female.Pre.Count)];
				name += " ";
				post += .5;
			}
			if (generated > _random.NextDouble())
			{
				name += male ? _male.Start[_random.Next(_male.Start.Count)] : _female.Start[_random.Next(_female.Start.Count)];
				if (vowel > _random.NextDouble())
				{
					name += _vowel[_random.Next(_vowel.Length)];
				}
				if (middle0 > _random.NextDouble())
				{
					name += male ? _male.Middle[_random.Next(_male.Middle.Count)] : _female.Middle[_random.Next(_female.Middle.Count)];
				}
				if (middle1 > _random.NextDouble())
				{
					name += male ? _male.Middle[_random.Next(_male.Middle.Count)] : _female.Middle[_random.Next(_female.Middle.Count)];
				}
				name += male ? _male.Finish[_random.Next(_male.Finish.Count)] : _female.Finish[_random.Next(_female.Finish.Count)];
			}
			else
				name += male ? _male.Name[_random.Next(_male.Name.Count)] : _female.Name[_random.Next(_female.Name.Count)];
			if (post > _random.NextDouble())
			{
				name += " ";
				name += male ? _male.Post[_random.Next(_male.Post.Count)] : _female.Post[_random.Next(_female.Post.Count)];
			}
			return name;
		}
	}
}

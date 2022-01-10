using System;
using System.Reflection;
using HarmonyLib;
using Kingmaker.Modding;
using Kingmaker.PubSubSystem;
using Owlcat.Runtime.Core.Logging;
using UnityEngine;

namespace OwlcatModification.Modifications.GiftedMasterCharacterClass
{
	// ReSharper disable once UnusedType.Global
	public static class ModificationRoot
	{
		public static Kingmaker.Modding.OwlcatModification Modification { get; private set; }

		public static bool IsEnabled { get; private set; } = true;

		public static LogChannel Logger => Modification.Logger;

		// ReSharper disable once UnusedMember.Global
		[OwlcatModificationEnterPoint]
		public static void Initialize(Kingmaker.Modding.OwlcatModification modification)
		{
			Modification = modification;

			var harmony = new Harmony(modification.Manifest.UniqueName);
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			TestData();
		}

		private static void TestData()
		{
			var data = Modification.LoadData<ModificationData>();
			Logger.Log($"TestModification: prev load time {data.LastLoadTime}");
			data.LastLoadTime = DateTime.Now.ToString();
			Logger.Log($"TestModification: current load time {data.LastLoadTime}");
			Modification.SaveData(data);
		}
	}
}

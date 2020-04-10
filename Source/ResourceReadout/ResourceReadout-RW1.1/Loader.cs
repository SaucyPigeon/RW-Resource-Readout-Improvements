using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using System.Reflection;

namespace ResourceReadout
{
	[StaticConstructorOnStartup]
	public static class Loader
	{
		static Loader()
		{
			const string Id = "com.saucypigeon.rimworld.mod.resourcereadout";
#if DEBUG
			Harmony.DEBUG = true;
			Log.Warning($"{Id} is in debug mode. Please contact the mod author if you see this.");
#endif
			var harmony = new Harmony(Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}

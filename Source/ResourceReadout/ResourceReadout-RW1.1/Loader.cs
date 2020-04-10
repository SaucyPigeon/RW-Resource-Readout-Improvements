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
		public static NodeOpenTracker NodeOpenTracker = new NodeOpenTracker();

		private static MethodInfo mi_Selector_ShiftIsHeld = AccessTools.PropertyGetter(typeof(Selector), "ShiftIsHeld");

		public static void SelectAllOnMap(ThingDef thingDef)
		{
#if DEBUG
			Log.Message("Select all on map - start");
#endif
			var things = Find.CurrentMap.spawnedThings.Where(x => x.def == thingDef && x.IsInAnyStorage());

			var selector = Find.Selector;
			if (!(bool)mi_Selector_ShiftIsHeld.Invoke(selector, new object[0]))
			{
				selector.ClearSelection();
			}
			things.Do(x => selector.Select(x));

#if DEBUG
			Log.Message("Select all on map - end");
#endif
		}

		static Loader()
		{
			const string Id = "com.saucypigeon.rimworld.mod.resourcereadout";
#if DEBUG
			Harmony.DEBUG = true;
			Log.Warning($"{Id} is in debug mode. Please contact the mod author if you see this.");
#endif
			var harmony = new Harmony(Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			NodeOpenTracker = new NodeOpenTracker();
		}
	}
}

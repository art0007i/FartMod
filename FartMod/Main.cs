using System;

using FrooxEngine;
using ResoniteModLoader;

namespace FartMod {
	public class Main : ResoniteMod {
		public const string VERSION_CONSTANT = "1.1.0";
		public override string Name => "FartMod";
		public override string Author => "art0007i";
		public override string Version => VERSION_CONSTANT;
		public override string Link => "https://github.com/art0007i/FartMod";

		public override void OnEngineInit() {
			Msg("FartMod Initialized!");
			Engine.Current.OnReady += ReadyHandler;
			Engine.Current.OnShutdown += ReadyHandler;
		}
		private void ReadyHandler() {
			Engine.Current.InputInterface.AfterInputsUpdate += AfterInputHandler;
		}

		private void ShutdownHandler() {
			Engine.Current.InputInterface.AfterInputsUpdate -= AfterInputHandler;
		}

		private void AfterInputHandler() {
			if (Engine.Current.InputInterface.GetKeyDown(Key.F)) // Trigger fart on "F"
			{
				PlayFart();
			}
		}

		public static Uri FartUri = new Uri("https://upload.wikimedia.org/wikipedia/commons/c/cf/Fart.ogg");
		private void PlayFart() {
			StaticAudioClip fartClip = Engine.Current.WorldManager.FocusedWorld.LocalUser.Root.Slot.GetComponentOrAttach<StaticAudioClip>(out bool attached, f => f.URL.Value == FartUri);
			if (attached) {
				fartClip.URL.Value = FartUri;
			}
			if (fartClip != null) {
				Engine.Current.WorldManager.FocusedWorld.LocalUser.Root.Slot.PlayOneShot(fartClip);
				Msg("Fart triggered!");
			}
		}
	}
}

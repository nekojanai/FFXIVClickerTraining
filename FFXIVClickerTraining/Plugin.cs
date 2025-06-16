using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using System;

namespace FFXIVClickerTraining
{
  public class Plugin : IDalamudPlugin
  {
    private readonly IDalamudPluginInterface pluginInterface;
    public string Name => "FFXIV Clicker Training";

    public Plugin(
      IDalamudPluginInterface pi,
      IClientState clientState,
      IFramework framework,
      IPluginLog pluginLog
    )
    {
      this.pluginInterface = pi;
      Service.ClientState = clientState;
      Service.Framework = framework;
      Service.PluginLog = pluginLog;

      Service.ClientState.LevelChanged += (uint classJobId, uint lvl) =>
      {
        Service.PluginLog.Debug($"Level changed: {lvl}");
        AudioEngine.PlayClickerSound();
      };

      Service.ClientState.ClassJobChanged += (uint classJobId) =>
      {
        Service.PluginLog.Debug($"ClassJob changed: {classJobId}");
        AudioEngine.PlayClickerSound();
      };

      // this.pluginInterface.UiBuilder.Draw += SoundController.Update;
    }

    #region IDisposable Support
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing) return;

      // this.pluginInterface.UiBuilder.Draw -= SoundController.Update;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    #endregion
  }

}
﻿using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace FFXIVClickerTraining;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public bool IsConfigWindowMovable { get; set; } = true;
    public bool SomePropertyToBeSavedAndWithADefault { get; set; } = true;

    public void Save()
    {
        // Plugin.pluginInterface.SavePluginConfig(this);
    }
}

using System;
using System.Threading;
using System.Collections.Generic;
using NAudio.Vorbis;
using NAudio.Wave;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Collections.Frozen;
using System.Reflection.Metadata;

namespace FFXIVClickerTraining
{
  public static class AudioEngine
  {
    public static string PlayClickerSound(float volume = 1.0f)
    {
      string message = "";

      new Thread(() =>
      {
        WaveStream reader = null;
        try
        {
          Service.PluginLog.Debug("Start of AudioEngine initialization");
          var clickerSoundFilePath = "FFXIVClickerTraining.Data.Clicker.wav";
          var assembly = Assembly.GetExecutingAssembly();
          var stream = assembly.GetManifestResourceStream(clickerSoundFilePath);
          reader = new WaveFileReader(stream);
        }
        catch (Exception e)
        {
          Service.PluginLog.Error($"Could not play sound file {e.Message}");
          message = "Error reading file.";
          return;
        }

        if (reader == null) return;

        volume = Math.Max(0, Math.Min(volume, 1));

        WaveChannel32 channel = null;

        try
        {
          channel = new(reader)
          {
            Volume = 1 - (float)Math.Sqrt(1 - (volume * volume)),
            PadWithZeroes = false,
          };
        }
        catch (Exception e)
        {
          Service.PluginLog.Error($"Error opening new channel {e.Message}");
          message = "Error reading file.";
          return;
        }

        using (reader)
        {
          using var output = new WaveOutEvent
          {
            DeviceNumber = -1,
          };
          output.Init(channel);
          output.Play();

          message = "Pass";

          while (output.PlaybackState == PlaybackState.Playing)
          {
            Thread.Sleep(100);
          }
        }
        ;
      }).Start();

      return message;
    }
  }
}
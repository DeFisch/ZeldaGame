using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using ZeldaGame;

public class AudioLoader {
    private readonly Game1 game;
    private Dictionary<string, SoundEffect> sfx = new Dictionary<string, SoundEffect>();
    private List<(string, SoundEffectInstance)> sfx_instances = new List<(string, SoundEffectInstance)>();
    private bool singletonIsPlaying = false;
    private bool isMuted = false;
    public AudioLoader(Game1 game) {
        this.game = game;
        LoadAudio();
    }
    public void PlayBGM(){
        SoundEffectInstance bgm = game.Content.Load<SoundEffect>("audio/BGM").CreateInstance();
        bgm.IsLooped = true;
        bgm.Volume = 0.4f;
        bgm.Play();
        sfx_instances.Add(("BGM", bgm));
    }
    public void LoadAudio() {
        var pid = Environment.OSVersion.Platform;
        DirectoryInfo dir; 
        switch (pid) {
            case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
                dir = new DirectoryInfo("../../../" + game.Content.RootDirectory + "/audio");  
                break;
            case PlatformID.MacOSX:
			case PlatformID.Unix:
                dir = new DirectoryInfo(game.Content.RootDirectory + "/audio");
                break;
            default:
                dir = new DirectoryInfo("../../../" + game.Content.RootDirectory + "/audio");  
                break;
        }             
        FileInfo[] files = dir.GetFiles("*.wav");
        foreach (FileInfo file in files) {
            string key = Path.GetFileNameWithoutExtension(file.Name);
            sfx[key] = game.Content.Load<SoundEffect>("audio/" + key);
        }
    }

    public void PlayDisregardMute(string key) {
        sfx[key].Play();
    }

    public void Play(string key, bool loop = false) {
        SoundEffectInstance instance = sfx[key].CreateInstance();
        if (loop)
            instance.IsLooped = true;
        if (isMuted)
            instance.Volume = 0;
        instance.Play();
        sfx_instances.Add((key, instance));
    }

    public void PlaySingleton(string key, bool loop = false) {
        if (singletonIsPlaying) {
            return;
        }
        singletonIsPlaying = true;
        SoundEffectInstance instance = sfx[key].CreateInstance();
        if (loop)
            instance.IsLooped = true;
        if (isMuted)
            instance.Volume = 0;
        instance.Play();
        sfx_instances.Add((key, instance));
    }

    public void StopSingleton(string key) {
        for (int i = 0; i < sfx_instances.Count; i++) {
            if (sfx_instances[i].Item1 == key) {
                sfx_instances[i].Item2.Stop();
                sfx_instances.RemoveAt(i);
                singletonIsPlaying = false;
                break;
            }
        }
    }

    public void Stop(string key) {
        for (int i = 0; i < sfx_instances.Count; i++) {
            if (sfx_instances[i].Item1 == key) {
                sfx_instances[i].Item2.Stop();
                sfx_instances.RemoveAt(i);
                break;
            }
        }
    }

    public void Mute() {
        if (isMuted) {
            foreach (var sfx in sfx_instances) {
                if (sfx.Item1 == "BGM")
                    sfx.Item2.Volume = 0.2f;
                else
                    sfx.Item2.Volume = 1;
            }
            isMuted = false;
        } else {
            foreach (var sfx in sfx_instances) {
                sfx.Item2.Volume = 0;
            }
            isMuted = true;
        }
    }

    public void Reset() {
        foreach (var sfx in sfx_instances) {
            sfx.Item2.Stop();
            sfx.Item2.Dispose();
        }
        sfx_instances.Clear();
        singletonIsPlaying = false;
    }
}
        

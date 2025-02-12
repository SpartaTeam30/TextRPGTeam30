using System;
using System.Collections.Generic;

public class SoundManager
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SoundManager();
            return _instance;
        }
    }

    private Dictionary<string, ISoundPlayer> soundScripts;

    private SoundManager()
    {
        soundScripts = new Dictionary<string, ISoundPlayer>();
        LoadSoundScripts();
    }

    private void LoadSoundScripts()
    {
        soundScripts["click"] = new 딸깍음();
        soundScripts["impact"] = new 타격음();
        soundScripts["potion"] = new 포션효과음();
        soundScripts["manaPotion"] = new 마나포션();
        soundScripts["dungeonBGM"] = new 던전음();
        soundScripts["background"] = new 배경음();

    }

    public void PlaySound(string soundName)
    {
        if (!soundScripts.ContainsKey(soundName))
        {
            Console.WriteLine($"[SoundManager] 사운드 '{soundName}'를 찾을 수 없습니다.");
            return;
        }

        Console.WriteLine($"[SoundManager] {soundName} 재생 시작");

        // 🔥 메인 스레드를 차단하지 않고, 비동기 실행
        Task.Run(() => soundScripts[soundName].Play());
    }



    public void StopSound(string soundName)
    {
        if (soundScripts.ContainsKey(soundName))
        {
            soundScripts[soundName].Stop();
        }
        else
        {
            Console.WriteLine($"[SoundManager] 사운드 '{soundName}' 를 찾을 수 없습니다.");
        }
    }
}

public interface ISoundPlayer
{
    void Play();
    void Stop();
}

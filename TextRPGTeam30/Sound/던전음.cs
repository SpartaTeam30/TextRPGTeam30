using System;
using System.Runtime.InteropServices;
using System.Threading;

public class 던전음 : ISoundPlayer  // 🔥 ISoundPlayer 추가
{
    const int CALLBACK_NULL = 0;
    const int WHDR_DONE = 0x00000001;
    const int WAVE_MAPPER = -1;

    private IntPtr hWaveOut;

    [StructLayout(LayoutKind.Sequential)]
    public class WAVEFORMATEX
    {
        public ushort wFormatTag = 1;
        public ushort nChannels = 1;
        public uint nSamplesPerSec = 44100;
        public uint nAvgBytesPerSec = 44100 * 2;
        public ushort nBlockAlign = 2;
        public ushort wBitsPerSample = 16;
        public ushort cbSize = 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WAVEHDR
    {
        public IntPtr lpData;
        public uint dwBufferLength;
        public uint dwBytesRecorded;
        public uint dwUser;
        public uint dwFlags;
        public uint dwLoops;
        public IntPtr lpNext;
        public uint reserved;
    }

    [DllImport("winmm.dll")]
    public static extern int waveOutOpen(out IntPtr hWaveOut, int uDeviceID, WAVEFORMATEX lpFormat,
                                         IntPtr dwCallback, IntPtr dwInstance, int dwFlags);

    [DllImport("winmm.dll")]
    public static extern int waveOutPrepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, uint uSize);

    [DllImport("winmm.dll")]
    public static extern int waveOutWrite(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, uint uSize);

    [DllImport("winmm.dll")]
    public static extern int waveOutUnprepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, uint uSize);

    [DllImport("winmm.dll")]
    public static extern int waveOutClose(IntPtr hWaveOut);

    static Random rnd = new Random();

    public void Play()
    {
        Console.WriteLine("[BattleBGM] Play() 실행됨");

        int sampleRate = 44100;
        int durationSeconds = 30;
        int totalSamples = sampleRate * durationSeconds;
        double[] mixBuffer = new double[totalSamples];

        GenerateBattleBGM(mixBuffer, sampleRate, durationSeconds);

        short[] samples = new short[totalSamples];
        for (int i = 0; i < totalSamples; i++)
        {
            samples[i] = (short)(mixBuffer[i] * 1.0 * short.MaxValue);
        }

        byte[] byteBuffer = new byte[samples.Length * 2];
        Buffer.BlockCopy(samples, 0, byteBuffer, 0, byteBuffer.Length);

        PlayPCM(byteBuffer, sampleRate);
    }

    public void Stop()
    {
        waveOutClose(hWaveOut);
    }

    static void GenerateBattleBGM(double[] buffer, int sampleRate, int durationSeconds)
    {
        double[] melodyNotes = { 440, 523, 587, 659, 698, 784 };
        double[] bassNotes = { 55, 65, 73, 82 };
        double startTime = 0;

        while (startTime < durationSeconds - 1)
        {
            double freq = melodyNotes[rnd.Next(melodyNotes.Length)];
            AddMelody(buffer, sampleRate, startTime, 0.15, freq, 0.4);
            startTime += 0.2;
        }

        AddBass(buffer, sampleRate, durationSeconds);
    }

    static void AddMelody(double[] buffer, int sampleRate, double startTime, double duration, double freq, double amplitude)
    {
        int startSample = (int)(startTime * sampleRate);
        int endSample = startSample + (int)(duration * sampleRate);
        for (int i = startSample; i < endSample && i < buffer.Length; i++)
        {
            buffer[i] += amplitude * Math.Sin(2 * Math.PI * freq * i / sampleRate);
        }
    }

    static void AddBass(double[] buffer, int sampleRate, int durationSeconds)
    {
        double bassFreq = 60;
        for (int i = 0; i < buffer.Length; i++)
        {
            double t = (double)i / sampleRate;
            buffer[i] += 0.3 * Math.Sin(2 * Math.PI * bassFreq * t);
        }
    }

    static void PlayPCM(byte[] byteBuffer, int sampleRate)
    {
        WAVEFORMATEX format = new WAVEFORMATEX();
        IntPtr hWaveOut;
        waveOutOpen(out hWaveOut, WAVE_MAPPER, format, IntPtr.Zero, IntPtr.Zero, CALLBACK_NULL);

        IntPtr pBuffer = Marshal.AllocHGlobal(byteBuffer.Length);
        Marshal.Copy(byteBuffer, 0, pBuffer, byteBuffer.Length);

        WAVEHDR header = new WAVEHDR()
        {
            lpData = pBuffer,
            dwBufferLength = (uint)byteBuffer.Length,
            dwFlags = 0,
            dwLoops = 0
        };

        waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
        waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

        while ((header.dwFlags & WHDR_DONE) == 0)
        {
            Thread.Sleep(100);
        }

        waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
        Marshal.FreeHGlobal(pBuffer);
        waveOutClose(hWaveOut);
    }
}

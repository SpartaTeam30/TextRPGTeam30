using System;
using System.Runtime.InteropServices;
using System.Threading;

public class 배경음 : ISoundPlayer
{
    const int CALLBACK_NULL = 0;
    const int WHDR_DONE = 0x00000001;
    const int WAVE_MAPPER = -1;

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
    public static extern int waveOutWrite(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, uint uSize);
    [DllImport("winmm.dll")]
    public static extern int waveOutClose(IntPtr hWaveOut);

    private IntPtr hWaveOut;
    private IntPtr pBuffer;
    private WAVEHDR header;

    public void Play()
    {
        Console.WriteLine("[배경음] Play() 실행됨");

        int sampleRate = 44100;
        int durationSeconds = 60;
        int totalSamples = sampleRate * durationSeconds;
        double[] buffer = new double[totalSamples];

        GenerateBackgroundMusic(buffer, sampleRate, durationSeconds);

        short[] samples = new short[totalSamples];
        for (int i = 0; i < totalSamples; i++)
        {
            samples[i] = (short)(buffer[i] * short.MaxValue);
        }

        byte[] byteBuffer = new byte[samples.Length * 2];
        Buffer.BlockCopy(samples, 0, byteBuffer, 0, byteBuffer.Length);

        Console.WriteLine("[배경음] waveOutWrite() 실행 전 샘플 최대값: " + samples.Max());

        PlayPCM(byteBuffer, sampleRate);

        // 🔥 소리가 끝나지 않도록 일정 시간 유지
        Thread.Sleep(60000);  // 60초 동안 프로그램이 종료되지 않도록 대기
    }


    public void Stop()
    {
        waveOutClose(hWaveOut);
        Marshal.FreeHGlobal(pBuffer);
    }

    private void GenerateBackgroundMusic(double[] buffer, int sampleRate, int durationSeconds)
    {
        double[] melodyNotes = { 220, 330, 440, 550, 660, 770 };
        double startTime = 0;

        while (startTime < durationSeconds - 1)
        {
            double freq = melodyNotes[new Random().Next(melodyNotes.Length)];
            AddMelody(buffer, sampleRate, startTime, 0.3, freq, 0.5);
            startTime += 0.5;
        }
    }

    private void AddMelody(double[] buffer, int sampleRate, double startTime, double duration, double freq, double amplitude)
    {
        amplitude = 1.0; // 🔥 볼륨 증가
        int startSample = (int)(startTime * sampleRate);
        int endSample = startSample + (int)(duration * sampleRate);
        for (int i = startSample; i < endSample && i < buffer.Length; i++)
        {
            buffer[i] += amplitude * Math.Sin(2 * Math.PI * freq * i / sampleRate);
        }
    }


    private void PlayPCM(byte[] byteBuffer, int sampleRate)
    {
        WAVEFORMATEX format = new WAVEFORMATEX();
        waveOutOpen(out hWaveOut, WAVE_MAPPER, format, IntPtr.Zero, IntPtr.Zero, CALLBACK_NULL);
        pBuffer = Marshal.AllocHGlobal(byteBuffer.Length);
        Marshal.Copy(byteBuffer, 0, pBuffer, byteBuffer.Length);

        header = new WAVEHDR()
        {
            lpData = pBuffer,
            dwBufferLength = (uint)byteBuffer.Length,
            dwFlags = 0,
            dwLoops = 1 // ✅ BGM 반복 가능하도록 설정
        };

        waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
    }
}
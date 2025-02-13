using System;
using System.Runtime.InteropServices;
using System.Threading;

public class 배경음 : ISoundPlayer // 🔥 ISoundPlayer 추가
{
    const int CALLBACK_NULL = 0;
    const int WHDR_DONE = 0x00000001;
    const int WAVE_MAPPER = -1;

    private IntPtr hWaveOut; // 🔥 클래스 멤버 변수 유지

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
    public static extern int waveOutReset(IntPtr hWaveOut); // 🔥 waveOutReset 추가


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

    public void Play()
    {
        Console.WriteLine("[BackgroundBGM] Play() 실행됨");

        int sampleRate = 44100;
        int durationSeconds = 30; // 전체 재생 시간: 30초
        int totalSamples = sampleRate * durationSeconds;
        double[] mixBuffer = new double[totalSamples];

        GenerateBackgroundBGM(mixBuffer, sampleRate, durationSeconds);

        short[] samples = new short[totalSamples];
        for (int i = 0; i < totalSamples; i++)
        {
            samples[i] = (short)(mixBuffer[i] * short.MaxValue);
        }

        byte[] byteBuffer = new byte[samples.Length * 2];
        Buffer.BlockCopy(samples, 0, byteBuffer, 0, byteBuffer.Length);

        PlayPCM(byteBuffer, sampleRate);
    }

    public void Stop()
    {
        if (hWaveOut != IntPtr.Zero)
        {
            Console.WriteLine("[배경음] 재생 중단");

            waveOutReset(hWaveOut); // 🔥 즉시 중단 추가
            waveOutClose(hWaveOut); // 🔥 장치 닫기
            hWaveOut = IntPtr.Zero; // 🔥 핸들 초기화
        }
    }

    private static void GenerateBackgroundBGM(double[] buffer, int sampleRate, int durationSeconds)
    {
        double[] melodyNotes = { 293.66, 349.23, 440.00, 392.00, 349.23 };
        double melodyStart = 2.0;
        double noteDuration = 0.5;
        double noteGap = 0.1;
        int noteIndex = 0;
        double tTime = melodyStart;

        while (tTime + noteDuration < durationSeconds - 4)
        {
            double freq = melodyNotes[noteIndex % melodyNotes.Length];
            AddMelodyNote(buffer, sampleRate, tTime, noteDuration, freq, 0.25);
            tTime += noteDuration + noteGap;
            noteIndex++;
        }
    }

    private static void AddMelodyNote(double[] buffer, int sampleRate, double startTime, double noteDuration, double frequency, double amplitude)
    {
        int startSample = (int)(startTime * sampleRate);
        int noteSamples = (int)(noteDuration * sampleRate);
        for (int i = 0; i < noteSamples && (startSample + i) < buffer.Length; i++)
        {
            double t = (double)i / sampleRate;
            buffer[startSample + i] += amplitude * Math.Sin(2 * Math.PI * frequency * t);
        }
    }

    private void PlayPCM(byte[] byteBuffer, int sampleRate)
    {
        WAVEFORMATEX format = new WAVEFORMATEX();
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
            Thread.Sleep(10);
        }

        waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
        Marshal.FreeHGlobal(pBuffer);
        waveOutClose(hWaveOut);
    }
}

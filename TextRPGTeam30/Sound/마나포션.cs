﻿using System.Runtime.InteropServices;

public class 마나포션 : ISoundPlayer
{
    const int CALLBACK_NULL = 0;
    const int WHDR_DONE = 0x00000001;
    const int WAVE_MAPPER = -1;

    private IntPtr hWaveOut;  // 🔥 클래스 멤버 변수로 선언

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
        int sampleRate = 44100;
        int durationMilliseconds = 500;
        int totalSamples = sampleRate * durationMilliseconds / 1000;
        double[] buffer = new double[totalSamples];

        GenerateManaPotionSound(buffer, sampleRate);

        short[] samples = new short[totalSamples];
        for (int i = 0; i < totalSamples; i++)
        {
            samples[i] = (short)(buffer[i] * short.MaxValue);
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

    static void GenerateManaPotionSound(double[] buffer, int sampleRate)
    {
        double[] freqs = { 1100, 800, 600, 1300 };

        for (int j = 0; j < freqs.Length; j++)
        {
            int startSample = (j * buffer.Length) / freqs.Length;
            int endSample = ((j + 1) * buffer.Length) / freqs.Length;
            for (int i = startSample; i < endSample; i++)
            {
                double t = (double)i / sampleRate;
                buffer[i] += Math.Sin(2 * Math.PI * freqs[j] * t) * 0.8;
            }
        }
    }

    void PlayPCM(byte[] byteBuffer, int sampleRate)
    {
        WAVEFORMATEX format = new WAVEFORMATEX();
        waveOutOpen(out hWaveOut, WAVE_MAPPER, format, IntPtr.Zero, IntPtr.Zero, CALLBACK_NULL); // 🔥 클래스 멤버 변수 사용

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

﻿using System;
using System.Runtime.InteropServices;
using System.Threading;

public class 던전음 : ISoundPlayer
{
    const int CALLBACK_NULL = 0;
    const int WHDR_DONE = 0x00000001;
    const int WAVE_MAPPER = -1;

    private IntPtr hWaveOut = IntPtr.Zero; // 🔥 클래스 멤버 변수로 유지

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

    static Random rnd = new Random();

    public void Play()
    {
        Console.WriteLine("[BackgroundBGM] Play() 실행됨");

        int sampleRate = 44100;
        int durationSeconds = 30; // 전체 재생 시간: 30초
        int totalSamples = sampleRate * durationSeconds;
        double[] mixBuffer = new double[totalSamples];

        GenerateBattleBGM(mixBuffer, sampleRate, durationSeconds);

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
        if (hWaveOut != IntPtr.Zero) // 🔥 이미 닫힌 상태라면 실행 안 함
        {
            Console.WriteLine("[배경음] 재생 중단");

            int result = waveOutReset(hWaveOut); // 🔥 즉시 중단
            if (result != 0)
            {
                Console.WriteLine($"[배경음] waveOutReset 실패: {result}"); // 🔥 오류 로그 추가
            }

            result = waveOutClose(hWaveOut); // 🔥 사운드 장치 닫기
            if (result != 0)
            {
                Console.WriteLine($"[배경음] waveOutClose 실패: {result}"); // 🔥 오류 로그 추가
            }

            hWaveOut = IntPtr.Zero; // 🔥 핸들 초기화
        }
        else
        {
            Console.WriteLine("[배경음] 이미 중지됨"); // ✅ 중복 호출 방지
        }
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

    void PlayPCM(byte[] byteBuffer, int sampleRate)
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

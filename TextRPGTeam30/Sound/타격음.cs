using System;
using System.Runtime.InteropServices;
using System.Threading;

public class 타격음 : ISoundPlayer // 🔥 ISoundPlayer 추가
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

    private bool isPlaying = false;
    private Thread loopThread;

    public void Play()
    {
        isPlaying = true;
        loopThread = new Thread(LoopSound);
        loopThread.IsBackground = true;
        loopThread.Start();
    }

    public void Stop()
    {
        isPlaying = false;
        loopThread?.Join(); // 🔥 루프가 완전히 종료될 때까지 대기
        waveOutClose(hWaveOut);
    }

    private void LoopSound()
    {
        int sampleRate = 44100;
        int durationMilliseconds = 150; // 0.15초 (타격음)
        int totalSamples = sampleRate * durationMilliseconds / 1000;
        double[] buffer = new double[totalSamples];

        GenerateImpactSound(buffer, sampleRate);

        short[] samples = new short[totalSamples];
        for (int i = 0; i < totalSamples; i++)
        {
            samples[i] = (short)(buffer[i] * short.MaxValue);
        }

        byte[] byteBuffer = new byte[samples.Length * 2];
        Buffer.BlockCopy(samples, 0, byteBuffer, 0, byteBuffer.Length);

        while (isPlaying)
        {
            PlayPCM(byteBuffer, sampleRate);
            Thread.Sleep(500); // 🔥 0.5초 간격으로 반복 재생
        }
    }

    private static void GenerateImpactSound(double[] buffer, int sampleRate)
    {
        Random rnd = new Random();
        for (int i = 0; i < buffer.Length; i++)
        {
            double decay = 1.0 - ((double)i / buffer.Length); // 🔥 점점 줄어드는 소리
            buffer[i] = decay * (rnd.NextDouble() * 2 - 1) * 0.8;
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

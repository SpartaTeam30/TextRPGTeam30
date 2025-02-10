using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    // waveOut API 상수
    const int CALLBACK_NULL = 0;
    const int WHDR_DONE = 0x00000001;
    const int WAVE_MAPPER = -1;

    // WAVEFORMATEX 구조체 (PCM 형식 설정)
    [StructLayout(LayoutKind.Sequential)]
    public class WAVEFORMATEX
    {
        public ushort wFormatTag;       // 형식 종류 (1: PCM)
        public ushort nChannels;        // 채널 수 (1: Mono)
        public uint nSamplesPerSec;     // 샘플링 주파수 (Hz)
        public uint nAvgBytesPerSec;    // 초당 바이트 수
        public ushort nBlockAlign;      // 블록 정렬
        public ushort wBitsPerSample;   // 비트 깊이 (예: 16)
        public ushort cbSize;           // 추가 정보 크기 (PCM이면 0)
    }

    // WAVEHDR 구조체 (버퍼 정보)
    [StructLayout(LayoutKind.Sequential)]
    public struct WAVEHDR
    {
        public IntPtr lpData;           // 데이터 버퍼 포인터
        public uint dwBufferLength;     // 버퍼 길이 (바이트)
        public uint dwBytesRecorded;    // (입력용) 녹음된 바이트 수
        public uint dwUser;             // 사용자 정의 데이터
        public uint dwFlags;            // 상태 플래그
        public uint dwLoops;            // 반복 횟수
        public IntPtr lpNext;           // 다음 헤더 포인터 (드라이버용)
        public uint reserved;           // 예약
    }

    // waveOut API 함수 선언
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

    static void Main1(string[] args)
    {
        // 재생 설정
        int sampleRate = 44100;       // 44.1 kHz
        int durationSeconds = 30;     // 전체 재생 시간: 30초
        int totalSamples = sampleRate * durationSeconds;
        double[] mixBuffer = new double[totalSamples];

        // ───────────────────────────────
        // [1] 멜로디 라인  
        // 2초부터 시작하여 0.5초 길이의 노트를 0.1초 간격으로 배치 (예시: D minor 계열)
        double melodyStart = 2.0;
        double noteDuration = 0.5;
        double noteGap = 0.1;
        double[] melodyFrequencies = { 293.66, 349.23, 440.00, 392.00, 349.23 }; // D4, F4, A4, G4, F4
        int noteIndex = 0;
        double tTime = melodyStart;
        while (tTime + noteDuration < durationSeconds - 4)  // 마지막 4초는 다른 효과와 fade-out 용
        {
            double freq = melodyFrequencies[noteIndex % melodyFrequencies.Length];
            AddMelodyNote(mixBuffer, sampleRate, tTime, noteDuration, freq, amplitude: 0.25);
            tTime += noteDuration + noteGap;
            noteIndex++;
        }

        // ───────────────────────────────
        // [2] Rough 효과 (강렬한 타격감 있는 사인파 혼합)
        // 0.5초부터 시작해 0.2초 길이의 효과음을 0.1초 간격으로 반복 (기본 150Hz)
        double clangDuration = 0.2;
        double clangGap = 0.1;
        double clangStartTime = 0.5;
        while (clangStartTime + clangDuration < durationSeconds - 2)
        {
            AddRoughEffect(mixBuffer, sampleRate, clangStartTime, clangDuration, frequency: 150, amplitude: 0.3);
            clangStartTime += clangDuration + clangGap;
        }

        // ───────────────────────────────
        // [3] 코드(Chord) 효과  
        // 3초부터 시작하여 0.3초 길이의 코드 효과를 10초 간격으로 반복 (예: C-E-G)
        double chordDuration = 0.3;
        double chordInterval = 10.0;
        double chordStartTime = 3.0;
        double[] chordFrequencies = { 261.63, 329.63, 392.00 }; // C, E, G
        while (chordStartTime + chordDuration < durationSeconds - 2)
        {
            AddChordEffect(mixBuffer, sampleRate, chordStartTime, chordDuration, chordFrequencies, amplitude: 0.15);
            chordStartTime += chordInterval;
        }

        // ───────────────────────────────
        // [4] Fade-Out 처리  
        // 마지막 3초 동안 전체 음량을 선형으로 감소시킵니다.
        int fadeStartSample = (durationSeconds - 3) * sampleRate;
        for (int i = fadeStartSample; i < totalSamples; i++)
        {
            double fadeFactor = 1.0 - ((double)(i - fadeStartSample) / (totalSamples - fadeStartSample));
            mixBuffer[i] *= fadeFactor;
        }

        // ───────────────────────────────
        // PCM 데이터 변환: double -> short (16비트 PCM)
        // 여기서 mixBuffer 값에 0.5를 곱하여 전체 볼륨을 50% 감소시킵니다.
        short[] samples = new short[totalSamples];
        for (int i = 0; i < totalSamples; i++)
        {
            double sVal = mixBuffer[i];
            if (sVal > 1.0) sVal = 1.0;
            if (sVal < -1.0) sVal = -1.0;
            samples[i] = (short)(sVal * 0.5 * short.MaxValue); // 0.5 곱함
        }

        // short 배열을 byte 배열로 변환
        byte[] byteBuffer = new byte[samples.Length * 2];
        Buffer.BlockCopy(samples, 0, byteBuffer, 0, byteBuffer.Length);

        // ───────────────────────────────
        // waveOut 재생을 위한 PCM 포맷 설정
        WAVEFORMATEX format = new WAVEFORMATEX();
        format.wFormatTag = 1; // PCM
        format.nChannels = 1;  // Mono
        format.nSamplesPerSec = (uint)sampleRate;
        format.wBitsPerSample = 16;
        format.nBlockAlign = (ushort)(format.nChannels * (format.wBitsPerSample / 8));
        format.nAvgBytesPerSec = format.nSamplesPerSec * format.nBlockAlign;
        format.cbSize = 0;

        // waveOut 장치 열기
        IntPtr hWaveOut;
        int result = waveOutOpen(out hWaveOut, WAVE_MAPPER, format, IntPtr.Zero, IntPtr.Zero, CALLBACK_NULL);
        if (result != 0)
        {
            Console.WriteLine("waveOutOpen 실패: " + result);
            return;
        }

        // unmanaged 메모리에 PCM 데이터 복사
        IntPtr pBuffer = Marshal.AllocHGlobal(byteBuffer.Length);
        Marshal.Copy(byteBuffer, 0, pBuffer, byteBuffer.Length);

        // WAVEHDR 구조체 설정
        WAVEHDR header = new WAVEHDR();
        header.lpData = pBuffer;
        header.dwBufferLength = (uint)byteBuffer.Length;
        header.dwFlags = 0;
        header.dwLoops = 0;

        result = waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
        if (result != 0)
        {
            Console.WriteLine("waveOutPrepareHeader 실패: " + result);
            Marshal.FreeHGlobal(pBuffer);
            return;
        }

        result = waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
        if (result != 0)
        {
            Console.WriteLine("waveOutWrite 실패: " + result);
            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            Marshal.FreeHGlobal(pBuffer);
            return;
        }

        // 재생 완료까지 대기 (WHDR_DONE 플래그)
        while ((header.dwFlags & WHDR_DONE) == 0)
        {
            Thread.Sleep(10);
        }

        // 정리
        waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
        Marshal.FreeHGlobal(pBuffer);
        waveOutClose(hWaveOut);

        Console.WriteLine("재생 완료.");
    }

    //──────────────────────────────
    // [1] 멜로디 노트 추가  
    // 주어진 시작 시각부터 noteDuration 동안 주어진 주파수의 사인파에 attack/release envelope 적용
    static void AddMelodyNote(double[] buffer, int sampleRate, double startTime, double noteDuration, double frequency, double amplitude)
    {
        int startSample = (int)(startTime * sampleRate);
        int noteSamples = (int)(noteDuration * sampleRate);
        for (int i = 0; i < noteSamples && (startSample + i) < buffer.Length; i++)
        {
            double t = (double)i / noteSamples;
            double attackTime = 0.1;
            double releaseTime = 0.1;
            double envelope = 1.0;
            if (t < attackTime)
                envelope = t / attackTime;
            else if (t > 1 - releaseTime)
                envelope = (1 - t) / releaseTime;
            double sample = Math.Sin(2 * Math.PI * frequency * ((double)i / sampleRate));
            buffer[startSample + i] += amplitude * envelope * sample;
        }
    }

    //──────────────────────────────
    // [2] Rough 효과 추가  
    // 기본 사인파와 약간 detune된 사인파, 소량의 노이즈를 혼합하여 강렬하고 거친 느낌을 줍니다.
    static void AddRoughEffect(double[] buffer, int sampleRate, double startTime, double duration, double frequency, double amplitude)
    {
        int startSample = (int)(startTime * sampleRate);
        int effectSamples = (int)(duration * sampleRate);
        double detuneRatio = 1.03; // 3% detune
        for (int i = 0; i < effectSamples && (startSample + i) < buffer.Length; i++)
        {
            double t = (double)i / sampleRate;
            double envelope = 1.0 - ((double)i / effectSamples);
            double baseWave = Math.Sin(2 * Math.PI * frequency * t);
            double detunedWave = Math.Sin(2 * Math.PI * frequency * detuneRatio * t);
            double combined = (baseWave + detunedWave) / 2.0;
            double noise = (rnd.NextDouble() * 2 - 1) * 0.1;
            buffer[startSample + i] += amplitude * envelope * (combined + noise);
        }
    }

    //──────────────────────────────
    // [3] 코드(Chord) 효과 추가  
    // 여러 주파수를 동시에 울리며, 각 음에 약간의 랜덤 detune과 선형 페이드아웃 적용하여 풍부한 효과를 냅니다.
    static void AddChordEffect(double[] buffer, int sampleRate, double startTime, double duration, double[] frequencies, double amplitude)
    {
        int startSample = (int)(startTime * sampleRate);
        int effectSamples = (int)(duration * sampleRate);
        for (int i = 0; i < effectSamples && (startSample + i) < buffer.Length; i++)
        {
            double t = (double)i / sampleRate;
            double envelope = 1.0 - ((double)i / effectSamples);
            double sum = 0;
            foreach (double freq in frequencies)
            {
                double detune = 0.98 + rnd.NextDouble() * 0.04;
                sum += Math.Sin(2 * Math.PI * freq * detune * t);
            }
            double chordSample = (sum / frequencies.Length) * envelope * amplitude;
            buffer[startSample + i] += chordSample;
        }
    }
}

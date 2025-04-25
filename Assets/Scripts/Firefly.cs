using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Firefly : MonoBehaviour
{
    public Light pointLight;
    int waitCount = 0;
    private UInt64 timeStep = 0;

    public SineWave sineWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        System.Random random = new System.Random();
        double min = 1.0;
        double max = Math.PI;
        double randomNumber = random.NextDouble() * (max - min) + min;
        sineWave = new SineWave(1,48, randomNumber);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        timeStep += 1;
        if (waitCount % 48 == 0)
        {
            var enabled = !pointLight.enabled;
            pointLight.enabled = enabled;
            Debug.Log(string.Format("Sine val {0}", sineWave.GetValue(timeStep)));
            waitCount = 0;
        }
        
    }
}

public class SineWave
{
    private double amplitude;
    private double period;
    private double phase;
    private double frequency;

    public SineWave(double amplitude, double period, double phase = 0)
    {
        SetAmplitude(amplitude);
        SetPeriod(period);
        SetPhase(phase);
    }

    // Sampling method
    public double GetValue(double time)
    {
        return amplitude * Math.Sin(2 * Math.PI * frequency * time + phase);
    }

    // Amplitude
    public void SetAmplitude(double newAmplitude)
    {
        amplitude = newAmplitude;
    }

    public double GetAmplitude()
    {
        return amplitude;
    }

    // Period (recalculates frequency)
    public void SetPeriod(double newPeriod)
    {
        if (newPeriod <= 0)
            throw new ArgumentException("Period must be greater than zero.");

        period = newPeriod;
        frequency = 1.0 / period;
    }

    public double GetPeriod()
    {
        return period;
    }

    public double GetFrequency()
    {
        return frequency;
    }

    // Phase
    public void SetPhase(double newPhase)
    {
        phase = newPhase;
    }

    public double GetPhase()
    {
        return phase;
    }
}
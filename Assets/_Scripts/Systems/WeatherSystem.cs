using UnityEngine;
using System; 


public enum WeatherType { Sunny, Scorching, Rainy, Snowy, Hail, AcidRain }


public class WeatherSystem : MonoBehaviour
{
    public WeatherType CurrentWeather { get; private set; } = WeatherType.Sunny;

    private int consecutiveSunny = 0;
    private int consecutiveWet = 0;

    // Vægte: Sunny=40%, Rainy=30%, Snowy=15%, AcidRain=15%
    // Scorching og Hail rulles aldrig direkte – de trigges af streak
    private readonly WeatherType[] weatherPool = new WeatherType[]
    {
        WeatherType.Sunny,   WeatherType.Sunny,   WeatherType.Sunny,   WeatherType.Sunny,
        WeatherType.Rainy,   WeatherType.Rainy,   WeatherType.Rainy,
        WeatherType.Snowy,   WeatherType.Snowy,
        WeatherType.AcidRain
    };

    public WeatherType RollWeather()
    {
        WeatherType rolled = weatherPool[UnityEngine.Random.Range(0, weatherPool.Length)];

        // Streak tracking + ekstrem vejr
        if (rolled == WeatherType.Sunny)
        {
            consecutiveWet = 0;
            consecutiveSunny++;
            if (consecutiveSunny >= 3)
            {
                consecutiveSunny = 0;
                return CurrentWeather = WeatherType.Scorching;
            }
        }
        else if (rolled == WeatherType.Rainy || rolled == WeatherType.Snowy)
        {
            consecutiveSunny = 0;
            consecutiveWet++;
            if (consecutiveWet >= 3)
            {
                consecutiveWet = 0;
                return CurrentWeather = WeatherType.Hail;
            }
        }
        else
        {
            consecutiveSunny = 0;
            consecutiveWet = 0;
        }

        return CurrentWeather = rolled;
    }

    public float GetWateringEnergyCost(float baseCost) => CurrentWeather switch
    {
        WeatherType.Rainy => baseCost * 0.5f,
        WeatherType.Scorching => baseCost * 1.5f,
        _ => baseCost
    };
}

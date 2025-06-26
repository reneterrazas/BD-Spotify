namespace Spotify.Core.Persistencia;

public interface IMatcheo
{
    public List<string>? Matcheo(string Cadena);
    Task<List<string>?> MatcheoAsync(string Cadena);
}

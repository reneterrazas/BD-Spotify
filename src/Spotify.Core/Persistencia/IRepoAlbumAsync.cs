namespace Spotify.Core.Persistencia;

public interface IRepoAlbumAsync : IAltaAsync<Album, uint>, IListadoAsync<Album>, IEliminar<uint>, IDetallePorIdAsync<Album, uint>
{}
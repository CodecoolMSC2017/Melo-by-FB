using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Service.Interface
{
    public interface IMusicCategorizer
    {
        List<Album> CreateAlbumList();
        List<Artist> CreateArtistList();
    }
}

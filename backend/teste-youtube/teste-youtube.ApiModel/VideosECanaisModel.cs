using teste_youtube.ApiModel.Enumeradores;

namespace teste_youtube.ApiModel
{
    public class VideosECanaisModel
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public TipoRecurso Tipo { get; set; }
        public string MiniaturaUrl { get; set; }
    }
}

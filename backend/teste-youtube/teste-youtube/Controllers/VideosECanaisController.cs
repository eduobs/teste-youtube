using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using teste_youtube.ApiModel;

namespace teste_youtube.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosECanaisController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string filtro, int totalRegistros=100)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDWCLw4NEUo33KSHyNSoVDC9bbqGG05ONU",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = filtro; // Replace with your search term.
            searchListRequest.MaxResults = totalRegistros;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<VideosECanaisModel> itens = processarResultado(searchListResponse);
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            

            return Ok(itens);
        }

        private List<VideosECanaisModel> processarResultado(SearchListResponse searchListResponse)
        {
            List<VideosECanaisModel> resultado;

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("Id: {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                            searchResult.Id.VideoId,
                            searchResult.Snippet.Title,
                            searchResult.Snippet.Description,
                            searchResult.Snippet.PublishedAt,
                            searchResult.Snippet.ChannelTitle,
                            searchResult.Snippet.Thumbnails.Default__.Url,
                            searchResult.Snippet.Thumbnails.Default__.Height,
                            searchResult.Snippet.Thumbnails.Default__.Width));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }
        }
    }
}
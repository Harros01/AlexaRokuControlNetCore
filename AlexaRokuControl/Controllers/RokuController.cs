using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using AlexaRokuControl.Models.Config;

namespace AlexaRokuControl.Controllers
{
    public class RokuController : Controller
    {

        private APISettings ConfigSettings { get; set; }

        public RokuController(IOptions<APISettings> settings)
        {
            ConfigSettings = settings.Value;
        }

        public async Task<IActionResult> playpause()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/Play", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> rewind()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/rev", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> fastforward()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/fwd", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> up()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/up", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> down()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/down", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> left()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/left", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> right()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/right", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> back()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/back", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> instantreplay()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/instantreplay", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> select()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/select", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> home()
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "keypress/home", "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> launch(string channel)
        {
            var post = await RequestPost(ConfigSettings.RokuUrl, "launch/" + channel, "post", "");
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> volumeup()
        {
            string body = "{\"KEYLIST\": [{\"CODESET\": 5,\"CODE\": 1,\"ACTION\": \"KEYPRESS\"}]}";
            await RequestPost(ConfigSettings.Vizio.SoundBarUrl, "key_command/", "put", body);
            await RequestPost(ConfigSettings.Vizio.SoundBarUrl, "key_command/", "put", body);
            var post = await RequestPost(ConfigSettings.Vizio.SoundBarUrl, "key_command/", "put", body);
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> volumedown()
        {
            string body = "{\"KEYLIST\": [{\"CODESET\": 5,\"CODE\": 0,\"ACTION\": \"KEYPRESS\"}]}";
            await RequestPost(ConfigSettings.Vizio.SoundBarUrl, "key_command/", "put", body);
            await RequestPost(ConfigSettings.Vizio.SoundBarUrl, "key_command/", "put", body);
            var post = await RequestPost(ConfigSettings.Vizio.SoundBarUrl, "key_command/", "put", body);
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> tvoff()
        {
            string body = "{\"KEYLIST\": [{\"CODESET\": 11,\"CODE\": 0,\"ACTION\": \"KEYPRESS\"}]}";
            var post = await RequestPost(ConfigSettings.Vizio.TVUrl, "key_command/", "put", body, true);
            if (post)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<bool> RequestPost(string baseUrl, string path, string method, string body, bool? auth = false)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                // Ignore SSL errors. Vizio certificate is unsigned.
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        // Build request
                        client.BaseAddress = new Uri(baseUrl);
                        if (auth == true)
                        {
                            // Auth key from Vizio Pairing. Only required for TV.
                            client.DefaultRequestHeaders.Add("Auth", ConfigSettings.Vizio.VizioAuth);
                        }
                        // Encode body to HttpContent
                        HttpContent contentPost = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
                        HttpResponseMessage response;
                        // Check required method
                        if (method == "post")
                        {
                            response = await client.PostAsync(path, contentPost);
                            response.EnsureSuccessStatusCode();
                        }
                        if (method == "put")
                        {
                            response = await client.PutAsync(path, contentPost);
                            response.EnsureSuccessStatusCode();
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}

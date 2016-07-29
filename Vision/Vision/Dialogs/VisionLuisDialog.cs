using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vision.Models;
using Vision.Extensions;
using System.Web;

namespace Vision.Dialogs
{
    [LuisModel("cea37070-74fc-40e1-8b1e-1ce0d6f2be23", "e686ad6ca03b4cf29d26288783935568")]
    [Serializable]
    public class VisonLuisDialog : LuisDialog<object>
    {
        public String current = "";
        public String phone = "";
        public const string EntityNameCity = "location";
        public const string OpenWeatherMapApiKey = "0336767b42b40c645b1e00afdd8a803";
        public const string TraktApiKey = "87b237d0ecf17ee943c258835dffc5f87c4756d19f3b62c3193b128328bb0c1e";

        public String getImageUrlShows(String imdb)
        {
            String url = "";
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv/") })
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("trakt-api-version", "2");

                client.DefaultRequestHeaders.TryAddWithoutValidation("trakt-api-key", "468a92c26d3411be7886881b7f40afea47288963a91d9c5a0f43257521ceab74");

                //client.DefaultRequestHeaders.TryAddWithoutValidation("trakt-api-version", "2");
                //client.DefaultRequestHeaders.Add("trakt.api.key", "468a92c26d3411be7886881b7f40afea47288963a91d9c5a0f43257521ceab74");
                using (var response = client.GetAsync("search/imdb/" + imdb + "?extended=images").Result)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;

                    var responseJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<List<anticipated>>(responseString);

                    url = responseJSON[0].movie.images.fanart.full;
                }
            }
            return url;
        }
        [LuisIntent("intent.number")]
        public async Task number(IDialogContext context, LuisResult result)
        {
            string message = $"Selection error please try use the .Search Command";
            if (current == "menu")
            {
                if (result.Query.ToLower().ToString().Contains("1"))
                {
                    message = $"comming soon";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("2"))
                {
                    message = $"Vision SMS{Environment.NewLine}{Environment.NewLine}To Send A message Simply use the following format {Environment.NewLine}{Environment.NewLine}.SMS[Space][Phone Number you wish to send to]{Environment.NewLine}{Environment.NewLine}>Note:There are limited Sms's are avalible per day";
                    current = "sms";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("3"))
                {
                    message = $"{Environment.NewLine}{Environment.NewLine}Select from the following.{Environment.NewLine}{Environment.NewLine} > 1) Anticipated Movies {Environment.NewLine}{Environment.NewLine} > 2) Most Played Movies {Environment.NewLine}{Environment.NewLine} > 3) Popular Movies {Environment.NewLine}{Environment.NewLine} > 4) Trending Movies";
                   
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                    result.Query="";
                    current = "movies";

                }
                if (result.Query.ToLower().ToString().Contains("4"))
                {
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("5"))
                {
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("6"))
                {
                    String helpMenu = $"{Environment.NewLine}{Environment.NewLine}Vision SupportMenu.{Environment.NewLine}{Environment.NewLine} > 1) News {Environment.NewLine}{Environment.NewLine} > 2) Sms {Environment.NewLine}{Environment.NewLine} > 3) Vision Movies {Environment.NewLine}{Environment.NewLine} > 4) Vision Weather {Environment.NewLine}{Environment.NewLine} > 5) Vision Commands  {Environment.NewLine}{Environment.NewLine} > 6) Vision  Support {Environment.NewLine}{Environment.NewLine} > 7) Vision Search ";
                    String hint = $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}HINT:" + " Please enter the number from the this menu you require help with";
                    message = $"" + helpMenu + hint; current = "help";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("7"))
                {
                    message = $"Enter what you wish to search ";
                    current = "search";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
            }

            if (current == "help")
            {
                if (result.Query.ToLower().ToString().Contains("1"))
                {
                    message = $"News Gives you a list of links of news papers ";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("2"))
                {
                    message = $"comming soon";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("3"))
                {
                    message = $"comming soon";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("4"))
                {
                    message = $"comming soon";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("5"))
                {
                    message = $"comming soon";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("6"))
                {
                    message = $"comming soon";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
            }
            if (current == "movies")
            {
                if (result.Query.ToLower().ToString().Contains("1"))
                {
                    message = $"News Gives you a list of links of news papers ";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("2"))
                {
                    message = $"comming soon";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("3"))
                {
                    message = $"comming soon";
                    await Popular(context, result);
                    context.Wait(MessageReceived);
                }
                if (result.Query.ToLower().ToString().Contains("4"))
                {
                    message = $"comming soon";
                    await context.PostAsync(message);
                    context.Wait(MessageReceived);
                }

            }
            }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand :pensive: ";
            if (current == "menu")
            {
                message = $"Sorry view the help documentation for more assistance :pensive:";
                current = "";
            }
            if (current == "sms")
            {
                String answer = result.Query.ToLower().ToString();
                int n;
                bool isNumeric = int.TryParse(answer, out n);
                if(isNumeric==true){
                    if(answer.Length == 9)
                    {

                    }
                    else { message = $"There is a problem with the number you sent please try again or use .reset command to reset :pensive:"; }

                }
            }
            else { message = $"There is a problem with the number you sent please try again or use .reset command to reset :pensive:"; }
               
            

            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("command")]
        public async Task command(IDialogContext context, LuisResult result)
        {
            string message = "command Unknown";
            if (result.Query.ToLower().ToString().Contains("men"))
            {
                current = "";
                String menu = $"{Environment.NewLine}{Environment.NewLine}Vision MainMenu.{Environment.NewLine}{Environment.NewLine} > 1) News {Environment.NewLine}{Environment.NewLine} > 2) Sms {Environment.NewLine}{Environment.NewLine} > 3) Vision Movies {Environment.NewLine}{Environment.NewLine} > 4) Vision Weather {Environment.NewLine}{Environment.NewLine} > 5) Vision Commands  {Environment.NewLine}{Environment.NewLine} > 6) Vision  Support {Environment.NewLine}{Environment.NewLine} > 7) Vision Search ";
                String options = $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}HINT:"+" You can also use the .Search command as well as other commands for easy access";
                 message = $""+ menu + options;
                current = "menu";
                await context.PostAsync(message);
                context.Wait(MessageReceived);
            }
            if (result.Query.ToLower().ToString().Contains("hel"))
            {
                String helpMenu = $"{Environment.NewLine}{Environment.NewLine}Vision SupportMenu.{Environment.NewLine}{Environment.NewLine} > 1) News {Environment.NewLine}{Environment.NewLine} > 2) Sms {Environment.NewLine}{Environment.NewLine} > 3) Vision Movies {Environment.NewLine}{Environment.NewLine} > 4) Vision Weather {Environment.NewLine}{Environment.NewLine} > 5) Vision Commands  {Environment.NewLine}{Environment.NewLine} > 6) Vision  Support {Environment.NewLine}{Environment.NewLine} > 7) Vision Search ";
                String hint = $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}HINT:" + " Please enter the number from the this menu you require help with";
                message = $"" + helpMenu+hint; current = "help";
                await context.PostAsync(message);
                context.Wait(MessageReceived);
            }
            if (result.Query.ToLower().ToString().Contains(".list"))
            {
                //5 result.Query = "";
                current = "list";
                await Popular(context, result);
                
            }
            if (result.Query.ToLower().ToString().Contains(".sms"))
            {
                await context.PostAsync(message);
                context.Wait(MessageReceived);
            }
            if (result.Query.ToLower().ToString().Contains(".res"))
            {
                current = "";
                message = "Sysytem has been reset...";
                await context.PostAsync(message);
                context.Wait(MessageReceived);
            }
           
        }

        [LuisIntent("convo")]
        public async Task convo(IDialogContext context, LuisResult result)
        {
            current = "";
            String[] greet = { "Hey there :grinning: ",
                   "Hello can i help you with anything today",
                   "Hey! Its good to have you on board my friend.",
                   "Hey i was wondering when you would use me"};
            Random rand = new Random();
            int chooseGreetingMessage = rand.Next(0, 5);
            String greetingMessage = greet[chooseGreetingMessage];
            string message = "";
            if (result.Query.ToLower().ToString().Contains("hey"))
            {
                message = ($"" + greetingMessage);
            }
            if (result.Query.ToLower().ToString().Contains("time"))
            {
                DateTime time = DateTime.Now;
              
                message = ($"The Time is :" + (time.ToString("h:mm:ss tt")));
            }

            await context.PostAsync(message);
            context.Wait(MessageReceived);
        
        }
        [LuisIntent("intent.vision.show")]
        public async Task show(IDialogContext context, LuisResult result)
        {
            current = "";
            //bat man
            var ImagesUrl = new Uri("https://www.youtube.com/watch?v=eX_iASz1Si8");
            var ImagesUrl1 = new Uri("https://i.ytimg.com/vi/AVyMJ1k2Rac/maxresdefault.jpg");

            var ImagesUr2 = new Uri("https://www.youtube.com/watch?v=YoHD9XEInc0");
            var ImagesUrl2 = new Uri("http://medicalfuturist.com/wp-content/media/2013/12/inception-collapsing.jpeg");

            var ImagesUr3 = new Uri("https://www.youtube.com/watch?v=JAUoeqvedMo");
            var ImagesUrl3 = new Uri("http://wallpapercave.com/wp/fhhj9Vv.jpg");

            var ImagesUr4 = new Uri("https://www.youtube.com/watch?v=FyKWUTwSYAs");
            var ImagesUrl4 = new Uri("https://i.ytimg.com/vi/yV-NnN4pOng/maxresdefault.jpg");

            var ImagesUr5 = new Uri("https://www.youtube.com/watch?v=d96cjJhvlMA");
            var ImagesUrl5 = new Uri("http://www.shauntmax30.com/data/out/17/1069911-pictures-of-guardians-of-the-galaxy-hd.jpeg");
            var ImagesUrl9=("http://dev.virtualearth.net/REST/V1/Imagery/Map/Road/Umhlanga%20kwazulu-natal?mapLayer=TrafficFlow&key=AtJOhxfxUJ4T7fnHzflbMVKBJz4qsrcbj-tYuBXtKYewQfOr2O_Dgch2yDw36wxz");
            var ImagesUrl8 = new Uri("http://dev.virtualearth.net/REST/v1/Imagery/Map/AerialWithLabels/Durban%20Center?mapSize=500,400&key=AtJOhxfxUJ4T7fnHzflbMVKBJz4qsrcbj-tYuBXtKYewQfOr2O_Dgch2yDw36wxz");
            var ImagesUrl6 = new Uri("http://dev.virtualearth.net/REST/v1/Imagery/Map/AerialWithLabels/Umhlanga%20kwazulu-natal?mapSize=500,400&key=AtJOhxfxUJ4T7fnHzflbMVKBJz4qsrcbj-tYuBXtKYewQfOr2O_Dgch2yDw36wxz");
            var ImagesUr6 = new Uri("https://www.youtube.com/watch?v=eX_iASz1Si8");
            var ImagesUr7 = new Uri("https://www.youtube.com/watch?v=eX_iASz1Si8");
            var ImagesUrl7 = new Uri("https://media.licdn.com/mpr/mpr/shrinknp_400_400/p/3/000/27d/34f/1cbc766.jpg");
            string message = "";

            if (result.Query.ToLower().ToString().Contains("batman"))
            {
                message = ($"[![BATMAN](" + ImagesUrl1 + ")]");

            }

            if (result.Query.ToLower().ToString().Contains("inception"))
            {
                message = ($"[![Inception](" + ImagesUrl2 + ")]");

            }
            if (result.Query.ToLower().ToString().Contains("the avengers"))
            {
                message = ($"[![The Avengers](" + ImagesUrl3 + ")]");

            }
            if (result.Query.ToLower().ToString().Contains("deadpool"))
            {
                message = ($"[![Deadpool](" + ImagesUrl4 + ")]");

            }
            if (result.Query.ToLower().ToString().Contains(" guardians"))
            {
                message = ($"[![ guardians](" + ImagesUrl5 + ")](" + ImagesUr5 + ")");

            }
            if (result.Query.ToLower().ToString().Contains("naik"))
            {
                message = ($"[![Mentor](" + ImagesUrl7 + ")]");

            }

            if (result.Query.ToLower().ToString().Contains("map"))
            {
                message = ($"[![MapTest a sucess](" + ImagesUrl6 + ")]");

            }



            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("intent.vision.search")]
        public async Task search(IDialogContext context, LuisResult result)
        {
            current = "";
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["q"] = "bill gates";
            queryString["count"] = "10";
            queryString["offset"] = "0";
            queryString["mkt"] = "en-us";
            queryString["safesearch"] = "Moderate";
            //  using (var client = new HttpClient { BaseAddress = new Uri("https://api.cognitive.microsoft.com/bing/v5.0/search?q=bill gates&count=10&offset=0&mkt=en-us&safesearch=Moderate") })
            {



                var uri = "https://api.cognitive.microsoft.com/bing/v5.0/search?" + queryString;
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "f471964855c94e518c7049820f5b8658");
                var response = await client.GetAsync(uri);

                //   var response = await client.GetAsync("movies/trending");
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VisonSearch>>(responseString);
            }

            string message = $"lol: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("intent.vision.trending")]
        public async Task Trending(IDialogContext context, LuisResult result)
        {
            current = "";
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "87b237d0ecf17ee943c258835dffc5f87c4756d19f3b62c3193b128328bb0c1e");

                var response = await client.GetAsync("movies/trending");
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TrendingMovie>>(responseString);
            }

            string message = $"lol: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }



        [LuisIntent("intent.vision.anticipated")]
        public async Task Anticipated(IDialogContext context, LuisResult result)
        {
            current = "";
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "87b237d0ecf17ee943c258835dffc5f87c4756d19f3b62c3193b128328bb0c1e");

                var response = await client.GetAsync("movies/anticipated");
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TrendingMovie>>(responseString);
            }

            string message = $"lol: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("intent.vision.popular")]

        public async Task Popular(IDialogContext context, LuisResult result)
        {
           
            int count = 0;
            String output = "Popular movies";
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv") })
            {
                client.DefaultRequestHeaders.Add("trakt-api-key", "87b237d0ecf17ee943c258835dffc5f87c4756d19f3b62c3193b128328bb0c1e");

                var response = await client.GetAsync("movies/popular");
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Movie>>(responseString);
                if(current=="list")
                {
                    for (int i = 0; i < responseJSON.Count; i++)
                    {
                        count++;
                        String kk = responseJSON[i].ids.imdb;
                        String image = $"[![watch trailer and find more info](" + getImageUrlShows(kk) + ")](" + getImageUrlShows(kk) + ")";
                        output += $"{Environment.NewLine}{Environment.NewLine} > " + count + ")" + responseJSON[i].title;
                        current = "";
                    }
                }
                else
                { 
                for (int i = 0; i < responseJSON.Count; i++)
                {
                    count++;
                    current = "popular";
                    String kk = responseJSON[i].ids.imdb;
                    String image = $"[![watch trailer and find more info](" + getImageUrlShows(kk) + ")](" + getImageUrlShows(kk) + ")";
                    output += $"{Environment.NewLine}{Environment.NewLine} > " + count + ")" + responseJSON[i].title+image;
                    
                }
                }
            }
            string message = $"" + output;
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }



        [LuisIntent("intent.vision.weather")]
        public async Task CurrentWeather(IDialogContext context, LuisResult result)
        {
            current = "";
            using (var client = new HttpClient())
            {
                int count = 0;
                string output = "Ishu";
                var city = result.TryFindEntity(EntityNameCity).Entity;
                var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={OpenWeatherMapApiKey}";

                var response = await client.GetAsync(url);
                var res = await response.Content.ReadAsStringAsync();
                var currentWeather = Newtonsoft.Json.JsonConvert.DeserializeObject<CurrentWeatherResponse>(res);
                
                await context.PostAsync($"{currentWeather.weather.First().description} in {city} and a temperature of {currentWeather.main.temp}°C");
                context.Wait(MessageReceived);
            }
        }
    }
}
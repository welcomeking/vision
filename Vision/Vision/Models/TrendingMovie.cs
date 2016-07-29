namespace Vision.Models
{
    public class TrendingMovie
    {
        public int watchers { get; set; }
        public Movie movie { get; set; }
    }

    public class Movie
    {
        public string title { get; set; }
        public int year { get; set; }
        public Ids ids { get; set; }
    }

    public class Ids
    {
        public int trakt { get; set; }
        public string slug { get; set; }
        public string imdb { get; set; }
        public int tmdb { get; set; }
    }
    public class anticipated
    {
        public int watchers { get; set; }
        public Show2 movie { get; set; }
    }
   
    public class MostPlayedMovies
    {
        public long watcher_count { get; set; }
        public long play_count { get; set; }
        public long collected_count { get; set; }
        public Movie movie { get; set; }
    }
    public class AnticipatedMovies
    {
        public long list_count { get; set; }
        public Movie movie { get; set; }
    }

    public class Show2
    {
        public string title { get; set; }
        public int year { get; set; }
        public Ids2 ids { get; set; }
        public Images images { get; set; }
    }

    public class Ids2
    {
        public int trakt { get; set; }
        public string slug { get; set; }
        public int tvdb { get; set; }
        public string imdb { get; set; }
        public int tmdb { get; set; }
        public int tvrage { get; set; }
    }

    public class Images
    {
        public Fanart fanart { get; set; }
        public Poster poster { get; set; }
        public Logo logo { get; set; }
        public Clearart clearart { get; set; }
        public Banner banner { get; set; }
        public Thumb thumb { get; set; }
    }

    public class Fanart
    {
        public string full { get; set; }
        public string medium { get; set; }
        public string thumb { get; set; }
    }

    public class Poster
    {
        public string full { get; set; }
        public string medium { get; set; }
        public string thumb { get; set; }
    }

    public class Logo
    {
        public string full { get; set; }
    }

    public class Clearart
    {
        public string full { get; set; }
    }

    public class Banner
    {
        public string full { get; set; }
    }

    public class Thumb
    {
        public string full { get; set; }
    }
}
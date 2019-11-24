namespace UrbanNoise.Importer.Components.Domain.ValueObjects
{
    public class Coordinates
    {
        public Coordinates()
        {

        }

        public Coordinates(string Latitude, string Longitude)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}

namespace Assignment2.Model
{
    public class FixerResponseModelV2
    {
        public bool success { get; set; }
        public Query query { get; set; }
        public Info info { get; set; }
        public string date { get; set; }
        public double result { get; set; }
        public Error error { get; set; }
        //Add new parameter for version 2
    }
}

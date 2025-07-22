namespace E_Commerce_Test_Project_MVC.Models
{
    public class SliderInfo:BaseEntity
    {
        public string Description { get; set; }
        public string Text { get; set; }
        public string Signature { get; set; }
        public string MiniText { get; set; }
        public bool IsMain { get; set; }

    }
}

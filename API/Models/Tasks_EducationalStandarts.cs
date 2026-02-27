namespace API.Models
{
    public class Tasks_EducationalStandarts
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Tasks Task { get; set; }
        public int EducationalStandartId { get; set; }
        public EducationalStandarts EducationalStandart { get; set; }


    }
}

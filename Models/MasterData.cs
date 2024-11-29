using ServiceReport.Data;

namespace ServiceReport.Models
{
    public class MasterData: BaseModel
    {
        public int Id { get; set; }  // Assuming there's a unique identifier
       public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string MachineSerialNumber { get; set; }
        public double LaserPower { get; set; }
    }
}

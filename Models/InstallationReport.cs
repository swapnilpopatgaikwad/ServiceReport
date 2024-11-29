//using ServiceReport.Data;

//namespace ServiceReport.Models
//{
//    // MachineInstallationCommissioningReport.cs
//    public class InstallationReport :BaseModel
//    {
//        public int Id { get; set; }
//        public string CustomerName { get; set; }
//        public string ContactPerson { get; set; }
//        public string EngineerName { get; set; }
//        public string Address { get; set; }
//        public DateTime StartDate { get; set; }
//        public DateTime EndDate { get; set; }
//        public int NoOfWorkingDays { get; set; }
//        public string MachineModel { get; set; }
//        public string MachineSerialNumber { get; set; }
//        public string MachineType { get; set; }

//        // Machine Details
//        public List<MachineDetail> MachineDetails { get; set; }

//        // Power Supply Details
//        public PowerSupplyDetails PowerSupplyDetails { get; set; }

//        // General Capabilities
//        public GeneralCapability GeneralCapabilities { get; set; }

//        // Machine Specifications
//        public List<MachineSpecification> MachineSpecifications { get; set; }

//        // Cutting Material Details
//        public List<CuttingMaterialDetail> CuttingMaterialDetails { get; set; }

//        // Signature and Comments
//        public string EngineerSign { get; set; }
//        public string ContactPersonSign { get; set; }
//        public string ContactPersonComments { get; set; }
//    }

//    // MachineDetail.cs
//    public class MachineDetail
//    {
//        public int Id { get; set; }
//        public string MachineFunction { get; set; }
//        public string WorkingStatus { get; set; }
//        public string Remark { get; set; }
//    }

//    // PowerSupplyDetails.cs
//    public class PowerSupplyDetails
//    {
//        public int Id { get; set; }
//        public string PhaseToPhase { get; set; }
//        public string PhaseToNeutral { get; set; }
//        public string PhaseToEarth { get; set; }
//        public string NeutralToEarth { get; set; }
//        public string EarthResistance { get; set; }
//    }

//    // GeneralCapability.cs
//    public class GeneralCapability
//    {
//        public int Id { get; set; }
//        public string MaxCuttingLength { get; set; }
//        public string MaxCuttingWidth { get; set; }
//        public string CuttingTolerance { get; set; }
//        public string Repeatability { get; set; }
//        public string CircularAccuracy { get; set; }
//        public string Diagonal { get; set; }
//    }

//    // MachineSpecification.cs
//    public class MachineSpecification
//    {
//        public int Id { get; set; }
//        public string Parameter { get; set; }
//        public string RangeDescription { get; set; }
//        public string Remark { get; set; }
//    }

//    // CuttingMaterialDetail.cs
//    public class CuttingMaterialDetail
//    {
//        public int Id { get; set; }
//        public string CuttingMaterial { get; set; }
//        public decimal Thickness { get; set; }
//        public string Result { get; set; }
//        public string Remark { get; set; }
//    }

//}

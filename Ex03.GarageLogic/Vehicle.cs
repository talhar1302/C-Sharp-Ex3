using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private List<Wheel> m_Wheels;
        private string m_OwnerName;
        public string m_OwnerPhone;
        private eVehicleStatus m_status;
        private string m_WheelsManufacturerName;
        private float m_WheelsAirPressure;
        public string ModelName { get => m_ModelName; set => m_ModelName = value; }
        public string LicenseNumber { get => m_LicenseNumber; set => m_LicenseNumber = value; }
        public List<Wheel> Wheels { get =>m_Wheels; set=>m_Wheels=value; }
        public string OwnerName { get => m_OwnerName; set => m_OwnerName = value; }
        public string OwnerPhone { get => m_OwnerPhone; set => m_OwnerPhone = value; }
        public eVehicleStatus Status { get=>m_status; set=>m_status=value; }
        public string WheelsManufacturerName
        {
            get => m_WheelsManufacturerName;
            set
            {
                m_WheelsManufacturerName = value;
                foreach (Wheel wheel in m_Wheels)
                {
                    wheel.ManufacturerName = value;
                }
            }
        }
        public float WheelsAirPressure
        {
            get => m_WheelsAirPressure;
            set
            {
                m_WheelsAirPressure = value;
                foreach (Wheel wheel in m_Wheels)
                {
                    wheel.CurrentAirPressure = value;
                }
            }
        }

        protected Vehicle()
        {
            Wheels = new List<Wheel>();
            m_status = eVehicleStatus.UnderRepair;
        }

        public virtual List<FieldDescriptor> GetFieldDescriptors()
        {
            return new List<FieldDescriptor>
            {
            new FieldDescriptor("ModelName", typeof(string), InputValidator.ValidateModelName),
            new FieldDescriptor("OwnerName", typeof(string), InputValidator.ValidateLettersOnly),
            new FieldDescriptor("OwnerPhone", typeof(string), InputValidator.ValidatePhoneNumber)          
            // Add other fields as necessary
            };
        }

        public virtual string GetDetails()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine($"License Number: {m_LicenseNumber}");
            details.AppendLine($"Model Name: {m_ModelName}");
            details.AppendLine($"Owner Name: {m_OwnerName}");
            details.AppendLine($"Owner Phone: {m_OwnerPhone}");
            details.AppendLine($"Vehicle Status: {m_status}");
            details.AppendLine("Wheels:");
            foreach (Wheel wheel in m_Wheels)
            {
                details.AppendLine(wheel.ToString());
            }
            return details.ToString();
        }
        public abstract void InflateWheelsToMax();
    }
}

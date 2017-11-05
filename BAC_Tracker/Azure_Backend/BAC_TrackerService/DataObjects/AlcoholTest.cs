using Microsoft.Azure.Mobile.Server;

namespace BAC_TrackerService.DataObjects
{
    public class AlcoholTest : EntityData
    {
        public string Name { get; set; }
        public float Volume { get; set; }
        public bool Finished { get; set; }
    }
}
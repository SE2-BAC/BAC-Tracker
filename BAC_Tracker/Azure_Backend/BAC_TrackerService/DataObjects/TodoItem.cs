using Microsoft.Azure.Mobile.Server;

namespace BAC_TrackerService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace HRMS.Models
{
    public class AuditEntry
    {
        public EntityEntry Entity { get; set; }
        public AuditEntry(EntityEntry entry)
        {
            Entity = entry;
        }
        public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, Object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, Object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, Object> NewValues { get; } = new Dictionary<string, object>();

        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.UserId = UserId;
            audit.AuditType = AuditType.ToString();
            audit.TableName = TableName;
            audit.DateTime = DateTime.Now;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }

    }
}

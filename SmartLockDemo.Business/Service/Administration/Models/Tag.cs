using System.Collections.Generic;

namespace SmartLockDemo.Business.Service.Administration
{
    /// <summary>
    /// Business model equivalent of Door entity 
    /// </summary>
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Door> Doors { get; set; }
    }
}

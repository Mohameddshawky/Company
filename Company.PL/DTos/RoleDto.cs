using System.Collections.Generic;

namespace Company.PL.DTos
{
    public class RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public RoleDto()
        {
            Id=Guid.NewGuid().ToString();                       
        }
        public List<UserRoleDto> users { get; set; } = new List<UserRoleDto>();
    }
}

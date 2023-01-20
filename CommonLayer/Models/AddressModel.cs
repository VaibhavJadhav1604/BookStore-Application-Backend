using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int TypeId { get; set; }
        public AddressTypeModel addressType { get; set; }
        public int UserId { get; set; }
    }
}

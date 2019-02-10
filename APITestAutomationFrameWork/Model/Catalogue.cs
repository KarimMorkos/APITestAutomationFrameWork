using System.Collections.Generic;

namespace APITestAutomationFrameWork.Model
{
    public class Catalogue
    {
        public string Name { get; set; }
        public bool CanRelist { get; set; }
        public List<Promotion> Promotions { get; set; }
    }
}
